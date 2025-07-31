using UnityEngine;
using UnityEngine.UI;

public class MissionResourcePanel : MonoBehaviour
{
    [SerializeField] private PanelDoMoveX _panelDoMoveX;
    [SerializeField] private Button _button;

    private void Start()
    {
        CustomEvents.OnStartTutorialStep += ClosePanel;
    }

    public void PanelMove(bool needSound = true)
    {
        if (_button.enabled == false) return;
        _panelDoMoveX.PanelMove(needSound);
        CustomEvents.FireCompleteTutorialStep(TutorialStepEnum.MissionOpenResourcePanel_19);
    }

    private void ClosePanel(TutorialStepEnum tutorialStepEnum)
    {
        if (tutorialStepEnum == TutorialStepEnum.MissionAddCardsDescription_29)
        {
            _panelDoMoveX.PanelClose();
        }
    }

    private void OnDestroy()
    {

    }
}
