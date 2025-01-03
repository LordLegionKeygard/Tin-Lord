using System;
using System.IO;
using UnityEngine;

public class WorldSaveGameDataWriter : MonoBehaviour
{
    public string SaveDataDirectoryPath = "";
    private string _worldDataSaveFileName = "Mission_";

    public WorldSaveGameDataWriter(string saveDataDirectoryPath)
    {
        SaveDataDirectoryPath = saveDataDirectoryPath;
    }

    public WorldSaveData LoadMissionDataFromJson(string missionId)
    {
        string savePath = Path.Combine(SaveDataDirectoryPath, _worldDataSaveFileName + missionId + ".txt");

        WorldSaveData loadedSaveData = null;

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

    public void WriteMissionDataToSaveFile(WorldSaveData worldData, string missionId)
    {
        string savePath = Path.Combine(SaveDataDirectoryPath, _worldDataSaveFileName + missionId + ".txt");

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            // Debug.Log("Save Path " + savePath);

            string dataToStore = JsonUtility.ToJson(worldData, true);

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

    public void DeleteSaveFile(string missionId)
    {
        File.Delete(Path.Combine(SaveDataDirectoryPath, _worldDataSaveFileName + missionId + ".txt"));
    }

    // public bool CheckIfSaveFileExists()
    // {

    // } 

}
