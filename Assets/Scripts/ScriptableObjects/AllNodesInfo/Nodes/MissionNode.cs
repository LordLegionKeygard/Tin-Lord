using UnityEngine;

[CreateAssetMenu(fileName = "MissionNode", menuName = "TinLord/Nodes/MissionNode")]
public class MissionNode : NodeData
{
    public int MissionId;
    public Landscape Landscape;
    public Objective Objective;
    public EnemiesSpawner EnemiesSpawner;
}
