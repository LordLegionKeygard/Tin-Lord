using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;
using UnityEngine.UI;

public class LoadingScreenController : MonoBehaviour
{
    [Inject] private readonly SpaceSaveGame _spaceSaveGame;
    [SerializeField] private TextMeshProUGUI _loading;
    [SerializeField] private Image _loadingScreen;
    [SerializeField] private GameObject _blackScreen;
    [SerializeField] private Sprite _defaultSprite;

    private void Start()
    {
        _loading.text = Language.TextStatic[30];
        CustomEvents.OnLoadingScreenToggle += ScreenToggle;
    }

    public void ShowLoadingScreen(Sprite sprite)
    {
        _loadingScreen.sprite = sprite == null ? _defaultSprite : sprite;
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
        CustomEvents.FireFade(FadeType.FadeOut);
        ScreenToggle(false);
    }

    private void OnDestroy()
    {
        CustomEvents.OnLoadingScreenToggle -= ScreenToggle;
    }
}
