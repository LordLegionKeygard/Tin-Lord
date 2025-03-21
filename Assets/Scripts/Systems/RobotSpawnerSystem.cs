using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RobotSpawnerSystem : MonoBehaviour
{
    [Inject] DiContainer _diContainer;
    [SerializeField] private TileMapBuilder _mapBuilder;
    [SerializeField] private GameObject[] _robotsPrefabs;
    [SerializeField] private Transform _parent;
    [SerializeField] private CurrentRobotSystem _currentRobotSystem;
    private TileObject _machineProductionTileObject;

    public void SetTileObject(TileObject tileObject) => _machineProductionTileObject = tileObject;

    private int GetNearRoadTileObject(List<TileObject> roadTiles)
    {
        var nearTileObject = _machineProductionTileObject.GetNearNeighbourCrossRoad();

        for (int i = 0; i < roadTiles.Count; i++)
        {
            if(roadTiles[i] == nearTileObject) return i;
        }
        Debug.Log("Dont find NearTileObject");
        return 0;
    }

    public void SpawnRobot(RobotType robotType)
    {
        var roadTiles = _mapBuilder.GetRoadTiles();
        Vector3 spawnPosition = _machineProductionTileObject.GetNearNeighbourCrossRoad().transform.position;

        _currentRobotSystem.SetNewRobot(_diContainer.InstantiatePrefab(_robotsPrefabs[(int)robotType], spawnPosition, Quaternion.identity, _parent), robotType);
        _currentRobotSystem.RobotPatrolPath().InitializePatrolPoints(roadTiles, GetNearRoadTileObject(roadTiles));
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
