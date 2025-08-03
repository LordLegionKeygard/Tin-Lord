using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MissionHolderPanel : MonoBehaviour
{
    [Inject] TutorialSystem _tutorialSystem;
    [SerializeField] private DownPanelDoMoveX _panelDoMoveX;
    [SerializeField] private Button _button;


    public void PanelMove()
    {
        if (!_tutorialSystem.IsCompleteMissionTutorial() && _tutorialSystem.GetTutorialStepEnum() < TutorialStepEnum.MissionOpenSkillsPanel_51) return;

        CustomEvents.FireCompleteTutorialStep(TutorialStepEnum.MissionOpenSkillsPanel_51);
        _panelDoMoveX.PanelMove();
    }
}
