using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenuButtons : MonoBehaviour
{
    [Inject] readonly CommandCenterSaveGame CommandCenterSaveGame;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private TextMeshProUGUI[] _buttonsText;
    [SerializeField] private GameObject _continueButtonObject;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _areYouSurePanel;

    private bool HaveSaveData() => CommandCenterSaveGame.GetSaveGameDataWriter().CheckIfSaveFileExists();

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

    public void Continue()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.DefaultClick, transform.position);
        CustomEvents.FireFade(FadeType.StartFade);
        CommandCenterSaveGame.LoadGameData();
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
            CommandCenterSaveGame.NewGame();
        }
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
        _areYouSurePanel.SetActive(false);
        CommandCenterSaveGame.NewGame();
        _continueButtonObject.SetActive(false);
    }

    public void AreYouSureNo()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.DefaultClick, transform.position);
        _areYouSurePanel.SetActive(false);
        ButtonsToggle(true);
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
