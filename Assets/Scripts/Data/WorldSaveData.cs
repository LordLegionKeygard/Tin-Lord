using Unity.VisualScripting;
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
    public int Tick;
    public int Radiation;
    public int GameSpeed; //Pause = 0, Default = 1, Double = 2, Triple = 3,

    [Header("Resources")]
    public float[] ResourcesData;

    [Header("Cards")]
    public int[] Cards;

    [Header("DayEvents")]
    public DayEventData[] DayEventsData;

    [Header("Enemies")]
    public EnemyData[] EnemyData;

    [Header("Tiles")]
    public bool IsHaveRiver;
    public bool IsHaveBase;
    public bool IsHaveMachineProduction;
    public TileDataWrapper[] TilesData;
    public int[] RoadTilesId;

    [Header("Machines")]
    public MachinesExperienceData[] MachinesExperienceData; //Tank = 0, Sniper = 1, Engineer = 2
    public MachineData MachineData;

    [Header("Objectives")]
    public int[] ObjectiveAmount;
}


[System.Serializable]
public class EnemyData
{
    public int EnemyEnum;
    public float PositionX;
    public float PositionY;
    public float PositionZ;
    public float Rotation;
    public int EnemyLevel;
    public float EnemyHealth;
}


[System.Serializable]
public class DayEventData
{
    public int GameEventTypeNumber;
    public float AlreadyElapsedTime;
}

[System.Serializable]
public class MachineData
{
    public bool IsHaveMachineNow;
    public int MachineType;
    public float PositionX;
    public float PositionY;
    public float PositionZ;
    public float Rotation;
    public int NextPatrolIndex;
    public float MachineHealth;
}

[System.Serializable]
public class MachinesExperienceData
{
    public int Level;
    public int Experience;
}

[System.Serializable]
public class TileDataWrapper
{
    public GroundData GroundData;
    public BuildingData BuildingData;
    public WaterData WaterData;
}

[System.Serializable]
public class GroundData
{
    public int GroundTileId;
    public float GroundTileRotation;
    public float GroundModelRotation;
    public bool IsForwardRoad;
    public int RiftViewNumber;
}

[System.Serializable]
public class BuildingData
{
    public int BuildingTileTypeId;
    public int BuildingTileLevel;
    public float BuildingHealth;
    public bool IsBuildingWork;
    public float BuildingTilePositionX;
    public float BuildingTilePositionY;
    public float BuildingTilePositionZ;
    public float BuildingRotation;
    public int RequiredResource;
    public float RequiredResourceAmount;
    public int ResourceProduction;
    public bool IsConstructionNow;
    public bool IsUpgradeBase;
    public float PreviousBaseBuildingHealth;
}

[System.Serializable]
public class WaterData
{
    public bool IsLake;
    public int RiverNumber;
    public bool IsBridge;
    public bool IsLastRiverTile;
    public int RiverType;
    public int RiverRotation;
}


