using UnityEngine;

[CreateAssetMenu(fileName = "MissionDefinition", menuName = "TinLord/Missions/Definition")]
public class MissionDefinition : ScriptableObject
{
    public int MissionId;
    public EnemiesSpawner Spawner;
    public ObjectiveSet[] ObjectiveSets;
}

[System.Serializable]
public class ObjectiveRange
{
    public ObjectiveEnum ObjectiveEnum;
    public int[] Values;
}

[System.Serializable]
public class ObjectiveSet
{
    public ObjectiveRange[] Objectives;
}

[System.Serializable]
public class BiomeSpawner
{
    public MonsterType Biome;
    public EnemiesSpawner Spawner;
}

