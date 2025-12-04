using UnityEngine;

public class WorldGameInfo
{
    [Header("Build")]
    public static bool IsSteam = false;
    public static bool IsDemo = false;

    [Header("Ai Performance")]
    public static float TargetScanInterval = 0.5f;
    public static int MaxReachabilityChecks = 6;

    [Header("Cards")]
    public static int ChanceAddOneCard = 50;
    public static float TacticCardIncreaseDamageFactor = 0.25f;
    public static float TacticCardIncreaseHealthFactor = 0.2f;
    public static int AddTacticCardChance = 25;
    public static int TacticCardChangeSuccessRarityChance = 55;

    [Header("Price")]
    public static int StartWeaponEnigneerUpgradePrice = 5;
    public static int FactorWeaponEnigneerUpgradePrice = 5;

    [Header("Currency")]
    public static float BeamEnergyAfterSetNewTile = 0.5f;
    public static float QuantDropChance = 0.1f;
    public static int MaxAiCores = 12;

    [Header("Skills")]
    public const int SkillsCount = 15;

    [Header("Time")]
    public const int OneDayTicksCount = 24;
    public const float PausedTimeScale = 0.00001f;
    public const float DefaultTimeScale = 1.5f;
    public const float DoubleTimeScale = 2.2f;
    public const float TripleTimeScale = 3;
    public const float TickSpeed = 2;

    [Header("Enemies")]
    public const float MiniBossScale = 2;

    [Header("GoogleSheet")]
    public const string GoogleSheetId = "1phPv5iEL7Iw6A6CCdU4SdSgN5LwbEprCKEzY10dwDr4";
    public const string BuildingGridId = "1534982770";

    [Header("EnemyReachedDistance")]
    public const int EnemyReachedMachineDistance = 9;
    public const int EnemyReachedTileDistance = 9;
    public const int EnemyReachedFourTileDistance = 15;

    [Header("Turret")]
    public const float TurretMaximumAttackAngle = 10;
    public const float TurretMinimumAttackAngle = -10;

    [Header("CityRobots")]
    public const float CityRobotMaximumAttackAngle = 10;
    public const float CityRobotMinimumAttackAngle = -10;

    [Header("Radiadtion")]
    public const int MaximumRadiation = 50;

    [Header("DayEvent")]
    public const int DayEventChance = 30;
    public const int OilReleaseChance = 30;
    public const int EatchQuakeChance = 60;

    [Header("Duration")]
    public const int AcidRainTicks = 24;
    public const int ToxicGasTicks = 48;

    [Header("Machine")]
    public const int MachineExperienceFromTick = 1;
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
    public const float DestroyConstructionBuildingResourcePercent = 0.5f;
    public const float ConstructionSpeed = 2;
    public const float FirstBaseConstructionSpeed = 50;
    public const float TutorialBaseConstructionSpeed = 10;

    [Header("Load")]
    public const int LoadSceneTime = 2;

    [Header("EndMissionFragmentsPercent")]
    public const int DefeatFragmentsPercent = 10;
    public const int EscapeFragmentsPercent = 60;
    public const int VictoryFragmentsPercent = 100;

    [Header("Sounds")]
    public const int EnemiesDeathSoundChance = 20;

    [Header("Positions")]
    public const float TerrainPosition = 1.68f;
    public const float EnvironmentPosition = 0;

    [Header("Settings")]
    public static bool StaticBlood = true;
    public static int LanguageLength = 1100;

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
    public const float ExplosionDamageFactor = 4;
    public const float AcidRainTriggerStayDamageFactor = 0.25f;
    public const float ToxicGasTriggerStayDamageFactor = 0.15f;

    [Header("Hangar")]
    public const int HangarRobotsCount = 3;
    public const int HangarCratesCount = 3;
    public const int HangarSkillsCount = 4;
    public const int HangarShipWeaponsCount = 10;

    public const int PatchPassiveAbility = 10;
    public const int TitanPassiveAbility = 20;
    public const int AimBotPassiveAbility = 20;

    [Header("Tooltip Pivot")]
    public const float NodePivot = 2.5f;
    public const float ButtonPivot = -0.9f;
    public const float ResourcePivot = -0.5f;
    public const float BuildinTypePivot = -0.9f;

    [Header("Rewards")]
    public const int AiCoreLow = 1;
    public const int AiCoreMedium = 2;
    public const int QuantsLowMin = 10;
    public const int QuantsLowMax = 30;
    public const int QuantsMediumMin = 30;
    public const int QuantsMediumMax = 50;
    public const int QuantsHightMin = 50;
    public const int QuantsHightMax = 100;
    public const int MemoryLowMin = 10;
    public const int MemoryLowMax = 30;
    public const int MemoryMediumMin = 30;
    public const int MemoryMediumMax = 50;
    public const int MemoryHightMin = 50;
    public const int MemoryHightMax = 100;
    public const int ResourceLowMin = 5;
    public const int ResourceLowMax = 10;
    public const int ResourceMediumMin = 10;
    public const int ResourceMediumMax = 15;
    public const int ResourceHightMin = 15;
    public const int ResourceHightMax = 20;
    public const int MaterialLowMin = 2;
    public const int MaterialLowMax = 5;
    public const int MaterialMediumMin = 5;
    public const int MaterialMediumMax = 10;
    public const int MaterialHightMin = 10;
    public const int MaterialHightMax = 15;
}
