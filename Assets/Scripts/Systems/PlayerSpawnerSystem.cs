using UnityEngine;
using Zenject;

public class PlayerSpawnerSystem : MonoBehaviour
{
    [Inject] DiContainer _diContainer;
    [SerializeField] private MapBuilder _mapBuilder;
    [SerializeField] private GameObject[] _robotsPrefabs;
    [SerializeField] private Transform _parent;

    [Header("CurrentRobot")]
    private GameObject _currentRobotPrefab;
    private RobotHealth _currentRobotHealth;
    public bool HaveRobot() => _currentRobotPrefab != null;
    public bool RobotDeath() => _currentRobotHealth.IsDeath();

    private void Start()
    {

    }

    private void OnDestroy()
    {

    }

    private void SpawnPlayer(RobotType robotType)
    {
        var roadTiles = _mapBuilder.GetRoadTiles();

        int randomIndex = Random.Range(0, roadTiles.Count);
        var randomTile = roadTiles[randomIndex];

        Vector3 spawnPosition = randomTile.transform.position;

        _currentRobotPrefab = _diContainer.InstantiatePrefab(_robotsPrefabs[(int)robotType], spawnPosition, Quaternion.identity, _parent);

        _currentRobotHealth = _currentRobotPrefab.GetComponent<RobotHealth>();
        var patrolPath = _currentRobotPrefab.GetComponent<RobotPatrolPath>();

        patrolPath.InitializePatrolPoints(roadTiles, randomIndex);
    }
}

[System.Serializable]
public enum RobotType
{
    Tank = 0,
    Sniper = 1,
    Engineer = 2,
}
