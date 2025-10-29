using UnityEngine;

[CreateAssetMenu(fileName = "Landscape", menuName = "TinLord/Missions/Landscape")]
public class Landscape : ScriptableObject
{
    public string[] Name; //0 eng, 1 rus
    public LandscapeEnum LandscapeEnum;
    public MonsterType MonsterType;
    public MissionView MissionView;
    public Sprite LoadingScreenSprite;
    public int StartEcology;
    public int MapWidth;
    public int MapLength;
    public int MapEdge = 4;
    public int StartPosEdge = 4;
    public GameEventInfo[] MissionEvents;
    public Tile[] Cards;
    public DescriptionWrapper[] DescriptionWrappers;

    [Header("Cosmos")]
    public CosmosVariations[] CosmosVariations;
}

[System.Serializable]
public class MissionView
{
    public BiomEnum BiomEnum;
    public Texture RockTexture;
    public MissionLight MissionLight;
}

[System.Serializable]
public class MissionLight
{
    public Vector2 LightRotation;
    [ColorUsage(true, true)] public Color SkyColor;

    [ColorUsage(true, true)] public Color EquatorColor;

    [ColorUsage(true, true)] public Color GroundColor;

    public Color FilterColor;
    public int Temperature;
    public float Intencity;
}

[System.Serializable]
public class DescriptionWrapper
{
    public int PanelHeight;
    [TextArea(1, 10)] public string Description;
}

[System.Serializable]
public enum LandscapeEnum
{
    Canyon = 0,
    CityOfJunk = 1,
    Wasteland = 2,
    FrozenValley = 3,
    IceLake = 4,
    ScorchedLands = 5,
    Megastructure = 6,
    AcidForest = 7,
    Swamp = 8,
    DeepCrags = 9,
    BasaltValley = 10,
    Ashlands = 11,
}
