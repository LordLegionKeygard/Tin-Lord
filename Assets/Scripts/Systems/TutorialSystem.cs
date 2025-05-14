using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSystem : MonoBehaviour
{
    [SerializeField] private GameObject _tutorial;
    [SerializeField] private EscapePanelWorld _escapePanel;
    [SerializeField] private GameObject[] _chapters;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private TextMeshProUGUI[] _buttonTexts;
    private bool _needSound;

    private void OnEnable()
    {
        _needSound = true;
    }

    public void OpenTutorial()
    {
        _escapePanel.PanelViewToggle(false);
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
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
        _tutorial.SetActive(false);
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        _escapePanel.PanelViewToggle(true);
    }

    private void OnDisable()
    {
        _needSound = false;
        OpenChapter(0);
    }
}
