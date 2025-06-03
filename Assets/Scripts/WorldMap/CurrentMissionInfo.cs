using UnityEngine;

public class CurrentMissionInfo : MonoBehaviour
{
    public static CurrentMissionInfo Instance;
    [SerializeField] private AllMissionsInfo _allMissionsInfo;
    [SerializeField] private int _currentMissionId;
    [SerializeField] private int _lastOpenedMissionId;

    private void Awake()
    {
        Instance = this;
    }

    public int LastMissionRemainderFromSubtraction() => _lastOpenedMissionId - _currentMissionId;

    public void LoadMission(int missionId, int lastOpenedMissionId)
    {
        _currentMissionId = missionId;
        _lastOpenedMissionId = lastOpenedMissionId;
    }

    public bool IsOpenNewMission(MissionEndEnum missionEndEnum)
    {
        if (IsLastOpenedMission() && missionEndEnum == MissionEndEnum.Victory)
        {
            return true;
        }
        return false;
    }

    public int GetLastOpenedMissionId() => _lastOpenedMissionId;

    public bool IsLastOpenedMission() => _currentMissionId == _lastOpenedMissionId;

    public Mission GetCurrentMission()
    {
        return _allMissionsInfo.AllMissions[_currentMissionId];
    }

}
