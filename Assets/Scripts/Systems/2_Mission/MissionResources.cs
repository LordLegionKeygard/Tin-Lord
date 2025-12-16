using UnityEngine;
using TMPro;
using System;

public class MissionResources : MonoBehaviour
{
    [SerializeField] private bool _test;
    [SerializeField] private MissionResourcesWrapper[] _resourcesWrapper;
    public Resource GetResourceForNumber(int number) => _resourcesWrapper[number].Resource;
    public float GetResourceAmountForEnum(ResourceEnum resourceEnum) => _resourcesWrapper[(int)resourceEnum].Amount;
    private static float FloorToSingleDecimal(float amount) => Mathf.Floor(amount * 10f) * 0.1f;
    public int GetResourceNumberForResource(Resource resource)
    {
        for (int i = 0; i < _resourcesWrapper.Length; i++)
        {
            if (resource == _resourcesWrapper[i].Resource)
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
        resources.Amount += amount;
        resources.Text.text = FloorToSingleDecimal(resources.Amount).ToString("0.0");

        UpdateResourceObjectives(resourceEnum);
    }

    private void UpdateResourceObjectives(ResourceEnum resourceEnum)
    {
        switch (resourceEnum)
        {
            case ResourceEnum.DataFragment:
                CustomEvents.FireObjectiveAmountChange(ObjectiveEnum.CollectDataFragments, (int)_resourcesWrapper[(int)resourceEnum].Amount);
                break;
            case ResourceEnum.IronIngot:
                CustomEvents.FireObjectiveAmountChange(ObjectiveEnum.CollectIronIngots, (int)_resourcesWrapper[(int)resourceEnum].Amount);
                break;
            case ResourceEnum.Wood:
                CustomEvents.FireObjectiveAmountChange(ObjectiveEnum.CollectWood, (int)_resourcesWrapper[(int)resourceEnum].Amount);
                break;
        }
    }

    private void UpdateAllTexts()
    {
        for (int i = 0; i < _resourcesWrapper.Length; i++)
        {
            var displayAmount = FloorToSingleDecimal(_resourcesWrapper[i].Amount);
            _resourcesWrapper[i].Text.text = displayAmount.ToString("0.0");
        }
    }

    public void UseResourcesForBuilding(ResourceWrapper[] resourcesForBuildWrapper)
    {
        if (_test) return;

        for (int i = 0; i < resourcesForBuildWrapper.Length; i++)
        {
            var resourceEnum = resourcesForBuildWrapper[i].ResourceEnum;
            _resourcesWrapper[(int)resourceEnum].Amount -= resourcesForBuildWrapper[i].RecourceAmount;
        }
        UpdateAllTexts();
    }

    public void AddResourcesAfterDestroyBuilding(ResourceWrapper[] resourcesForBuildWrapper, float buildingHealthPercent)
    {
        for (int i = 0; i < resourcesForBuildWrapper.Length; i++)
        {
            float healthFactor = Mathf.Clamp01(buildingHealthPercent); // Убеждаемся, что значение в пределах [0, 1]
            float returnedAmount = resourcesForBuildWrapper[i].RecourceAmount * WorldGameInfo.DestroyConstructionBuildingResourcePercent * healthFactor;
            var resourceEnum = resourcesForBuildWrapper[i].ResourceEnum;
            _resourcesWrapper[(int)resourceEnum].Amount += returnedAmount;
        }

        UpdateAllTexts();
    }

    public bool ResourcesEnough(ResourceWrapper[] resourcesForBuildWrapper)
    {
        if (_test) return true;

        for (int i = 0; i < resourcesForBuildWrapper.Length; i++)
        {
            var resourceEnum = resourcesForBuildWrapper[i].ResourceEnum;
            if (resourcesForBuildWrapper[i].RecourceAmount > _resourcesWrapper[(int)resourceEnum].Amount)
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
