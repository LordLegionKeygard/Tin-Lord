using UnityEngine;

public class CurrentMissionInfo : MonoBehaviour
{
    public static CurrentMissionInfo Instance;
    [SerializeField] private AllMissionsInfo _allMissionsInfo;
    [SerializeField] private int _currentMissionId;

    private void Awake()
    {
        Instance = this;
    }

    public void LoadMission(int missionId) => _currentMissionId = missionId;

    public Mission GetCurrentMission()
    {
        return _allMissionsInfo.AllMissions[_currentMissionId];
    }

}
