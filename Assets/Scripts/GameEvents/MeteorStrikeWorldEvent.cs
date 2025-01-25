using System.Collections;
using UnityEngine;
using Zenject;

public class MeteorStrikeWorldEvent : BaseWorldEvent
{
    [Inject] private readonly TilesSystem _tilesSystem;
    private float _delay = 1.5f;
    private int _meteorDamagePercent = 30;
    public override void StartEvent()
    {
        base.StartEvent();

        var groundTile = GetTileObject().GroundTileObject();

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

        GetTileObject().BuildingHealth().CalculateDamage(GetTileObject().BuildingHealth().CalculateHealthFromPercent(_meteorDamagePercent));
    }

    private IEnumerator DestroyBuildingCoroutine()
    {
        yield return new WaitForSeconds(_delay);

        GetTileObject().BuildingTileObject().DestroyBuildingTile(true);
    }


    private IEnumerator SpawnGroundTileAndDestroyBuildingCoroutine()
    {
        yield return new WaitForSeconds(_delay);

        GetTileObject().BuildingTileObject().DestroyBuildingTile(true);
        GetTileObject().GroundTileObject().SetGroundTile(_tilesSystem.GetGroundTileForEnum(GroundTileViewEnum.Crater));
        GetTileObject().GroundTileObject().SpawnGroundTile();
    }
}
