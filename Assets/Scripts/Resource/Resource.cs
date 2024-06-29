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
    Scrap = 0,
    Wood = 1,
    Stone = 2,
    IronOre = 3,
    CopperOre = 4,
    Coal = 5,
    Oil = 6,
    Water = 7,
    Sand = 8,
    Sulfur = 9,
    StoneBlock = 10,
    IronIngot = 11,
    SteelIngot = 12,
    CopperPlate = 13,
    Concrete = 14,
    Gunpowder = 15,
}
