using UnityEngine;
using UnityEngine.EventSystems;

public class BaseSkillTooltipListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private BaseSkill _baseSkill;
    private float _xOfsset = -0.1f;
    private float _yOfsset = -0.11f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_baseSkill == null || !_baseSkill.IsOpen()) return;

        var skill = _baseSkill.GetSkill();

        CustomEvents.FireTooltipToggle(true, 1);
        CustomEvents.FireUpdateSkillToolTipTransform(transform.position.x, transform.position.y, skill, _baseSkill.ResourceEnough(), _xOfsset, _yOfsset);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_baseSkill == null || !_baseSkill.IsOpen()) return;
        CustomEvents.FireTooltipToggle(false, 1);
    }
}
