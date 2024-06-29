using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    [SerializeField] private ResourcesWrapper[] _resourcesWrapper;


    public void AddResources(ResourceEnum resourceEnum, int amount)
    {
        _resourcesWrapper[(int)resourceEnum].Amount += amount;
    }
}

[System.Serializable]
public class ResourcesWrapper
{
    [HideInInspector] public string ElementName;
    public Resource Resource;
    public int Amount;

}
