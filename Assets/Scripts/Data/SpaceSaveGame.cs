using UnityEngine;

public class SpaceSaveGame : MonoBehaviour
{
    public SpaceSaveLoad SpaceSaveLoad;

    [Header("SaveDataWriter")]
    private SpaceSaveGameDataWriter _spaceSaveGameDataWriter;
    public SpaceSaveGameDataWriter GetCommandCenterSaveGameDataWriter() => _spaceSaveGameDataWriter;

    [Header("CurrentCommandCenterData")]
    public SpaceSaveData SpaceSaveData;
    

    private void Awake()
    {
        _spaceSaveGameDataWriter = new SpaceSaveGameDataWriter(Application.persistentDataPath);
    }

    public void NewCommandCenterData(SpaceSaveData spaceSaveData)
    {
        this.SpaceSaveData = spaceSaveData;

        _spaceSaveGameDataWriter.WriteSpaceDataToSaveFile(this.SpaceSaveData);

        LoadDataFromJson();
        CustomEvents.FireLoadScene(SceneEnum.Space, WorldGameInfo.LoadSceneTime, null);
    }

    public void SaveDataToJson()
    {
        _spaceSaveGameDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        SpaceSaveLoad.SaveData(ref SpaceSaveData);
        _spaceSaveGameDataWriter.WriteSpaceDataToSaveFile(SpaceSaveData);
    }

    public void LoadDataFromJson()
    {
        _spaceSaveGameDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        SpaceSaveData = _spaceSaveGameDataWriter.LoadSpaceDataFromJson();
    }

    public void SaveEndMissionDataToJson(int memoryFragments, int aiCores, int quants)
    {
        _spaceSaveGameDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        SpaceSaveData.HangarCommandCenterData.MainResourcesData[(int)ResourceEnum.DataFragment] += memoryFragments;
        SpaceSaveData.AiCores += aiCores;
        SpaceSaveData.Quants += quants;
        _spaceSaveGameDataWriter.WriteSpaceDataToSaveFile(SpaceSaveData);
    }

    public void RemoveOneAiCoreDataToJson()
    {
        _spaceSaveGameDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        SpaceSaveData.AiCores -= 1;
        _spaceSaveGameDataWriter.WriteSpaceDataToSaveFile(SpaceSaveData);
    }

    public void CompletePrologue()
    {
        if (SpaceSaveData.PrologueCompleted == true) return;

        _spaceSaveGameDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        SpaceSaveData.PrologueCompleted = true;
        _spaceSaveGameDataWriter.WriteSpaceDataToSaveFile(SpaceSaveData);
    }
}
