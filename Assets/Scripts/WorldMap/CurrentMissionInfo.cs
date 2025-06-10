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

    public void LoadMission()
    {
        
    }

    public Landscape GetCurrentLandscape()
    {
        return _allMissionsInfo.MapChapters[(int)ChaptersEnum.Desert].Landscapes[_currentMissionId]; 
    }

    public EnemiesSpawner GetEnemiesSpawnerInformation()
    {
        return null;
    }

    public Objective GetObjective()
    {
        return null;
    }

}
