using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class GamePlayPanel : MonoBehaviour
{
    [Header("Bools")]
    private bool _blood;

    [Header("Toggles")]
    [SerializeField] private Toggle _bloodToggle;

    [Header("Other")]
    [Inject] private WorldSaveSettings _worldSaveSettings;
    private ApplySettings _applySettings;
    // private bool _needSound;

    private void Awake()
    {
        _applySettings = GetComponent<ApplySettings>();
    }

    public void SetSettingsToData()
    {
        var data = _worldSaveSettings.CurrentSettingsSaveData;

        data.Blood = _blood;
    }

    public void SetSettingsFromData()
    {
        var data = _worldSaveSettings.CurrentSettingsSaveData;

        _blood = data.Blood;

        ApplySettingsToUI();

        // _needSound = true;
        SetGameSettings();
    }

    private void ApplySettingsToUI()
    {
        _bloodToggle.SetIsOnWithoutNotify(_blood);
    }

    public void ChangeBlood()
    {
        _blood = _bloodToggle.isOn;
        ChangeSettings(true);
    }

    private void ChangeSettings(bool state)
    {
        if (state) AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        SetGameSettings();
        _applySettings.ApplyToggle(true);
    }

    public void Reset()
    {
        _blood = WorldGameInfo.Blood;
        _bloodToggle.SetIsOnWithoutNotify(_blood);

        _applySettings.ApplyToggle(true);
        SetGameSettings();
    }

    public void SetGameSettings()
    {
        WorldGameInfo.StaticBlood = _blood;
    }
}
