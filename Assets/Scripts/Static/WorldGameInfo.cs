using UnityEngine;

public class WorldGameInfo
{
    public static bool IsDemo = true;
    public static int LastAvailableDemoMissionId = 2;
    public static bool StaticBlood = true;
    public const float PausedTimeScale = 0.00001f;
    public const float DefaultTimeScale = 1.5f;
    public const float DoubleTimeScale = 2.2f;
    public const float TripleTimeScale = 3;
    public const float TickSpeed = 2;

    [Header("GoogleSheet")]
    public const string GoogleSheetId = "1phPv5iEL7Iw6A6CCdU4SdSgN5LwbEprCKEzY10dwDr4";
    public const string BuildingGridId = "1534982770";

    [Header("EnemyReachedDistance")]
    public const int EnemyReachedMachineDistance = 9;
    public const int EnemyReachedTileDistance = 9;
    public const int EnemyReachedFourTileDistance = 13;

    [Header("Turret")]
    public const float TurretMaximumAttackAngle = 10;
    public const float TurretMinimumAttackAngle = -10;

    [Header("Radiadtion")]
    public const int MaximumRadiation = 50;

    [Header("DayEvent")]
    public const int DayEventChance = 30;

    [Header("Machine")]
    public const int MachinesCount = 1;
    public const float MachineEngineerRepairBuildingsDistance = 5.5f;
    public const float MachineDieDelay = 10;
    public const float MachineDieDuration = 3;

    [Header("Bullets")]
    public const float BulletHeightOffset = 2.0f; // Смещение по высоте
    public const float BulletLateralOffset = -1.5f; // Горизонтальное смещение

    [Header("TurretPatrol")]
    public const float MinTurretPatrolTime = 5;
    public const float MaxTurretPatrolTime = 15;
    public const float TurretPatrolRotateSpeedFactor = 0.5f;

    [Header("Buildings")]
    public const float ConstructionSpeed = 5;
    public const float FirstBaseConstructionSpeed = 40;

    [Header("Load")]
    public const int LoadSceneTime = 2;
    public const int DefaultLoadingScreenSpriteId = -1;

    [Header("EndMissionFragmentsPercent")]
    public const int DefeatFragmentsPercent = 25;
    public const int EscapeFragmentsPercent = 50;
    public const int VictoryFragmentsPercent = 100;

    [Header("Sounds")]
    public const int EnemiesDeathSoundChance = 10;

    [Header("Terrain")]
    public const float TerrainOffset = 1.68f;

    [Header("VideoSettings")]
    public const int ScreenMode = 1;
    public const int Resolution = 17;
    public const int Quality = 1;
    public const int AntiAliasing = 1;
    public const int UpscalingFilter = 0;
    public const bool Glow = true;
    public const int FrameRate = 100;

    [Header("GameplaySettings")]
    public const float CameraSpeed = 20;
    public const bool Blood = true;

    [Header("AudioSettings")]
    public const float MasterVolume = 0.6f;
    public const float SfxVolume = 0.6f;
    public const float UiVolume = 0.6f;
    public const float MusicVolume = 0.6f;

    [Header("Damage")]
    public const float ConstructionExtraDamage = 3;
    public const float FortificationSkillDamage = 0.5f;

}
