using UnityEngine;


[CreateAssetMenu(fileName = "New Tile", menuName = "TinLord/Tile")]
public class Tile : ScriptableObject
{
    [Header("Base")]
    public GameObject TileObject;
    public Sprite Icon;
    public TileTypeEnum TileTypeEnum;

    [Header("Ground")]
    public GroundTileViewEnum GroundTileView;
    public int GroundEcology;
    public bool IsWater;
    public bool IsFourTile;
    public BuildingsOnTile[] BuildingsOnTile;

    [Header("Building")]
    public BuildingTileViewEnum BuildingTileView;
    public Resource Resource;
    public UpgradeBuildingWrapper[] UpgradeBuildingWrapper;
    public Building[] Buildings;

    [Header("TextInfo")]
    public string[] Name; //0 eng, 1 rus
}

[System.Serializable]
public class UpgradeBuildingWrapper
{
    public string[] Name; //0 eng, 1 rus
    public Sprite BuildingSprite;
    public int BuildingEcology;
    public float ResourceExtractedAmount; // за 1 тик времени

    [Header("Requires")]
    public ResourcesForBuildWrapper[] ResourcesForBuild;
    public ResourcesForWorkWrapper[] ResourcesForWork;
}

[System.Serializable]
public class ResourcesForWorkWrapper
{
    public Resource ResourceForWork;
    public float ResourcesForWorkAmount;
}

[System.Serializable]
public class ResourcesForBuildWrapper
{
    public ResourceEnum ResourcesForBuild;
    public int RecourcesForBuildAmount;
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
    OilField = 8,
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
}

public enum BuildingTileViewEnum
{
    None = 0,
    Base = 1,
    ElectricPowerIndustry = 2,
    CoalMining = 3,
    IronOreMining = 4,
    WoodExtraction = 5,
    SandMining = 6,
    OilProduction = 7,
    StoneMining = 8,
    LandWaterExtraction = 9,
    Bridge = 10,
    CopperOreMining = 11,
    // HydroPowerIndustry = 12,
    // RiverWaterExtraction = 13,
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
