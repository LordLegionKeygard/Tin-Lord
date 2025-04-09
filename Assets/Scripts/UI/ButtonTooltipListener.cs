using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ButtonTooltipListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Main")]
    [SerializeField] private int _textNumber;

    [Header("InputAction")]
    [SerializeField] private InputActionReference _action;
    private string _actionText;

    private void Awake()
    {
        CustomEvents.OnUpdateBindingText += SetText;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var text = _action != null ? $"{Language.TextStatic[_textNumber]} [{_actionText}]" : Language.TextStatic[_textNumber];
        CustomEvents.FireTooltipToggle(true, 0);
        CustomEvents.FireUpdateButtonToolTipTransform(transform.position.x, transform.position.y, text);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CustomEvents.FireTooltipToggle(false, 0);
    }

    private void SetText(string text, InputActionReference inputActionReference)
    {
        if (inputActionReference == _action)
        {
            _actionText = text;
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnUpdateBindingText -= SetText;
    }
}
