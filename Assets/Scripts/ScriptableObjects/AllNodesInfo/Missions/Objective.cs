using UnityEngine;

[CreateAssetMenu(fileName = "Objective", menuName = "TinLord/Missions/Objective")]
public class Objective : ScriptableObject
{
    public ObjectiveWrapper[] Objectives;
}

[System.Serializable]
public class ObjectiveWrapper
{
    public ObjectiveEnum ObjectiveEnum;
    public int ObjectiveAmount;
}

[System.Serializable]
public enum ObjectiveEnum
{
    RestoreEcology = 0,
    KillEnemies = 1,
    ConstructBuilding = 2,
    SurviveDays = 3,
    KillBoss = 4,
}
