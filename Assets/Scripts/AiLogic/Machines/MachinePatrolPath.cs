using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MachinePatrolPath : MonoBehaviour
{
    [FormerlySerializedAs("_robotPatrolState")] [SerializeField] private MachinePatrolState machinePatrolState;
    private List<TileObject> _roadTileObjects;
    public MachinePatrolState RobotPatrolState() => machinePatrolState;

    public void InitializePatrolPoints(List<TileObject> roadTiles, int startIndex)
    {
        machinePatrolState.InitializePatrol(startIndex);
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

        return hasBuilding && buildingTileView == BuildingTileViewEnum.ProtectiveStructures;
    }

    public bool ShouldChangeDirection(int index)
    {
        var tile = _roadTileObjects[index];
        var isWaterTile = tile.GroundTileObject().IsWaterTile();
        var hasBuilding = tile.BuildingTileObject().HaveTile();
        var buildingTileView = hasBuilding ? tile.BuildingTileObject().CurrentBuildingTile().BuildingTileView : BuildingTileViewEnum.None;

        return (isWaterTile && !hasBuilding) || (isWaterTile && hasBuilding && buildingTileView != BuildingTileViewEnum.Bridge);
    }

    public TileObject GetTile(int index)
    {
        return _roadTileObjects[index];
    }
}

