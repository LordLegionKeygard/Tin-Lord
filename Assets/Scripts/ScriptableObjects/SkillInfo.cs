using FMODUnity;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "TinLord/Skill")]
public class SkillInfo : ScriptableObject
{
    [Header("Main")]
    public SkillEnum SkillEnum;
    public int QuantPrice;
    public int ShardPrice;
    public Sprite Icon;
    public float TriggerStayDamageFactor;
    public int CooldownTicks;
    public int DurationTicks;
    public SkillResource RequiredResource;
    public EventReference Sound;

    [Header("Tooltip")]
    public int NameNumber;
    public int InfoNumber;
    public string Input;
}

public enum SkillEnum
{
    GeneralRepair = 0,
    Ignite = 1,
    Fortification = 2,
    ProductionOptimization = 3,
}

[System.Serializable]
public class SkillResource
{
    public Resource Resource;
    public int RecourceAmount;
}
