using UnityEngine;
using TMPro;
using System;

public class MissionResources : MonoBehaviour
{
    [SerializeField] private bool _test;
    [SerializeField] private MissionResourcesWrapper[] _resourcesWrapper;
    public Resource GetResourceForNumber(int number) => _resourcesWrapper[number].Resource;
    public float GetResourceAmountForEnum(ResourceEnum resourceEnum) => _resourcesWrapper[(int)resourceEnum].Amount;
    public int GetResourceNumberForResource(Resource resource)
    {
        for (int i = 0; i < _resourcesWrapper.Length; i++)
        {
            if(resource == _resourcesWrapper[i].Resource)
            {
                return i;
            }
        }

        return -1;
    }

    private void Start()
    {
        UpdateAllTexts();
    }

    public void LoadResources(float[] resourcesData)
    {
        for (int i = 0; i < _resourcesWrapper.Length; i++)
        {
            _resourcesWrapper[i].Amount = resourcesData[i];
        }
        UpdateAllTexts();
    }

    public float[] GetAllResourcesAmount()
    {
        var resources = new float[_resourcesWrapper.Length];

        for (int i = 0; i < _resourcesWrapper.Length; i++)
        {
            resources[i] = _resourcesWrapper[i].Amount;
        }

        return resources;
    }

    public void ChangeResource(ResourceEnum resourceEnum, float amount)
    {
        var resources = _resourcesWrapper[(int)resourceEnum];
        resources.Amount = (float)Math.Round(resources.Amount + amount, 1, MidpointRounding.AwayFromZero);
        resources.Text.text = resources.Amount.ToString("0.0");
    }

    private void UpdateAllTexts()
    {
        for (int i = 0; i < _resourcesWrapper.Length; i++)
        {
            _resourcesWrapper[i].Text.text = _resourcesWrapper[i].Amount.ToString("0.0");
        }
    }

    public void UseResourcesForBuilding(ResourceWrapper[] resourcesForBuildWrapper)
    {
        if(_test) return;
        
        for (int i = 0; i < resourcesForBuildWrapper.Length; i++)
        {
            _resourcesWrapper[(int)resourcesForBuildWrapper[i].ResourceEnum].Amount -= resourcesForBuildWrapper[i].RecourceAmount;
        }
        UpdateAllTexts();
    }

    public void AddResourcesAfterDestroyBuilding(ResourceWrapper[] resourcesForBuildWrapper, float buildingHealthPercent)
    {
        for (int i = 0; i < resourcesForBuildWrapper.Length; i++)
        {
            float healthFactor = Mathf.Clamp01(buildingHealthPercent); // Убеждаемся, что значение в пределах [0, 1]
            float returnedAmount = resourcesForBuildWrapper[i].RecourceAmount / 2 * healthFactor;
            _resourcesWrapper[(int)resourcesForBuildWrapper[i].ResourceEnum].Amount += returnedAmount;
        }

        UpdateAllTexts();
    }

    public bool ResourcesEnough(ResourceWrapper[] resourcesForBuildWrapper)
    {
        if(_test) return true;

        for (int i = 0; i < resourcesForBuildWrapper.Length; i++)
        {
            if (resourcesForBuildWrapper[i].RecourceAmount > _resourcesWrapper[(int)resourcesForBuildWrapper[i].ResourceEnum].Amount)
            {
                return false;
            }
        }
        return true;
    }

    public bool ResourceEnough(ResourceEnum resourceEnum, float amount)
    {
        return _resourcesWrapper[(int)resourceEnum].Amount >= amount;
    }
}

[System.Serializable]
public class MissionResourcesWrapper
{
    public Resource Resource;
    public float Amount;
    public TextMeshProUGUI Text;

}
