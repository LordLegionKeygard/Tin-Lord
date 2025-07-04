using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicGasMissionEvent : BaseMissionEvent
{
    private float _delay = 1.5f;
    public override void StartEvent()
    {
        StartCoroutine(nameof(ToxicGasCoroutine));
    }

    private IEnumerator ToxicGasCoroutine()
    {
        // AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.EarthQuake, transform.position);
        yield return new WaitForSeconds(_delay);
        UseToxicGas();
    }

    private void UseToxicGas()
    {
        var validTiles = new List<TileObject>();

        // Собираем все подходящие тайлы
        foreach (var tileObject in GetAllTileObjects().TileObjects)
        {
            if (tileObject.GroundTileObject().CurrentGroundTile() == null)
            {
                validTiles.Add(tileObject);
            }
        }

        // Если есть подходящие тайлы, выбираем случайный и выполняем действия
        if (validTiles.Count > 0)
        {
            var randomTile = validTiles[Random.Range(0, validTiles.Count)];
            randomTile.GetTileObjectEvents().ActiveEvent(WorldGameInfo.ToxicGasTicks);
        }
    }
}
