using UnityEngine;

public class CommandCenterSaveGame : MonoBehaviour
{
    public CommandCenterSaveLoad CommandCenterSaveLoad;

    [Header("SaveDataWriter")]
    private CommandCenterSaveGameDataWriter _commandCenterSaveGameDataWriter;
    public CommandCenterSaveGameDataWriter GetCommandCenterSaveGameDataWriter() => _commandCenterSaveGameDataWriter;

    [Header("CurrentCommandCenterData")]
    public CommandCenterSaveData CommandCenterSaveData;
    

    private void Awake()
    {
        _commandCenterSaveGameDataWriter = new CommandCenterSaveGameDataWriter(Application.persistentDataPath);
    }

    public void NewCommandCenterData(CommandCenterSaveData commandCenterSaveData)
    {
        CommandCenterSaveData = commandCenterSaveData;

        _commandCenterSaveGameDataWriter.WriteCommandCenterDataToSaveFile(CommandCenterSaveData);

        LoadDataFromJson();
        CustomEvents.FireLoadScene(SceneEnum.CommandCenter, WorldGameInfo.LoadSceneTime, null);
    }

    public void SaveDataToJson()
    {
        _commandCenterSaveGameDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        CommandCenterSaveLoad.SaveData(ref CommandCenterSaveData);
        _commandCenterSaveGameDataWriter.WriteCommandCenterDataToSaveFile(CommandCenterSaveData);
    }

    public void LoadDataFromJson()
    {
        _commandCenterSaveGameDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        CommandCenterSaveData = _commandCenterSaveGameDataWriter.LoadCommandCenterDataFromJson();
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
