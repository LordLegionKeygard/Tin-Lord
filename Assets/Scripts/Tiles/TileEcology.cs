using UnityEngine;

public class TileEcology : MonoBehaviour
{
    private TileObject _tileObject;

    private void Awake()
    {
        _tileObject = GetComponent<TileObject>();
    }
    public float GetEcology(GetEcologyEnum getEcologyEnum)
    {
        var haveBuildingTile = _tileObject.BuildingTileObject().HaveTile();
        var isConstructionNow = haveBuildingTile && _tileObject.BuildingTileObject().IsConstructionNow();

        float buildingEcology = 0;


        var groundEcology = _tileObject.GroundTileObject().CurrentGroundTile().GroundEcology;

        if (haveBuildingTile && !isConstructionNow)
        {
            var currentBuilding = _tileObject.BuildingTileObject().CurrentBuilding();
            var baseBuildingEcology = currentBuilding.BuildingEcology;

            if (_tileObject.BuildingTileObject().IsEcologyBuilding())
            {
                buildingEcology = _tileObject.IsBuildingWork() ? currentBuilding.BuildingEcologyPurifier : baseBuildingEcology;
            }
            else
            {
                buildingEcology = _tileObject.IsBuildingWork() ? baseBuildingEcology : baseBuildingEcology / 2;
            }
        }

        return getEcologyEnum switch
        {
            GetEcologyEnum.Ground => groundEcology,
            GetEcologyEnum.Building => buildingEcology,
            GetEcologyEnum.Total => groundEcology + buildingEcology,
            _ => 0,
        };
    }
}

public enum GetEcologyEnum
{
    Ground = 0,
    Building = 1,
    Total = 2,
}
