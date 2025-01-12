using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesSpawnerInformation", menuName = "TinLord/Info/EnemySpawner")]
public class EnemiesSpawnerInformation : ScriptableObject
{
    public Spawner[] Spawners;
}

[System.Serializable]
public class Spawner
{
    public int DaySpawn;
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

