using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class OilReleaseMissionEvent : BaseMissionEvent
{
    [Inject] private readonly TilesSystem _tilesSystem;
    public override void StartEvent()
    {
        UseOilReleaseEvent();
    }

    private void UseOilReleaseEvent()
    {
        var rnd = Random.Range(0, 100);

        if (rnd <= 30) return;

        var validTiles = new List<TileObject>();

        foreach (var tileObject in GetAllTileObjects().TileObjects)
        {
            if (tileObject.GroundTileObject().CheckTileView(GroundTileViewEnum.CoalDeposits)
             || tileObject.GroundTileObject().CheckTileView(GroundTileViewEnum.Desert)
             || tileObject.GroundTileObject().CheckTileView(GroundTileViewEnum.Forest)
             || tileObject.GroundTileObject().CheckTileView(GroundTileViewEnum.Ground)
             || tileObject.GroundTileObject().CheckTileView(GroundTileViewEnum.Meadow)
             || tileObject.GroundTileObject().CheckTileView(GroundTileViewEnum.Oasis)
             || tileObject.GroundTileObject().CheckTileView(GroundTileViewEnum.Plain))
            {
                validTiles.Add(tileObject);
            }
        }

        if (validTiles.Count > 0)
        {
            var randomTile = validTiles[Random.Range(0, validTiles.Count)];
            randomTile.GroundTileObject().SetGroundTile(_tilesSystem.GetGroundTileForEnum(GroundTileViewEnum.OilSwamp));
            randomTile.GroundTileObject().SpawnGroundTile();
        }
    }
}
