using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingTypeTooltipListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] BuildingType _buildingType;

    public void OnPointerEnter(PointerEventData eventData)
    {
        CustomEvents.FireTooltipToggle(true, 0);
        CustomEvents.FireUpdateButtonToolTipTransform(transform.position.x, transform.position.y, _buildingType.CurrentTile().Name[Language.LanguageNumber]);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CustomEvents.FireTooltipToggle(false, 0);
    }
}
