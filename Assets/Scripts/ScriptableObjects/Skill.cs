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
    public int RequiredOpenedMission;
    public string ActionText;
    public EventReference Sound;

    [Header("Description")]
    public int DescriptionLanguageNumber;
    public int MaxWidth;
    public int Padding;
}

public enum SkillEnum
{
    GeneralRepair = 0,
    Fortification = 1,
    ProductionOptimization = 2,
}
