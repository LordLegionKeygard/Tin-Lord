using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ButtonsMainMenu : MonoBehaviour
{
    [Inject] private readonly CommandCenterSaveGame _commandCenterSaveGame;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private TextMeshProUGUI[] _buttonsText;
    [SerializeField] private GameObject _continueButtonObject;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _areYouSurePanel;
    [SerializeField] private MusicFade _musicFade;
    [SerializeField] private GameObject _terminal;

    private bool HaveSaveData() => _commandCenterSaveGame.GetCommandCenterSaveGameDataWriter().CheckIfSaveFileExists();


    private void Start()
    {
        UpdateContinueButton();
    }

    private void UpdateContinueButton()
    {
        if (HaveSaveData())
        {
            _continueButtonObject.SetActive(true);
        }
    }

    public void NewGame()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);

        if (HaveSaveData())
        {
            _areYouSurePanel.SetActive(true);
            ButtonsToggle(false);
        }
        else
        {
            CustomEvents.FireFade(FadeType.StartFade);
            StartCoroutine(nameof(PrepareTerminal));
        }
    }

    private IEnumerator PrepareTerminal()
    {
        _musicFade.FadeOutMusic();
        yield return new WaitForSecondsRealtime(1);
        CustomEvents.FireFade(FadeType.FadeOut);
        _terminal.SetActive(true);
    }

    public void Settings()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
    }

    public void Quit()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        Application.Quit();
    }

    public void AreYouSureYes()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        CustomEvents.FireFade(FadeType.StartFade);
        StartCoroutine(nameof(PrepareTerminal));
        _areYouSurePanel.SetActive(false);
        CustomEvents.FireCloseTooltips();
    }

    public void AreYouSureNo()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        ButtonsToggle(true);
        _areYouSurePanel.SetActive(false);
        CustomEvents.FireCloseTooltips();
    }

    private void ButtonsToggle(bool state)
    {
        foreach (var item in _buttons)
        {
            item.interactable = state;
        }

        foreach (var item in _buttonsText)
        {
            item.color = state == false ? Colors.GreySix : Color.white;
        }
    }
}
