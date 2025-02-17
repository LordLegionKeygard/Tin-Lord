using UnityEngine;

[CreateAssetMenu(fileName = "Mission", menuName = "TinLord/WorldMap/Mission")]

public class Mission : ScriptableObject
{
    public string[] Name; //0 eng, 1 rus
    public BiomeEnum MusicTheme;
    public MissionLight MissionLight;
    public Sprite MissionSprite;
    public int MissionId;
    public int StartEcology;
    public Vector2 PlanetTarget;
    public int MapWidth;
    public int MapLength;
    public int MapEdge = 4;
    public int StartPosEdge = 4;
    public EnemiesSpawnerInformation EnemiesSpawnerInfo;
    public ResourceWrapper[] StartResources;
    public ObjectiveWrapper[] Objectives;
    [TextArea(1, 8)] public string[] Description;
}

[System.Serializable]
public class ObjectiveWrapper
{
    public ObjectiveEnum ObjectiveEnum;
    public int ObjectiveAmount;
}

[System.Serializable]
public enum ObjectiveEnum
{
    RestoreEcology = 0,
    KillEnemies = 1,
    ConstructBuilding = 2,
    SurviveDays = 3,
}

[System.Serializable]
public class MissionLight
{
    public Vector2 LightRotation;
    [ColorUsage(true, true)] public Color AmbientColor;
    public int Temperature;
}
