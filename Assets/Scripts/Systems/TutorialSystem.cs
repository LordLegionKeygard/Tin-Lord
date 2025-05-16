using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TutorialSystem : MonoBehaviour
{
    [Inject] private readonly CommandCenterSaveGame _commandCenterSaveGame;
    [SerializeField] private GameObject _tutorial;
    [SerializeField] private EscapePanelWorld _escapePanel;
    [SerializeField] private GameObject[] _chapters;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private TextMeshProUGUI[] _buttonTexts;
    private bool _needSound;
    private bool _isFirstOpen;

    private void OnEnable()
    {
        _needSound = false;
        OpenChapter(0);
        _needSound = true;
    }

    public void OpenTutorial(bool firstOpen)
    {
        _isFirstOpen = firstOpen;

        if (!_isFirstOpen)
        {
            _escapePanel.PanelViewToggle(false);
            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        }

        _tutorial.SetActive(true);
    }

    public void OpenChapter(int number)
    {
        if (_needSound) AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);

        foreach (var item in _chapters)
        {
            item.SetActive(false);
        }

        foreach (var item in _buttons)
        {
            item.interactable = true;
        }

        foreach (var item in _buttonTexts)
        {
            item.color = Colors.GreySix;
        }

        _chapters[number].SetActive(true);
        _buttons[number].interactable = false;
        _buttonTexts[number].color = Color.white;
    }

    public void CloseTutorial()
    {
        _commandCenterSaveGame.CompleteTutorial();
        _tutorial.SetActive(false);
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        if (!_isFirstOpen) _escapePanel.PanelViewToggle(true);
    }
}
