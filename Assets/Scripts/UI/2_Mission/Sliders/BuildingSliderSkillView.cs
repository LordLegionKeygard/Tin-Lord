using UnityEngine;
using Zenject;

public class BuildingSliderSkillView : MonoBehaviour
{
    [Inject] private readonly AllSkills _allSkills;
    [SerializeField] private GameObject[] _skillIcons;
    [SerializeField] private Building _building;

    private void Awake()
    {
        CustomEvents.OnUseSkill += UseSkill;
        CustomEvents.OnEndSkill += EndSkill;
    }

    public void SetBuildingTile(Building building)
    {
        _building = building;
        CheckActiveSkills();
    }

    private void CheckActiveSkills()
    {
        var allSkills = _allSkills.GetAllSkills();

        for (int i = 0; i < allSkills.Length; i++)
        {
            if (allSkills[i].IsActive())
            {
                UseSkill(allSkills[i].GetSkill());
            }
        }
    }

    private void UseSkill(SkillInfo skill)
    {
        switch (skill.SkillEnum)
        {
            case SkillEnum.Fortification:
                _skillIcons[0].SetActive(true);
                break;
            case SkillEnum.ProductionOptimization:
                if (_building.ResourcesProduction.Length != 0)
                {
                    _skillIcons[1].SetActive(true);
                }
                break;
        }
    }

    private void EndSkill(SkillInfo skill)
    {
        switch (skill.SkillEnum)
        {
            case SkillEnum.Fortification:
                _skillIcons[0].SetActive(false);
                break;
            case SkillEnum.ProductionOptimization:
                if (_building.ResourcesProduction.Length != 0)
                {
                    _skillIcons[1].SetActive(false);
                }
                break;
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnUseSkill -= UseSkill;
        CustomEvents.OnEndSkill -= EndSkill;
    }
}
