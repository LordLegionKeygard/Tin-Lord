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
    Base = 0,
    BuildingMaterial = 1,
    Robotics = 2,
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
    Cop = 14,
    Conc = 15,
}
