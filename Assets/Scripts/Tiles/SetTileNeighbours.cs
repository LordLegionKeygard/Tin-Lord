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
            tilesData[i] = new TileDataWrapper
            {
                GroundTileId = groundHaveTile ? (int)tileObject.GroundTileObject().CurrentGroundTile().GroundTileView : (int)GroundTileViewEnum.None,
                GroundTileRotation = 0,
                BuildingTileId = buildingHaveTile ? (int)tileObject.BuildingTileObject().CurrentBuildingTile().BuildingTileView : (int)BuildingTileViewEnum.None,
                BuildingTileRotation = 0,
                BuildingHealth = buildingHaveTile ? tileObject.BuildingHealth().CurrentHealth : 0,
                IsBuildingWork = buildingHaveTile && tileObject.IsBuildingWork(),

                IsLake = isWater && riverTile.IsLake(),
                RiverNumber = isWater ? riverTile.GetRiverNumber() : 0,
                IsBridge = isWater && riverTile.IsBridge(),
                IsLastRiverTile = isWater && riverTile.IsLastRiverTile(),
                RiverType = isWater ? (int)riverTile.GetRiverTypeEnum() : (int)RiverTypeEnum.None,
                RiverRotation = riverTile.GetRiverRotation(),
            };

        }
        return tilesData;
    }

    public void LoadGroundTiles(TileDataWrapper[] tilesData, bool isStartMission)
    {
        if(isStartMission) return;

        for (int i = 0; i < TileObjects.Count; i++)
        {
            if(tilesData[i].GroundTileId == 0) continue;

            TileObjects[i].GroundTileObject().LoadGroundTile(tilesData[i].GroundTileId, tilesData[i]);
        }

        CustomEvents.FireSpawnRoadComplete();
    }
}
