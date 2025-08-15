using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Хранит дату о миссии
/// </summary>
/// 
[System.Serializable]
public class MissionSaveData
{
    [Header("Main")]
    public bool IsStartMission;

    [Header("UpPanel")]
    public int Day;
    public int Tick;
    public int Radiation;
    public int[] EveryDayEcology;
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
    public int BaseLevel;
    public bool IsHaveMachineProduction;
    public TileDataWrapper[] TilesData;
    public int[] RoadTilesId;

    [Header("Machines")]
    public MachineData MachineData;

    [Header("Objectives")]
    public int[] ObjectiveAmount;

    [Header("ShipCannons")]
    public MissionShipWeaponsData ShipCannonsData;

    [Header("Skills")]
    public int[] SkillsCooldown;
    public int[] SkillsDuration;

    [Header("Quants")]
    public float QuantsAmount;
    public QuantPickupData[] QuantPickups;

    [Header("Hazards")]
    public HazardSaveData[] Hazards;
}

[System.Serializable]
public class MissionShipWeaponsData
{
    public bool IsWeaponMode;
    public int LeftWeaponBulletsCount;
    public int RightWeaponBulletsCount;
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
    public int HealthFactor;
    public int DamageFactor;
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
public class TileDataWrapper
{
    public GroundData GroundData;
    public BuildingData BuildingData;
    public WaterData WaterData;
    public TileWorldEventData TileWorldEventData;
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
    public bool IsGeneralRepairSelect;
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

[System.Serializable]
public class TileWorldEventData
{
    public int ToxicGasTicksNumber;
}

[System.Serializable]
public class QuantPickupData
{
    public float PosX;
    public float PosY;
    public float PosZ;
    public float TimeLeft;
}

[System.Serializable]
public struct HazardSaveData
{
    public int HazardType;
    public float PosX;
    public float PosY;
    public float PosZ;
    public float RotationY;
    public float TimeLeft;
    public float DamageFactor;
}


