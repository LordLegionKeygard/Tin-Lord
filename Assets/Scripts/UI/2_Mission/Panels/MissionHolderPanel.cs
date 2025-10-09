using UnityEngine;
using Zenject;

public class MissionHolderPanel : MonoBehaviour
{
    [Inject] private readonly EscapePanelMission _escapePanel;
    [Inject] private readonly MissionModeSystem _missionModeSystem;
    [Inject] private readonly TutorialSystem _tutorialSystem;
    [SerializeField] private DownPanelDoMoveX _panelDoMoveX;

    public void PanelMove()
    {
        if (!_missionModeSystem.IsPlanetMode() || _escapePanel.IsEscapeMode()) return;

        if (!_tutorialSystem.IsCompleteMissionTutorial() && _tutorialSystem.GetTutorialStepEnum() < TutorialStepEnum.MissionOpenSkillsPanel_51) return;

        CustomEvents.FireCompleteTutorialStep(TutorialStepEnum.MissionOpenSkillsPanel_51);
        _panelDoMoveX.PanelMove();
    }

    public void PanelClose()
    {
        _panelDoMoveX.PanelClose();
    }
}
