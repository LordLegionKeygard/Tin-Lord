using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneLoader : MonoBehaviour
{
    [Inject] private readonly SpaceSaveGame _spaceSaveGame;
    [Inject] private readonly MissionSaveGame _missionCenterSaveGame;
    [Inject] private readonly HangarSaveGame _hangarSaveGame;
    [Inject] private SettingsSaveGame _settingsSaveGame;
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

        if (sceneEnum == SceneEnum.Space) _spaceSaveGame.SpaceSaveLoad.LoadGameData(ref _spaceSaveGame.SpaceSaveData);
        else if (sceneEnum == SceneEnum.Mission) _missionCenterSaveGame.MissionSaveLoad.LoadGameData(ref _missionCenterSaveGame.CurrentMissionSaveData);
        else if (sceneEnum == SceneEnum.Hangar) _hangarSaveGame.HangarSaveLoad.LoadGameData(ref _hangarSaveGame.HangarSaveData);
        _settingsSaveGame.SettingsSaveLoad.SetAllSettingsFromData();
    }

    private void CheckSaveLoad(SceneEnum sceneEnum)
    {
        switch (sceneEnum)
        {
            case SceneEnum.Hangar:
                _hangarSaveGame.HangarSaveLoad ??= FindObjectOfType<HangarSaveLoad>();
                break;
            case SceneEnum.Space:
                _spaceSaveGame.SpaceSaveLoad ??= FindObjectOfType<SpaceSaveLoad>();
                break;
            case SceneEnum.Mission:
                _missionCenterSaveGame.MissionSaveLoad ??= FindObjectOfType<MissionSaveLoad>();
                break;
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnLoadScene -= LoadSceneAsynchronously;
    }
}
