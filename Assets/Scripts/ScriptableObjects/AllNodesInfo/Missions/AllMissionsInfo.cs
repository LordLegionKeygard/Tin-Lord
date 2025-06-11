using UnityEngine;

[CreateAssetMenu(fileName = "AllMissions", menuName = "TinLord/Missions/AllMissions")]
public class AllMissionsInfo : ScriptableObject
{
    [Header("Nodes")]
    public StartNode StartNode; // стартовая точка
    public EventNode[] Events; // все возможные ивенты
    public TraderNode[] Traders; // все возможные торговцы
    public BossNode BossNode; // финальный босс

    [Header("Templates")]
    public MissionNode MissionNodeTemplate; // шаблон для получения иконки
    

    [Header("Parts for MissionNode")]
    public Landscape[] Landscapes; // случайные ландшафты
    public EnemiesSpawner[] EnemiesSpawnerInformation; // информация о врагах
    public Objective[] Objectives; // информация о целях миссии

}
