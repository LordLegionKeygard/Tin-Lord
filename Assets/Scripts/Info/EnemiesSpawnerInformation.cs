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
    public GameObject[] Enemies;

}
