public class WorldGameInfo
{
    public static bool StaticBlood = true;
    public const float PausedTimeScale = 0.00001f;

    //GoogleSheet
    public const string GoogleSheetId = "1phPv5iEL7Iw6A6CCdU4SdSgN5LwbEprCKEzY10dwDr4";
    public const string BuildingGridId = "1534982770";

    //EnemyReachedDistance
    public const int EnemyReachedRobotDistance = 5;
    public const int EnemyReachedTileDistance = 8;
    public const int EnemyReachedFourTileDistance = 13;

    //Turret
    public const float TurretMaximumAttackAngle = 10;
    public const float TurretMinimumAttackAngle = -10;

    //Radiadtion
    public const int MaximumRadiation = 50;

    //DayEvent
    public const int DayEventChance = 20;

    //Robot
    public const int RobotsCount = 3;
    public const float RobotEngineerRepairBuildingsDistance = 5.5f;
    public const float RobotDieDelay = 10;
    public const float RobotDieDuration = 3;

    //Bullets
    public const float BulletHeightOffset = 2.0f; // Смещение по высоте
    public const float BulletLateralOffset = -1.5f; // Горизонтальное смещение

    //TurretPatrol
    public const float MinTurretPatrolTime = 5;
    public const float MaxTurretPatrolTime = 15;
    public const float TurretPatrolRotateSpeedFactor = 0.5f;

    //Buildings
    public const float ConstructionSpeed = 20;

    //Load
    public const int LoadSceneTime = 2;

    //EndMissionFragmentsPercent
    public const int DefeatFragmentsPercent = 10;
    public const int EscapeFragmentsPercent = 50;
    public const int VictoryFragmentsPercent = 100;

    //Sounds
    public const int EnemiesDeathSoundChance = 10;

    //Terrain
    public const float TerrainOffset = 1.68f;

}
