using UnityEngine;

public class CurrentMissionInfo : MonoBehaviour
{
    public static CurrentMissionInfo Instance;
    private MissionNode _currentMissionNode;

    private void Awake()
    {
        Instance = this;
    }

    public void LoadMission(MissionNode node)
    {
        _currentMissionNode = node;
    }

    public Landscape GetCurrentLandscape()
    {
        return _currentMissionNode.Landscape;
    }

    public EnemiesSpawner GetEnemiesSpawnerInformation()
    {
        return _currentMissionNode.EnemiesSpawner;
    }

    public Objective GetObjective()
    {
        return _currentMissionNode.Objective;
    }

}
