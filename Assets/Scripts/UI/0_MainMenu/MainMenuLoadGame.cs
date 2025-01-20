using System.Collections;
using UnityEngine;
using Zenject;

public class MainMenuLoadGame : MonoBehaviour
{
    [Inject] private readonly CommandCenterSaveGame _commandCenterSaveGame;
    [Inject] private readonly WorldSaveGame _worldSaveGame;
    [SerializeField] private MusicFade _mainMenuMusic;
    [SerializeField] private MusicFade _terminalMusic;
    private bool _isContinueGame;

    public void NewGame()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Terminal], transform.position);
        _terminalMusic.FadeOutMusic();
        CustomEvents.FireFade(FadeType.StartFade);
        _isContinueGame = false;
        StartCoroutine(nameof(PrepareLoad));
    }

    public void ContinueGame()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        CustomEvents.FireFade(FadeType.StartFade);
        _isContinueGame = true;
        StartCoroutine(nameof(PrepareLoad));
    }

    private IEnumerator PrepareLoad()
    {
        _mainMenuMusic.FadeOutMusic();
        yield return new WaitForSecondsRealtime(1);

        if (_isContinueGame)
        {
            _commandCenterSaveGame.LoadGameData();
        }
        else
        {
            _worldSaveGame.DeleteAllMissionsGameData();
            _commandCenterSaveGame.NewGame();
        }
    }
}
