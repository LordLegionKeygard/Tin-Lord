using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AllTileObjects : MonoBehaviour
{
    [Inject] readonly PlayerResources _playerResources;
    public List<TileObject> TileObjects;

    public void SetNeighbours(int mapLength)
    {
        float rightEdge = (mapLength - 1) * 10f;
        float leftEdge = 0f;

        for (int i = 0; i < TileObjects.Count; i++)
        {
            TileObjects[i].SetNeighbourTiles(new TileObject[]
            {
            (i + mapLength > TileObjects.Count - 1) ? null: TileObjects[i + mapLength],
            (i + mapLength + 1 > TileObjects.Count - 1)? null: (TileObjects[i].transform.position.x == rightEdge ? null: TileObjects[i + mapLength + 1]),
            (i + 1 > TileObjects.Count - 1) ? null: (TileObjects[i].transform.position.x == rightEdge ? null: TileObjects[i + 1]),
            (i - (mapLength - 1) < 0) ? null: (TileObjects[i].transform.position.x == rightEdge ? null: TileObjects[i - (mapLength - 1)]),
            (i - mapLength < 0)? null: TileObjects[i - mapLength],
            (i - (mapLength + 1) < 0) ? null: (TileObjects[i].transform.position.x == leftEdge ? null: TileObjects[i - (mapLength + 1)]),
            (i - 1 < 0)? null: (TileObjects[i].transform.position.x == leftEdge ? null: TileObjects[i - 1]),
            (i + (mapLength - 1) > TileObjects.Count - 1)? null: (TileObjects[i].transform.position.x == leftEdge ? null: TileObjects[i + (mapLength - 1)])
            });
        }

        SetId();
    }


    private void SetId()
    {
        for (int i = 0; i < TileObjects.Count; i++)
        {
            TileObjects[i].SetId(i);
        }
    }

    public TileDataWrapper[] GetAllTileObjects()
    {
        var tilesData = new TileDataWrapper[TileObjects.Count];

        for (int i = 0; i < TileObjects.Count; i++)
        {
            var tileObject = TileObjects[i];
            var groundHaveTile = tileObject.GroundTileObject().HaveTile();
            var buildingHaveTile = tileObject.BuildingTileObject().HaveTile() && !tileObject.BuildingHealth().IsDeath();
            var isWater = tileObject.GroundTileObject().IsWaterTile();
            var riverTile = tileObject.GroundTileObject().CurrentTileRiver();
            var haveBuildingTileGameObject = tileObject.BuildingTileObject().HaveBuildingGameObject();
            var haveRotationView = buildingHaveTile && haveBuildingTileGameObject && tileObject.BuildingTileObject().CurrentBuildingGameObject().GetComponent<RotationView>() != null;
            var haveRequiredResource = buildingHaveTile && tileObject.CurrentResourceRequired() != null;
            var haveProductionResource = buildingHaveTile && tileObject.CurrentResourceProduction() != null;

            tilesData[i] = new TileDataWrapper
            {
                GroundData = new GroundData
                {
                    GroundTileId = groundHaveTile ? (int)tileObject.GroundTileObject().CurrentGroundTile().GroundTileView : (int)GroundTileViewEnum.None,
                    GroundTileRotation = groundHaveTile ? tileObject.GroundTileObject().CurrentGroundTileObject().transform.eulerAngles.y : 0,
                    GroundModelRotation = groundHaveTile ? tileObject.GroundTileObject().GroundModelRotation() : 0,
                    IsForwardRoad = groundHaveTile ? tileObject.GroundTileObject().IsForwardRoad() : false,
                    RiftViewNumber = groundHaveTile ? tileObject.GetRiftViewNumber() : -1,
                },
                BuildingData = new BuildingData
                {
                    BuildingTileTypeId = buildingHaveTile ? (int)tileObject.BuildingTileObject().CurrentBuildingTile().BuildingTileView : -1,
                    BuildingTileLevel = buildingHaveTile ? tileObject.BuildingTileObject().CurrentBuildingLevel() : -1,
                    BuildingHealth = buildingHaveTile ? tileObject.BuildingHealth().GetCurrentHealth() : 0,
                    IsBuildingWork = buildingHaveTile && tileObject.IsBuildingWork(),
                    BuildingTilePositionY = buildingHaveTile ? tileObject.BuildingTileObject().GetBuildingTileTransform().GetPositionY() : 0,
                    BuildingTilePositionX = buildingHaveTile ? tileObject.BuildingTileObject().GetBuildingTileTransform().GetPositionX() : 0,
                    BuildingTilePositionZ = buildingHaveTile ? tileObject.BuildingTileObject().GetBuildingTileTransform().GetPositionZ() : 0,
                    BuildingRotation = buildingHaveTile && haveBuildingTileGameObject && haveRotationView ? tileObject.BuildingTileObject().CurrentBuildingGameObject().GetComponent<RotationView>().GetObjectRotation() : 0,
                    RequiredResource = haveRequiredResource ? _playerResources.GetResourceNumberForResource(tileObject.CurrentResourceRequired()) : -1,
                    RequiredResourceAmount = buildingHaveTile ? tileObject.CurrentResourceRequiredAmount() : 0,
                    ResourceProduction = haveProductionResource ? _playerResources.GetResourceNumberForResource(tileObject.CurrentResourceProduction()) : -1,
                    IsConstructionNow = buildingHaveTile && tileObject.BuildingTileObject().IsConstructionNow(),
                    IsUpgradeBase = buildingHaveTile && tileObject.BuildingTileObject().IsUpgradeBase(),
                    PreviousBaseBuildingHealth = buildingHaveTile && tileObject.BuildingTileObject().IsUpgradeBase() ? tileObject.BuildingTileObject().PreviousBaseBuildingHealth() : 0,
                },
                WaterData = new WaterData
                {
                    IsLake = isWater && riverTile.IsLake(),
                    RiverNumber = isWater ? riverTile.GetRiverNumber() : 0,
                    IsBridge = isWater && riverTile.IsBridge(),
                    IsLastRiverTile = isWater && riverTile.IsLastRiverTile(),
                    RiverType = isWater ? (int)riverTile.GetRiverTypeEnum() : (int)RiverTypeEnum.None,
                    RiverRotation = isWater ? riverTile.GetRiverRotation() : 0
                }
            };
        }

        return tilesData;
    }


    public void LoadTiles(TileDataWrapper[] tilesData, bool isStartMission)
    {
        if (isStartMission) return;

        for (int i = 0; i < TileObjects.Count; i++)
        {
            if (tilesData[i].GroundData.GroundTileId == (int)GroundTileViewEnum.None) continue;
            TileObjects[i].GroundTileObject().LoadGroundTile(tilesData[i]);
        }

        CustomEvents.FireSpawnRoadComplete();

        for (int i = 0; i < TileObjects.Count; i++)
        {
            if (tilesData[i].BuildingData.BuildingTileTypeId == (int)BuildingTileViewEnum.None) continue;
            TileObjects[i].BuildingTileObject().LoadBuildingTile(tilesData[i]);
        }
    }
}
