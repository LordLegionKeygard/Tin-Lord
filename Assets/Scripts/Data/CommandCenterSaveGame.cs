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
            Quants = 35,
            AiCores = 6,
            MainResourcesData = new float[WorldGameInfo.ResourcesCount],
            PrologueCompleted = false,
            TutorialCompleted = false,
            BuildingsLearned = new bool[_configLoaderBuildings.AllBuidingsCount()],
            OpenedSkills = new bool[WorldGameInfo.SkillsCount],
        };

        CommandCenterSaveData.BuildingsLearned[0] = true; // Shelter
        CommandCenterSaveData.BuildingsLearned[20] = true; // WoodManualMining
        CommandCenterSaveData.BuildingsLearned[32] = true; // StoneManualMining
        CommandCenterSaveData.BuildingsLearned[75] = true; // Ballista

        CommandCenterSaveData.MainResourcesData[(int)ResourceEnum.Wood] = 100;
        CommandCenterSaveData.MainResourcesData[(int)ResourceEnum.Stone] = 50;

        CommandCenterSaveData.OpenedSkills[0] = true;

        _commandCenterSaveGameDataWriter.WriteCommandCenterDataToSaveFile(CommandCenterSaveData);

        LoadGameData();
    }

    public void SaveGameData(bool loadMainMenu)
    {
        _commandCenterSaveGameDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        CommandCenterSaveLoad.SaveData(ref CommandCenterSaveData);
        _commandCenterSaveGameDataWriter.WriteCommandCenterDataToSaveFile(CommandCenterSaveData);
        if (loadMainMenu) CustomEvents.FireLoadScene(SceneEnum.MainMenu, WorldGameInfo.LoadSceneTime, true, null);
    }

    public void LoadGameData()
    {
        _commandCenterSaveGameDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        CommandCenterSaveData = _commandCenterSaveGameDataWriter.LoadCommandCenterDataFromJson();
        CustomEvents.FireLoadScene(SceneEnum.CommandCenter, WorldGameInfo.LoadSceneTime, true, null);
    }

    public void SaveEndMissionData(int memoryFragments, int aiCores, int quants)
    {
        _commandCenterSaveGameDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        CommandCenterSaveData.MainResourcesData[(int)ResourceEnum.DataFragment] += memoryFragments;
        CommandCenterSaveData.AiCores += aiCores;
        CommandCenterSaveData.Quants += quants;
        _commandCenterSaveGameDataWriter.WriteCommandCenterDataToSaveFile(CommandCenterSaveData);
    }

    public void CompletePrologue()
    {
        if (CommandCenterSaveData.PrologueCompleted == true) return;

        _commandCenterSaveGameDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        CommandCenterSaveData.PrologueCompleted = true;
        _commandCenterSaveGameDataWriter.WriteCommandCenterDataToSaveFile(CommandCenterSaveData);
    }

    public void CompleteTutorial()
    {
        if (CommandCenterSaveData.TutorialCompleted == true) return;

        _commandCenterSaveGameDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        CommandCenterSaveData.TutorialCompleted = true;
        _commandCenterSaveGameDataWriter.WriteCommandCenterDataToSaveFile(CommandCenterSaveData);
    }
}
