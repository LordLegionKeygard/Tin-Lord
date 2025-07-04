using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

public class ButtonsMainMenu : MonoBehaviour
{
    [Inject] readonly SpaceSaveGame _spaceSaveGame;
    [SerializeField] private GameObject _continueButtonObject;
    [SerializeField] private TextMeshProUGUI[] _buttonsText;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private HangarSystem _hangarSystem;

    private bool HaveSaveData() => _spaceSaveGame.GetCommandCenterSaveGameDataWriter().CheckIfSaveFileExists();

    private void Start()
    {
        if (HaveSaveData()) _continueButtonObject.SetActive(true);
    }

    public void NewGameButton()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);

        _hangarSystem.OpenHangar();
    }

    public void ContinueButton()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);

        CustomEvents.FireFade(FadeType.StartFade);
        StartCoroutine(nameof(PrepareLoadGame));
    }

    private IEnumerator PrepareLoadGame()
    {
        yield return new WaitForSecondsRealtime(1);
        _spaceSaveGame.LoadDataFromJson();
        CustomEvents.FireLoadScene(SceneEnum.Space, WorldGameInfo.LoadSceneTime, null);
    }

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
}