using UnityEngine;


[CreateAssetMenu(fileName = "New Tile", menuName = "TinLord/Tile")]
public class Tile : ScriptableObject
{
    [Header("TextInfo")]
    public string[] Name; //0 eng, 1 rus

    [Header("Base")]
    public GameObject TileObject;
    public Sprite Icon;
    public TileTypeEnum TileTypeEnum;

    [Header("Ground")]
    public GroundTileViewEnum GroundTileView;
    public int GroundEcology;
    public bool IsWater;
    public bool IsFourTile;
    public bool IsTurret;
    public Tile[] BuildingTypes;
    public ProductionOnGroundResources[] ProductionOnGroundResources;

    [Header("Building")]
    public BuildingTileViewEnum BuildingTileView;
    public Building[] Buildings;
    public bool IsHaveProdictionResources() => Buildings[0].ResourcesProduction.Length > 0;
}

[System.Serializable]
public class ProductionOnGroundResources
{
    public Resource ProductionOnGroundResource;
    public float ProductionOnGroundResourceModifier;
}

[System.Serializable]
public class BuildingsOnTile
{
    public Tile BuildingTile;
    public float ResourceModifier;
}

public enum TileTypeEnum
{
    Ground = 0,
    Building = 1,
}

public enum GroundTileViewEnum
{
    None = 0,
    Plain = 1,
    Meadow = 2,
    Road = 3,
    Forest = 4,
    Mountain = 5,
    IronDeposits = 6,
    CopperDeposits = 7,
    OilSwamp = 8,
    Desert = 9,
    Barrenland = 10,
    Ground = 11,
    CoalDeposits = 12,
    Highland = 13,
    River = 14,
    PollutedRiver = 15,
    DeadForest = 16,
    Oasis = 17,
    DesertRiver = 18,
    ScarceCoalDeposits = 19,
    BaseFoundation = 20,
    BlackDesert = 21, 
    DriedOasis = 22,
}

public enum BuildingTileViewEnum
{
    None = -1,
    Base = 0,
    ElectricPowerIndustry = 1,
    CoalMining = 2,
    OreMining = 3,
    WoodExtraction = 4,
    SandMining = 5,
    OilProduction = 6,
    StoneMining = 7,
    LandWaterExtraction = 8,
    Bridge = 9,
    StoneBlockProduction = 10,
    SmeltingProduction = 11,
    ConcreteProduction = 12,
    SteamProduction = 13,
    ComponentsProduction = 14,
    AttackSructures = 15,
}

public enum TileDirectionEnum
{
    North = 0,
    NorthEast = 1,
    East = 2,
    SouthEast = 3,
    South = 4,
    SouthWest = 5,
    West = 6,
    NorthWest = 7
}
