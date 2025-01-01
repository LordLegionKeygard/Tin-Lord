using UnityEngine;

public class CommandCenterSaveGame : MonoBehaviour
{
    public CommandCenterSaveLoad SaveLoad;

    [Header("Save Data Writer")]
    private CommandCenterGameDataWriter _saveGameDataWriter;
    public CommandCenterGameDataWriter GetSaveGameDataWriter() => _saveGameDataWriter;

    [Header("Current Character Data")]
    public CommandCenterSaveData CommandCenterSaveData;
    [SerializeField] private string _fileName;
    [SerializeField] private ConfigLoaderBuildings _configLoaderBuildings;


    private void Awake()
    {
        _saveGameDataWriter = new CommandCenterGameDataWriter(Application.persistentDataPath, _fileName);
    }

    public void NewGame()
    {
        CommandCenterSaveData = new CommandCenterSaveData
        {
            MemoryFragments = 0,
            LastOpenedMissionId = 0,
            BuildingsLearned = new bool[_configLoaderBuildings.AllBuidingsCount()],
        };

        CommandCenterSaveData.BuildingsLearned[0] = true; // Base

        _saveGameDataWriter.WriteCharacterDataToSaveFile(CommandCenterSaveData);

        Debug.Log("SaveNewGame");
        Debug.Log("File saved new game as: " + _fileName);

        Invoke(nameof(LoadCommandCenterGameData), 2f);
    }

    public void DeleteGameData()
    {
        _saveGameDataWriter.DeleteSaveFile();
    }


    public void SaveGameData()
    {
        _saveGameDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        _saveGameDataWriter.DataSaveFileName = _fileName;

        SaveLoad.SaveData(ref CommandCenterSaveData);

        _saveGameDataWriter.WriteCharacterDataToSaveFile(CommandCenterSaveData);

        Debug.Log("Save");
        Debug.Log("File saved as: " + _fileName);
    }

    public void LoadCommandCenterGameData()
    {
        _saveGameDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        _saveGameDataWriter.DataSaveFileName = _fileName;

        CommandCenterSaveData = _saveGameDataWriter.LoadCommandCenterDataFromJson();

        CustomEvents.FireLoadScene(SceneEnum.CommandCenter, 5f, true);
    }
}
