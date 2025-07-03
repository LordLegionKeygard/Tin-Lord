using UnityEngine;

public class HangarSaveGame : MonoBehaviour
{
    public HangarSaveLoad HangarSaveLoad;

    [Header("SaveDataWriter")]
    private HangarSaveGameDataWriter _hangarSaveGameDataWriter;
    public HangarSaveGameDataWriter GetHangarSaveGameDataWriter() => _hangarSaveGameDataWriter;

    [Header("CurrentCommandCenterData")]
    public HangarSaveData HangarSaveData;

    private void Awake()
    {
        _hangarSaveGameDataWriter = new HangarSaveGameDataWriter(Application.persistentDataPath);
    }

    private void Start()
    {
        CheckHangarSaveData();
    }

    private void CheckHangarSaveData()
    {
        if (!_hangarSaveGameDataWriter.CheckIfSaveFileExists())
        {
            NewHangarData();
        }
        else
        {
            LoadDataFromJson();
            HangarSaveLoad?.LoadGameData(ref HangarSaveData);
        }
    }

    public void NewHangarData()
    {
        HangarSaveData = new HangarSaveData
        {
            Shards = 0,
            OpenedRobots = new bool[WorldGameInfo.HangarRobotsCount],
            OpenedDrones = new bool[WorldGameInfo.HangarDronesCount],
            OpenedCrates = new bool[WorldGameInfo.HangarCratesCount],
            OpenedSkills = new bool[WorldGameInfo.HangarSkillsCount]
        };

        HangarSaveData.OpenedRobots[0] = true; // Patch
        HangarSaveData.OpenedCrates[0] = true; // Base Create

        _hangarSaveGameDataWriter.WriteHangarDataToSaveFile(HangarSaveData);

        HangarSaveLoad.LoadGameData(ref HangarSaveData);
    }

    public void SaveDataToJson()
    {
        _hangarSaveGameDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        HangarSaveLoad.SaveData(ref HangarSaveData);
        _hangarSaveGameDataWriter.WriteHangarDataToSaveFile(HangarSaveData);
    }

    public void LoadDataFromJson()
    {
        _hangarSaveGameDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        HangarSaveData = _hangarSaveGameDataWriter.LoadHangarDataFromJson();
    }

    public void SaveEndGameDataToJson(int calculateShards)
    {
        _hangarSaveGameDataWriter.SaveDataDirectoryPath = Application.persistentDataPath;
        HangarSaveData.Shards += calculateShards;
        _hangarSaveGameDataWriter.WriteHangarDataToSaveFile(HangarSaveData);
    }
}
