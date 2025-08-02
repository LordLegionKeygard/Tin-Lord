using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SpaceMainResourcePanel : MonoBehaviour
{
    [Inject] private TutorialSystem _tutorialSystem;
    [SerializeField] private PanelDoMoveX _panelDoMoveX;
    [SerializeField] private Button _button;

    public void PanelMove(bool needSound = true)
    {
        if (_tutorialSystem.GetTutorialStepEnum() < TutorialStepEnum.SpaceOpenResourcePanel_3
        || _tutorialSystem.GetTutorialStepEnum() == TutorialStepEnum.SpaceMapDescription_6
        || _tutorialSystem.GetTutorialStepEnum() == TutorialStepEnum.SpaceSelectNode_7) return;

        _panelDoMoveX.PanelMove(needSound);
        CustomEvents.FireCompleteTutorialStep(TutorialStepEnum.SpaceOpenResourcePanel_3);
    }
}
