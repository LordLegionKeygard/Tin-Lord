using System.Globalization;
using UnityEngine;

public class AllSkills : MonoBehaviour
{
    [SerializeField] private BaseSkill[] _baseSkills;
    [SerializeField] private SkillView[] _allSkillView;
    public BaseSkill GetSkill(int skillNumber) => _baseSkills[skillNumber];
    
    public void TimeTickAllSkill()
    {
        for (int i = 0; i < _baseSkills.Length; i++)
        {
            _baseSkills[i].TimeTick();
        }
    }

    public void LoadAllSkills(int[] skillCooldown, int[] skillDuration, int lastOpenedMissionId)
    {
        if (skillCooldown == null || skillDuration == null || skillCooldown.Length == 0 || skillDuration.Length == 0)
        {
            skillCooldown = new int[_allSkillView.Length];
            skillDuration = new int[_allSkillView.Length];
        }

        for (int i = 0; i < _baseSkills.Length; i++)
        {
            _baseSkills[i].LoadSkill(skillCooldown[i], skillDuration[i], lastOpenedMissionId);
        }
    }

    public int[] GetAllSkillsCooldown()
    {
        var skillsData = new int[_allSkillView.Length];

        for (int i = 0; i < _allSkillView.Length; i++)
        {
            skillsData[i] = _allSkillView[i].GetCurrentCooldown();
        }

        return skillsData;
    }

    public int[] GetAllSkillsDuration()
    {
        var skillsData = new int[_baseSkills.Length];

        for (int i = 0; i < _baseSkills.Length; i++)
        {
            skillsData[i] = _baseSkills[i].GetCurrentDurationTick();
        }

        return skillsData;
    }
}
