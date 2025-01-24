using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class EnemiesSpawnerSystem : MonoBehaviour
{
    [Inject] DiContainer _diContainer;
    [SerializeField] private AllEnemies _allEnemies;
    [SerializeField] private Transform _enemiesParent;
    private Vector3 _bottomLeft = new Vector3(-155, 11, -213);
    private Vector3 _bottomRight = new Vector3(380, 11, -213);
    private Vector3 _topRight = new Vector3(380, 11, 327);
    private Vector3 _topLeft = new Vector3(-155, 11, 327);
    private float _bottomLength, _rightLength, _topLength, _leftLength, _totalPerimeter;
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
                EnemyHealth = _currentEnemiesData[i].EnemyObject.GetComponent<EnemyHealth>().GetCurrentHealth(),
            };
        }

        return data;
    }

    private void Awake()
    {
        CustomEvents.OnDayEnd += PrepareSpawn;
        CustomEvents.OnEnemyDeath += RemoveEnemyFromList;
    }

    private void Start()
    {
        CalculateDistance();
    }

    private void CalculateDistance()
    {
        _bottomLength = Vector3.Distance(_bottomLeft, _bottomRight);
        _rightLength = Vector3.Distance(_bottomRight, _topRight);
        _topLength = Vector3.Distance(_topRight, _topLeft);
        _leftLength = Vector3.Distance(_topLeft, _bottomLeft);
        _totalPerimeter = _bottomLength + _rightLength + _topLength + _leftLength;
    }

    private void PrepareSpawn(int day)
    {
        var spawner = CurrentMissionInfo.Instance.GetCurrentMission().EnemiesSpawnerInfo.Spawners.FirstOrDefault(spawner => spawner.DaySpawn == day);

        if (spawner == null) return;

        SpawnEnemies(spawner);
    }

    private void SpawnEnemies(Spawner spawner)
    {
        var rndCount = Random.Range(spawner.MinCount, spawner.MaxCount + 1);

        for (int i = 0; i < rndCount; i++)
        {
            var rndEnemy = Random.Range(0, spawner.EnemiesSpawnerInfo.Length);
            var enemyObject = _diContainer.InstantiatePrefab(_allEnemies.GetEnemyForEnum(spawner.EnemiesSpawnerInfo[rndEnemy].EnemyEnum), GetRandomPerimeterPosition() + GetRandomizePosition(), Quaternion.identity, null);
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
            enemyObject.GetComponent<EnemyHealth>().LoadStartStats(enemyData[i].EnemyHealth);
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

    private Vector3 GetRandomPerimeterPosition()
    {
        // Выбираем случайную точку вдоль периметра
        float randomPoint = Random.Range(0f, _totalPerimeter);

        if (randomPoint <= _bottomLength)
        {
            // Нижний край (между bottomLeft и bottomRight)
            return Vector3.Lerp(_bottomLeft, _bottomRight, randomPoint / _bottomLength);
        }
        else if (randomPoint <= _bottomLength + _rightLength)
        {
            // Правый край (между bottomRight и topRight)
            return Vector3.Lerp(_bottomRight, _topRight, (randomPoint - _bottomLength) / _rightLength);
        }
        else if (randomPoint <= _bottomLength + _rightLength + _topLength)
        {
            // Верхний край (между topRight и topLeft)
            return Vector3.Lerp(_topRight, _topLeft, (randomPoint - _bottomLength - _rightLength) / _topLength);
        }
        else
        {
            // Левый край (между topLeft и bottomLeft)
            return Vector3.Lerp(_topLeft, _bottomLeft, (randomPoint - _bottomLength - _rightLength - _topLength) / _leftLength);
        }
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
        CustomEvents.OnDayEnd -= PrepareSpawn;
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
