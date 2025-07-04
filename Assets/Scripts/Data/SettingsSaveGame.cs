using UnityEngine;
using UnityEngine.Serialization;

public class SettingsSaveGame : MonoBehaviour
{
    public SettingsSaveLoad SettingsSaveLoad;

    [Header("Save Settings Data Writer")]
    private SettingsSaveDataWriter _settingsSaveDataWritter;

    [Header("Current Settings Data")]
    public SettingsSaveData CurrentSettingsSaveData;
    private string _fileName = "SettingsSave.txt";

    private void Awake()
    {
        _settingsSaveDataWritter = new SettingsSaveDataWriter(Application.persistentDataPath, _fileName);
    }

    public void NewUserSettings()
    {
        _settingsSaveDataWritter.WriteSettingsDataToSaveFile(CurrentSettingsSaveData);
    }

    public void SaveSettingsToJson()
    {
        _settingsSaveDataWritter.SaveSettingsDataDirectoryPath = Application.persistentDataPath;
        _settingsSaveDataWritter.SettingsDataSaveFileName = _fileName;

        SettingsSaveLoad.SetAllSettingsToData();

        _settingsSaveDataWritter.WriteSettingsDataToSaveFile(CurrentSettingsSaveData);
    }

    public void LoadSettingsFromJson()
    {
        _settingsSaveDataWritter.SaveSettingsDataDirectoryPath = Application.persistentDataPath;
        _settingsSaveDataWritter.SettingsDataSaveFileName = _fileName;

        CurrentSettingsSaveData = _settingsSaveDataWritter.LoadSettingsDataFromJson();
    }

    public SettingsSaveData GetSettingsData()
    {
        return _settingsSaveDataWritter.LoadSettings();
    }
}
