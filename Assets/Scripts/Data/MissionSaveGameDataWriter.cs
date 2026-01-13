using System;
using System.IO;
using UnityEngine;

public class MissionSaveGameDataWriter
{
    public string SaveDataDirectoryPath = "";
    private string _worldDataSaveFileName => WorldGameInfo.IsDemo ? "DemoMissionSave0.txt" : "MissionSave.txt";

    public MissionSaveGameDataWriter(string saveDataDirectoryPath)
    {
        SaveDataDirectoryPath = saveDataDirectoryPath;
    }

    public MissionSaveData LoadMissionDataFromJson()
    {
        string savePath = Path.Combine(SaveDataDirectoryPath, _worldDataSaveFileName);

        MissionSaveData loadedSaveData = null;

        if (File.Exists(savePath))
        {
            try
            {
                string saveDataToLoad = "";

                using (FileStream stream = new(savePath, FileMode.Open))
                {
                    using StreamReader reader = new(stream);
                    saveDataToLoad = reader.ReadToEnd();
                }
                loadedSaveData = JsonUtility.FromJson<MissionSaveData>(saveDataToLoad);
            }
            catch (Exception exception)
            {
                Debug.LogWarning(exception.Message);
            }
        }
        else
        {
            Debug.Log("Save file does not exist");
        }
        return loadedSaveData;
    }

    public void WriteMissionDataToSaveFile(MissionSaveData missionData)
    {
        string savePath = Path.Combine(SaveDataDirectoryPath, _worldDataSaveFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            // Debug.Log("Save Path " + savePath);

            string dataToStore = JsonUtility.ToJson(missionData, true);

            using FileStream stream = new(savePath, FileMode.Create);
            using StreamWriter writer = new(stream);
            writer.Write(dataToStore);
        }
        catch (Exception exception)
        {
            Debug.LogError("Error while trying to save data, game could not be saved - " + exception);
        }
    }

    public void DeleteMissionSaveFile()
    {
        var missionPath = Path.Combine(SaveDataDirectoryPath, _worldDataSaveFileName);
        if (File.Exists(missionPath))
        {
            File.Delete(missionPath);
        }
    }

    public bool CheckIfSaveFileExists()
    {
        if (File.Exists(Path.Combine(SaveDataDirectoryPath, _worldDataSaveFileName))) return true;

        else return false;
    }

}
