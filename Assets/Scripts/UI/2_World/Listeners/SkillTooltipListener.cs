using UnityEngine;
using UnityEngine.EventSystems;

public class SkillTooltipListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private SkillView _skillView;

    private void Awake()
    {
        _skillView = GetComponent<SkillView>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_skillView.IsOpen()) return;

        var skill = _skillView.GetSkill();

        var text = $"{Language.TextStatic[skill.DescriptionLanguageNumber]} [{skill.ActionText}]";
        CustomEvents.FireTooltipToggle(true, 1);
        CustomEvents.FireUpdateSkillToolTipTransform(transform.position.x, transform.position.y, skill.MaxWidth, skill.Padding, text);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_skillView.IsOpen()) return;
        CustomEvents.FireTooltipToggle(false, 1);
    }
}
