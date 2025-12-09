using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicGasMissionEvent : BaseMissionEvent
{
    [SerializeField] private GameObject _spawnPrefab;
    private GameObject _currentPrefab;
    public override void StartEvent()
    {
        var validTiles = new List<TileObject>();

        // Собираем все подходящие тайлы
        foreach (var tileObject in GetAllTileObjects().TileObjects)
        {
            var buildingTileObject = tileObject.BuildingTileObject();
            if (buildingTileObject.IsHaveTile() 
            && buildingTileObject.GetCurrentBuildingTile().BuildingTileView != BuildingTileViewEnum.Base 
            && !buildingTileObject.IsExtraBaseTileObject()
            && !buildingTileObject.IsConstructionNow()
            && !tileObject.GetTileObjectEvents().IsToxicGasActive())
            {
                validTiles.Add(tileObject);
            }
        }

        // Если есть подходящие тайлы, выбираем случайный и выполняем действия
        if (validTiles.Count > 0)
        {
            var randomTile = validTiles[Random.Range(0, validTiles.Count)];
            randomTile.GetTileObjectEvents().ActiveEvent(WorldGameInfo.ToxicGasTicks);

            _currentPrefab = Instantiate(_spawnPrefab, randomTile.transform.position, Quaternion.identity);
            _currentPrefab.GetComponent<OnTriggerStayDealDamage>().SetInfo(WorldGameInfo.ToxicGasTicks, WorldGameInfo.ToxicGasTriggerStayDamageFactor);
            SpawnedHazardSystem.RegisterHazard((int)HazardEnum.ToxicGas, _currentPrefab, WorldGameInfo.ToxicGasTicks, WorldGameInfo.ToxicGasTriggerStayDamageFactor);
        }
    }
}
