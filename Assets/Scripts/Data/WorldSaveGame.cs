using UnityEngine;

public class WorldSaveGame : MonoBehaviour
{
    public WorldSaveLoad WorldSaveLoad;
    private string _selectedMissionId = "";

    [Header("Save Data Writer")]
    private WorldSaveGameDataWriter _worldGameSaveDataWriter;

    [Header("CurrentWorldData")]
    public WorldSaveData CurrentWorldSaveData;

    [Header("Other")]
    [SerializeField] private ResourceSpritesInfo _resourceSpritesInfo;

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
            MissionId = mission.MissionId,
            MissionName = mission.Name,
            Day = 0,
            StartEcology = mission.StartEcology,
            Radiation = 0,
            GameSpeed = 1,
            ObjectivesData = new ObjectiveDataWrapper[mission.Objectives.Length],
            ResourcesData = new int[_resourceSpritesInfo.Sprites.Length],
        };

        for (int i = 0; i < mission.Objectives.Length; i++)
        {
            CurrentWorldSaveData.ObjectivesData[i] = new ObjectiveDataWrapper
            {
                ObjectiveEnumNumber = (int)mission.Objectives[i].ObjectiveEnum,
                ObjectiveAmount = mission.Objectives[i].ObjectiveAmount,
            };
        }

        for (int i = 0; i < mission.StartResources.Length; i++)
        {
            int resourceIndex = (int)mission.StartResources[i].ResourceEnum;
            CurrentWorldSaveData.ResourcesData[resourceIndex] = mission.StartResources[i].RecourceAmount;
        }

        _worldGameSaveDataWriter.WriteMissionDataToSaveFile(CurrentWorldSaveData, _selectedMissionId);
        Invoke(nameof(LoadMissionGameData), 2f);

        Debug.Log("SaveNewMission");
    }

    public void DeleteCommandCenterGameData() //будет использоваться в случае проигрыша на мисии
    {
        _worldGameSaveDataWriter.DeleteSaveFile(_selectedMissionId);
    }

    public void SaveMissionGameData()
    {
        _worldGameSaveDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        WorldSaveLoad.SaveData(ref CurrentWorldSaveData);
        _worldGameSaveDataWriter.WriteMissionDataToSaveFile(CurrentWorldSaveData, _selectedMissionId);

        Debug.Log("Save Mission");
    }

    public void ResetMissionGameData()
    {
        _worldGameSaveDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        WorldSaveLoad.ResetData(ref CurrentWorldSaveData);
        _worldGameSaveDataWriter.WriteMissionDataToSaveFile(CurrentWorldSaveData, _selectedMissionId);
    }

    public void LoadMissionGameData()
    {
        _worldGameSaveDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        CurrentWorldSaveData = _worldGameSaveDataWriter.LoadMissionDataFromJson(_selectedMissionId);
        CustomEvents.FireLoadScene(SceneEnum.World, 5f, true);
    }
}
