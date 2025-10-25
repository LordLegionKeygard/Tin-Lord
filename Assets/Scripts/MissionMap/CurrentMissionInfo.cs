using UnityEngine;

public class CurrentMissionInfo : MonoBehaviour
{
    public static CurrentMissionInfo Instance;
    private MissionNode _currentMissionNode;
    private int _missionDeckIndex;
    public Landscape GetCurrentLandscape() => _currentMissionNode?.Landscape;
    public EnemiesSpawner GetEnemiesSpawnerInformation() => _currentMissionNode.EnemiesSpawner;
    public Objective GetObjective() => _currentMissionNode.Objective;
    public int GetMissionDeckIndex() => _missionDeckIndex;


    private void Awake()
    {
        Instance = this;
    }

    public void LoadMission(MissionNode node, int missionDeckIndex)
    {
        _currentMissionNode = node;
        _missionDeckIndex = missionDeckIndex;
    }
}
