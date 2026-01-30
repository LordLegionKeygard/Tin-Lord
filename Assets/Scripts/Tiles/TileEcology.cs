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
        if (_tileObject == null)
        {
            return 0;
        }

        var haveBuildingTile = _tileObject.BuildingTileObject().IsHaveTile();
        var isConstructionNow = haveBuildingTile && _tileObject.BuildingTileObject().IsConstructionNow();

        float buildingEcology = 0;


        var currentGroundTile = _tileObject.GroundTileObject().CurrentGroundTile();
        var groundEcology = currentGroundTile != null ? currentGroundTile.GroundEcology : 0f;
        var rarityBonusEcology = currentGroundTile != null ? _tileObject.GetRarity() : 0;

        if (haveBuildingTile && !isConstructionNow)
        {
            var currentBuilding = _tileObject.BuildingTileObject().GetCurrentBuilding();
            var baseBuildingEcology = currentBuilding.BuildingEcology;

            if (_tileObject.BuildingTileObject().IsEcologyBuilding())
            {
                buildingEcology = _tileObject.IsBuildingWork() ? currentBuilding.BuildingEcologicalRestoration : baseBuildingEcology;
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
            GetEcologyEnum.Total => groundEcology + rarityBonusEcology - 1 + buildingEcology,
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
