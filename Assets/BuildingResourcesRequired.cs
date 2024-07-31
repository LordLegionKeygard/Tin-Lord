using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingResourcesRequired : MonoBehaviour
{
    [SerializeField] private Resource _electricity;
    public void ChangeResourcesRequired(TileObject tileObject, ResourceRequiredEnum resourceRequiredEnum)
    {
        switch (resourceRequiredEnum)
        {
            case ResourceRequiredEnum.None:

                break;
            case ResourceRequiredEnum.Fuel:

                break;
            case ResourceRequiredEnum.Electricity:
                tileObject.CurrentResourceRequired = _electricity;
                tileObject.CurrentResourceRequiredAmount = 1;
                CustomEvents.FireChangeResourceRequired(tileObject, _electricity, 1);
                break;
        }
    }
}
