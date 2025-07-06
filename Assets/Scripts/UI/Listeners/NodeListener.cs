using UnityEngine;
using UnityEngine.EventSystems;

public class NodeListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private UINode _uiNode;
    public void OnPointerEnter(PointerEventData eventData)
    {
        CustomEvents.FireTooltipToggle(true, 0);
        CustomEvents.FireUpdateToolTipTransform(transform.position.x, transform.position.y, Language.TextStatic[_uiNode.GetDescriptionTextNumber()], 0.5f, WorldGameInfo.NodePivot);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CustomEvents.FireTooltipToggle(false, 0);
    }
}
