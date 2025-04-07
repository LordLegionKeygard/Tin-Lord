using UnityEngine;
using UnityEngine.EventSystems;

public class SkillTooltipListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private SkillPanel _skillPanel;
    [SerializeField] private Skill _skill;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _skillPanel.SetText(Language.TextStatic[_skill.DescriptionLanguageNumber]);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _skillPanel.ResetText();
    }
}
