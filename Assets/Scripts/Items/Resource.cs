using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "TinLord/Resource")]
public class Resource : ScriptableObject
{
    [Header("Base")]
    public Sprite Icon;
    public ResourceEnum ResourceEnum;
    public ResourceType ResourceType;
    
    [Header("TextInfo")]
    public string[] Name; //0 eng, 1 rus
}

public enum ResourceType
{
    Resource = 0,
    Material = 1,
    Component = 2,
}

public enum ResourceEnum
{
    None = -1,
    Wood = 0,
    Stone = 1,
    IronOre = 2,
    CopperOre = 3,
    Coal = 4,
    Oil = 5,
    Water = 6,
    Sand = 7,
    Electricity = 8,
    StoneBlock = 9,
    IronIngot = 10,
    SteelIngot = 11,
    CopperPlate = 12,
    Concrete = 13,
    Steam = 14,
    Glass = 15,
    CopperWire = 16,
    GearWheel = 17,
    ElectronicCircuit = 18,
    Processor = 19,
    Engine = 20,
    ElectricEngine = 21,
    MemoryFragment = 22,
}
