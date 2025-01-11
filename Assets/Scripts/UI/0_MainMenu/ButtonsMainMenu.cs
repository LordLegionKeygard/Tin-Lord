using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ButtonsMainMenu : MonoBehaviour
{
    [Inject] readonly CommandCenterSaveGame CommandCenterSaveGame;
    [Inject] readonly WorldSaveGame WorldSaveGame;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private TextMeshProUGUI[] _buttonsText;
    [SerializeField] private GameObject _continueButtonObject;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _areYouSurePanel;
    [SerializeField] private MusicFade _musicFade;
    private bool _isContinueGame;

    private bool HaveSaveData() => CommandCenterSaveGame.GetCommandCenterSaveGameDataWriter().CheckIfSaveFileExists();

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
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.DefaultClick, transform.position);

        if (HaveSaveData())
        {
            _areYouSurePanel.SetActive(true);
            ButtonsToggle(false);
        }
        else
        {
            CustomEvents.FireFade(FadeType.StartFade);
            _isContinueGame = false;
            StartCoroutine(nameof(PrepareLoad));
        }
    }

    public void Continue()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.DefaultClick, transform.position);

        CustomEvents.FireFade(FadeType.StartFade);
        _isContinueGame = true;
        StartCoroutine(nameof(PrepareLoad));
    }

    private IEnumerator PrepareLoad()
    {
        _musicFade.FadeOutMusic();
        yield return new WaitForSecondsRealtime(1);

        if (_isContinueGame)
        {
            CommandCenterSaveGame.LoadGameData();
        }
        else
        {
            StartNewGame();
        }
    }

    private void StartNewGame()
    {
        WorldSaveGame.DeleteAllMissionsGameData();
        CommandCenterSaveGame.NewGame();
    }

    public void Settings()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.DefaultClick, transform.position);
        // _settingsPanel.SetActive(true);
    }


    public void Quit()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.DefaultClick, transform.position);
        Application.Quit();
    }

    public void AreYouSureYes()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.DefaultClick, transform.position);
        CustomEvents.FireFade(FadeType.StartFade);
        _isContinueGame = false;
        _continueButtonObject.SetActive(false);
        StartCoroutine(nameof(PrepareLoad));
        _areYouSurePanel.SetActive(false);
        CustomEvents.FireCloseTooltips();
    }

    public void AreYouSureNo()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.DefaultClick, transform.position);
        _areYouSurePanel.SetActive(false);
        ButtonsToggle(true);
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
