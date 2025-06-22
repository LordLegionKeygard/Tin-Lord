using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesSpawner", menuName = "TinLord/Missions/EnemySpawner")]
public class EnemiesSpawner : ScriptableObject
{
    public MonsterBiome Biome; 
    public Spawner[] Spawners;
    public int LastDaySpawn;

    [Header("Boss")]
    public EnemyEnum BossEnum = EnemyEnum.None;
    public int BossLevel;
    public int BossDaySpawn;
}

[System.Serializable]
public class Spawner
{
    public int StartDaySpawn;
    public int SpawnPeriod;
    public int Count;
    public EnemySpawnerInfo[] EnemiesSpawnerInfo;

}

[System.Serializable]
public class EnemySpawnerInfo
{
    public EnemyEnum EnemyEnum = EnemyEnum.None;
    public int EnemyLevel;
}

public enum MonsterBiome
{
    Desert = 0,
    Winter = 1,
}

