using System;
using UnityEngine;
using UnityEngine.Serialization;

public class MissionSaveGame : MonoBehaviour
{
    public MissionSaveLoad MissionSaveLoad;

    [Header("Save Data Writer")]
    private MissionSaveGameDataWriter _missionGameSaveDataWriter;
    public MissionSaveGameDataWriter GetWorldGameSaveDataWriter() => _missionGameSaveDataWriter;

    [Header("CurrentWorldData")]
    public MissionSaveData CurrentMissionSaveData;

    [Header("Other")]
    [SerializeField] private ActInfo _allMissionsInfo;

    private void Awake()
    {
        _missionGameSaveDataWriter = new MissionSaveGameDataWriter(Application.persistentDataPath);
    }

    public void NewMissionData(Landscape landscape, float[] startResources)
    {
        CurrentMissionSaveData = new MissionSaveData
        {
            IsStartMission = true,
            GameSpeed = (int)GameSpeedEnum.Default,
            ResourcesData = new float[Enum.GetValues(typeof(ResourceEnum)).Length - 1],
        };

        for (int i = 0; i < startResources.Length; i++)
        {
            CurrentMissionSaveData.ResourcesData[i] = startResources[i];
        }

        CurrentMissionSaveData.ResourcesData[(int)ResourceEnum.DataFragment] = 0;

        _missionGameSaveDataWriter.WriteMissionDataToSaveFile(CurrentMissionSaveData);
        LoadMissionFromJson();
        CustomEvents.FireLoadScene(SceneEnum.Mission, WorldGameInfo.LoadSceneTime, landscape.LoadingScreenSprite);
    }

    public void SaveMissionToJson()
    {
        _missionGameSaveDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        MissionSaveLoad.SaveMissionData(ref CurrentMissionSaveData);
        _missionGameSaveDataWriter.WriteMissionDataToSaveFile(CurrentMissionSaveData);
    }

    public void LoadMissionFromJson()
    {
        _missionGameSaveDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        CurrentMissionSaveData = _missionGameSaveDataWriter.LoadMissionDataFromJson();
    }

    public void ResetMissionJson()
    {
        DeleteMissionJson();
        _missionGameSaveDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        MissionSaveLoad.ResetMissionData(ref CurrentMissionSaveData);
        _missionGameSaveDataWriter.WriteMissionDataToSaveFile(CurrentMissionSaveData);
    }

    public void DeleteMissionJson()
    {
        _missionGameSaveDataWriter.DeleteMissionSaveFile();
    }
}
