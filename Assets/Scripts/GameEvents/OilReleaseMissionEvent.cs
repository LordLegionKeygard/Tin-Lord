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
            if (tileObject.GroundTileObject().CheckTileView(GroundTileViewEnum.Ground))
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
