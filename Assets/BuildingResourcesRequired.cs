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
                tileObject.CurrentResourceRequired = null;
                tileObject.CurrentResourceRequiredAmount = 0;
                CustomEvents.FireChangeResourceRequired(tileObject, tileObject.CurrentResourceRequired, tileObject.CurrentResourceRequiredAmount);
                break;
            case ResourceRequiredEnum.Fuel:
                if (tileObject.CurrentResourceRequired == null) //ставим дефолтный ресурс для работы дерево
                {
                    tileObject.CurrentResourceRequired = _wood;
                    tileObject.CurrentResourceRequiredAmount = 1;
                    CustomEvents.FireChangeResourceRequired(tileObject, tileObject.CurrentResourceRequired, tileObject.CurrentResourceRequiredAmount);
                }
                break;
            case ResourceRequiredEnum.Electricity:
                tileObject.CurrentResourceRequired = _electricity;
                tileObject.CurrentResourceRequiredAmount = 1;
                CustomEvents.FireChangeResourceRequired(tileObject, tileObject.CurrentResourceRequired, tileObject.CurrentResourceRequiredAmount);
                break;
        }
    }

    public void ChangeResourceRequired(TileObject tileObject, Resource resource)
    {
        tileObject.CurrentResourceRequired = resource;
        tileObject.CurrentResourceRequiredAmount = 1;
        CustomEvents.FireChangeResourceRequired(tileObject, tileObject.CurrentResourceRequired, tileObject.CurrentResourceRequiredAmount);
    }
}
