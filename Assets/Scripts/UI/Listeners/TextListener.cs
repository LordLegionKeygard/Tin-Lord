using UnityEngine;
using UnityEngine.EventSystems;

public class TextListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private int _textNumber;
    [SerializeField] private float _xPivot;
    [SerializeField] private float _yPivot;
    public void OnPointerEnter(PointerEventData eventData)
    {
        CustomEvents.FireTooltipToggle(true, 0);
        CustomEvents.FireUpdateToolTipTransform(transform.position.x, transform.position.y, Language.TextStatic[_textNumber], _xPivot, _yPivot);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CustomEvents.FireTooltipToggle(false, 0);
    }
}

