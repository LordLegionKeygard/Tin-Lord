using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AllTileObjects : MonoBehaviour
{
    [Inject] readonly MissionResources _missionResources;
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
            var buildingHaveTile = tileObject.BuildingTileObject().IsHaveTile() && !tileObject.BuildingHealth().IsDeath();
            var isWater = tileObject.GroundTileObject().IsWaterTile();
            var riverTile = tileObject.GroundTileObject().CurrentTileRiver();
            var haveBuildingTileGameObject = tileObject.BuildingTileObject().IsHaveBuildingGameObject();
            var haveRotationView = buildingHaveTile && haveBuildingTileGameObject && tileObject.BuildingTileObject().CurrentBuildingGameObject().GetComponent<RotationView>() != null;
            var haveRequiredResource = buildingHaveTile && tileObject.CurrentResourceForWork() != null;
            var haveProductionResource = buildingHaveTile && tileObject.GetCurrentResourceProduction() != null;

            tilesData[i] = new TileDataWrapper
            {
                GroundData = new GroundData
                {
                    GroundTileId = groundHaveTile ? tileObject.GroundTileObject().CurrentGroundTile().Id : -1,
                    GroundTileRotation = groundHaveTile ? tileObject.GroundTileObject().CurrentGroundTileObject().transform.eulerAngles.y : 0,
                    GroundModelRotation = groundHaveTile ? tileObject.GroundTileObject().GroundModelRotation() : 0,
                    IsForwardRoad = groundHaveTile ? tileObject.GroundTileObject().IsForwardRoad() : false,
                    RiftViewTileId = groundHaveTile ? tileObject.GetRiftViewTileId() : -1,
                    Rarity = groundHaveTile ? tileObject.GetRarity() : 0
                },
                BuildingData = new BuildingData
                {
                    BuildingTileTypeId = buildingHaveTile ? (int)tileObject.BuildingTileObject().GetCurrentBuildingTile().BuildingTileView : -1,
                    BuildingTileLevel = buildingHaveTile ? tileObject.BuildingTileObject().GetCurrentBuildingLevel() : -1,
                    BuildingHealth = buildingHaveTile ? tileObject.BuildingHealth().GetCurrentHealth() : 0,
                    IsBuildingWork = buildingHaveTile && tileObject.IsBuildingWork(),
                    BuildingTilePositionY = buildingHaveTile ? tileObject.BuildingTileObject().GetBuildingTileTransform().GetPositionY() : 0,
                    BuildingTilePositionX = buildingHaveTile ? tileObject.BuildingTileObject().GetBuildingTileTransform().GetPositionX() : 0,
                    BuildingTilePositionZ = buildingHaveTile ? tileObject.BuildingTileObject().GetBuildingTileTransform().GetPositionZ() : 0,
                    BuildingRotation = buildingHaveTile && haveBuildingTileGameObject && haveRotationView ? tileObject.BuildingTileObject().CurrentBuildingGameObject().GetComponent<RotationView>().GetObjectRotation() : 0,
                    RequiredResource = haveRequiredResource ? _missionResources.GetResourceNumberForResource(tileObject.CurrentResourceForWork()) : -1,
                    RequiredResourceAmount = buildingHaveTile ? tileObject.CurrentResourceForWorkAmount() : 0,
                    ResourceProduction = haveProductionResource ? _missionResources.GetResourceNumberForResource(tileObject.GetCurrentResourceProduction()) : -1,
                    IsConstructionNow = buildingHaveTile && tileObject.BuildingTileObject().IsConstructionNow(),
                    IsUpgradeBase = buildingHaveTile && tileObject.BuildingTileObject().IsUpgradeBase(),
                    PreviousBaseBuildingHealth = buildingHaveTile && tileObject.BuildingTileObject().IsUpgradeBase() ? tileObject.BuildingTileObject().PreviousBaseBuildingHealth() : 0,
                    IsGeneralRepairSelect = buildingHaveTile && tileObject.IsGeneralRepairSelect(),
                    TacticCardIncreaseDamage = buildingHaveTile ? tileObject.BuildingTileObject().GetTacticCardIncreaseDamageLevel() : 0,
                    TacticCardIncreaseHealth = buildingHaveTile ? tileObject.BuildingTileObject().GetTacticCardIncreaseHealthLevel() : 0,
                },
                WaterData = new WaterData
                {
                    IsLake = isWater && riverTile.IsLake(),
                    RiverNumber = isWater ? riverTile.GetRiverNumber() : 0,
                    IsBridge = isWater && riverTile.IsBridge(),
                    IsLastRiverTile = isWater && riverTile.IsLastRiverTile(),
                    RiverType = isWater ? (int)riverTile.GetRiverTypeEnum() : (int)RiverTypeEnum.None,
                    RiverRotation = isWater ? riverTile.GetRiverRotation() : 0
                },
                TileWorldEventData = new TileWorldEventData
                {
                    ToxicGasTicksNumber = tileObject.GetTileObjectEvents().GetToxicGasTicks(),
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
            if (tilesData[i].GroundData.GroundTileId == -1) continue;
            TileObjects[i].GroundTileObject().LoadGroundTile(tilesData[i]);
        }

        CustomEvents.FireSpawnRoadComplete();

        for (int i = 0; i < TileObjects.Count; i++)
        {
            if (tilesData[i].BuildingData.BuildingTileTypeId == (int)BuildingTileViewEnum.None) continue;
            TileObjects[i].BuildingTileObject().LoadBuildingTile(tilesData[i]);
        }

        for (int i = 0; i < TileObjects.Count; i++)
        {
            var toxicGasTicksNumber = tilesData[i].TileWorldEventData.ToxicGasTicksNumber;
            if (toxicGasTicksNumber == 0) continue;
            TileObjects[i].GetTileObjectEvents().ActiveEvent(toxicGasTicksNumber);
        }

        CustomEvents.FireCompleteLoadTiles();
    }

    public TileObject FindGroundTileObject(GroundTileViewEnum GroundTileViewEnum)
    {
        for (int i = 0; i < TileObjects.Count; i++)
        {
            if (TileObjects[i].GroundTileObject().CurrentGroundTile() == null) continue;

            if (TileObjects[i].GroundTileObject().CurrentGroundTile().GroundTileView == GroundTileViewEnum)
            {
                return TileObjects[i];
            }
        }

        return null;
    }

    public TileObject FindBuildingOnTileObject(BuildingTileViewEnum findBuildingTileView)
    {
        for (int i = 0; i < TileObjects.Count; i++)
        {
            if (TileObjects[i].GroundTileObject().CurrentGroundTile() == null) continue;

            if (!TileObjects[i].BuildingTileObject().IsHaveBuildingGameObject()) continue;

            if (TileObjects[i].BuildingTileObject().GetCurrentBuildingTile().BuildingTileView == findBuildingTileView)
            {
                return TileObjects[i];
            }
        }

        return null;
    }

    public TileObject FindDamagedBuildingOnTileObject()
    {
        for (int i = 0; i < TileObjects.Count; i++)
        {
            if (TileObjects[i].GroundTileObject().CurrentGroundTile() == null) continue;

            if (!TileObjects[i].BuildingTileObject().IsHaveBuildingGameObject()) continue;

            if (!TileObjects[i].BuildingHealth().IsFullHealth())
            {
                return TileObjects[i];
            }
        }

        return null;
    }
}
