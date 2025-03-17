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

    public int GetLastOpenedMissionId(MissionEndEnum missionEndEnum)
    {
        if (_currentMissionId == _lastOpenedMissionId && missionEndEnum == MissionEndEnum.Victory)
        {
            _lastOpenedMissionId++;
            return _lastOpenedMissionId;
        }
        return _lastOpenedMissionId;
    }

    public Mission GetCurrentMission()
    {
        return _allMissionsInfo.AllMissions[_currentMissionId];
    }

}
