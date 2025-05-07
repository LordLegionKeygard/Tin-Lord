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
        var enemiesSpawnerInfo = CurrentMissionInfo.Instance.GetCurrentMission().EnemiesSpawnerInfo;
        var allSpawners = enemiesSpawnerInfo.Spawners;

        if(enemiesSpawnerInfo.BossEnum!= EnemyEnum.None)
        {
            if(enemiesSpawnerInfo.BossDaySpawn == day) SpawnBoss(enemiesSpawnerInfo);
        }

        if (allSpawners.Length == 0) return;
        if (enemiesSpawnerInfo.LastDaySpawn != 0 && day > enemiesSpawnerInfo.LastDaySpawn) return;
        var spawner = allSpawners
            .Where(s => s.StartDaySpawn <= day)
            .OrderByDescending(s => s.StartDaySpawn)
            .FirstOrDefault();
        if (spawner == null) return;
        if (day % spawner.SpawnPeriod != 0) return;
        SpawnEnemies(spawner);
    }

    private void SpawnBoss(EnemiesSpawnerInformation info)
    {
        var enemyObject = _diContainer.InstantiatePrefab(_allEnemies.GetEnemyForEnum(info.BossEnum), GetRandomSpawnTransform() + GetRandomizePosition(), Quaternion.identity, null);
        enemyObject.GetComponent<EnemyLevel>().SetLevel(info.BossLevel);
        enemyObject.GetComponent<EnemyInfo>().SetEnemyInfo(_enemyNumber);
        enemyObject.GetComponent<BossHealth>().SetStartStats();
        enemyObject.transform.SetParent(_enemiesParent);

        AddEnemyToList((int)info.BossEnum, _enemyNumber, enemyObject);
        _enemyNumber++;
    }


    private void SpawnEnemies(Spawner spawner)
    {
        for (int i = 0; i < spawner.Count; i++)
        {
            var rndEnemy = Random.Range(0, spawner.EnemiesSpawnerInfo.Length);
            var enemyObject = _diContainer.InstantiatePrefab(_allEnemies.GetEnemyForEnum(spawner.EnemiesSpawnerInfo[rndEnemy].EnemyEnum), GetRandomSpawnTransform() + GetRandomizePosition(), Quaternion.identity, null);
            enemyObject.GetComponent<EnemyLevel>().SetLevel(spawner.EnemiesSpawnerInfo[rndEnemy].EnemyLevel);
            enemyObject.GetComponent<EnemyInfo>().SetEnemyInfo(_enemyNumber);
            enemyObject.GetComponent<EnemyHealth>().SetStartStats();
            enemyObject.transform.SetParent(_enemiesParent);

            AddEnemyToList((int)spawner.EnemiesSpawnerInfo[rndEnemy].EnemyEnum, _enemyNumber, enemyObject);

            _enemyNumber++;
        }
    }

    public void LoadEnemies(EnemyData[] enemyData, bool isStartMission)
    {
        if (isStartMission) return;

        for (int i = 0; i < enemyData.Length; i++)
        {
            var position = new Vector3(enemyData[i].PositionX, enemyData[i].PositionY, enemyData[i].PositionZ);
            var rotation = Quaternion.Euler(0f, enemyData[i].Rotation, 0f);
            var enemyObject = _diContainer.InstantiatePrefab(_allEnemies.GetEnemyForNumber(enemyData[i].EnemyEnum), position, rotation, null);
            enemyObject.GetComponent<EnemyLevel>().SetLevel(enemyData[i].EnemyLevel);
            enemyObject.GetComponent<EnemyInfo>().SetEnemyInfo(_enemyNumber);
            enemyObject.GetComponent<BaseHealth>().LoadStartStats(enemyData[i].EnemyHealth);
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

    private Vector3 GetRandomSpawnTransform()
    {
        var missionId = CurrentMissionInfo.Instance.GetCurrentMission().MissionId;
        var randomTransform = Random.Range(0, _enemiesBiomeSpawnTransforms[missionId].SpawnPoints.Length);
        return _enemiesBiomeSpawnTransforms[missionId].SpawnPoints[randomTransform].position;
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
    public Transform[] SpawnPoints;
}
