using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerResources : MonoBehaviour
{
    [SerializeField] private PlayerResourcesWrapper[] _resourcesWrapper;


    public void AddResource(ResourceEnum resourceEnum, float amount)
    {
        var resources = _resourcesWrapper[(int)resourceEnum];
        resources.Amount += amount;
        resources.Text.text = ((int)resources.Amount).ToString();
    }

    private void UpdateAllTexts()
    {
        for (int i = 0; i < _resourcesWrapper.Length; i++)
        {
            _resourcesWrapper[i].Text.text = ((int)_resourcesWrapper[i].Amount).ToString();
        }
    }

    public void RemoveResourcesFromBuild(ResourcesForBuildWrapper[] resourcesForBuildWrapper)
    {
        for (int i = 0; i < resourcesForBuildWrapper.Length; i++)
        {
            _resourcesWrapper[(int)resourcesForBuildWrapper[i].ResourcesForBuild].Amount -= resourcesForBuildWrapper[i].RecourcesForBuildAmount;
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
}

[System.Serializable]
public class PlayerResourcesWrapper
{
    [HideInInspector] public string ElementName;
    public Resource Resource;
    public float Amount;
    public TextMeshProUGUI Text;

}
