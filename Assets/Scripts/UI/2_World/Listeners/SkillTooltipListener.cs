using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SkillTooltipListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private SkillView _skillView;
    private string _actionText;

    private void Awake()
    {
        _skillView = GetComponent<SkillView>();
        CustomEvents.OnUpdateBindingText += SetText;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_skillView.IsOpen()) return;

        var text = $"{Language.TextStatic[_skillView.GetSkill().DescriptionLanguageNumber]} [{_actionText}]";
        CustomEvents.FireTooltipToggle(true, 1);
        CustomEvents.FireUpdateToolTipTransform(transform.position.x, transform.position.y, text);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_skillView.IsOpen()) return;
        CustomEvents.FireTooltipToggle(false, 1);
    }

    private void SetText(string text, InputActionReference inputActionReference)
    {
        if (!_skillView.IsOpen()) return;

        var action = _skillView.GetSkill().InputAction;
        if (inputActionReference == action)
        {
            _actionText = text;
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnUpdateBindingText -= SetText;
    }
}
