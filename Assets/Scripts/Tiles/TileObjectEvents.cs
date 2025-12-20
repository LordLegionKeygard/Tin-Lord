using UnityEngine;

//Класс необходим, чтобы логика других скриптов могла сюда обратиться и узнать активен ли сейчас ивент на данном именно тайле 
public class TileObjectEvents : MonoBehaviour
{
    [Header("ToxicGas")]
    [SerializeField] private int _toxicGasTicksNumber;
    [SerializeField] private TileObject _tileObject;
    public int GetToxicGasTicks() => _toxicGasTicksNumber;
    public bool IsToxicGasActive() => _toxicGasTicksNumber > 0;

    private void Start()
    {
        CustomEvents.OnTimeTick += EventTick;
    }

    public void ActiveEvent(int ticksNumber)
    {
        if (IsToxicGasActive()) return;
        _toxicGasTicksNumber = ticksNumber;

        var buildingTileObject = _tileObject.BuildingTileObject();

        if (!buildingTileObject.IsHaveTile()) return;

        var haveProdictionResources = buildingTileObject.GetCurrentBuildingTile().IsHaveProductionResources();

        if (haveProdictionResources || buildingTileObject.IsEcologyBuilding())
        {
            _tileObject.SetBuildingWork(false);
            CustomEvents.FireChangeEcology(_tileObject.TileEcology().GetEcology(GetEcologyEnum.Total), _tileObject.GetId(), false);
            _tileObject.ChangeResourceProduction();
            CustomEvents.FireChangeResourceForWork(_tileObject, _tileObject.CurrentResourceForWork(), 0, _tileObject.CurrentResourceRecept());
            CustomEvents.FireToxicGasEventActive(_tileObject.GetId());
        }

        if (buildingTileObject.GetCurrentBuildingTile().BuildingTileView == BuildingTileViewEnum.AttackingStructures)
        {
            _tileObject.SetTurretBuildingCantShoot();
        }
    }

    private void UncativeEvent()
    {
        _tileObject.SetTurretBuildingCantShoot();
        
        _tileObject.SetBuildingWork(true);
        CustomEvents.FireChangeEcology(_tileObject.TileEcology().GetEcology(GetEcologyEnum.Total), _tileObject.GetId(), false);
        _tileObject.ChangeResourceProduction();
        CustomEvents.FireChangeResourceForWork(_tileObject, _tileObject.CurrentResourceForWork(), 0, _tileObject.CurrentResourceRecept());

    }

    private void EventTick()
    {
        if (_toxicGasTicksNumber == 0) return;

        _toxicGasTicksNumber--;

        if (_toxicGasTicksNumber == 0)
        {
            UncativeEvent();
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnTimeTick -= EventTick;
    }
}
