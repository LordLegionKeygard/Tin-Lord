using System.Collections;
using UnityEngine;
using Zenject;

public class SaveLoadSettings : MonoBehaviour
{
    [Inject] private WorldSaveSettings _worldSaveSettings;
    private VideoPanel _videoPanel;
    private ControlPanel _controlPanel;
    private AudioPanel _audioPanel;
    private GamePlayPanel _gameplayPanel;

    private void Awake()
    {
        _worldSaveSettings.SaveLoadSettings = this;

        _videoPanel = GetComponent<VideoPanel>();
        _controlPanel = GetComponent<ControlPanel>();
        _audioPanel = GetComponent<AudioPanel>();
        _gameplayPanel = GetComponent<GamePlayPanel>();
    }

    public void SetAllSettingsToData()
    {
        _videoPanel.SetSettingsToData();
        _controlPanel.SetSettingsToData();
        _audioPanel.SetSettingsToData();
        _gameplayPanel.SetSettingsToData();
    }

    public void SetAllSettingsFromData()
    {
        _videoPanel.SetSettingsFromData();
        _controlPanel.SetSettingsFromData();
        _audioPanel.SetSettingsFromData();
        _gameplayPanel.SetSettingsFromData();
    }
}
