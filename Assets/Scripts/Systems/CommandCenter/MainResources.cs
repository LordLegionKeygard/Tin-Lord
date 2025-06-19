using System;
using TMPro;
using UnityEngine;

public class MainResources : MonoBehaviour
{
    [SerializeField] private MainResourcesWrapper[] _resourcesWrapper;
    [SerializeField] private TextMeshProUGUI _memoryFragmentsText;
    public float GetResourceAmountForEnum(ResourceEnum resourceEnum) => _resourcesWrapper[(int)resourceEnum].Amount;

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

        _memoryFragmentsText.text = _resourcesWrapper[(int)ResourceEnum.MemoryFragment].Amount.ToString();
    }

    public bool ResourceEnough(ResourceEnum resourceEnum, float amount)
    {
        return _resourcesWrapper[(int)resourceEnum].Amount >= amount;
    }
}

[System.Serializable]
public class MainResourcesWrapper
{
    public Resource Resource;
    public float Amount;
    public TextMeshProUGUI Text;

}
