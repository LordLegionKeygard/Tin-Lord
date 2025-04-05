using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeneralRepairSystem : MonoBehaviour
{
    [SerializeField] private GameObject _activeIcon;
    [SerializeField] private PlayerResources _playerResources;
    [SerializeField] private List<TileObject> _repairList;
    private bool _useRepairOnThisDay;

    private void Start()
    {
        CustomEvents.OnChangeGeneralRepairTileObject += ChangeGeneralRepairTileObject;
        CustomEvents.OnBuildingDestroyed += BuildingDestroyed;
        CustomEvents.OnDayEnd += ChangeUseRepaintOnThisDay;
    }

    public void ChangeGeneralRepairTileObject(TileObject tileObject)
    {
        if (tileObject.IsGeneralRepairSelect())
        {
            _repairList.Add(tileObject);
        }
        else
        {
            _repairList.Remove(tileObject);
        }
        CheckView();
    }

    private void ChangeUseRepaintOnThisDay(int _)
    {
        _useRepairOnThisDay = false;
        CheckView();
    }

    private void ToggleView(bool state)
    {
        _activeIcon.SetActive(state);
    }

    private void CheckView()
    {
        ToggleView(_repairList.Count != 0 && !_useRepairOnThisDay);
    }

    private void BuildingDestroyed(int id)
    {
        for (int i = 0; i < _repairList.Count; i++)
        {
            if (_repairList[i].GetId() == id)
            {
                _repairList.Remove(_repairList[i]);
            }
        }
        CheckView();
    }

    public void RepairAllBuildingButton()
    {
        if (_repairList.Count == 0 || _useRepairOnThisDay)
        {
            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Error], transform.position);
            return;
        }
        _useRepairOnThisDay = true;
        CheckView();
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Repair], transform.position);
        //нужен вызов на инпут
        for (int i = 0; i < _repairList.Count; i++)
        {
            var resources = GetResourcesForRepair(_repairList[i]);
            if (_playerResources.ResourcesEnough(resources))
            {
                _playerResources.UseResourcesForBuilding(resources);
                _repairList[i].BuildingHealth().FullRepair();
            }
        }
    }

    public ResourceWrapper[] GetResourcesForRepair(TileObject tileObject)
    {
        var building = tileObject.BuildingTileObject().CurrentBuilding();

        var buildingHealth = tileObject.BuildingHealth();
        float healthPercentage = (float)(buildingHealth.GetMaxHealth() - buildingHealth.GetCurrentHealth()) / buildingHealth.GetMaxHealth();

        return building.ResourcesForBuild.Select(resource => new ResourceWrapper
        {
            ResourceEnum = resource.ResourceEnum,
            RecourceAmount = Mathf.CeilToInt(resource.RecourceAmount * healthPercentage)
        }).ToArray();
    }

    private void OnDestroy()
    {
        CustomEvents.OnChangeGeneralRepairTileObject -= ChangeGeneralRepairTileObject;
        CustomEvents.OnBuildingDestroyed -= BuildingDestroyed;
        CustomEvents.OnDayEnd -= ChangeUseRepaintOnThisDay;
    }
}
