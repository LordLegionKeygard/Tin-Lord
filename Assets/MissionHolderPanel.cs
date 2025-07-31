using UnityEngine;
using UnityEngine.UI;

public class MissionHolderPanel : MonoBehaviour
{
    [SerializeField] private DownPanelDoMoveX _panelDoMoveX;
    [SerializeField] private Button _button;

    private void Start()
    {
        CustomEvents.OnStartTutorialStep += ClosePanel;
    }

    public void PanelMove()
    {
        if (_button.enabled == false) return;
        _panelDoMoveX.PanelMove();
    }

    private void ClosePanel(TutorialStepEnum tutorialStepEnum)
    {
        if (tutorialStepEnum == TutorialStepEnum.MissionAfterBaseSetStartTimer_23)
        {
            _panelDoMoveX.PanelClose();
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnStartTutorialStep -= ClosePanel;
    }
}
