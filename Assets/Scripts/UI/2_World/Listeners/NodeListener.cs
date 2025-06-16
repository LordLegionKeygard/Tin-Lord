using UnityEngine;
using UnityEngine.EventSystems;

public class NodeListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private UINode _uiNode;
    public void OnPointerEnter(PointerEventData eventData)
    {
        CustomEvents.FireTooltipToggle(true, 0);
        CustomEvents.FireUpdateButtonToolTipTransform(transform.position.x, transform.position.y - 120, Language.TextStatic[_uiNode.GetDescriptionTextNumber()]);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CustomEvents.FireTooltipToggle(false, 0);
    }
}
