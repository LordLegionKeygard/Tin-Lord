using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ButtonTooltipListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Main")]
    [SerializeField] private int _textNumber;
    [SerializeField] private float _xPivot = 0.5f;
    [SerializeField] private float _yPivot = WorldGameInfo.ButtonPivot;

    [Header("InputAction")]
    [SerializeField] private InputActionReference _action;
    private string _actionText;

    private void Awake()
    {
        CustomEvents.OnUpdateBindingText += SetText;
    }

    private void Start()
    {
        if (_action == null || _action.action == null) return;

        int bindingIndex = _action.action.GetBindingIndexForControl(_action.action.controls[0]);
        _actionText = _action.action.GetBindingDisplayString(bindingIndex, out _, out _);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var text = _action != null ? $"{Language.TextStatic[_textNumber]} [{_actionText}]" : Language.TextStatic[_textNumber];
        CustomEvents.FireTooltipToggle(true, 0);
        CustomEvents.FireUpdateToolTipTransform(transform.position.x, transform.position.y, text, _xPivot, _yPivot);
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
