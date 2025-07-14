using System.Collections.Generic;
using UnityEngine;

public class MachinePatrolPath : MonoBehaviour
{
    [SerializeField] private MachinePatrolState _machinePatrolState;
    private List<TileObject> _roadTileObjects;
    public MachinePatrolState MachinePatrolState() => _machinePatrolState;

    public void InitializePatrolPoints(List<TileObject> roadTiles, int startIndex)
    {
        _machinePatrolState.InitializePatrol(startIndex);
        _roadTileObjects = roadTiles;
    }

    public int GetNextPointIndex(int currentIndex, int direction)
    {
        return (currentIndex + direction + _roadTileObjects.Count) % _roadTileObjects.Count;
    }

    public int GetPreviousPointIndex(int currentIndex, int direction)
    {
        return (currentIndex - direction + _roadTileObjects.Count) % _roadTileObjects.Count;
    }

    public bool CheckTileForGate(int index)
    {
        var tile = _roadTileObjects[index];
        var hasBuilding = tile.BuildingTileObject().HaveTile();
        var buildingTileView = hasBuilding ? tile.BuildingTileObject().CurrentBuildingTile().BuildingTileView : BuildingTileViewEnum.None;

        return hasBuilding && buildingTileView == BuildingTileViewEnum.Gates;
    }

    public bool ShouldChangeDirection(int index)
    {
        var tile = _roadTileObjects[index];
        var isWaterTile = tile.GroundTileObject().IsWaterTile();
        var hasBuilding = tile.BuildingTileObject().HaveTile();
        var isConstructionNow = tile.BuildingTileObject().IsConstructionNow();
        var buildingTileView = hasBuilding ? tile.BuildingTileObject().CurrentBuildingTile().BuildingTileView : BuildingTileViewEnum.None;

        return isConstructionNow || (isWaterTile && !hasBuilding) || (isWaterTile && hasBuilding && buildingTileView != BuildingTileViewEnum.Bridge) || hasBuilding && buildingTileView is BuildingTileViewEnum.Traps or BuildingTileViewEnum.Walls;
    }

    public TileObject GetTile(int index)
    {
        return _roadTileObjects[index];
    }
}

