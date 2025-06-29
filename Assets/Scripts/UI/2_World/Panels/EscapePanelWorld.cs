using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EscapePanelWorld : MonoBehaviour
{
    [Inject] private readonly WorldSaveGame _worldSaveGame;
    [SerializeField] private RectTransform _escapePanelTransform;
    [SerializeField] private GameSpeedSystem _gameSpeedSystem;
    [SerializeField] private GameObject _escapePanelBackgroundBlack;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private Button _escapeButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _restartButton;

    [Header("ExtraQuitPanel")]
    [SerializeField] private GameObject _extraQuitPanel;
    [SerializeField] private TextMeshProUGUI _extraText;
    [SerializeField] private ObjectivesPanel _objectivesPanel;
    [SerializeField] private Button _yesButton;
    [SerializeField] private Image _yesIcon;
    [SerializeField] private RectTransform _extraPanelTransform;
    private bool _isOpen;

    public void PanelViewToggle(bool changeSpeed)
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.EscapePanel, transform.position);

        _isOpen = !_isOpen;

        _gameSpeedSystem.SpeedButtonInteractableToggle(_isOpen);
        _escapePanelBackgroundBlack.SetActive(_isOpen);

        if (_isOpen)
        {
            if (changeSpeed) _gameSpeedSystem.ChangeGameSpeedButton((int)GameSpeedEnum.Pause, true);

            _escapePanelTransform.DOAnchorPosY(-185.5f, 0.8f).SetUpdate(true);
        }
        else
        {
            if (changeSpeed) _gameSpeedSystem.ChangeGameSpeed((int)GameSpeedEnum.Default);
            _escapePanelTransform.DOAnchorPosY(-55, 0.8f).SetUpdate(true);
            Reset();
        }
    }

    private void Reset()
    {
        _extraQuitPanel.SetActive(false);
        ToggleButtons(true);
    }

    private void ToggleButtons(bool state)
    {
        _escapeButton.interactable = state;
        _exitButton.interactable = state;
        _restartButton.interactable = state;
    }

    public void RestartButton()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        ToggleButtons(true);
        _restartButton.interactable = false;
        _extraQuitPanel.SetActive(true);
        ChangePanelPosition(-71.6f);

        _extraText.text = Language.TextStatic[68];
        ToggleYesButton(true);
    }

    public void EscapeButton()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        ToggleButtons(true);
        _escapeButton.interactable = false;
        _extraQuitPanel.SetActive(true);
        ChangePanelPosition(-138);

        _extraText.text = $"{string.Format(Language.TextStatic[66], WorldGameInfo.EscapeFragmentsPercent)}";
        ToggleYesButton(_objectivesPanel.CanEscape());
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
        ToggleButtons(true);
        _exitButton.interactable = false;
        _extraQuitPanel.SetActive(true);
        ChangePanelPosition(-71.6f);

        _extraText.text = Language.TextStatic[67];
        ToggleYesButton(true);
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
        if (_restartButton.interactable == false)
        {
            CustomEvents.FireFade(FadeType.StartFade);
            StartCoroutine(nameof(PrepareRestartMission));
        }
        else if (_escapeButton.interactable == false)
        {
            CustomEvents.FireMissionEnd(MissionEndEnum.Escape);
        }
        else
        {
            CustomEvents.FireFade(FadeType.StartFade);
            StartCoroutine(nameof(PrepareSaveMission));
        }
        CustomEvents.FireCloseTooltips();
    }

    private IEnumerator PrepareRestartMission()
    {
        yield return new WaitForSecondsRealtime(1);
        _worldSaveGame.ResetMissionJson();
        CustomEvents.FireLoadScene(SceneEnum.World, WorldGameInfo.LoadSceneTime, true, CurrentMissionInfo.Instance.GetCurrentLandscape().LoadingScreenSprite);
    }

    private IEnumerator PrepareSaveMission()
    {
        yield return new WaitForSecondsRealtime(1);
        _worldSaveGame.SaveMissionToJson();
        CustomEvents.FireLoadScene(SceneEnum.CommandCenter, WorldGameInfo.LoadSceneTime, true, null);
    }
}
