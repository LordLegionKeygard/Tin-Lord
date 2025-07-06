using UnityEngine;
using UnityEngine.EventSystems;

public class SkillTooltipListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private SkillInfo _skill;
    [SerializeField] private float _xOfsset;
    [SerializeField] private float _yOfsset;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_skill == null) return;
        CustomEvents.FireTooltipToggle(true, 1);
        CustomEvents.FireUpdateSkillToolTipTransform(transform.position.x, transform.position.y, _skill, true, _xOfsset, _yOfsset);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_skill == null) return;
        CustomEvents.FireTooltipToggle(false, 1);
    }
}
