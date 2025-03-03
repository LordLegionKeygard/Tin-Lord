using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
using Unity.Mathematics;

public class AudioPanel : MonoBehaviour
{
    [Inject] private WorldSaveSettings _worldSaveSettings;
    [SerializeField] private Slider[] _sliders;
    [SerializeField] private TextMeshProUGUI[] _volumeTexts;
    private int[] _volume = new int[4];
    private ApplySettings _applySettings;
    private bool _needSound;

    private void Awake()
    {
        _applySettings = GetComponent<ApplySettings>();
    }

    public void SetSettingsToData()
    {
        var data = _worldSaveSettings.CurrentSettingsSaveData;

        data.MasterVolume = _volume[0];
        data.SfxVolume = _volume[1];
        data.UiVolume = _volume[2];
        data.MusicVolume = _volume[3];
    }

    public void SetSettingsFromData()
    {
        var data = _worldSaveSettings.CurrentSettingsSaveData;

        _volume[0] = data.MasterVolume;
        _volume[1] = data.SfxVolume;
        _volume[2] = data.UiVolume;
        _volume[3] = data.MusicVolume;

        ApplySettingsToUI();
        _needSound = true;
        SetGameSettings();
    }

    private void ApplySettingsToUI()
    {
        for (int i = 0; i < _sliders.Length; i++)
        {
            _sliders[i].value = _volume[i];
            _volumeTexts[i].text = _volume[i].ToString();
        }
    }

    public void ChangeVolume(int soundNumber)
    {
        if (_sliders[soundNumber].value % 2 == 0 && _needSound) AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        _volume[soundNumber] = (int)_sliders[soundNumber].value;
        _volumeTexts[soundNumber].text = _volume[soundNumber].ToString();
        _applySettings.ApplyToggle(true);

        SetGameSettings();
    }

    public void Reset()
    {
        _volume[0] = (int)(WorldGameInfo.MasterVolume * 100);
        _volume[1] = (int)(WorldGameInfo.SfxVolume * 100);
        _volume[2] = (int)(WorldGameInfo.UiVolume * 100);
        _volume[3] = (int)(WorldGameInfo.MusicVolume * 100);

        for (int i = 0; i < _sliders.Length; i++)
        {
            _volumeTexts[i].text = _volume[i].ToString();
            _sliders[i].value = _volume[i];
        }
        _applySettings.ApplyToggle(true);

        SetGameSettings();
    }

    public void SetGameSettings()
    {
        AudioManager.Instance.MasterVolume = (float)_volume[0] / 100;
        AudioManager.Instance.SfxVolume = (float)_volume[1] / 100;
        AudioManager.Instance.UiVolume = (float)_volume[2] / 100;
        AudioManager.Instance.MusicVolume = (float)_volume[3] / 100;
    }
}
