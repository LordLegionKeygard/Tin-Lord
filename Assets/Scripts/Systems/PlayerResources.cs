using UnityEngine;
using TMPro;
using System;

public class PlayerResources : MonoBehaviour
{
    [SerializeField] private PlayerResourcesWrapper[] _resourcesWrapper;

    private void Start()
    {
        UpdateAllTexts();
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
public class PlayerResourcesWrapper
{
    public Resource Resource;
    public float Amount;
    public TextMeshProUGUI Text;

}
