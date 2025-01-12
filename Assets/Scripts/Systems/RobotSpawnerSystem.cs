using UnityEngine;
using Zenject;

public class RobotSpawnerSystem : MonoBehaviour
{
    [Inject] DiContainer _diContainer;
    [SerializeField] private TileMapBuilder _mapBuilder;
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

    public void LoadSpawnRobot(WorldSaveData worldSaveData)
    {
        if (worldSaveData.IsStartMission || !worldSaveData.RobotData.IsHaveRobotNow) return;

        var roadTiles = _mapBuilder.GetRoadTiles();

        var spawnPosition = new Vector3(worldSaveData.RobotData.PositionX, worldSaveData.RobotData.PositionY, worldSaveData.RobotData.PositionZ);
        var rotation = Quaternion.Euler(0f, worldSaveData.RobotData.Rotation, 0f);

        // Приведение int к enum
        var robotTypeEnum = (RobotType)worldSaveData.RobotData.RobotType;

        _currentRobotSystem.SetNewRobot(_diContainer.InstantiatePrefab(_robotsPrefabs[worldSaveData.RobotData.RobotType], spawnPosition, rotation, _parent), robotTypeEnum);
        _currentRobotSystem.RobotPatrolPath().InitializePatrolPoints(roadTiles, worldSaveData.RobotData.NextPatrolIndex);


    }
}
