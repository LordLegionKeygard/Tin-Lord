using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class LoadingScreenController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _loading;
    [SerializeField] private GameObject _loadingScreen;

    private void Start()
    {
        _loading.text = Language.TextStatic[30];
        CustomEvents.OnLoadingScreenToggle += ScreenToggle;
    }

    public void ScreenToggle(bool state)
    {
        _loadingScreen.SetActive(state);
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
                CustomEvents.FireFade(FadeType.FadeOut);
                ScreenToggle(false);
                break;
            case (int)SceneEnum.Planet:
                 //TO  вызываем FadeOut и ScreenToggle сами после спавна скорее всего
                break;
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnLoadingScreenToggle -= ScreenToggle;
    }
}
