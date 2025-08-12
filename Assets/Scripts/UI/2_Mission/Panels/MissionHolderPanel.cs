using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MissionHolderPanel : MonoBehaviour
{
    [Inject] private readonly MissionModeSystem _missionModeSystem;
    [Inject] TutorialSystem _tutorialSystem;
    [SerializeField] private DownPanelDoMoveX _panelDoMoveX;
    [SerializeField] private Button _button;


    public void PanelMove()
    {
        if (!_missionModeSystem.IsPlanetMode()) return;

        if (!_tutorialSystem.IsCompleteMissionTutorial() && _tutorialSystem.GetTutorialStepEnum() < TutorialStepEnum.MissionOpenSkillsPanel_51) return;

        CustomEvents.FireCompleteTutorialStep(TutorialStepEnum.MissionOpenSkillsPanel_51);
        _panelDoMoveX.PanelMove();
    }

    public void PanelClose()
    {
        _panelDoMoveX.PanelClose();
    }
}
