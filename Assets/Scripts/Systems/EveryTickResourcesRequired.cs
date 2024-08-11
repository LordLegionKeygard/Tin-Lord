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
    }

    private void ChangeResourceRequired(TileObject tileObject, Resource resource, float amount)
    {
        var info = _resourcesRequiresTilesInfoList.Find(info => info.TileObject == tileObject);

        if (resource == null)
        {
            if (info != null)
            {
                _resourcesRequiresTilesInfoList.Remove(info);
            }
            return;
        }

        if (info != null)
        {
            info.ResourceEnum = resource.ResourceEnum;
            info.Amount = amount;
            info.TileObject.CheckResourceRequired(true);
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
    }

    public void UseEveryTickRequiredResources()
    {
        for (int i = 0; i < _resourcesRequiresTilesInfoList.Count; i++)
        {
            var info = _resourcesRequiresTilesInfoList[i];
            if (info.TileObject.IsBuildingWork)
            {
                info.TileObject.CheckResourceRequired(false);
                if (info.TileObject.IsHaveRequiredResource()) _playerResources.ChangeResource(info.ResourceEnum, -info.Amount);
            }
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnChangeResourceRequired -= ChangeResourceRequired;
    }
}

[System.Serializable]
public class ResourcesRequiredTilesInfo
{
    public TileObject TileObject;
    public ResourceEnum ResourceEnum;
    public float Amount;
}
