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
        StartCoroutine(InitializeSettings());
    }

    private IEnumerator InitializeSettings()
    {
        yield return StartCoroutine(SetVideoSettings());
        yield return new WaitForSeconds(0.1f);

        yield return StartCoroutine(SetControlSettings());
        yield return new WaitForSeconds(0.1f);

        yield return StartCoroutine(SetAudioSettings());
        yield return new WaitForSeconds(0.1f);

        yield return StartCoroutine(SetGameplaySettings());
    }

    private IEnumerator SetVideoSettings()
    {
        _videoPanel.SetSettingsFromData();
        yield return null;
    }

    private IEnumerator SetControlSettings()
    {
        _controlPanel.SetSettingsFromData();
        yield return null;
    }

    private IEnumerator SetAudioSettings()
    {
        _audioPanel.SetSettingsFromData();
        yield return null;
    }

    private IEnumerator SetGameplaySettings()
    {
        _gameplayPanel.SetSettingsFromData();
        yield return null;
    }
}
