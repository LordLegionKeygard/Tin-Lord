using UnityEngine;

public class BuildingSkillView : MonoBehaviour
{
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
    }

    private void UseSkill(Skill skill)
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

    private void EndSkill(Skill skill)
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
