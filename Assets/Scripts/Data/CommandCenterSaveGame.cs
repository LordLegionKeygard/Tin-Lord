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
            NewGame = true,
            BuildingsLearned = new bool[_configLoaderBuildings.AllBuidingsCount()],
        };

        CommandCenterSaveData.BuildingsLearned[0] = true; // Shelter
        CommandCenterSaveData.BuildingsLearned[20] = true; // WoodManualMining
        CommandCenterSaveData.BuildingsLearned[32] = true; // StoneManualMining
        CommandCenterSaveData.BuildingsLearned[75] = true; // Ballista

        _commandCenterSaveGameDataWriter.WriteCommandCenterDataToSaveFile(CommandCenterSaveData);

        LoadGameData();
    }

    public void SaveGameData(bool loadMainMenu)
    {
        _commandCenterSaveGameDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        CommandCenterSaveLoad.SaveData(ref CommandCenterSaveData);
        _commandCenterSaveGameDataWriter.WriteCommandCenterDataToSaveFile(CommandCenterSaveData);
        if (loadMainMenu) CustomEvents.FireLoadScene(SceneEnum.MainMenu, WorldGameInfo.LoadSceneTime, true, WorldGameInfo.DefaultLoadingScreenSpriteId);
    }

    public void LoadGameData()
    {
        _commandCenterSaveGameDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        CommandCenterSaveData = _commandCenterSaveGameDataWriter.LoadCommandCenterDataFromJson();
        CustomEvents.FireLoadScene(SceneEnum.CommandCenter, WorldGameInfo.LoadSceneTime, true, WorldGameInfo.DefaultLoadingScreenSpriteId);
    }

    public void SaveCommandCenterWorldData(int memoryFragments, int lastOpenedMissionId)
    {
        _commandCenterSaveGameDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        CommandCenterSaveData.MemoryFragments += memoryFragments;
        CommandCenterSaveData.LastOpenedMissionId = lastOpenedMissionId;
        _commandCenterSaveGameDataWriter.WriteCommandCenterDataToSaveFile(CommandCenterSaveData);
    }
}
