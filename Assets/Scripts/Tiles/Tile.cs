using UnityEngine;


[CreateAssetMenu(fileName = "New Tile", menuName = "Tile")]
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

    [Header("Building")]
    public BuildingTileViewEnum BuildingTileView;
    public UpgradeBuildingWrapper[] UpgradeBuildingWrapper;

    [Header("TextInfo")]
    public string[] Name; //0 eng, 1 rus
}

[System.Serializable]
public class UpgradeBuildingWrapper
{
    public string[] Name; //0 eng, 1 rus
    public int BuildingEcology;

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
    Junkyard = 16,
    DeadForest = 17,
    Oasis = 18,
    DesertRiver = 19,
    ScarceCoalDeposits = 20,
    BaseFoundation = 21,
    BlackDesert = 22,
    
}

public enum BuildingTileViewEnum
{
    None = 0,
    Base = 1,
    ElectricPowerIndustry = 2,
    CoalMining = 3,
    OreMining = 4,
    WoodExtraction = 5,
    SandMining = 6,
    OilProduction = 7,
    StoneMining = 8,
    WaterExtraction = 9,
    ScrapMining = 10,
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
