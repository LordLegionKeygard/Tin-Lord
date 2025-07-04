using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;
using Zenject;

public class CheckSettingsData : MonoBehaviour
{
    [Inject] private SettingsSaveGame _settingsSaveGame;
    [SerializeField] private BaseResolution _baseResolution;
    [SerializeField] private UniversalRenderPipelineAsset[] _urpAsset;
    [SerializeField] private SettingsSaveLoad _settingsSaveLoad;

    private void Start()
    {
        InitializeSettings();
    }

    private void InitializeSettings()
    {
        //здесь настройки берутся из префаба WorldSaveSettings
        if (_settingsSaveGame.GetSettingsData() == null)
        {
            _settingsSaveGame.NewUserSettings();
        }

        else
        {
            _settingsSaveGame.LoadSettingsFromJson();
        }

        _settingsSaveLoad.SetAllSettingsFromData();
    }
}
