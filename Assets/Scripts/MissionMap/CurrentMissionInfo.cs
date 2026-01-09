using UnityEngine;

public class CurrentMissionInfo : MonoBehaviour
{
    public static CurrentMissionInfo Instance;
    private MissionNode _currentMissionNode;
    [SerializeField] private int _missionId; // реальный id миссии по порядку, задан в испекторе миссий и передается
    private int _missionDeckIndex; // Содержит id миссии именно в текущем акте, для 2 акта первая миссия будет иметь id 0
    private int _act;
    public Landscape GetCurrentLandscape() => _currentMissionNode?.Landscape;
    public EnemiesSpawner GetEnemiesSpawnerInformation() => _currentMissionNode.EnemiesSpawner;
    public Objective GetObjective() => _currentMissionNode.Objective;
    public int GetMissionId() => _missionId;
    public int GetMissionDeckIndex() => _missionDeckIndex;
    public int GetAct() => _act;


    private void Awake()
    {
        Instance = this;
    }

    public void LoadMission(MissionNode node, int missionDeckIndex, int act)
    {
        _currentMissionNode = node;
        _missionId = node.MissionId;
        _missionDeckIndex = missionDeckIndex;
        _act = act;
    }
}
