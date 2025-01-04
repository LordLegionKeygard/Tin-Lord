using UnityEngine;

public class CommandCenterSaveGame : MonoBehaviour
{
    public CommandCenterSaveLoad CommandCenterSaveLoad;

    [Header("SaveDataWriter")]
    private CommandCenterSaveGameDataWriter _commandCenterSaveGameDataWriter;
    public CommandCenterSaveGameDataWriter GetCommandCenterSaveGameDataWriter() => _commandCenterSaveGameDataWriter;

    [Header("CurrentCommandCenterData")]
    public CommandCenterSaveData CommandCenterSaveData;

    [Header("Other")]
    [SerializeField] private ConfigLoaderBuildings _configLoaderBuildings;


    private void Awake()
    {
        _commandCenterSaveGameDataWriter = new CommandCenterSaveGameDataWriter(Application.persistentDataPath);
    }

    public void NewGame()
    {
        CommandCenterSaveData = new CommandCenterSaveData
        {
            MemoryFragments = 0,
            LastOpenedMissionId = 0,
            BuildingsLearned = new bool[_configLoaderBuildings.AllBuidingsCount()],
        };

        CommandCenterSaveData.BuildingsLearned[0] = true; // Shelter
        CommandCenterSaveData.BuildingsLearned[12] = true; // CoalMining
        CommandCenterSaveData.BuildingsLearned[16] = true; // OreManualMining
        CommandCenterSaveData.BuildingsLearned[20] = true; // WoodManualMining
        CommandCenterSaveData.BuildingsLearned[32] = true; // StoneManualMining

        _commandCenterSaveGameDataWriter.WriteCommandCenterDataToSaveFile(CommandCenterSaveData);

        Invoke(nameof(LoadGameData), 2f);
        
        // Debug.Log("SaveNewGame");
    }

    // public void DeleteCommandCenterGameData() //не используется так как мы перезаписываем данные
    // {
    //     _commandCenterSaveGameDataWriter.DeleteSaveFile();
    // }

    public void SaveGameData()
    {
        _commandCenterSaveGameDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        CommandCenterSaveLoad.SaveData(ref CommandCenterSaveData);
        _commandCenterSaveGameDataWriter.WriteCommandCenterDataToSaveFile(CommandCenterSaveData);

        // Debug.Log("Save CommandCenter");
    }

    public void LoadGameData()
    {
        _commandCenterSaveGameDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        CommandCenterSaveData = _commandCenterSaveGameDataWriter.LoadCommandCenterDataFromJson();
        CustomEvents.FireLoadScene(SceneEnum.CommandCenter, 5f, true);
    }
}
