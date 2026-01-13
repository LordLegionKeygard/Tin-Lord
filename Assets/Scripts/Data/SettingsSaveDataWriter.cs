using System;
using System.IO;
using UnityEngine;

public class SettingsSaveDataWriter
{
    public string SaveSettingsDataDirectoryPath = "";
    private string _settingsDataSaveFileName => WorldGameInfo.IsDemo ? "DemoSettingsSave0.txt" : "SettingsSave.txt";

    public SettingsSaveDataWriter(string SaveSettingsDataDirectoryPath)
    {
        this.SaveSettingsDataDirectoryPath = SaveSettingsDataDirectoryPath;
    }

    public SettingsSaveData LoadSettingsDataFromJson()
    {
        string savePath = Path.Combine(SaveSettingsDataDirectoryPath, _settingsDataSaveFileName);

        SettingsSaveData settingsLoadedSaveData = null;

        if (File.Exists(savePath))
        {
            try
            {
                string settingsSaveDataToLoad = "";

                using (FileStream stream = new FileStream(savePath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        settingsSaveDataToLoad = reader.ReadToEnd();
                    }
                }
                settingsLoadedSaveData = JsonUtility.FromJson<SettingsSaveData>(settingsSaveDataToLoad);
            }
            catch (Exception exception)
            {

                Debug.LogWarning(exception.Message);
            }
        }
        else
        {
            // Debug.Log("Save settings file does not exist");
        }
        return settingsLoadedSaveData;
    }

    public void WriteSettingsDataToSaveFile(SettingsSaveData settingsSaveData)
    {
        string savePath = Path.Combine(SaveSettingsDataDirectoryPath, _settingsDataSaveFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));

            string dataToStore = JsonUtility.ToJson(settingsSaveData, true);

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
            Debug.Log("Error while trying to save data, settings could not be saved - " + exception);
        }
    }

    public SettingsSaveData LoadSettings()
    {
        string fullPath = Path.Combine(SaveSettingsDataDirectoryPath, _settingsDataSaveFileName);

        if (!File.Exists(fullPath))
        {
            // Debug.Log("Skipping directory when loading settings because it does no contain data");
        }

        SettingsSaveData settingsData = LoadSettingsDataFromJson();

        return settingsData;
    }
}
