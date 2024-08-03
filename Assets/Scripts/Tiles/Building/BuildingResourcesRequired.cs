using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingResourcesRequired : MonoBehaviour
{
    [SerializeField] private Resource _electricity;
    [SerializeField] private Resource _wood;
    [SerializeField] private Resource _coal;
    public void SetResourceRequiredAfterSpawnOrUpgradeBuilding(TileObject tileObject, ResourceRequiredEnum resourceRequiredEnum)
    {
        switch (resourceRequiredEnum)
        {
            case ResourceRequiredEnum.None:
                tileObject.SetResourceRequied(null, 0);
                break;
            case ResourceRequiredEnum.Fuel:
                if (tileObject.CurrentResourceRequired() == null) //ставим дефолтный ресурс для работы дерево
                {
                    tileObject.SetResourceRequied(_wood, 1);
                }
                break;
            case ResourceRequiredEnum.Electricity:
                tileObject.SetResourceRequied(_electricity, 1);
                break;
        }
    }

    public void ChangeResourceRequired(TileObject tileObject, Resource resource)
    {
        tileObject.SetResourceRequied(resource, 1);
    }
}
