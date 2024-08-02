using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EveryTickResourcesRequired : MonoBehaviour
{
    [SerializeField] private PlayerResources _playerResources;
    [SerializeField] private List<ResourcesRequiredTilesInfo> _resourcesRequiresTilesInfoList = new();

    private void Awake()
    {
        CustomEvents.OnChangeResourceRequired += ChangeResourceRequired;
        CustomEvents.OnTimeTick += UseEveryTickRequiredResourced;
    }

    private void ChangeResourceRequired(TileObject tileObject, Resource resource, float amount)
    {
        CustomEvents.FireHaveRequiredResource(tileObject.GetId(), tileObject.IsHaveRequiredResource());
        if (resource == null)
        {
            for (int i = 0; i < _resourcesRequiresTilesInfoList.Count; i++)
            {
                if (_resourcesRequiresTilesInfoList[i].TileObject == tileObject)
                {
                    _resourcesRequiresTilesInfoList.Remove(_resourcesRequiresTilesInfoList[i]);
                    UseEveryTickRequiredResourced();
                    return;
                }
            }
            return;
        }

        for (int i = 0; i < _resourcesRequiresTilesInfoList.Count; i++)
        {
            if (_resourcesRequiresTilesInfoList[i].TileObject == tileObject)
            {
                _resourcesRequiresTilesInfoList[i].ResourceEnum = resource.ResourceEnum;
                _resourcesRequiresTilesInfoList[i].Amount = amount;
                UseEveryTickRequiredResourced();
                return;
            }
        }

        _resourcesRequiresTilesInfoList.Add(new ResourcesRequiredTilesInfo
        {
            TileObject = tileObject,
            ResourceEnum = resource.ResourceEnum,
            Amount = amount,
        });
        UseEveryTickRequiredResourced();
        
    }

    private void UseEveryTickRequiredResourced()
    {
        for (int i = 0; i < _resourcesRequiresTilesInfoList.Count; i++)
        {
            if (_playerResources.ResourceEnough(_resourcesRequiresTilesInfoList[i].ResourceEnum, _resourcesRequiresTilesInfoList[i].Amount))
            {
                _playerResources.ChangeResource(_resourcesRequiresTilesInfoList[i].ResourceEnum, -_resourcesRequiresTilesInfoList[i].Amount);

                if (!_resourcesRequiresTilesInfoList[i].TileObject.IsHaveRequiredResource()) CustomEvents.FireHaveRequiredResource(_resourcesRequiresTilesInfoList[i].TileObject.GetId(), true);
            }
            else
            {
                if (_resourcesRequiresTilesInfoList[i].TileObject.IsHaveRequiredResource()) CustomEvents.FireHaveRequiredResource(_resourcesRequiresTilesInfoList[i].TileObject.GetId(), false);
            }
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnChangeResourceRequired -= ChangeResourceRequired;
        CustomEvents.OnTimeTick -= UseEveryTickRequiredResourced;
    }
}

[System.Serializable]
public class ResourcesRequiredTilesInfo
{
    public TileObject TileObject;
    public ResourceEnum ResourceEnum;
    public float Amount;
}
