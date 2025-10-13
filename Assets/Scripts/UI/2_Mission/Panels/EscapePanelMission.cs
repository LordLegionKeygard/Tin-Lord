using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EscapePanelMission : MonoBehaviour
{
    [Inject] private readonly SpaceSaveGame _spaceSaveGame;
    [SerializeField] private MissionSaveLoad _missionSaveLoad;
    [SerializeField] private RectTransform _escapePanelTransform;
    [SerializeField] private GameSpeedSystem _gameSpeedSystem;
    [SerializeField] private GameObject _escapePanelBackgroundBlack;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private Button _escapeButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _restartButton;

    [Header("ExtraQuitPanel")]
    [SerializeField] private GameObject _extraPanel;
    [SerializeField] private TextMeshProUGUI _extra;
    [SerializeField] private ObjectivesPanel _objectivesPanel;
    [SerializeField] private Button _yesButton;
    [SerializeField] private Image _yesIcon;
    [SerializeField] private RectTransform _extraPanelTransform;
    private bool _escapePanelIsOpen;

    [Header("View")]
    [SerializeField] private Image _escapeImage;
    [SerializeField] private Sprite[] _escapeSprites;

    public bool IsEscapeMode() => _escapePanelIsOpen || _settingsPanel.activeInHierarchy;

    public void PanelViewToggle(bool changeSpeed)
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.EscapePanel, transform.position);

        _escapePanelIsOpen = !_escapePanelIsOpen;

        _gameSpeedSystem.RefreshSpeedButtonInteractable();
        _escapePanelBackgroundBlack.SetActive(_escapePanelIsOpen);

        if (_escapePanelIsOpen)
        {
            if (changeSpeed) _gameSpeedSystem.ChangeGameSpeedButton((int)GameSpeedEnum.Pause, true);
            _escapePanelTransform.DOAnchorPosY(-185.5f, 0.8f).SetUpdate(true);
        }
        else
        {
            if (changeSpeed) _gameSpeedSystem.ChangeGameSpeed((int)GameSpeedEnum.Default, false);
            _escapePanelTransform.DOAnchorPosY(-55, 0.8f).SetUpdate(true);
            Reset();
        }
        UpdateButtonView();
    }

    private void UpdateButtonView()
    {
        _escapeImage.sprite = IsEscapeMode() ? _escapeSprites[0] : _escapeSprites[1];
    }

    private void Reset()
    {
        _extraPanel.SetActive(false);
        ToggleAllEscapePanelButtons(true);
    }

    private void ToggleAllEscapePanelButtons(bool state)
    {
        _escapeButton.interactable = state;
        _exitButton.interactable = state;
        _restartButton.interactable = state;
    }

    public void RestartButton()
    {
        var haveAiCore = _spaceSaveGame.SpaceSaveData.AiCores > 1;

        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        ToggleAllEscapePanelButtons(true);
        _restartButton.interactable = false;
        ToggleYesButton(haveAiCore);
        _extraPanel.SetActive(true);
        ChangePanelPosition(-93f);


        _extra.text = haveAiCore ? Language.TextStatic[68] : Language.TextStatic[76];
    }

    public void EscapeButton()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        ToggleAllEscapePanelButtons(true);
        _escapeButton.interactable = false;
        ToggleYesButton(_objectivesPanel.CanEscape());
        _extraPanel.SetActive(true);
        ChangePanelPosition(-155);

        _extra.text = Language.TextStatic[66];
    }

    public void SettingsButton()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        _settingsPanel.SetActive(true);
        PanelViewToggle(false);
    }

    public void ExitButton()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        ToggleAllEscapePanelButtons(true);
        _exitButton.interactable = false;
        ToggleYesButton(true);
        _extraPanel.SetActive(true);
        ChangePanelPosition(-71.6f);

        _extra.text = Language.TextStatic[67];
    }

    private void ChangePanelPosition(float newYPosition)
    {
        Vector3 position = _extraPanelTransform.anchoredPosition;
        position.y = newYPosition;
        _extraPanelTransform.anchoredPosition = position;
    }

    private void ToggleYesButton(bool state)
    {
        _yesButton.interactable = state;
        _yesIcon.color = state ? Colors.GreySeven : Colors.AlphaGreyFive;
    }

    public void NoButton()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        Reset();
        CustomEvents.FireCloseTooltips();
    }

    public void YesButton()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        if (_restartButton.interactable == false) // restart
        {
            CustomEvents.FireFade(FadeType.StartFade);
            _spaceSaveGame.RemoveOneAiCoreDataToJson();
            _missionSaveLoad.PrepareRestartMission();
        }
        else if (_escapeButton.interactable == false) //escape
        {
            CustomEvents.FireMissionEnd(MissionEndEnum.Escape);
        }
        else // exit
        {
            CustomEvents.FireFade(FadeType.StartFade);
            _missionSaveLoad.PrepareSaveMission();
        }
        CustomEvents.FireCloseTooltips();
    }
}
