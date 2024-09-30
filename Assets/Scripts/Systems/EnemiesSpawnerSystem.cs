using System.Linq;
using UnityEngine;
using Zenject;

public class EnemiesSpawnerSystem : MonoBehaviour
{
    [Inject] DiContainer _diContainer;
    [SerializeField] private EnemiesSpawnerInformation _enemiesSpawnerInfo;
    [SerializeField] private Transform _enemiesParent;
    [SerializeField] TilesSystem _tilesSystem;
    private Vector3 _bottomLeft = new Vector3(-155, 11, -213);
    private Vector3 _bottomRight = new Vector3(380, 11, -213);
    private Vector3 _topRight = new Vector3(380, 11, 327);
    private Vector3 _topLeft = new Vector3(-155, 11, 327);

    private float _bottomLength, _rightLength, _topLength, _leftLength, _totalPerimeter;

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

    public void PrepareSpawn(int day)
    {
        if(!_tilesSystem.IsHaveBase) return;
        
        var spawner = _enemiesSpawnerInfo.Spawners.FirstOrDefault(spawner => spawner.DaySpawn == day);

        if (spawner == null) return;

        SpawnEnemies(spawner);
    }

    private void SpawnEnemies(Spawner spawner)
    {
        var rndCount = Random.Range(spawner.MinCount, spawner.MaxCount + 1);
        Vector3 spawnPosition = GetRandomPerimeterPosition();

        for (int i = 0; i < rndCount; i++)
        {
            var rndEnemy = Random.Range(0, spawner.Enemies.Length);
            var enemy = _diContainer.InstantiatePrefab(spawner.Enemies[rndEnemy], spawnPosition + GetRandomizePosition(), Quaternion.identity, null);
            enemy.GetComponent<BaseLevel>().SetLevel(spawner.EnemyLevel);
            enemy.transform.SetParent(_enemiesParent);
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
}
