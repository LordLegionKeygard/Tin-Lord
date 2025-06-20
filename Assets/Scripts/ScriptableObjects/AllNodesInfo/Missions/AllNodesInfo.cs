using UnityEngine;

[CreateAssetMenu(fileName = "AllNodesInfo", menuName = "TinLord/Missions/AllNodesInfo")]
public class AllNodesInfo : ScriptableObject
{
    [Header("Nodes")]
    public StartNode StartNode; // стартовая точка
    public EventPool[] EventPools; // все возможные ивенты
    public ResourceTraderNode[] ResourceTraders; // все торговцы ресурсами
    public SkillTraderNode[] SkillTraders; // все торговцы ресурсами
    public BossNode BossNode; // финальный босс

    [Header("Templates")]
    public MissionNode MissionNodeTemplate; // шаблон для получения иконки миссии


    [Header("Parts for MissionNode")]
    public Landscape[] Landscapes; // случайные ландшафты
    public EnemiesSpawner[] EnemiesSpawnerInformation; // информация о врагах
    public Objective[] Objectives; // информация о целях миссии

}

[System.Serializable]
public class EventPool
{
    public NodeData Node;
    [Min(1)] public int MaxOnMap = 1;
    public bool RepeatOneEventSameTime = false;
}
