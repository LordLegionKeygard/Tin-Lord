using UnityEngine;

[CreateAssetMenu(fileName = "Landscape", menuName = "TinLord/Missions/Landscape")]
public class Landscape : ScriptableObject
{
    public string[] Name; //0 eng, 1 rus
    public LandscapeEnum LandscapeEnum;
    public MusicThemeEnum MusicTheme;
    public MissionView MissionView;
    public Sprite MissionSprite;
    public int StartEcology;
    public int MapWidth;
    public int MapLength;
    public int MapEdge = 4;
    public int StartPosEdge = 4;
    public ResourceWrapper[] StartResources;
    public GameEventInfo[] MissionEvents;
    public Tile[] Cards;
    [TextArea(1, 8)] public string[] Description;

    [Header("Cosmos")]
    public CosmosVariations[] CosmosVariations;

    [Header("Terminal")]
    public int[] StoryTextsIndexes;
    public int[] ConsoleTextsIndexes;
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
    public int Temperature;
    public float Intencity;
}

[System.Serializable]
public enum LandscapeEnum
{
    Canyon = 0,
    CityOfJunk = 1,
    Wasteland = 2,
    Winter = 3,
}

[System.Serializable]
public class CosmosVariations
{
    public Texture PlanetTexture;
    public Vector3 PlanetPosition;
    public Vector3 PlanetRotation;
    public Material CosmosSkybox;

    public Vector3 LightRotation;
    public float Temperature;
}
