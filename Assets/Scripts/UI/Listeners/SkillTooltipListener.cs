using UnityEngine;
using UnityEngine.EventSystems;

public class SkillTooltipListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Skill _skill;
    [SerializeField] private int[] _xOfsset;
    [SerializeField] private int[] _yOfsset;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_skill == null) return;
        CustomEvents.FireTooltipToggle(true, 1);
        CustomEvents.FireUpdateSkillToolTipTransform(transform.position.x + _xOfsset[Language.LanguageNumber], transform.position.y + _yOfsset[Language.LanguageNumber], _skill, true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_skill == null) return;
        CustomEvents.FireTooltipToggle(false, 1);
    }
}
