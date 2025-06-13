using UnityEngine;

[CreateAssetMenu(fileName = "AllMissions", menuName = "TinLord/Missions/AllMissions")]
public class AllMissionsInfo : ScriptableObject
{
    [Header("Nodes")]
    public StartNode StartNode; // стартовая точка
    public EventNode[] Events; // все возможные ивенты
    public ModuleTraderNode[] ModuleTraders; // все торговцы модулями
    public SkillTraderNode[] SkillTraders; // все торговцы умениями
    public BossNode BossNode; // финальный босс

    [Header("Templates")]
    public MissionNode MissionNodeTemplate; // шаблон для получения иконки миссии
    public Material DefaultCosmos;
    

    [Header("Parts for MissionNode")]
    public Landscape[] Landscapes; // случайные ландшафты
    public EnemiesSpawner[] EnemiesSpawnerInformation; // информация о врагах
    public Objective[] Objectives; // информация о целях миссии

}
