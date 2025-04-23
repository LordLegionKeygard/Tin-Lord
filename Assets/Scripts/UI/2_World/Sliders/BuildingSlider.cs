using UnityEngine;

public class BuildingSlider : BaseSlider
{
    [SerializeField] private GameObject _fortificationIcon;

    private void Awake()
    {
        CustomEvents.OnUseSkill += UseSkill;
        CustomEvents.OnEndSkill += EndSkill;
    }

    private void UseSkill(Skill skill)
    {
        if (skill.SkillEnum == SkillEnum.Fortification)
        {
            _fortificationIcon.SetActive(true);
        }
    }

    private void EndSkill(Skill skill)
    {
        if (skill.SkillEnum == SkillEnum.Fortification)
        {
            _fortificationIcon.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnUseSkill -= UseSkill;
        CustomEvents.OnEndSkill -= EndSkill;
    }
}
