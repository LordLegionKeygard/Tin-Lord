using UnityEngine;

public class TutorialSystem : MonoBehaviour
{
    [SerializeField] private GameObject _tutorial;
    [SerializeField] private EscapePanelWorld _escapePanel;

    public void OpenTutorial()
    {
        _escapePanel.PanelViewToggle(false);
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        _tutorial.SetActive(true);
    }

    public void CloseTutorial()
    {
        _tutorial.SetActive(false);
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        _escapePanel.PanelViewToggle(true);
    }
}
