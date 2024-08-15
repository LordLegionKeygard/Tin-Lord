using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EveryTickResourcesProduction : MonoBehaviour
{
    [SerializeField] private PlayerResources _playerResources;
    [SerializeField] private EveryTickResourcesWrapper[] _everyTickResourceProduction;
    [SerializeField] private List<ResourcesExtractionTilesInfo> _resourcesExtractionTilesInfoList = new();

    private void Awake()
    {
        CustomEvents.OnChangeResourceProduction += ChangeResourceProduction;
    }

    private void ChangeResourceProduction(ResourceEnum resourceEnum, float amount, int tileId, bool remove)
    {
        for (int i = 0; i < _resourcesExtractionTilesInfoList.Count; i++)
        {
            if (_resourcesExtractionTilesInfoList[i].Id == tileId)
            {
                if (remove)
                {
                    _resourcesExtractionTilesInfoList.Remove(_resourcesExtractionTilesInfoList[i]);
                }
                else
                {
                    _resourcesExtractionTilesInfoList[i].ResourceEnum = resourceEnum;
                    _resourcesExtractionTilesInfoList[i].Amount = amount;
                }
                RefreshEverydayResourcedExtraction();
                return;
            }
        }

        if(remove) return;

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
        for (int i = 0; i < _everyTickResourceProduction.Length; i++)
        {
            _everyTickResourceProduction[i].Amount = 0;
        }

        for (int i = 0; i < _resourcesExtractionTilesInfoList.Count; i++)
        {
            _everyTickResourceProduction[(int)_resourcesExtractionTilesInfoList[i].ResourceEnum].Amount += _resourcesExtractionTilesInfoList[i].Amount;
        }
    }

    public void AddEveryTickResources()
    {
        for (int i = 0; i < _everyTickResourceProduction.Length; i++)
        {
            _playerResources.ChangeResource(_everyTickResourceProduction[i].Resource.ResourceEnum, _everyTickResourceProduction[i].Amount);
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnChangeResourceProduction -= ChangeResourceProduction;
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
    public Resource Resource;
    public float Amount;
}
