using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EveryTickResourcesRequired : MonoBehaviour
{
    [Inject] private MissionResources _missionResources;
    [SerializeField] private List<ResourcesRequiredTilesInfo> _resourcesRequiresTilesInfoList = new();

    private void Awake()
    {
        CustomEvents.OnChangeResourceForWork += ChangeResourceRequired;
    }

    private void ChangeResourceRequired(TileObject tileObject, Resource resourceForWork, float amount, ResourceRecept[] resourceRecept)
    {
        var info = _resourcesRequiresTilesInfoList.Find(info => info.TileObject == tileObject);

        if (resourceForWork == null && resourceRecept == null)
        {
            if (info != null)
            {
                _resourcesRequiresTilesInfoList.Remove(info);
            }
            return;
        }

        if (info != null)
        {
            info.ResourceForWork = resourceForWork;
            info.ResourceForWorkAmount = amount;
            info.ResourceRecept = resourceRecept;
            info.TileObject.CheckResourceRequired(true);
        }
        else
        {
            _resourcesRequiresTilesInfoList.Add(new ResourcesRequiredTilesInfo
            {
                TileObject = tileObject,
                ResourceForWork = resourceForWork,
                ResourceForWorkAmount = amount,
                ResourceRecept = resourceRecept
            });
        }
    }

    public void UseEveryTickRequiredResources()
    {
        for (int i = 0; i < _resourcesRequiresTilesInfoList.Count; i++)
        {
            var info = _resourcesRequiresTilesInfoList[i];
            if (info.TileObject.IsBuildingWork())
            {
                info.TileObject.CheckResourceRequired(true);
                if (info.TileObject.IsHaveRequiredResource())
                {
                    if (info.ResourceForWork != null) _missionResources.ChangeResource(info.ResourceForWork.ResourceEnum, -info.ResourceForWorkAmount);

                    if (info.ResourceRecept == null) continue; //это здание требует ресурс для работы, но ничего не создает, например "Очистка экологии"

                    for (int k = 0; k < info.ResourceRecept.Length; k++)
                    {
                        _missionResources.ChangeResource(info.ResourceRecept[k].ResourceForRecept.ResourceEnum, -info.ResourceRecept[k].ResourcesForReceptAmount);
                    }
                }
            }
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnChangeResourceForWork -= ChangeResourceRequired;
    }
}

[System.Serializable]
public class ResourcesRequiredTilesInfo
{
    public TileObject TileObject;
    public Resource ResourceForWork;
    public float ResourceForWorkAmount;
    public ResourceRecept[] ResourceRecept;

}


