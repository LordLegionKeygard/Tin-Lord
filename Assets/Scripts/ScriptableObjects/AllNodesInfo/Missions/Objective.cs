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
