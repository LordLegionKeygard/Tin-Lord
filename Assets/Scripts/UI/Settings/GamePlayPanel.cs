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

    [Header("CameraSpeed")]
    [SerializeField] private CameraMovement _cameraMovement;
    [SerializeField] private Slider _cameraSpeedSlider;
    [SerializeField] private TextMeshProUGUI _cameraSpeedValueText;
    private float _cameraSpeed;

    [Header("Other")]
    [Inject] private WorldSaveSettings _worldSaveSettings;
    private ApplySettings _applySettings;
    private bool _needSound;

    private void Awake()
    {
        _applySettings = GetComponent<ApplySettings>();
    }

    public void SetSettingsToData()
    {
        var data = _worldSaveSettings.CurrentSettingsSaveData;

        data.CameraSpeed = _cameraSpeed;
        data.Blood = _blood;
    }

    public void SetSettingsFromData()
    {
        var data = _worldSaveSettings.CurrentSettingsSaveData;

        _cameraSpeed = data.CameraSpeed == 0 ? WorldGameInfo.CameraSpeed : data.CameraSpeed;
        _blood = data.Blood;

        ApplySettingsToUI();

        _needSound = true;
        SetGameSettings();
    }

    private void ApplySettingsToUI()
    {
        _cameraSpeedSlider.value = _cameraSpeed;
        _cameraSpeedValueText.text = _cameraSpeed.ToString();
        _bloodToggle.SetIsOnWithoutNotify(_blood);
    }

    public void ChangeBlood()
    {
        _blood = _bloodToggle.isOn;
        ChangeSettings(true);
    }

    public void ChangeCameraSpeed()
    {
        if (_needSound) AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        _cameraSpeed = _cameraSpeedSlider.value;
        _cameraSpeedValueText.text = _cameraSpeedSlider.value.ToString();
        ChangeSettings(false);
    }

    private void ChangeSettings(bool state)
    {
        if (state) AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        SetGameSettings();
        _applySettings.ApplyToggle(true);
    }

    public void Reset()
    {
        _cameraSpeed = WorldGameInfo.CameraSpeed;
        _blood = WorldGameInfo.Blood;

        _cameraSpeedSlider.value = _cameraSpeed;
        _bloodToggle.SetIsOnWithoutNotify(_blood);

        _applySettings.ApplyToggle(true);
        SetGameSettings();
    }

    public void SetGameSettings()
    {
        if (_cameraMovement != null) _cameraMovement.ChangeCameraSpeedCoeff(_cameraSpeed);
        WorldGameInfo.StaticBlood = _blood;
    }
}
