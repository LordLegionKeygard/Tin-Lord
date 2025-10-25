using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesSpawner", menuName = "TinLord/Missions/EnemySpawner")]
public class EnemiesSpawner : ScriptableObject
{
    public Spawner[] Spawners;
    public MiniBossSpawner[] MiniBossSpawners;
    public BossSpawner BossSpawner;
}

[System.Serializable]
public class Spawner
{
    public int StartDaySpawn;
    public int SpawnPeriod;
    public int Count;
    public LandscapeSpawnSide[] LandscapeSpawnSide;
    public EnemySpawnerInfo[] EnemiesSpawnerInfo;
}

[System.Serializable]
public class EnemySpawnerInfo
{
    public EnemyBiomeInfo[] EnemyBiomeInfo;
    public int EnemyLevel;
}

[System.Serializable]
public class EnemyBiomeInfo
{
    public MonsterType MonsterType;
    public EnemyEnum EnemyEnum = EnemyEnum.None;
}

public enum MonsterType
{
    Desert = 0,
    Winter = 1,
    Robots = 2,
    Acid = 3,
    Stones = 4,
}

[System.Serializable]
public class MiniBossSpawner
{
    public int DaySpawn;
    public int Count;
    public int HealthFactor;
    public int DamageFactor;
    public LandscapeSpawnSide[] LandscapeSpawnSide;
    public EnemySpawnerInfo EnemySpawnerInfo;
}

[System.Serializable]
public class BossSpawner
{
    public EnemyBiomeInfo[] Bosses;
    public int BossLevel;
    public int BossDaySpawn;
    public LandscapeSpawnSide[] LandscapeSpawnSide;
}

[System.Serializable]
public class LandscapeSpawnSide
{
    public LandscapeEnum LandscapeEnum;
    public SpawnSide SpawnSide;
}

[System.Serializable]
public enum SpawnSide
{
    RandomSide = -1,
    Side_0 = 0,
    Side_1 = 1,
    Side_2 = 2,
    Side_3 = 3,
}



