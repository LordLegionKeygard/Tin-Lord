using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneLoader : MonoBehaviour
{
    [Inject] private readonly CommandCenterSaveGame _commandCenterSaveGame;
    [Inject] private readonly WorldSaveGame _worldCenterSaveGame;
    [Inject] private readonly HangarSaveGame _hangarSaveGame;
    [Inject] private WorldSaveSettings _worldSaveSettings;
    [SerializeField] private LoadingScreenController _loadingScreenController;

    private void Start()
    {
        CustomEvents.OnLoadScene += LoadSceneAsynchronously;
    }
    private void LoadSceneAsynchronously(SceneEnum sceneEnum, float timeInSec, Sprite sprite)
    {
        _loadingScreenController.ShowLoadingScreen(sprite);
        CheckSaveLoad(sceneEnum);
        StartCoroutine(LoadScene(sceneEnum, timeInSec));
    }


    private IEnumerator LoadScene(SceneEnum sceneEnum, float timeInSec)
    {
        yield return new WaitForSecondsRealtime(timeInSec);

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync((int)sceneEnum);

        while (!loadOperation.isDone)
        {
            yield return null;
        }

        System.Threading.Tasks.Task task = _loadingScreenController.CheckCurrentScene();

        if (sceneEnum == SceneEnum.CommandCenter) _commandCenterSaveGame.CommandCenterSaveLoad.LoadGameData(ref _commandCenterSaveGame.CommandCenterSaveData);
        else if (sceneEnum == SceneEnum.World) _worldCenterSaveGame.WorldSaveLoad.LoadGameData(ref _worldCenterSaveGame.CurrentWorldSaveData);
        else if (sceneEnum == SceneEnum.MainMenu) _hangarSaveGame.HangarSaveLoad.LoadGameData(ref _hangarSaveGame.HangarSaveData);
        _worldSaveSettings.SaveLoadSettings.SetAllSettingsFromData();
    }

    private void CheckSaveLoad(SceneEnum sceneEnum)
    {
        switch (sceneEnum)
        {
            case SceneEnum.MainMenu:
                _hangarSaveGame.HangarSaveLoad ??= FindObjectOfType<HangarSaveLoad>();
                break;
            case SceneEnum.CommandCenter:
                _commandCenterSaveGame.CommandCenterSaveLoad ??= FindObjectOfType<CommandCenterSaveLoad>();
                break;
            case SceneEnum.World:
                _worldCenterSaveGame.WorldSaveLoad ??= FindObjectOfType<WorldSaveLoad>();
                break;
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnLoadScene -= LoadSceneAsynchronously;
    }
}
