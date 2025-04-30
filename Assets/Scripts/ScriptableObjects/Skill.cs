using FMODUnity;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "TinLord/Skill")]
public class Skill : ScriptableObject
{
    [Header("Main")]
    public SkillEnum SkillEnum;
    public Sprite Icon;
    public int CooldownTicks;
    public int DurationTicks;
    public SkillResource RequiredResource;
    public int RequiredOpenedMission;
    public EventReference Sound;

    [Header("Tooltip")]
    public string[] Name;
    public string[] Info;
    public string Input;
}

public enum SkillEnum
{
    GeneralRepair = 0,
    Fortification = 1,
    ProductionOptimization = 2,
}

[System.Serializable]
public class SkillResource
{
    public Resource Resource;
    public int RecourceAmount;
}
