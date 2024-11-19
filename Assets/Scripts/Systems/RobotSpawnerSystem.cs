using UnityEngine;
using Zenject;

public class RobotSpawnerSystem : MonoBehaviour
{
    [Inject] DiContainer _diContainer;
    [SerializeField] private MapBuilder _mapBuilder;
    [SerializeField] private GameObject[] _robotsPrefabs;
    [SerializeField] private Transform _parent;
    [SerializeField] private CurrentRobotSystem _currentRobotSystem;

    public void SpawnRobot(RobotType robotType)
    {
        var roadTiles = _mapBuilder.GetRoadTiles();

        int randomIndex = Random.Range(0, roadTiles.Count);
        var randomTile = roadTiles[randomIndex];

        Vector3 spawnPosition = randomTile.transform.position;

        _currentRobotSystem.SetNewRobot(_diContainer.InstantiatePrefab(_robotsPrefabs[(int)robotType], spawnPosition, Quaternion.identity, _parent), robotType);
        _currentRobotSystem.RobotPatrolPath().InitializePatrolPoints(roadTiles, randomIndex);
    }
}
