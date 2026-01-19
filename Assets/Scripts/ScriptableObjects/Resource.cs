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
    Oil = 5, // 10_ResourceDialogue
    Water = 6, // 6_ResourceDialogue
    Sand = 7, // 7_ResourceDialogue
    Electricity = 8, // 5_ResourceDialogue
    StoneBlock = 9, // 1_MaterialDialogue
    IronIngot = 10, // 2_MaterialDialogue
    SteelIngot = 11, // 3_MaterialDialogue
    CopperPlate = 12, // 4_MaterialDialogue
    Concrete = 13, // 5_MaterialDialogue
    Steam = 14, // 6_MaterialDialogue
    Glass = 15, // 7_ResourceDialogue
    CopperWire = 16, // 8_ResourceComponentDialogue
    GearWheel = 17, // 0_ComponentDialogue
    ElectronicCircuit = 18, // 1_ComponentDialogue
    Processor = 19, // 2_ComponentDialogue
    Engine = 20, // 3_ComponentDialogue
    ElectricEngine = 21, // 4_ComponentDialogue
    DataFragment = 22,
    BeamEnergy = 23,
}
