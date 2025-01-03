using System;
using System.IO;
using UnityEngine;

public class CommandCenterSaveGameDataWriter
{
    public string SaveDataDirectoryPath = "";
    private string _mainDataSaveFileName = "MainSave.txt";

    public CommandCenterSaveGameDataWriter(string saveDataDirectoryPath)
    {
        SaveDataDirectoryPath = saveDataDirectoryPath;
    }

    public CommandCenterSaveData LoadCommandCenterDataFromJson()
    {
        string savePath = Path.Combine(SaveDataDirectoryPath, _mainDataSaveFileName);

        CommandCenterSaveData loadedSaveData = null;

        if (File.Exists(savePath))
        {
            try
            {
                string saveDataToLoad = "";

                using (FileStream stream = new FileStream(savePath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        saveDataToLoad = reader.ReadToEnd();
                    }
                }
                loadedSaveData = JsonUtility.FromJson<CommandCenterSaveData>(saveDataToLoad);
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

    public void WriteCommandCenterDataToSaveFile(CommandCenterSaveData characterData)
    {
        string savePath = Path.Combine(SaveDataDirectoryPath, _mainDataSaveFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            Debug.Log("Save Path " + savePath);

            string dataToStore = JsonUtility.ToJson(characterData, true);

            using (FileStream stream = new FileStream(savePath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception exception)
        {
            Debug.LogError("Error while trying to save data, game could not be saved - " + exception);
        }
    }

    public void DeleteSaveFile()
    {
        File.Delete(Path.Combine(SaveDataDirectoryPath, _mainDataSaveFileName));
    }

    public bool CheckIfSaveFileExists()
    {
        if (File.Exists(Path.Combine(SaveDataDirectoryPath, _mainDataSaveFileName))) return true;

        else return false;
    }
}