using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesSpawner", menuName = "TinLord/Missions/EnemySpawner")]
public class EnemiesSpawner : ScriptableObject
{
    public Spawner[] Spawners;
    public MiniBossSpawner[] MiniBossSpawners;
    public BossSpawner BossSpawner;


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

        foreach (var item in MiniBossSpawners)
        {
            if (item?.EnemySpawnerInfo == null) continue;

            var group = item.EnemySpawnerInfo;
            for (int i = 0; i < group.EnemyBiomeInfo.Length; i++)
            {
                int biomeIndex = i % biomeCount;
                group.EnemyBiomeInfo[i].Biome = (MonsterBiome)biomeIndex;
            }
        }

        if (BossSpawner.Bosses != null)
        {
            for (int i = 0; i < BossSpawner.Bosses.Length; i++)
            {
                int biomeIndex = i % biomeCount;
                BossSpawner.Bosses[i].Biome = (MonsterBiome)biomeIndex;
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
    public MonsterBiome Biome;
    public EnemyEnum EnemyEnum = EnemyEnum.None;
}

public enum MonsterBiome
{
    Desert = 0,
    Winter = 1,
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



