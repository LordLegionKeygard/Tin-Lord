using UnityEngine;

public class AllSkills : MonoBehaviour
{
    [SerializeField] private BaseSkill[] _baseSkills;
    [SerializeField] private SkillView[] _allSkills;
    [SerializeField] private FortificationSkill _fortificationSkill;
    public FortificationSkill GetFortificationSkill() => _fortificationSkill;
    public void TimeTickAllSkill()
    {
        for (int i = 0; i < _baseSkills.Length; i++)
        {
            _baseSkills[i].TimeTick();
        }
    }

    public void LoadAllSkills(int[] skillCooldown, int lastOpenedMissionId)
    {
        if (skillCooldown == null || skillCooldown.Length == 0) skillCooldown = new int[_allSkills.Length];

        for (int i = 0; i < _baseSkills.Length; i++)
        {
            _baseSkills[i].LoadSkill(skillCooldown[i], lastOpenedMissionId);
        }
    }

    public int[] GetAllSkillsCooldown()
    {
        var skillsData = new int[_allSkills.Length];

        for (int i = 0; i < _allSkills.Length; i++)
        {
            skillsData[i] = _allSkills[i].GetCurrentCooldown();
        }

        return skillsData;
    }
}
