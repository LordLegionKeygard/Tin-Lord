using UnityEngine;
using UnityEngine.EventSystems;

public class SkillTooltipListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private SkillInfo _skill;
    [SerializeField] private SkillOfsset[] _skillOfssets;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_skill == null) return;
        CustomEvents.FireTooltipToggle(true, 1);
        CustomEvents.FireUpdateSkillToolTipTransform(transform.position.x, transform.position.y, _skill, true, _skillOfssets[Language.LanguageNumber].Xofsset, _skillOfssets[Language.LanguageNumber].Yofsset);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_skill == null) return;
        CustomEvents.FireTooltipToggle(false, 1);
    }
}

[System.Serializable]
public class SkillOfsset
{
    public float Xofsset;
    public float Yofsset;
}
