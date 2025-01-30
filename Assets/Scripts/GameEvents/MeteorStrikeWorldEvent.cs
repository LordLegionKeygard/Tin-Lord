using System.Collections;
using UnityEngine;
using Zenject;

public class MeteorStrikeWorldEvent : BaseWorldEvent
{
    [Inject] private readonly TilesSystem _tilesSystem;
    private float _delay = 1.5f;
    private int _meteorDamagePercent = 30;
    private GroundTileViewEnum _groundTileView = GroundTileViewEnum.None;
    
    public override void StartEvent()
    {
        base.StartEvent();
        StartCoroutine(nameof(MeteorCoroutine));
    }
    
    private IEnumerator MeteorCoroutine()
    {
        yield return new WaitForSeconds(_delay);
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.MeteorStrike, GetCurrentPrefab().transform.position);
        
        var extraTileObject = GetTileObject().BuildingTileObject().IsExtraBaseTileObject();

        if (extraTileObject != null)
        {
            extraTileObject.BuildingHealth().CalculateDamage(extraTileObject.BuildingHealth().CalculateHealthFromPercent(_meteorDamagePercent));
            yield break;
        }

        var groundTile = GetTileObject().GroundTileObject();
        if (groundTile.CurrentGroundTile() == null) yield break;
        
        
        _groundTileView = groundTile.CurrentGroundTile().GroundTileView;


        switch (_groundTileView)
        {
            case GroundTileViewEnum.BaseFoundation:
                GetTileObject().BuildingHealth().CalculateDamage(GetTileObject().BuildingHealth().CalculateHealthFromPercent(_meteorDamagePercent));
                break;
            case GroundTileViewEnum.Road or GroundTileViewEnum.River or GroundTileViewEnum.PollutedRiver or GroundTileViewEnum.DesertRiver:
                GetTileObject().BuildingTileObject().DestroyBuildingTile(false);
                break;
            case GroundTileViewEnum.Rift or GroundTileViewEnum.Crater:
                //ничего
                break;
            default:
                GetTileObject().BuildingTileObject().DestroyBuildingTile(false);
                GetTileObject().GroundTileObject().SetGroundTile(_tilesSystem.GetGroundTileForEnum(GroundTileViewEnum.Crater));
                GetTileObject().GroundTileObject().SpawnGroundTile();
                break;
        }
    }
}
