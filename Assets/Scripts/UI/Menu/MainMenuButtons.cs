using UnityEngine;
using Zenject;

public class MainMenuButtons : MonoBehaviour
{
    [Inject] WorldSaveGame WorldSaveGame;
    [SerializeField] private GameObject _settingsPanel;

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
