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
    public const int EnemyReachedFourTileDistance = 12;

    //Turret
    public const float TurretMaximumAttackAngle = 10;
    public const float TurretMinimumAttackAngle = -10;

    //Radiadtion
    public const int MaximumRadiation = 50;

    //DayEvent
    public const int DayEventChance = 20;

    //Robot
    public const float RobotEngineerRepairBuildingsDistance = 5.5f;
    public const float RobotDieDelay = 10;
    public const float RobotDieDuration = 3;
}
