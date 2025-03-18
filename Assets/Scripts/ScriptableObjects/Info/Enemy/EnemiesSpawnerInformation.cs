using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesSpawnerInformation", menuName = "TinLord/Info/EnemySpawner")]
public class EnemiesSpawnerInformation : ScriptableObject
{
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
    public int MinCount;
    public int MaxCount;
    public EnemySpawnerInfo[] EnemiesSpawnerInfo;

}

[System.Serializable]
public class EnemySpawnerInfo
{
    public EnemyEnum EnemyEnum = EnemyEnum.None;
    public int EnemyLevel;
}

