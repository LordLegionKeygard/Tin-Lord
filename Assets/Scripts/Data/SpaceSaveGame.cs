using UnityEngine;
using UnityEngine.Serialization;

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

        _spaceSaveGameDataWriter.WriteCommandCenterDataToSaveFile(this.SpaceSaveData);

        LoadDataFromJson();
        CustomEvents.FireLoadScene(SceneEnum.Space, WorldGameInfo.LoadSceneTime, null);
    }

    public void SaveDataToJson()
    {
        _spaceSaveGameDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        SpaceSaveLoad.SaveData(ref SpaceSaveData);
        _spaceSaveGameDataWriter.WriteCommandCenterDataToSaveFile(SpaceSaveData);
    }

    public void LoadDataFromJson()
    {
        _spaceSaveGameDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        SpaceSaveData = _spaceSaveGameDataWriter.LoadCommandCenterDataFromJson();
    }

    public void SaveEndMissionData(int memoryFragments, int aiCores, int quants)
    {
        _spaceSaveGameDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        SpaceSaveData.MainResourcesData[(int)ResourceEnum.DataFragment] += memoryFragments;
        SpaceSaveData.AiCores += aiCores;
        SpaceSaveData.Quants += quants;
        _spaceSaveGameDataWriter.WriteCommandCenterDataToSaveFile(SpaceSaveData);
    }

    public void CompletePrologue()
    {
        if (SpaceSaveData.PrologueCompleted == true) return;

        _spaceSaveGameDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        SpaceSaveData.PrologueCompleted = true;
        _spaceSaveGameDataWriter.WriteCommandCenterDataToSaveFile(SpaceSaveData);
    }

    public void CompleteTutorial()
    {
        if (SpaceSaveData.TutorialCompleted == true) return;

        _spaceSaveGameDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        SpaceSaveData.TutorialCompleted = true;
        _spaceSaveGameDataWriter.WriteCommandCenterDataToSaveFile(SpaceSaveData);
    }
}
