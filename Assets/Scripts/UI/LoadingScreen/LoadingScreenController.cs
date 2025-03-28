using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;
using UnityEngine.UI;

public class LoadingScreenController : MonoBehaviour
{
    [Inject] private readonly CommandCenterSaveGame _commandCenterSaveGame;
    [SerializeField] private TextMeshProUGUI _loading;
    [SerializeField] private Image _loadingScreen;
    [SerializeField] private GameObject _blackScreen;
    [SerializeField] private Sprite[] _loadingScreenSprites;

    private void Start()
    {
        _loading.text = Language.TextStatic[30];
        CustomEvents.OnLoadingScreenToggle += ScreenToggle;
    }

    public void ShowLoadingScreen(int missionId)
    {
        _loadingScreen.sprite = _loadingScreenSprites[missionId + 1];     
        ScreenToggle(true);
    }

    public void ScreenToggle(bool state)
    {
        _loadingScreen.gameObject.SetActive(state);
        _blackScreen.gameObject.SetActive(state);
    }

    public async Task CheckCurrentScene()
    {
        await Task.Delay(2000);
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case (int)SceneEnum.MainMenu:
                CustomEvents.FireFade(FadeType.FadeOut);
                ScreenToggle(false);
                break;
            case (int)SceneEnum.CommandCenter:
                if (_commandCenterSaveGame.CommandCenterSaveData.NewGame)
                {
                    CustomEvents.FireFade(FadeType.FadeOutPrologue);
                }
                else
                {
                    CustomEvents.FireFade(FadeType.FadeOut);
                }
                ScreenToggle(false);
                break;
            case (int)SceneEnum.World:
                CustomEvents.FireFade(FadeType.FadeOut);
                ScreenToggle(false);
                break;
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnLoadingScreenToggle -= ScreenToggle;
    }
}
