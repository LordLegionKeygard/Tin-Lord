using UnityEngine;

[CreateAssetMenu(fileName = "AllMissions", menuName = "TinLord/Missions/AllMissions")]
public class AllMissionsInfo : ScriptableObject
{
    public MapChapter[] MapChapters;
}

[System.Serializable]
public class MapChapter
{
    public ChaptersEnum ChaptersEnum;
    public Landscape[] Landscapes; //случайные ландшафты
    public EnemiesSpawner[] EnemiesSpawnerInformation; //информация о врагах, пока что стоит друг за другом подряд
    public Objective[] Objectives; //информация о целях миссии, пока что стоит друг за другом подряд
}

[System.Serializable]
public enum ChaptersEnum
{
    Desert = 0,
}
