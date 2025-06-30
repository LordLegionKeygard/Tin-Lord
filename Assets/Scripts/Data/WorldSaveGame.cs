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

    public void NewMissionData(Landscape landscape, float[] startResources)
    {
        CurrentWorldSaveData = new WorldSaveData
        {
            IsStartMission = true,
            GameSpeed = (int)GameSpeedEnum.Default,
            ResourcesData = new float[Enum.GetValues(typeof(ResourceEnum)).Length - 1],
        };

        for (int i = 0; i < startResources.Length; i++)
        {
            CurrentWorldSaveData.ResourcesData[i] = startResources[i];
        }

        CurrentWorldSaveData.ResourcesData[(int)ResourceEnum.DataFragment] = 0;

        _worldGameSaveDataWriter.WriteMissionDataToSaveFile(CurrentWorldSaveData);
        LoadMissionFromJson();
        CustomEvents.FireLoadScene(SceneEnum.World, WorldGameInfo.LoadSceneTime, landscape.LoadingScreenSprite);
    }

    public void SaveMissionToJson()
    {
        _worldGameSaveDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        WorldSaveLoad.SaveMissionData(ref CurrentWorldSaveData);
        _worldGameSaveDataWriter.WriteMissionDataToSaveFile(CurrentWorldSaveData);
    }

    public void LoadMissionFromJson()
    {
        _worldGameSaveDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        CurrentWorldSaveData = _worldGameSaveDataWriter.LoadMissionDataFromJson();
    }

    public void ResetMissionJson()
    {
        DeleteMissionJson();
        _worldGameSaveDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        WorldSaveLoad.ResetMissionData(ref CurrentWorldSaveData);
        _worldGameSaveDataWriter.WriteMissionDataToSaveFile(CurrentWorldSaveData);
    }

    public void DeleteMissionJson()
    {
        _worldGameSaveDataWriter.DeleteMissionSaveFile();
    }
}
