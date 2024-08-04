using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerResources : MonoBehaviour
{
    [SerializeField] private PlayerResourcesWrapper[] _resourcesWrapper;


    public void ChangeResource(ResourceEnum resourceEnum, float amount)
    {
        var resources = _resourcesWrapper[(int)resourceEnum];
        resources.Amount += amount;
        resources.Text.text = resources.Amount.ToString("0.0");
    }

    private void UpdateAllTexts()
    {
        for (int i = 0; i < _resourcesWrapper.Length; i++)
        {
            _resourcesWrapper[i].Text.text = _resourcesWrapper[i].Amount.ToString("0.0");
        }
    }

    public void UseResourcesFromBuilding(ResourcesForBuildWrapper[] resourcesForBuildWrapper)
    {
        for (int i = 0; i < resourcesForBuildWrapper.Length; i++)
        {
            _resourcesWrapper[(int)resourcesForBuildWrapper[i].ResourcesForBuild].Amount -= resourcesForBuildWrapper[i].RecourcesForBuildAmount;
        }
        UpdateAllTexts();
    }

    public void AddResourcesFromDestroyBuilding(ResourcesForBuildWrapper[] resourcesForBuildWrapper)
    {
        for (int i = 0; i < resourcesForBuildWrapper.Length; i++)
        {
            _resourcesWrapper[(int)resourcesForBuildWrapper[i].ResourcesForBuild].Amount += resourcesForBuildWrapper[i].RecourcesForBuildAmount / 2;
        }
        UpdateAllTexts();
    }

    public bool ResourcesForBuildEnough(ResourcesForBuildWrapper[] resourcesForBuildWrapper)
    {
        for (int i = 0; i < resourcesForBuildWrapper.Length; i++)
        {
            if (resourcesForBuildWrapper[i].RecourcesForBuildAmount > _resourcesWrapper[(int)resourcesForBuildWrapper[i].ResourcesForBuild].Amount)
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
    [HideInInspector] public string ElementName;
    public Resource Resource;
    public float Amount;
    public TextMeshProUGUI Text;

}
