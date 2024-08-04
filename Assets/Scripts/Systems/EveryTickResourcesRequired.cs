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
        CustomEvents.OnTimeTick += UseEveryTick;
    }

    private void UseEveryTick()
    {
        UseEveryTickRequiredResources(false);
    }

    private void ChangeResourceRequired(TileObject tileObject, Resource resource, float amount)
    {
        var info = _resourcesRequiresTilesInfoList.Find(info => info.TileObject == tileObject);

        if (resource == null)
        {
            if (info != null)
            {
                _resourcesRequiresTilesInfoList.Remove(info);
                UseEveryTickRequiredResources(true);
            }
            return;
        }

        if (info != null)
        {
            info.ResourceEnum = resource.ResourceEnum;
            info.Amount = amount;
        }
        else
        {
            _resourcesRequiresTilesInfoList.Add(new ResourcesRequiredTilesInfo
            {
                TileObject = tileObject,
                ResourceEnum = resource.ResourceEnum,
                Amount = amount
            });
        }
        UseEveryTickRequiredResources(true);
    }

    private void UseEveryTickRequiredResources(bool needCheck)
    {
        foreach (var info in _resourcesRequiresTilesInfoList)
        {
            if (!info.TileObject.IsBuildingWork) return;
            
            var state = info.TileObject.IsHaveRequiredResource();
            info.TileObject.CheckResourceRequired(state, needCheck);
            if (state)
            {
                _playerResources.ChangeResource(info.ResourceEnum, -info.Amount);
            }
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnChangeResourceRequired -= ChangeResourceRequired;
        CustomEvents.OnTimeTick -= UseEveryTick;
    }
}

[System.Serializable]
public class ResourcesRequiredTilesInfo
{
    public TileObject TileObject;
    public ResourceEnum ResourceEnum;
    public float Amount;
}
