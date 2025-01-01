using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneLoader : MonoBehaviour
{
    [Inject] private CommandCenterSaveGame _worldSaveGame;
    // [Inject] private WorldSaveSettings _worldSaveSettings;
    [SerializeField] private LoadingScreenController _loadingScreenController;
    public float LoadingProgress;

    private void Start()
    {
        CustomEvents.OnLoadScene += LoadSceneAsynchronously;
    }
    private void LoadSceneAsynchronously(SceneEnum sceneEnum, float timeInSec, bool isLoadData)
    {
        LoadingProgress = 0;
        _loadingScreenController.ScreenToggle(true);
        StartCoroutine(LoadWorldSceneAsynchronously(sceneEnum, timeInSec, isLoadData));
    }


    private IEnumerator LoadWorldSceneAsynchronously(SceneEnum sceneEnum, float timeInSec, bool isLoadData)
    {
        yield return new WaitForSeconds(timeInSec);
        if (_worldSaveGame.SaveLoad == null && isLoadData)
            _worldSaveGame.SaveLoad = FindObjectOfType<CommandCenterSaveLoad>();

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync((int)sceneEnum);


        while (!loadOperation.isDone)
        {
            LoadingProgress = Mathf.Clamp01(loadOperation.progress / 0.9f) + 0.5f;

            yield return null;
        }
        System.Threading.Tasks.Task task = _loadingScreenController.CheckCurrentScene();

        if (isLoadData)
        {
            _worldSaveGame.SaveLoad.LoadData(ref _worldSaveGame.CommandCenterSaveData);
            // _worldSaveSettings.SaveLoadSettings.SetAllSettingsFromData();
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnLoadScene -= LoadSceneAsynchronously;
    }
}
