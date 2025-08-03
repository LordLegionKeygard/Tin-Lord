using UnityEngine;

public class SettingsSaveGame : MonoBehaviour
{
    public SettingsSaveLoad SettingsSaveLoad;

    [Header("Save Settings Data Writer")]
    private SettingsSaveDataWriter _settingsSaveDataWritter;

    [Header("Current Settings Data")]
    public SettingsSaveData CurrentSettingsSaveData;

    private void Awake()
    {
        _settingsSaveDataWritter = new SettingsSaveDataWriter(Application.persistentDataPath);
    }

    public void NewUserSettings()
    {
        _settingsSaveDataWritter.WriteSettingsDataToSaveFile(CurrentSettingsSaveData);
    }

    public void SaveSettingsToJson()
    {
        _settingsSaveDataWritter.SaveSettingsDataDirectoryPath = Application.persistentDataPath;
        SettingsSaveLoad.SetAllSettingsToData();
        _settingsSaveDataWritter.WriteSettingsDataToSaveFile(CurrentSettingsSaveData);
    }

    public void LoadSettingsFromJson()
    {
        _settingsSaveDataWritter.SaveSettingsDataDirectoryPath = Application.persistentDataPath;
        CurrentSettingsSaveData = _settingsSaveDataWritter.LoadSettingsDataFromJson();
    }

    public SettingsSaveData GetSettingsData()
    {
        return _settingsSaveDataWritter.LoadSettings();
    }
}
