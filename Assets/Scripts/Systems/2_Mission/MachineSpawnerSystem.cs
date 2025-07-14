using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MachineSpawnerSystem : MonoBehaviour
{
    [Inject] DiContainer _diContainer;
    [SerializeField] private TileMapBuilder _mapBuilder;
    [SerializeField] private GameObject[] _machinePrefabs;
    [SerializeField] private Transform _parent;
    [SerializeField] private CurrentMachineSystem _currentMachineSystem;
    private TileObject _machineProductionTileObject;

    public void SetTileObject(TileObject tileObject) => _machineProductionTileObject = tileObject;

    private int GetNearRoadTileObject(List<TileObject> roadTiles)
    {
        var nearTileObject = _machineProductionTileObject.GetNearNeighbourCrossRoad();
        var buildingTileObject = nearTileObject.BuildingTileObject();

        for (int i = 0; i < roadTiles.Count; i++)
        {
            if (roadTiles[i] == nearTileObject)
            {
                if (buildingTileObject.HaveTile())
                {
                    if (buildingTileObject.IsConstructionNow())
                    {
                        buildingTileObject.DestroyBuildingTile(false);
                    }
                    else
                    {
                        nearTileObject.BuildingHealth().Death();
                    }
                }
                return i;
            }
        }
        Debug.Log("Dont find NearTileObject");
        return 0;
    }

    public void SpawnMachine(MachineType machineType)
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.MachinesSpawn[(int)machineType], transform.position);

        var roadTiles = _mapBuilder.GetRoadTiles();
        Vector3 spawnPosition = _machineProductionTileObject.GetNearNeighbourCrossRoad().transform.position;

        _currentMachineSystem.SetNewMachine(_diContainer.InstantiatePrefab(_machinePrefabs[(int)machineType], spawnPosition, Quaternion.identity, _parent), machineType);
        _currentMachineSystem.GetMachinePatrolPath().InitializePatrolPoints(roadTiles, GetNearRoadTileObject(roadTiles));
        _currentMachineSystem.GetMachineHealth().SetHealth();
        _currentMachineSystem.GetMachineDamage().SetDamage();
    }

    public void LoadSpawnRobot(MissionSaveData missionSaveData)
    {
        if (missionSaveData.IsStartMission || !missionSaveData.MachineData.IsHaveMachineNow) return;

        var roadTiles = _mapBuilder.GetRoadTiles();

        var spawnPosition = new Vector3(missionSaveData.MachineData.PositionX, missionSaveData.MachineData.PositionY, missionSaveData.MachineData.PositionZ);
        var rotation = Quaternion.Euler(0f, missionSaveData.MachineData.Rotation, 0f);

        // Приведение int к enum
        var robotTypeEnum = (MachineType)missionSaveData.MachineData.MachineType;

        _currentMachineSystem.SetNewMachine(_diContainer.InstantiatePrefab(_machinePrefabs[missionSaveData.MachineData.MachineType], spawnPosition, rotation, _parent), robotTypeEnum);
        _currentMachineSystem.GetMachinePatrolPath().InitializePatrolPoints(roadTiles, missionSaveData.MachineData.NextPatrolIndex);

        _currentMachineSystem.GetMachineHealth().LoadHealth(missionSaveData.MachineData.MachineHealth);
    }
}
