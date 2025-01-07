using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTileNeighbours : MonoBehaviour
{
    public List<TileObject> TileObjects;

    public void SetNeighbours()
    {
        for (int i = 0; i < TileObjects.Count; i++)
        {
            TileObjects[i].SetNeighbourTiles(new TileObject[] {(i + 20 > TileObjects.Count - 1) ? null : TileObjects[i + 20],
                                                            (i + 21 > TileObjects.Count - 1) ? null : TileObjects[i].transform.position.x == 190 ? null : TileObjects[i + 21],
                                                            (i + 1 > TileObjects.Count - 1) ? null : TileObjects[i].transform.position.x == 190 ? null : TileObjects[i + 1],
                                                            (i - 19 < 0) ? null : TileObjects[i].transform.position.x == 190 ? null : TileObjects[i - 19],
                                                            (i - 20 < 0) ? null : TileObjects[i - 20],
                                                            (i - 21 < 0) ? null : TileObjects[i].transform.position.x == 0 ? null : TileObjects[i - 21],
                                                            (i - 1 < 0) ? null : TileObjects[i].transform.position.x == 0 ? null : TileObjects[i - 1],
                                                            (i + 19 > TileObjects.Count - 1) ? null : TileObjects[i].transform.position.x == 0 ? null : TileObjects[i + 19],  });
        }
    }

    public TileDataWrapper[] GetAllTileObjects()
    {
        var tilesData = new TileDataWrapper[TileObjects.Count];

        for (int i = 0; i < TileObjects.Count; i++)
        {
            var tileObject = TileObjects[i];
            var groundHaveTile = tileObject.GroundTileObject().HaveTile();
            var buildingHaveTile = tileObject.BuildingTileObject().HaveTile();
            var isWater = tileObject.GroundTileObject().IsWaterTile();
            var riverTile = tileObject.GroundTileObject().CurrentTileRiver();
            var buildingTileGameObject = tileObject.BuildingTileObject().CurrentBuildingGameObject();
            var haveRotationView = buildingHaveTile && buildingTileGameObject.GetComponent<RotationView>() != null;

            tilesData[i] = new TileDataWrapper
            {
                GroundData = new GroundData
                {
                    GroundTileId = groundHaveTile ? (int)tileObject.GroundTileObject().CurrentGroundTile().GroundTileView : (int)GroundTileViewEnum.None,
                    GroundTileRotation = groundHaveTile ? tileObject.GroundTileObject().CurrentGroundTileObject().transform.eulerAngles.y : 0,
                    GroundModelRotation = groundHaveTile ? tileObject.GroundTileObject().GroundModelRotation() : 0,
                    IsForwardRoad = groundHaveTile ? tileObject.GroundTileObject().IsForwardRoad() : false
                },
                BuildingData = new BuildingData
                {
                    BuildingTileTypeId = buildingHaveTile ? (int)tileObject.BuildingTileObject().CurrentBuildingTile().BuildingTileView : -1,
                    BuildingTileLevel = buildingHaveTile ? tileObject.BuildingTileObject().CurrentBuildingLevel() : -1,
                    BuildingHealth = buildingHaveTile ? tileObject.BuildingHealth().CurrentHealth : 0,
                    IsBuildingWork = buildingHaveTile && tileObject.IsBuildingWork(),
                    BuildingTilePositionY = buildingHaveTile ? tileObject.BuildingTileObject().BuildingTileTransform().GetPositionY() : 0,
                    BuildingTilePositionX = buildingHaveTile ? tileObject.BuildingTileObject().BuildingTileTransform().GetPositionX() : 0,
                    BuildingTilePositionZ = buildingHaveTile ? tileObject.BuildingTileObject().BuildingTileTransform().GetPositionZ() : 0,
                    BuildingRotation = buildingHaveTile && haveRotationView ? buildingTileGameObject.GetComponent<RotationView>().GetObjectRotation() : 0
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

            if (tilesData[i].BuildingData.BuildingTileTypeId == (int)BuildingTileViewEnum.None) continue;

            TileObjects[i].BuildingTileObject().LoadBuildingTile(tilesData[i]);
        }

        CustomEvents.FireSpawnRoadComplete();
    }
}
