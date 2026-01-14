using UnityEngine;

[CreateAssetMenu(fileName = "MachineInfo", menuName = "TinLord/Info/MachineInfo")]
public class MachineInfo : ScriptableObject
{
    public string[] Name; //0 eng, 1 rus
    public Sprite MachineSprite;
    public MachineType MachineType;
    public float DetectionRadius;
    public int RequiredBuildingLevel;
    public MachineLevelInfo MachineLevelInfo;
    public ResourceWrapper[] ResourcesForBuild; // кол-во ресурсов для строительства

    public float GetDurability(int level) => MachineLevelInfo.StartDurability + MachineLevelInfo.FactorDurability * level;
    public int GetRangeDamage(int level) => (int)(MachineLevelInfo.StartRangeDamage + MachineLevelInfo.FactorRangeDamage * level);
}

[System.Serializable]
public class MachineLevelInfo
{
    [Header("StartParams")]
    public int StartDurability;
    public int StartRangeDamage;

    [Header("Factor")]
    public int FactorDurability;
    public float FactorRangeDamage;
}
