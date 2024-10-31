using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEcology : MonoBehaviour
{
    private TileObject _tileObject;

    private void Awake()
    {
        _tileObject = GetComponent<TileObject>();
    }
    public int GetEcology(GetEcologyEnum getEcologyEnum)
    {
        var haveBuildingTile = _tileObject.BuildingTileObject().HaveTile();
        var baseBuildingEcology = haveBuildingTile ? _tileObject.BuildingTileObject().CurrentBuilding().BuildingEcology : 0;

        var groundEcology = _tileObject.GroundTileObject().CurrentGroundTile().GroundEcology;

        int buildingEcology;

        if (_tileObject.BuildingTileObject().IsEcologyBuilding())
        {
            buildingEcology = haveBuildingTile ? _tileObject.IsBuildingWork() ? _tileObject.BuildingTileObject().CurrentBuilding().BuildingEcologyPurifier : baseBuildingEcology : 0;
        }
        else
        {
            buildingEcology = haveBuildingTile ? _tileObject.IsBuildingWork() ? baseBuildingEcology : baseBuildingEcology / 2 : 0;
        }

        switch (getEcologyEnum)
        {
            case GetEcologyEnum.Ground:
                return groundEcology;
            case GetEcologyEnum.Building:
                return buildingEcology;
            case GetEcologyEnum.Total:
                return groundEcology + buildingEcology;
            default: return 0;
        }
    }
}

public enum GetEcologyEnum
{
    Ground = 0,
    Building = 1,
    Total = 2,
}
