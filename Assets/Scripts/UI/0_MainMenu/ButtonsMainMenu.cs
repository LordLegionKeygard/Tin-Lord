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
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _areYouSurePanel;
    [SerializeField] private HangarSystem _hangarSystem;

    private bool HaveSaveData() => CommandCenterSaveGame.GetCommandCenterSaveGameDataWriter().CheckIfSaveFileExists();

    private void Start()
    {
        if (HaveSaveData()) _buttons[0].gameObject.SetActive(true);
    }

    public void NewGame()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);

        _hangarSystem.OpenHangar();
        // if (HaveSaveData())
        // {
        //     _areYouSurePanel.SetActive(true);
        //     ButtonsToggle(false);
        // }
        // else
        // {
        //     CustomEvents.FireFade(FadeType.StartFade);
        //     StartCoroutine(nameof(PrepareLoadNewGame));
        // }
    }

    public void Continue()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);

        CustomEvents.FireFade(FadeType.StartFade);
        StartCoroutine(nameof(PrepareLoadGame));
    }

    private IEnumerator PrepareLoadGame()
    {
        yield return new WaitForSecondsRealtime(1);
        CommandCenterSaveGame.LoadGameData();
    }

    // private IEnumerator PrepareLoadNewGame()
    // {
    //     yield return new WaitForSecondsRealtime(1);
    //     StartNewGame();       
    // }

    // private void StartNewGame()
    // {
    //     WorldSaveGame.DeleteMissionGameData();
    //     CommandCenterSaveGame.NewGame(null);
    // }

    public void SettingsButton()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        _settingsPanel.SetActive(true);
    }


    public void QuitButton()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        Application.Quit();
    }

    public void AreYouSureYes()
    {
        // AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        // CustomEvents.FireFade(FadeType.StartFade);
        // ToggleContinueButton(false);
        // StartCoroutine(nameof(PrepareLoadNewGame));
        // _areYouSurePanel.SetActive(false);
        // CustomEvents.FireCloseTooltips();
    }

    public void AreYouSureNo()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
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