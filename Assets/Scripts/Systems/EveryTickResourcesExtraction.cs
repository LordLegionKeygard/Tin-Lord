using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EveryTickResourcesExtraction : MonoBehaviour
{
    [SerializeField] private PlayerResources _playerResources;
    [SerializeField] private EveryTickResourcesWrapper[] _everyTickResourceExtraction;
    [SerializeField] private List<ResourcesExtractionTilesInfo> _resourcesExtractionTilesInfoList = new List<ResourcesExtractionTilesInfo>();

    private void Awake()
    {
        CustomEvents.OnChangeResourceExtraction += ChangeResourceExtraction;
        CustomEvents.OnTimeTick += TheDayIsOverAddEverydayResources;
    }

    public void ChangeResourceExtraction(ResourceEnum resourceEnum, float amount, int tileId)
    {
        for (int i = 0; i < _resourcesExtractionTilesInfoList.Count; i++)
        {
            if (_resourcesExtractionTilesInfoList[i].Id == tileId)
            {
                _resourcesExtractionTilesInfoList[i].Amount = amount;
                RefreshEverydayResourcedExtraction();
                return;
            }
        }

        _resourcesExtractionTilesInfoList.Add(new ResourcesExtractionTilesInfo()
        {
            Id = tileId,
            ResourceEnum = resourceEnum,
            Amount = amount,
        });
        RefreshEverydayResourcedExtraction();
    }

    public void RefreshEverydayResourcedExtraction()
    {
        for (int i = 0; i < _everyTickResourceExtraction.Length; i++) //возможен баг в конце дня что не даст ресурсов
        {
            _everyTickResourceExtraction[i].Amount = 0;
        }

        for (int i = 0; i < _resourcesExtractionTilesInfoList.Count; i++)
        {
            _everyTickResourceExtraction[(int)_resourcesExtractionTilesInfoList[i].ResourceEnum].Amount += _resourcesExtractionTilesInfoList[i].Amount;
        }
    }

    public void TheDayIsOverAddEverydayResources()
    {
        for (int i = 0; i < _everyTickResourceExtraction.Length; i++)
        {
            _playerResources.AddResource(_everyTickResourceExtraction[i].Resource.ResourceEnum, _everyTickResourceExtraction[i].Amount);
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnChangeResourceExtraction -= ChangeResourceExtraction;
        CustomEvents.OnTimeTick -= TheDayIsOverAddEverydayResources;
    }
}

[System.Serializable]
public class ResourcesExtractionTilesInfo
{
    public int Id;
    public ResourceEnum ResourceEnum;
    public float Amount;
}

[System.Serializable]
public class EveryTickResourcesWrapper
{
    [HideInInspector] public string ElementName;
    public Resource Resource;
    public float Amount;
}
