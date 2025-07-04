using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
using UnityEngine.Rendering.Universal;

public class VideoPanel : MonoBehaviour
{
    [Inject] private SettingsSaveGame _settingsSaveGame;
    [SerializeField] private UniversalRenderPipelineAsset[] _urpAsset;
    [SerializeField] private BaseResolution _baseResolution;
    [SerializeField] private TMP_Dropdown _screenModeDropDown;
    [SerializeField] private TMP_Dropdown _resolutionDropDown;
    [SerializeField] private TMP_Dropdown _qualityDropDown;
    [SerializeField] private TMP_Dropdown _antiAliasingDropDown;
    [SerializeField] private TMP_Dropdown _upscalingFilterDropDown;
    [SerializeField] private Toggle _glowToggle;
    [SerializeField] private Slider _frameRateSlider;
    [SerializeField] private TextMeshProUGUI _frameRateText;
    private int _screenMode;
    private int _resolution;
    private int _quality;
    private int _antiAliasing;
    private int _upscalingFilter;
    private bool _glow;
    private int _frameRate;
    private ApplySettings _applySettings;
    private UpscalingFilterSelection _upscalingFilterEnum;
    private bool _needSound;

    private void Awake()
    {
        _applySettings = GetComponent<ApplySettings>();
    }

    public void SetSettingsToData()
    {
        var data = _settingsSaveGame.CurrentSettingsSaveData;

        data.ScreenMode = _screenMode;
        data.Resolution = _resolution;
        data.Quality = _quality;
        data.AntiAliasing = _antiAliasing;
        data.UpscalingFilter = _upscalingFilter;
        data.Glow = _glow;
        data.FrameRate = _frameRate;
    }


    public void SetSettingsFromData()
    {
        var data = _settingsSaveGame.CurrentSettingsSaveData;

        _screenMode = data.ScreenMode;
        _quality = data.Quality;
        _resolution = data.Resolution;
        _antiAliasing = data.AntiAliasing;
        _upscalingFilter = data.UpscalingFilter;
        _glow = data.Glow;
        _frameRate = data.FrameRate;

        ApplySettingsToUI();

        _needSound = true;
        SetGameSettings();
    }

    private void ApplySettingsToUI()
    {
        _screenModeDropDown.value = _screenMode;
        _resolutionDropDown.value = _resolution;
        _qualityDropDown.value = _quality;
        _upscalingFilterDropDown.value = _upscalingFilter;
        _glowToggle.isOn = _glow;
        _frameRateSlider.value = _frameRate;
        _frameRateText.text = _frameRate.ToString();

        switch (_antiAliasing)
        {
            case 1:
                _antiAliasingDropDown.value = 0;
                break;
            case 2:
                _antiAliasingDropDown.value = 1;
                break;
            case 4:
                _antiAliasingDropDown.value = 2;
                break;
            case 8:
                _antiAliasingDropDown.value = 3;
                break;
        }
    }

    public void ChangeScreenMode()
    {
        _screenMode = _screenModeDropDown.value;
        ChangeSettings();
    }

    public void ChangeResolution()
    {
        _resolution = _resolutionDropDown.value;
        ChangeSettings();
    }

    public void ChangeQuality()
    {
        _quality = _qualityDropDown.value;
        ChangeSettings();
    }

    public void ChangeAntiAliasing()
    {
        switch (_antiAliasingDropDown.value)
        {
            case 0:
                _antiAliasing = 1;
                break;
            case 1:
                _antiAliasing = 2;
                break;
            case 2:
                _antiAliasing = 4;
                break;
            case 3:
                _antiAliasing = 8;
                break;
        }

        ChangeSettings();
    }

    public void ChangeUpscalingFiler()
    {
        _upscalingFilter = _upscalingFilterDropDown.value;
        ChangeSettings();
    }

    public void ChangeGlow()
    {
        _glow = _glowToggle.isOn;
        ChangeSettings();
    }

    public void ChangeTargetFrameRate()
    {
        if (_frameRateSlider.value % 2 == 0 && _needSound) AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        _frameRate = (int)_frameRateSlider.value;
        _applySettings.ApplyToggle(true);
        _frameRateText.text = _frameRate.ToString();
    }

    private void ChangeSettings()
    {
        if (_needSound) AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        SetGameSettings();
        _applySettings.ApplyToggle(true);
    }

    public void Reset()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        _screenMode = WorldGameInfo.ScreenMode;
        _resolution = WorldGameInfo.Resolution;
        _quality = WorldGameInfo.Quality;
        _antiAliasing = WorldGameInfo.AntiAliasing;
        _upscalingFilter = WorldGameInfo.UpscalingFilter;
        _glow = WorldGameInfo.Glow;
        _frameRate = WorldGameInfo.FrameRate;

        _screenModeDropDown.value = _screenMode;
        _resolutionDropDown.value = _resolution;
        _qualityDropDown.value = _quality;
        _upscalingFilterDropDown.value = _upscalingFilter;
        _glowToggle.isOn = _glow;
        _frameRateSlider.value = _frameRate;
        _frameRateText.text = _frameRate.ToString();

        switch (_antiAliasing)
        {
            case 1:
                _antiAliasingDropDown.value = 0;
                break;
            case 2:
                _antiAliasingDropDown.value = 1;
                break;
            case 4:
                _antiAliasingDropDown.value = 2;
                break;
            case 8:
                _antiAliasingDropDown.value = 3;
                break;
        }
        _applySettings.ApplyToggle(true);
    }

    public void SetGameSettings()
    {
        FullScreenMode mode = FullScreenMode.Windowed;

        switch (_screenMode)
        {
            case 0: // Полноэкранный (на весь экран с letterbox’ом)
                mode = FullScreenMode.ExclusiveFullScreen;
                Screen.SetResolution(_baseResolution.ResolutionWrapper[_resolution].Width,
                                     _baseResolution.ResolutionWrapper[_resolution].Height,
                                     mode);
                break;
            case 1: // Обычный оконный
                mode = FullScreenMode.Windowed;
                Screen.SetResolution(_baseResolution.ResolutionWrapper[_resolution].Width,
                                     _baseResolution.ResolutionWrapper[_resolution].Height,
                                     mode);
                break;
            case 2: // Безрамочный (в окне, но во весь экран)
                mode = FullScreenMode.FullScreenWindow;
                Screen.SetResolution(_baseResolution.ResolutionWrapper[_resolution].Width,
                                     _baseResolution.ResolutionWrapper[_resolution].Height,
                                     mode);
                break;
        }

        Screen.fullScreenMode = mode;

        QualitySettings.SetQualityLevel(_quality, false);

        switch (_upscalingFilter)
        {
            case 0:
                _upscalingFilterEnum = UpscalingFilterSelection.Linear;
                break;
            case 1:
                _upscalingFilterEnum = UpscalingFilterSelection.Point;
                break;
            case 2:
                _upscalingFilterEnum = UpscalingFilterSelection.FSR;
                break;
        }

        for (int i = 0; i < _urpAsset.Length; i++)
        {
            _urpAsset[i].msaaSampleCount = _antiAliasing;
            _urpAsset[i].upscalingFilter = _upscalingFilterEnum;
            _urpAsset[i].supportsHDR = _glow;
        }

        Application.targetFrameRate = _frameRate;
    }
}
