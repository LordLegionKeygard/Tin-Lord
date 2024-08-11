using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingResourcesRequired : MonoBehaviour
{
    public void SetResourceRequiredAfterSpawnOrUpgradeBuilding(TileObject tileObject, ResourcesForWorkWrapper[] resourcesForWork)
    {
        if (resourcesForWork.Length == 0)
        {
            tileObject.SetResourceRequied(null, 0);
        }
        else
        {
            tileObject.SetResourceRequied(resourcesForWork[0].ResourceForWork, resourcesForWork[0].ResourcesForWorkAmount); //ставим дефолтный ресурс для работы дерево  
        }
    }

    public void ChangeResourceRequired(TileObject tileObject, Resource resource, float amount)
    {
        tileObject.SetResourceRequied(resource, amount);
    }
}
