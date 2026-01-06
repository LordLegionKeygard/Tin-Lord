using System;
using System.IO;
using UnityEngine;

public class SpaceSaveGameDataWriter
{
    public string SaveDataDirectoryPath = "";
    private string _dataSaveFileName => WorldGameInfo.IsDemo ? "DemoSpaceSave.txt" : "SpaceSave.txt";

    public SpaceSaveGameDataWriter(string saveDataDirectoryPath)
    {
        SaveDataDirectoryPath = saveDataDirectoryPath;
    }

    public SpaceSaveData LoadSpaceDataFromJson()
    {
        string savePath = Path.Combine(SaveDataDirectoryPath, _dataSaveFileName);

        SpaceSaveData loadedSaveData = null;

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
                loadedSaveData = JsonUtility.FromJson<SpaceSaveData>(saveDataToLoad);
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

    public void WriteSpaceDataToSaveFile(SpaceSaveData spaceSaveData)
    {
        string savePath = Path.Combine(SaveDataDirectoryPath, _dataSaveFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            // Debug.Log("Save Path " + savePath);

            string dataToStore = JsonUtility.ToJson(spaceSaveData, true);

            using FileStream stream = new(savePath, FileMode.Create);
            using StreamWriter writer = new(stream);
            writer.Write(dataToStore);
        }
        catch (Exception exception)
        {
            Debug.LogError("Error while trying to save data, game could not be saved - " + exception);
        }
    }

    public void DeleteSaveFile()
    {
        var spacePath =  Path.Combine(SaveDataDirectoryPath, _dataSaveFileName);
        if (File.Exists(spacePath))
        {
            File.Delete(spacePath);
        }
    }

    public bool CheckIfSaveFileExists()
    {
        if (File.Exists(Path.Combine(SaveDataDirectoryPath, _dataSaveFileName))) return true;

        else return false;
    }
}