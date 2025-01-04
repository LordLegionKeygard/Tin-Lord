using UnityEngine;

public class WorldSaveGame : MonoBehaviour
{
    public WorldSaveLoad WorldSaveLoad;
    private string _selectedMissionId = "";

    [Header("Save Data Writer")]
    private WorldSaveGameDataWriter _worldGameSaveDataWriter;
    public WorldSaveGameDataWriter GetWorldGameSaveDataWriter() => _worldGameSaveDataWriter;

    [Header("CurrentWorldData")]
    public WorldSaveData CurrentWorldSaveData;

    [Header("Other")]
    [SerializeField] private ResourceSpritesInfo _resourceSpritesInfo;
    [SerializeField] private AllMissionsInfo _allMissionsInfo;

    private void Awake()
    {
        _worldGameSaveDataWriter = new WorldSaveGameDataWriter(Application.persistentDataPath);
    }

    public void ChangeSelectedMissionId(string newMissionId)
    {
        _selectedMissionId = newMissionId;
    }

    public void NewMission(Mission mission)
    {
        CurrentWorldSaveData = new WorldSaveData
        {
            IsStartMission = true,
            MissionId = mission.MissionId,
            GameSpeed = (int)GameSpeedEnum.Default,
            ResourcesData = new float[_resourceSpritesInfo.Sprites.Length],
        };

        for (int i = 0; i < mission.StartResources.Length; i++)
        {
            int resourceIndex = (int)mission.StartResources[i].ResourceEnum;
            CurrentWorldSaveData.ResourcesData[resourceIndex] = mission.StartResources[i].RecourceAmount;
        }

        _worldGameSaveDataWriter.WriteMissionDataToSaveFile(CurrentWorldSaveData, _selectedMissionId);
        Invoke(nameof(LoadMissionGameData), 2f);

        Debug.Log("SaveNewMission");
    }

    public void DeleteMissionGameData() //будет использоваться в случае проигрыша на миссии
    {
        _worldGameSaveDataWriter.DeleteMissionSaveFile(_selectedMissionId);
    }

    public void DeleteAllMissionsGameData()
    {
        _worldGameSaveDataWriter.DeleteAllMissionsSaveFiles(_allMissionsInfo.AllMissions.Length);
    }

    public void SaveMissionGameData()
    {
        _worldGameSaveDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        WorldSaveLoad.SaveMissionData(ref CurrentWorldSaveData);
        _worldGameSaveDataWriter.WriteMissionDataToSaveFile(CurrentWorldSaveData, _selectedMissionId);

        // Debug.Log("Save Mission");
    }

    public void ResetMissionGameData()
    {
        _worldGameSaveDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        WorldSaveLoad.ResetMissionData(ref CurrentWorldSaveData);
        _worldGameSaveDataWriter.WriteMissionDataToSaveFile(CurrentWorldSaveData, _selectedMissionId);
    }

    public void LoadMissionGameData()
    {
        _worldGameSaveDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        CurrentWorldSaveData = _worldGameSaveDataWriter.LoadMissionDataFromJson(_selectedMissionId);
        CustomEvents.FireLoadScene(SceneEnum.World, 5f, true);
    }
}
