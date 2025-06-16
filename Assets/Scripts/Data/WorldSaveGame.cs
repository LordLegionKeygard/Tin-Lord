using System;
using UnityEngine;

public class WorldSaveGame : MonoBehaviour
{
    public WorldSaveLoad WorldSaveLoad;

    [Header("Save Data Writer")]
    private WorldSaveGameDataWriter _worldGameSaveDataWriter;
    public WorldSaveGameDataWriter GetWorldGameSaveDataWriter() => _worldGameSaveDataWriter;

    [Header("CurrentWorldData")]
    public WorldSaveData CurrentWorldSaveData;

    [Header("Other")]
    [SerializeField] private AllNodesInfo _allMissionsInfo;

    private void Awake()
    {
        _worldGameSaveDataWriter = new WorldSaveGameDataWriter(Application.persistentDataPath);
    }

    public void NewMission(Landscape landscape)
    {
        CurrentWorldSaveData = new WorldSaveData
        {
            IsStartMission = true,
            GameSpeed = (int)GameSpeedEnum.Default,
            ResourcesData = new float[Enum.GetValues(typeof(ResourceEnum)).Length - 1],
        };

        for (int i = 0; i < landscape.StartResources.Length; i++)
        {
            int resourceIndex = (int)landscape.StartResources[i].ResourceEnum;
            CurrentWorldSaveData.ResourcesData[resourceIndex] = landscape.StartResources[i].RecourceAmount;
        }

        _worldGameSaveDataWriter.WriteMissionDataToSaveFile(CurrentWorldSaveData);
        LoadMissionGameData(landscape.LoadingScreenSprite);
    }

    public void DeleteMissionGameData()
    {
        _worldGameSaveDataWriter.DeleteMissionSaveFile();
    }

    public void SaveMissionGameData(bool loadCommandCenter)
    {
        _worldGameSaveDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        WorldSaveLoad.SaveMissionData(ref CurrentWorldSaveData);
        _worldGameSaveDataWriter.WriteMissionDataToSaveFile(CurrentWorldSaveData);
        if (loadCommandCenter) CustomEvents.FireLoadScene(SceneEnum.CommandCenter, WorldGameInfo.LoadSceneTime, true, null);
    }

    public void ResetMissionGameData()
    {
        DeleteMissionGameData();
        _worldGameSaveDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        WorldSaveLoad.ResetMissionData(ref CurrentWorldSaveData);
        _worldGameSaveDataWriter.WriteMissionDataToSaveFile(CurrentWorldSaveData);
        CustomEvents.FireLoadScene(SceneEnum.World, WorldGameInfo.LoadSceneTime, true, CurrentMissionInfo.Instance.GetCurrentLandscape().LoadingScreenSprite);
    }

    public void LoadMissionGameData(Sprite sprite)
    {
        _worldGameSaveDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        CurrentWorldSaveData = _worldGameSaveDataWriter.LoadMissionDataFromJson();
        CustomEvents.FireLoadScene(SceneEnum.World, WorldGameInfo.LoadSceneTime, true, sprite);
    }
}
