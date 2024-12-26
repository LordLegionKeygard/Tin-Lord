using UnityEngine;
using Zenject;

public class MainMenuButtons : MonoBehaviour
{
    [Inject] WorldSaveGame WorldSaveGame;
    [SerializeField] private GameObject _continueButtonObject;
    [SerializeField] private GameObject _settingsPanel;

    private void Start()
    {
        UpdateContinueButton();
    }

    private void UpdateContinueButton()
    {
        if (WorldSaveGame.GetSaveGameDataWriter().CheckIfSaveFileExists())
        {
            _continueButtonObject.SetActive(true);
        }
    }

    public void Continue()
    {
        CustomEvents.FireFade(FadeType.StartFade);
        WorldSaveGame.LoadCommandCenterGameData();
    }

    public void NewGame()
    {
        CustomEvents.FireFade(FadeType.StartFade);
        WorldSaveGame.NewGame();
    }

    public void OpenSettings()
    {
        _settingsPanel.SetActive(true);
        // AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.DefaultClick, transform.position);
    }


    public void QuitButton()
    {
        // AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.DefaultClick, transform.position);
        Application.Quit();
    }
}
