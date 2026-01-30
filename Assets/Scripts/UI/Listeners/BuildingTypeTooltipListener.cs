using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingTypeTooltipListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] BuildingType _buildingType;

    public void OnPointerEnter(PointerEventData eventData)
    {
        CustomEvents.FireTooltipToggle(true, 0);
        CustomEvents.FireUpdateToolTipTransform(transform.position.x, transform.position.y, Language.TextStatic[_buildingType.CurrentTile().NameLanguageNumber], 0.5f, WorldGameInfo.BuildinTypePivot);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CustomEvents.FireTooltipToggle(false, 0);
    }
}
