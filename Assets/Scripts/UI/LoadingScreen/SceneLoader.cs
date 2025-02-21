using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneLoader : MonoBehaviour
{
    [Inject] private readonly CommandCenterSaveGame _commandCenterSaveGame;
    [Inject] private readonly WorldSaveGame _worldCenterSaveGame;
    // [Inject] private WorldSaveSettings _worldSaveSettings;
    [SerializeField] private LoadingScreenController _loadingScreenController;

    private void Start()
    {
        CustomEvents.OnLoadScene += LoadSceneAsynchronously;
    }
    private void LoadSceneAsynchronously(SceneEnum sceneEnum, float timeInSec, bool isLoadData, int missionId)
    {
        _loadingScreenController.ShowLoadingScreen(missionId);
        if (isLoadData) CheckSaveLoad(sceneEnum);
        StartCoroutine(LoadScene(sceneEnum, timeInSec, isLoadData));
    }


    private IEnumerator LoadScene(SceneEnum sceneEnum, float timeInSec, bool isLoadData)
    {
        yield return new WaitForSecondsRealtime(timeInSec);

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync((int)sceneEnum);

        while (!loadOperation.isDone)
        {
            yield return null;
        }

        System.Threading.Tasks.Task task = _loadingScreenController.CheckCurrentScene();

        if (isLoadData)
        {
            if (sceneEnum == SceneEnum.CommandCenter) _commandCenterSaveGame.CommandCenterSaveLoad.LoadData(ref _commandCenterSaveGame.CommandCenterSaveData);
            else if (sceneEnum == SceneEnum.World) _worldCenterSaveGame.WorldSaveLoad.LoadMissionData(ref _worldCenterSaveGame.CurrentWorldSaveData);
            // _worldSaveSettings.SaveLoadSettings.SetAllSettingsFromData();
        }
    }

    private void CheckSaveLoad(SceneEnum sceneEnum)
    {
        if (sceneEnum == SceneEnum.CommandCenter)
        {
            if (_commandCenterSaveGame.CommandCenterSaveLoad == null)
                _commandCenterSaveGame.CommandCenterSaveLoad = FindObjectOfType<CommandCenterSaveLoad>();
        }
        else if (sceneEnum == SceneEnum.World)
        {
            if (_worldCenterSaveGame.WorldSaveLoad == null)
                _worldCenterSaveGame.WorldSaveLoad = FindObjectOfType<WorldSaveLoad>();
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnLoadScene -= LoadSceneAsynchronously;
    }
}
