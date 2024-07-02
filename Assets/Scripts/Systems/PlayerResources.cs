using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerResources : MonoBehaviour
{
    [SerializeField] private PlayerResourcesWrapper[] _resourcesWrapper;


    public void AddResources(ResourceEnum resourceEnum, float amount)
    {
        var resources = _resourcesWrapper[(int)resourceEnum];
        resources.Amount += amount;
        resources.Text.text = resources.Amount.ToString();

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
