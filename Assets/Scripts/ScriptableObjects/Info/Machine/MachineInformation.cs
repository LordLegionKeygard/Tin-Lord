using UnityEngine;

[CreateAssetMenu(fileName = "MachineInformation", menuName = "TinLord/Info/MachineInformation")]
public class MachineInformation : ScriptableObject
{
    public string[] Name; //0 eng, 1 rus
    public Sprite MachineSprite;
    public MachineType MachineType;
    public float DetectionRadius;
    [SerializeField] private MachineLevelInfo MachineLevelInfo;
    public ResourceWrapper[] ResourcesForBuild; // кол-во ресурсов для строительства

    public float GetDurability(int level) => MachineLevelInfo.StartDurability + MachineLevelInfo.FactorDurability * level;
    public int GetMeleeDamage(int level) => (int)(MachineLevelInfo.StartMeleeDamage + MachineLevelInfo.FactorMeleeDamage * level);
    public int GetRangeDamage(int level) => (int)(MachineLevelInfo.StartRangeDamage + MachineLevelInfo.FactorRangeDamage * level);
}

[System.Serializable]
public class MachineLevelInfo
{
    [Header("StartParams")]
    public int StartDurability;
    public int StartMeleeDamage;
    public int StartRangeDamage;

    [Header("Factor")]
    public int FactorDurability;
    public float FactorMeleeDamage;
    public float FactorRangeDamage;
}
