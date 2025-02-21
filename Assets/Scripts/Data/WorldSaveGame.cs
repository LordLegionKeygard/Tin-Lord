using System;
using UnityEngine;

public class WorldSaveGame : MonoBehaviour
{
    public WorldSaveLoad WorldSaveLoad;
    [SerializeField] private string _selectedMissionId = "";

    [Header("Save Data Writer")]
    private WorldSaveGameDataWriter _worldGameSaveDataWriter;
    public WorldSaveGameDataWriter GetWorldGameSaveDataWriter() => _worldGameSaveDataWriter;

    [Header("CurrentWorldData")]
    public WorldSaveData CurrentWorldSaveData;

    [Header("Other")]
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
            ResourcesData = new float[Enum.GetValues(typeof(ResourceEnum)).Length - 1],
        };

        for (int i = 0; i < mission.StartResources.Length; i++)
        {
            int resourceIndex = (int)mission.StartResources[i].ResourceEnum;
            CurrentWorldSaveData.ResourcesData[resourceIndex] = mission.StartResources[i].RecourceAmount;
        }

        _worldGameSaveDataWriter.WriteMissionDataToSaveFile(CurrentWorldSaveData, _selectedMissionId);
        LoadMissionGameData(mission.MissionId);
    }

    public void DeleteMissionGameData()
    {
        _worldGameSaveDataWriter.DeleteMissionSaveFile(_selectedMissionId);
    }

    public void DeleteAllMissionsGameData()
    {
        _worldGameSaveDataWriter.DeleteAllMissionsSaveFiles(_allMissionsInfo.AllMissions.Length);
    }

    public void SaveMissionGameData(bool loadCommandCenter)
    {
        _worldGameSaveDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        WorldSaveLoad.SaveMissionData(ref CurrentWorldSaveData);
        _worldGameSaveDataWriter.WriteMissionDataToSaveFile(CurrentWorldSaveData, _selectedMissionId);
        if (loadCommandCenter) CustomEvents.FireLoadScene(SceneEnum.CommandCenter, WorldGameInfo.LoadSceneTime, true, WorldGameInfo.DefaultLoadingScreenSpriteId);
    }

    public void ResetMissionGameData(int missionId)
    {
        DeleteMissionGameData();
        _worldGameSaveDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        WorldSaveLoad.ResetMissionData(ref CurrentWorldSaveData);
        _worldGameSaveDataWriter.WriteMissionDataToSaveFile(CurrentWorldSaveData, _selectedMissionId);
        CustomEvents.FireLoadScene(SceneEnum.World, WorldGameInfo.LoadSceneTime, true, missionId);
    }

    public void LoadMissionGameData(int missionId)
    {
        _worldGameSaveDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        CurrentWorldSaveData = _worldGameSaveDataWriter.LoadMissionDataFromJson(_selectedMissionId);
        CustomEvents.FireLoadScene(SceneEnum.World, WorldGameInfo.LoadSceneTime, true, missionId);
    }
}
