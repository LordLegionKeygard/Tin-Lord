using System;
using System.IO;
using UnityEngine;

public class WorldSaveGameDataWriter
{
    public string SaveDataDirectoryPath = "";
    private string _worldDataSaveFileName = "Mission";

    public WorldSaveGameDataWriter(string saveDataDirectoryPath)
    {
        SaveDataDirectoryPath = saveDataDirectoryPath;
    }

    public WorldSaveData LoadMissionDataFromJson()
    {
        string savePath = Path.Combine(SaveDataDirectoryPath, _worldDataSaveFileName + ".txt");

        WorldSaveData loadedSaveData = null;

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
                loadedSaveData = JsonUtility.FromJson<WorldSaveData>(saveDataToLoad);
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

    public void WriteMissionDataToSaveFile(WorldSaveData worldData)
    {
        string savePath = Path.Combine(SaveDataDirectoryPath, _worldDataSaveFileName + ".txt");

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            // Debug.Log("Save Path " + savePath);

            string dataToStore = JsonUtility.ToJson(worldData, true);

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
        var missionPath = Path.Combine(SaveDataDirectoryPath, _worldDataSaveFileName + ".txt");
        if (File.Exists(missionPath))
        {
            File.Delete(missionPath);
        }
    }

    public bool CheckIfSaveFileExists()
    {
        if (File.Exists(Path.Combine(SaveDataDirectoryPath, _worldDataSaveFileName + ".txt"))) return true;

        else return false;
    }

}
