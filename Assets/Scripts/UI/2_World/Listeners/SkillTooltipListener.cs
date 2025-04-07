using UnityEngine;
using UnityEngine.EventSystems;

public class SkillTooltipListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private SkillLogic _skillLogic;

    private void Awake()
    {
        _skillLogic = GetComponent<SkillLogic>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!_skillLogic.IsOpen()) return;
        CustomEvents.FireTooltipToggle(true, 1);
        CustomEvents.FireUpdateToolTipTransform(transform.position.x, transform.position.y, Language.TextStatic[_skillLogic.GetSkill().DescriptionLanguageNumber]);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!_skillLogic.IsOpen()) return;
        CustomEvents.FireTooltipToggle(false, 1);
    }
}
