using System.Collections;
using UnityEngine;

public class MeteorStrikeDayEvent : BaseDayEvent
{
    [SerializeField] private TilesSystem _tilesSystem;
    private float _delay = 1.5f;
    private int _meteorDamagePercent = 30;
    public override void StartEvent()
    {
        base.StartEvent();

        var groundTile = TileObject().GroundTileObject();

        if (groundTile.CurrentGroundTile() != null)
        {
            switch (groundTile.CurrentGroundTile().GroundTileView)
            {
                case GroundTileViewEnum.BaseFoundation:
                    StartCoroutine(nameof(DealDamageCoroutine));
                    break;
                case GroundTileViewEnum.Road or GroundTileViewEnum.River or GroundTileViewEnum.PollutedRiver or GroundTileViewEnum.DesertRiver:
                    StartCoroutine(nameof(DestroyBuildingCoroutine));
                    break;
                case GroundTileViewEnum.Rift or GroundTileViewEnum.Crater:
                    //ничего
                    break;
                default:
                    StartCoroutine(nameof(SpawnGroundTileAndDestroyBuildingCoroutine));
                    break;

            }
        }
    }

    private IEnumerator DealDamageCoroutine()
    {
        yield return new WaitForSeconds(_delay);

        TileObject().BuildingHealth().CalculateDamage(TileObject().BuildingHealth().GetHealthPercent(_meteorDamagePercent));
    }

    private IEnumerator DestroyBuildingCoroutine()
    {
        yield return new WaitForSeconds(_delay);

        TileObject().BuildingTileObject().DestroyBuildingTile(true);
    }


    private IEnumerator SpawnGroundTileAndDestroyBuildingCoroutine()
    {
        yield return new WaitForSeconds(_delay);

        TileObject().BuildingTileObject().DestroyBuildingTile(true);
        TileObject().GroundTileObject().SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.Crater));
        TileObject().GroundTileObject().SpawnGroundTile();
    }
}
