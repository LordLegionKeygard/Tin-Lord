using UnityEngine;
using UnityEngine.EventSystems;

public class TextListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private int _textNumber;
    [SerializeField] private int _xOfsset;
    [SerializeField] private int _yOfsset;
    public void OnPointerEnter(PointerEventData eventData)
    {
        CustomEvents.FireTooltipToggle(true, 0);
        CustomEvents.FireUpdateButtonToolTipTransform(transform.position.x + _xOfsset, transform.position.y + _yOfsset, Language.TextStatic[_textNumber]);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CustomEvents.FireTooltipToggle(false, 0);
    }
}

