using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTooltipListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private int _textNumber;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        CustomEvents.FireTooltipToggle(true, 0);
        CustomEvents.FireUpdateToolTipTransform(transform.position.x, transform.position.y, Language.TextStatic[_textNumber]);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CustomEvents.FireTooltipToggle(false, 0);
    }
}
