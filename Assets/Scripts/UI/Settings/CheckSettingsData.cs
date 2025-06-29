using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Zenject;

public class CheckSettingsData : MonoBehaviour
{
    [Inject] private WorldSaveSettings _worldSaveSettings;
    [SerializeField] private BaseResolution _baseResolution;
    [SerializeField] private UniversalRenderPipelineAsset[] _urpAsset;
    [SerializeField] private SaveLoadSettings _saveLoadSettings;

    private void Start()
    {
        InitializeSettings();
    }

    private void InitializeSettings()
    {
        //здесь настройки берутся из префаба WorldSaveSettings
        if (_worldSaveSettings.GetSettingsData() == null)
        {
            _worldSaveSettings.NewUserSettings();
        }

        else
        {
            _worldSaveSettings.LoadSettingsData();
        }

        _saveLoadSettings.SetAllSettingsFromData();
    }
}
