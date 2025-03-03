using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSaveSettings : MonoBehaviour
{
    public SaveLoadSettings SaveLoadSettings;

    [Header("Save Settings Data Writer")]
    private SaveSettingsDataWriter _saveSettingsDataWritter;

    [Header("Current Settings Data")]
    public SettingsSaveData CurrentSettingsSaveData;
    [SerializeField] private string _fileName;

    private void Awake()
    {
        _saveSettingsDataWritter = new SaveSettingsDataWriter(Application.persistentDataPath, _fileName);
    }

    public void NewUserSettings()
    {
        _saveSettingsDataWritter.WriteSettingsDataToSaveFile(CurrentSettingsSaveData);
    }

    public void SaveSettingsData()
    {
        _saveSettingsDataWritter.SaveSettingsDataDirectoryPath = Application.persistentDataPath;
        _saveSettingsDataWritter.SettingsDataSaveFileName = _fileName;

        SaveLoadSettings.SetAllSettingsToData();

        _saveSettingsDataWritter.WriteSettingsDataToSaveFile(CurrentSettingsSaveData);
    }

    public void LoadSettingsData()
    {
        _saveSettingsDataWritter.SaveSettingsDataDirectoryPath = Application.persistentDataPath;
        _saveSettingsDataWritter.SettingsDataSaveFileName = _fileName;

        CurrentSettingsSaveData = _saveSettingsDataWritter.LoadSettingsDataFromJson();
    }

    public SettingsSaveData GetSettingsData()
    {
        return _saveSettingsDataWritter.LoadSettings();
    }
}
