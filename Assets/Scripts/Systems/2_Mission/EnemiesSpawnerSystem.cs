using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class EnemiesSpawnerSystem : MonoBehaviour
{
    [Inject] DiContainer _diContainer;
    [SerializeField] private AllEnemies _allEnemies;
    [SerializeField] private Transform _enemiesParent;
    [SerializeField] private EnemiesBiomeSpawnTransforms[] _enemiesBiomeSpawnTransforms;
    private List<EnemiesForListData> _currentEnemiesData = new();
    private int _enemyNumber;
    private int _maxSpawnedEnemiesPerFrame = 8;           // сколько врагов максимум за кадр
    private Coroutine _spawnRoutine;
    private MonsterType GetCurrentBiome() => CurrentMissionInfo.Instance.GetCurrentLandscape().MonsterType;
    private int GetLandscapeNumber() => (int)CurrentMissionInfo.Instance.GetCurrentLandscape().LandscapeEnum;

    public EnemyData[] GetAllCurrentEnemies()
    {
        var data = new EnemyData[_currentEnemiesData.Count];

        for (int i = 0; i < _currentEnemiesData.Count; i++)
        {
            data[i] = new EnemyData
            {
                EnemyEnum = _currentEnemiesData[i].EnemyEnum,
                PositionX = _currentEnemiesData[i].EnemyObject.transform.position.x,
                PositionY = _currentEnemiesData[i].EnemyObject.transform.position.y,
                PositionZ = _currentEnemiesData[i].EnemyObject.transform.position.z,
                Rotation = _currentEnemiesData[i].EnemyObject.transform.eulerAngles.y,
                EnemyLevel = _currentEnemiesData[i].EnemyObject.GetComponent<EnemyLevel>().GetLevel(),
                EnemyHealth = _currentEnemiesData[i].EnemyObject.GetComponent<BaseHealth>().GetCurrentHealth(),
                HealthFactor = _currentEnemiesData[i].EnemyObject.GetComponent<EnemyInfo>().GetHealthFactor(),
                DamageFactor = _currentEnemiesData[i].EnemyObject.GetComponent<EnemyInfo>().GetDamageFactor(),
                IsMiniBoss = _currentEnemiesData[i].EnemyObject.GetComponent<EnemyInfo>().IsMiniBoss()
            };
        }

        return data;
    }

    private void Awake()
    {
        CustomEvents.OnDayEnd += PrepareSpawnEnemy;
        CustomEvents.OnEnemyDeath += RemoveEnemyFromList;
    }

    private void PrepareSpawnEnemy(int day)
    {
        var enemiesSpawnerInfo = CurrentMissionInfo.Instance.GetEnemiesSpawnerInformation();
        var allSpawners = enemiesSpawnerInfo.Spawners;

        var hasBossObjective = CurrentMissionInfo.Instance.GetObjective().Objectives.Any(o => o.ObjectiveEnum == ObjectiveEnum.KillBoss);
        if (hasBossObjective)
        {
            if (enemiesSpawnerInfo.BossSpawner.BossDaySpawn == day) SpawnBoss(enemiesSpawnerInfo);
        }

        var todayMiniBosses = enemiesSpawnerInfo.MiniBossSpawners.Where(mb => mb.DaySpawn == day).ToArray();

        if (todayMiniBosses.Length > 0)
        {
            SpawnMiniBoss(todayMiniBosses);
        }

        if (enemiesSpawnerInfo.Spawners.Length == 0) return;
        var spawner = allSpawners.Where(s => s.StartDaySpawn <= day).OrderByDescending(s => s.StartDaySpawn).FirstOrDefault();
        if (spawner == null || day % spawner.SpawnPeriod != 0) return;
        SpawnEnemies(spawner);
    }

    private void SpawnEnemies(Spawner spawner)
    {
        // если нужно, можно не останавливать предыдущую корутину — тогда пачки смешаются
        if (_spawnRoutine != null) StopCoroutine(_spawnRoutine);
        _spawnRoutine = StartCoroutine(SpawnEnemiesCoroutine(spawner));
    }

    private IEnumerator SpawnEnemiesCoroutine(Spawner spawner)
    {
        var biome = GetCurrentBiome();

        // Отфильтровали группы по биому один раз
        var groups = spawner.EnemiesSpawnerInfo.Where(g => g.EnemyBiomeInfo.Any(e => e.MonsterType == biome)).ToArray();
        if (groups.Length == 0) yield break;

        // Для каждой группы сразу подготовим варианты по текущему биому
        var variantsPerGroup = new EnemyBiomeInfo[groups.Length][];
        for (int gi = 0; gi < groups.Length; gi++)
            variantsPerGroup[gi] = groups[gi].EnemyBiomeInfo.Where(e => e.MonsterType == biome).ToArray();

        // Вычислим сторону спавна один раз (если не RandomSide)
        SpawnSide spawnSideEnum = SpawnSide.RandomSide;
        if (spawner.LandscapeSpawnSide != null && spawner.LandscapeSpawnSide.Length > 0)
        {
            var matched = spawner.LandscapeSpawnSide.FirstOrDefault(ls => (int)ls.LandscapeEnum == GetLandscapeNumber());
            if (matched != null) spawnSideEnum = matched.SpawnSide;
        }

        int spawnedThisFrame = 0;
        float frameStart = Time.realtimeSinceStartup;

        for (int i = 0; i < spawner.Count; i++)
        {
            // Выбираем группу и её варианты
            int gi = Random.Range(0, groups.Length);
            var group = groups[gi];
            var variants = variantsPerGroup[gi];
            if (variants.Length == 0) continue;

            var entry = variants[Random.Range(0, variants.Length)];

            // Точка спавна
            Vector3 basePoint = spawnSideEnum == SpawnSide.RandomSide ? GetRandomSpawnTransform() : GetSideSpawnTransform((int)spawnSideEnum);

            var enemyPrefab = _allEnemies.GetEnemyForEnum(entry.EnemyEnum);
            var enemyObject = _diContainer.InstantiatePrefab(enemyPrefab, basePoint + GetRandomizePosition(), Quaternion.identity, null);

            // Кэш компонентов (без повторных GetComponent)
            var enemyLevel = enemyObject.GetComponent<EnemyLevel>();
            var enemyInfo = enemyObject.GetComponent<EnemyInfo>();
            var enemyHealth = enemyObject.GetComponent<EnemyHealth>();
            var enemyDamage = enemyObject.GetComponent<EnemyDamage>();

            enemyLevel.SetLevel(group.EnemyLevel);
            enemyInfo.SetEnemyInfo(_enemyNumber, 1, 1, false);
            enemyHealth.SetHealth();
            enemyDamage.SetDamage();
            enemyObject.GetComponent<EnemyScale>().SetScale(false);

            enemyObject.transform.SetParent(_enemiesParent, false);

            AddEnemyToList((int)entry.EnemyEnum, _enemyNumber, enemyObject);
            _enemyNumber++;

            // Тайм-слайс: либо лимит по количеству, либо по времени
            spawnedThisFrame++;
            if (spawnedThisFrame >= _maxSpawnedEnemiesPerFrame)
            {
                spawnedThisFrame = 0;
                frameStart = Time.realtimeSinceStartup;
                yield return null; // следующая порция — в следующий кадр
            }
        }

        _spawnRoutine = null;
    }


    private void SpawnMiniBoss(MiniBossSpawner[] miniBossSpawners)
    {
        foreach (var spawner in miniBossSpawners)
        {
            var variants = spawner.EnemySpawnerInfo.EnemyBiomeInfo.Where(e => e.MonsterType == GetCurrentBiome()).ToArray();

            if (variants.Length == 0) continue;

            for (int i = 0; i < spawner.Count; i++)
            {
                var entry = variants[Random.Range(0, variants.Length)];
                var enemyPrefab = _allEnemies.GetEnemyForEnum(entry.EnemyEnum);

                SpawnSide spawnSideEnum = SpawnSide.RandomSide;

                if (spawner.LandscapeSpawnSide != null && spawner.LandscapeSpawnSide.Length > 0)
                {
                    var matched = spawner.LandscapeSpawnSide.FirstOrDefault(ls => (int)ls.LandscapeEnum == GetLandscapeNumber());

                    if (matched != null) spawnSideEnum = matched.SpawnSide;
                }

                var spawnPoint = spawnSideEnum == SpawnSide.RandomSide ? GetRandomSpawnTransform() : GetSideSpawnTransform((int)spawnSideEnum);
                var enemyObject = _diContainer.InstantiatePrefab(enemyPrefab, spawnPoint + GetRandomizePosition(), Quaternion.identity, null);

                enemyObject.GetComponent<EnemyLevel>().SetLevel(spawner.EnemySpawnerInfo.EnemyLevel);
                enemyObject.GetComponent<EnemyInfo>().SetEnemyInfo(_enemyNumber, spawner.HealthFactor, spawner.DamageFactor, true);
                enemyObject.GetComponent<EnemyHealth>().SetHealth();
                enemyObject.GetComponent<EnemyDamage>().SetDamage();
                enemyObject.GetComponent<EnemyScale>().SetScale(true);

                enemyObject.transform.SetParent(_enemiesParent);

                AddEnemyToList((int)entry.EnemyEnum, _enemyNumber, enemyObject);
                _enemyNumber++;
            }
        }
    }

    private void SpawnBoss(EnemiesSpawner enemiesSpawner)
    {
        var spawner = enemiesSpawner.BossSpawner;
        var bossEntry = spawner.Bosses.FirstOrDefault(b => b.MonsterType == GetCurrentBiome());
        var enemyPrefab = _allEnemies.GetEnemyForEnum(bossEntry.EnemyEnum);

        SpawnSide spawnSideEnum = SpawnSide.RandomSide;

        if (spawner.LandscapeSpawnSide != null && spawner.LandscapeSpawnSide.Length > 0)
        {
            var matched = spawner.LandscapeSpawnSide.FirstOrDefault(ls => (int)ls.LandscapeEnum == GetLandscapeNumber());

            if (matched != null) spawnSideEnum = matched.SpawnSide;
        }
        var spawnPoint = spawnSideEnum == SpawnSide.RandomSide ? GetRandomSpawnTransform() : GetSideSpawnTransform((int)spawnSideEnum);

        var enemyObject = _diContainer.InstantiatePrefab(enemyPrefab, spawnPoint + GetRandomizePosition(), Quaternion.identity, null);

        enemyObject.GetComponent<EnemyLevel>().SetLevel(spawner.BossLevel);
        enemyObject.GetComponent<EnemyInfo>().SetEnemyInfo(_enemyNumber, 1, 1, false);
        enemyObject.GetComponent<BossHealth>().SetHealth();
        enemyObject.GetComponent<BossDamage>().SetDamage();
        enemyObject.transform.SetParent(_enemiesParent);

        AddEnemyToList((int)bossEntry.EnemyEnum, _enemyNumber, enemyObject);
        _enemyNumber++;
    }


    public void LoadEnemies(EnemyData[] enemyData, bool isStartMission)
    {
        if (isStartMission) return;

        for (int i = 0; i < enemyData.Length; i++)
        {
            var position = new Vector3(enemyData[i].PositionX, enemyData[i].PositionY, enemyData[i].PositionZ);
            var rotation = Quaternion.Euler(0f, enemyData[i].Rotation, 0f);
            var enemyObject = _diContainer.InstantiatePrefab(_allEnemies.GetEnemyForNumber(enemyData[i].EnemyEnum), position, rotation, null);
            var isMiniBoss = enemyData[i].HealthFactor > 1 || enemyData[i].DamageFactor > 1;

            enemyObject.GetComponent<EnemyLevel>().SetLevel(enemyData[i].EnemyLevel);
            enemyObject.GetComponent<EnemyInfo>().SetEnemyInfo(_enemyNumber, enemyData[i].HealthFactor, enemyData[i].DamageFactor, enemyData[i].IsMiniBoss);
            enemyObject.GetComponent<BaseHealth>().LoadHealth(enemyData[i].EnemyHealth);
            enemyObject.GetComponent<BaseDamage>().SetDamage();
            enemyObject.GetComponent<EnemyScale>()?.SetScale(enemyData[i].IsMiniBoss);
            enemyObject.transform.SetParent(_enemiesParent);

            AddEnemyToList(enemyData[i].EnemyEnum, _enemyNumber, enemyObject);

            _enemyNumber++;
        }
    }

    private Vector3 GetRandomizePosition()
    {
        var x = Random.Range(-10, 10);
        var z = Random.Range(-10, 10);
        return new Vector3(x, 0, z);
    }

    /// <summary>
    /// берет случайную точку в любой из сторон
    /// </summary>
    private Vector3 GetRandomSpawnTransform()
    {
        var randomSide = Random.Range(0, _enemiesBiomeSpawnTransforms[GetLandscapeNumber()].SpawnSides.Length);
        var randomPoint = Random.Range(0, _enemiesBiomeSpawnTransforms[GetLandscapeNumber()].SpawnSides[randomSide].SpawnPoints.Length);
        return _enemiesBiomeSpawnTransforms[GetLandscapeNumber()].SpawnSides[randomSide].SpawnPoints[randomPoint].position;
    }

    /// <summary>
    /// берет случайную точку в определенной стороне спавна
    /// </summary>
    private Vector3 GetSideSpawnTransform(int side)
    {
        var spawnSides = _enemiesBiomeSpawnTransforms[GetLandscapeNumber()].SpawnSides;
        var spawnSideIndex = side <= spawnSides.Length - 1 ? side : 0;

        var randomPoint = Random.Range(0, spawnSides[spawnSideIndex].SpawnPoints.Length);
        return spawnSides[spawnSideIndex].SpawnPoints[randomPoint].position;
    }

    private void AddEnemyToList(int enemyEnum, int enemyNumber, GameObject prefab)
    {
        _currentEnemiesData.Add(new EnemiesForListData
        {
            EnemyObject = prefab,
            EnemyNumber = enemyNumber,
            EnemyEnum = enemyEnum,
        });
    }

    private void RemoveEnemyFromList(int enemyNumber)
    {
        var enemyToRemove = _currentEnemiesData.Find(el => el.EnemyNumber == enemyNumber);

        if (enemyToRemove != null) _currentEnemiesData.Remove(enemyToRemove);
    }

    private void OnDestroy()
    {
        CustomEvents.OnDayEnd -= PrepareSpawnEnemy;
        CustomEvents.OnEnemyDeath -= RemoveEnemyFromList;
    }
}

[System.Serializable]
public class EnemiesForListData
{
    public int EnemyEnum;
    public int EnemyNumber;
    public GameObject EnemyObject;

}

[System.Serializable]
public class EnemiesBiomeSpawnTransforms
{
    public LandscapeEnum LandscapeEnum;
    public SpawnSidePoints[] SpawnSides;
}

[System.Serializable]
public class SpawnSidePoints
{
    public Transform[] SpawnPoints;
}
