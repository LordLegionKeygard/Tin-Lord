using UnityEngine;


[CreateAssetMenu(fileName = "New Tile", menuName = "Tile")]
public class Tile : ScriptableObject
{
    [Header("Base")]
    public GameObject TileObject;
    public Sprite Icon;
    public TileTypeEnum TileTypeEnum;
    public TileViewEnum TileView;
    public bool IsWater;

    [Header("TextInfo")]
    public string[] Name; //0 eng, 1 rus
}

public enum TileTypeEnum
{
    Ground = 0,
    Building = 1,
}

public enum TileViewEnum
{
    Plain = 0,
    Meadow = 1,
    Road = 2,
    Forest = 3,
    Mountain = 4,
    IronVein = 5,
    CopperVein = 6,
    OilField = 7,
    Desert = 8,
    Badlands = 9,
    Ground = 10,
    CoalDeposits = 11,
    Highland = 12,
    River = 13,
    PollutedRiver = 14,
    
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
