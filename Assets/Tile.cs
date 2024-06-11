using UnityEngine;


[CreateAssetMenu(fileName = "New Tile", menuName = "Tile")]
public class Tile : ScriptableObject
{
    [Header("Base")]
    public GameObject TileObject;
    public TileTypeEnum TileTypeEnum;
    public TileView TileView;

    [Header("TextInfo")]
    public string[] Name; //0 eng, 1 rus
}

public enum TileTypeEnum
{
    Ground = 0,
    Building = 1,
}

public enum TileView
{
    Plain = 0,
    Meadow = 1,
    Road = 2,
    Forest = 3,
    Mountain = 4,
    IronVein = 5,
    CopperVein = 6,
    Peatland = 7,
    Desert = 8,
    Badlands = 9,
    Ground = 10,
}
