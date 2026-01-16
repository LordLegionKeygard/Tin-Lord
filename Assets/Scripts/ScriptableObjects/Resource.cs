using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "TinLord/Resource")]
public class Resource : ScriptableObject
{
    [Header("Base")]
    public Sprite Icon;
    public ResourceEnum ResourceEnum;
    public ResourceType ResourceType;
    public int Price;

    [Header("TextInfo")]
    public int NameNumber;
}

public enum ResourceType
{
    Resource = 0,
    Material = 1,
    Component = 2,
    Other = 3,
}

public enum ResourceEnum
{
    None = -1,
    Wood = 0, // 1_ResourceDialogue
    Stone = 1, // 4_ResourceDialogue
    IronOre = 2, // 3_ResourceDialogue
    CopperOre = 3, // 8_ResourceDialogue
    Coal = 4, // 9_ResourceDialogue
    Oil = 5,
    Water = 6, // 6_ResourceDialogue
    Sand = 7, // 7_ResourceDialogue
    Electricity = 8, // 5_ResourceDialogue
    StoneBlock = 9,
    IronIngot = 10,
    SteelIngot = 11,
    CopperPlate = 12,
    Concrete = 13,
    Steam = 14,
    Glass = 15, // 7_ResourceDialogue
    CopperWire = 16, // 8_ResourceDialogue
    GearWheel = 17,
    ElectronicCircuit = 18,
    Processor = 19,
    Engine = 20,
    ElectricEngine = 21,
    DataFragment = 22,
    BeamEnergy = 23,
}
