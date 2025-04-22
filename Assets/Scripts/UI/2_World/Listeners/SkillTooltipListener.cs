using UnityEngine;
using UnityEngine.EventSystems;

public class SkillTooltipListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private BaseSkill _baseSkill;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_baseSkill.IsOpen()) return;

        var skill = _baseSkill.GetSkill();

        var text = $"{Language.TextStatic[skill.DescriptionLanguageNumber]} [{skill.ActionText}]";
        CustomEvents.FireTooltipToggle(true, 1);
        CustomEvents.FireUpdateSkillToolTipTransform(transform.position.x, transform.position.y, skill.MaxWidth, skill.Padding, text);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_baseSkill.IsOpen()) return;
        CustomEvents.FireTooltipToggle(false, 1);
    }
}
