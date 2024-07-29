using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EverydayResourcesExtraction : MonoBehaviour
{
    [SerializeField] private PlayerResources _playerResources;
    [SerializeField] private EverydayResourcesWrapper[] _everydayResourceExtraction;
    [SerializeField] private List<ResourcesExtractionTilesInfo> _resourcesExtractionTilesInfoList = new List<ResourcesExtractionTilesInfo>();

    private void Awake()
    {
        CustomEvents.OnChangeResourceExtraction += ChangeResourceExtraction;
        CustomEvents.OnTheDayIsOver += TheDayIsOverAddEverydayResources;
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
        for (int i = 0; i < _everydayResourceExtraction.Length; i++) //возможен баг в конце дня что не даст ресурсов
        {
            _everydayResourceExtraction[i].Amount = 0;
        }

        for (int i = 0; i < _resourcesExtractionTilesInfoList.Count; i++)
        {
            _everydayResourceExtraction[(int)_resourcesExtractionTilesInfoList[i].ResourceEnum].Amount += _resourcesExtractionTilesInfoList[i].Amount;
        }
    }

    public void TheDayIsOverAddEverydayResources()
    {
        for (int i = 0; i < _everydayResourceExtraction.Length; i++)
        {
            _playerResources.AddResource(_everydayResourceExtraction[i].Resource.ResourceEnum, _everydayResourceExtraction[i].Amount);
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnChangeResourceExtraction -= ChangeResourceExtraction;
        CustomEvents.OnTheDayIsOver -= TheDayIsOverAddEverydayResources;
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
public class EverydayResourcesWrapper
{
    [HideInInspector] public string ElementName;
    public Resource Resource;
    public float Amount;
}
