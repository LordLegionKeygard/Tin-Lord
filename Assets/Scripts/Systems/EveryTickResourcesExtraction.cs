using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EveryTickResourcesExtraction : MonoBehaviour
{
    [SerializeField] private PlayerResources _playerResources;
    [SerializeField] private EveryTickResourcesWrapper[] _everyTickResourceExtraction;
    [SerializeField] private List<ResourcesExtractionTilesInfo> _resourcesExtractionTilesInfoList = new();

    private void Awake()
    {
        CustomEvents.OnChangeResourceExtraction += ChangeResourceExtraction;
        CustomEvents.OnTimeTick += AddEveryTickResources;
    }

    private void ChangeResourceExtraction(ResourceEnum resourceEnum, float amount, int tileId, bool remove)
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
        for (int i = 0; i < _everyTickResourceExtraction.Length; i++)
        {
            _everyTickResourceExtraction[i].Amount = 0;
        }

        for (int i = 0; i < _resourcesExtractionTilesInfoList.Count; i++)
        {
            _everyTickResourceExtraction[(int)_resourcesExtractionTilesInfoList[i].ResourceEnum].Amount += _resourcesExtractionTilesInfoList[i].Amount;
        }
    }

    private void AddEveryTickResources()
    {
        for (int i = 0; i < _everyTickResourceExtraction.Length; i++)
        {
            _playerResources.ChangeResource(_everyTickResourceExtraction[i].Resource.ResourceEnum, _everyTickResourceExtraction[i].Amount);
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnChangeResourceExtraction -= ChangeResourceExtraction;
        CustomEvents.OnTimeTick -= AddEveryTickResources;
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
