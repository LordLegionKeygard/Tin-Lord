using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTooltipListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string[] _tooltipTexts;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        CustomEvents.FireTooltipToggle(true, 0);
        CustomEvents.FireUpdateToolTipTransform(transform.position.x, transform.position.y, _tooltipTexts[Language.LanguageNumber]);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CustomEvents.FireTooltipToggle(false, 0);
    }
}
