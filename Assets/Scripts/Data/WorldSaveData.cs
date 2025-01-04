using UnityEngine;

/// <summary>
/// Хранит дату о миссии
/// </summary>
/// 

[System.Serializable]
public class WorldSaveData
{
    [Header("Main")]
    public int MissionId;
    public bool IsStartMission;

    [Header("UpPanel")]
    public int Day;
    public float Time;
    public int Radiation;
    public int GameSpeed; //    Pause = 0, Default = 1, Double = 2, Triple = 3,

    [Header("Experience")]
    public RobotDataWrapper[] RobotsData; //Tank = 0, Sniper = 1, Engineer = 2,

    [Header("Resources")]
    public float[] ResourcesData;

    [Header("Cards")]
    public int[] Cards;

    [Header("Tiles")]
    public bool IsHaveRiver; // возможно данные не нужны, при установке зданий эти значения сами установятся
    public bool IsHaveBase;
    public TileDataWrapper[] TilesData;
}

[System.Serializable]
public class RobotDataWrapper
{
    public int Level;
    public int Experience;
}

[System.Serializable]
public class TileDataWrapper
{
    public int GroundTileId;
    public int GroundTileRotation;
    public int BuildingTileId;
    public int BuildingTileRotation;
    public float BuildingHealth;
    public bool IsBuildingWork;

    public bool IsLake;
    public int RiverNumber;
    public bool IsBridge;
    public bool IsLastRiverTile;
    public int RiverType;
    public int RiverRotation;

    //соседи тайла?
}


