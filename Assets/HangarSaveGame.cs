using System.Collections;
using System.Collections.Generic;
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
        }
    }

    public void NewHangarData()
    {
        HangarSaveData = new HangarSaveData
        {
            Shards = 6,
            OpenedRobots = new bool[WorldGameInfo.RobotsCount],
        };

        HangarSaveData.OpenedRobots[0] = true; // Patch

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
}
