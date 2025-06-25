using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesSpawner", menuName = "TinLord/Missions/EnemySpawner")]
public class EnemiesSpawner : ScriptableObject
{
    public Spawner[] Spawners;
    public int LastDaySpawn;

    [Header("Boss")]
    public EnemyBiomeInfo[] Bosses;
    public int BossLevel;
    public int BossDaySpawn;

    private void OnValidate()
    {
        int biomeCount = System.Enum.GetValues(typeof(MonsterBiome)).Length;
        if (Spawners == null) return;

        foreach (var sp in Spawners)
        {
            if (sp?.EnemiesSpawnerInfo == null) continue;

            foreach (var group in sp.EnemiesSpawnerInfo)
            {
                if (group?.EnemyBiomeInfo == null) continue;

                for (int i = 0; i < group.EnemyBiomeInfo.Length; i++)
                {
                    int biomeIndex = i % biomeCount;
                    group.EnemyBiomeInfo[i].Biome = (MonsterBiome)biomeIndex;
                }
            }
        }

        if (Bosses != null)
        {
            for (int i = 0; i < Bosses.Length; i++)
            {
                int biomeIndex = i % biomeCount;
                Bosses[i].Biome = (MonsterBiome)biomeIndex;
            }
        }
    }

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
    public EnemyBiomeInfo[] EnemyBiomeInfo;
    public int EnemyLevel;
}

[System.Serializable]
public class EnemyBiomeInfo
{
    public MonsterBiome Biome;
    public EnemyEnum EnemyEnum = EnemyEnum.None;
}

public enum MonsterBiome
{
    Desert = 0,
    Winter = 1,
}

