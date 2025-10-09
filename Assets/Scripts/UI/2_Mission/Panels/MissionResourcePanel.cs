using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MissionResourcePanel : MonoBehaviour
{
    [Inject] private readonly EscapePanelMission _escapePanel;
    [Inject] private readonly MissionModeSystem _missionModeSystem;
    [Inject] private readonly TutorialSystem _tutorialSystem;
    [SerializeField] private PanelDoMoveX _panelDoMoveX;
    [SerializeField] private Button _button;

    private void Start()
    {
        CustomEvents.OnStartTutorialStep += OpenOrClosePanel;
    }

    public void PanelMove(bool needSound = true)
    {
        if (!_missionModeSystem.IsPlanetMode() || _escapePanel.IsEscapeMode()) return;

        if (!_tutorialSystem.IsCompleteMissionTutorial())
        {
            CustomEvents.FireCompleteTutorialStep(TutorialStepEnum.MissionOpenResourcePanel_19);

            if (_tutorialSystem.GetTutorialStepEnum() < TutorialStepEnum.MissionOpenResourcePanel_19) return;
        }

        _panelDoMoveX.PanelMove(needSound);

    }

    public void PanelClose()
    {
        _panelDoMoveX.PanelClose();
    }

    private void OpenOrClosePanel(TutorialStepEnum tutorialStepEnum)
    {
        if (tutorialStepEnum == TutorialStepEnum.MissionAddCardsDescription_29)
        {
            _panelDoMoveX.PanelClose();
        }

        if (tutorialStepEnum is TutorialStepEnum.MissionEnergyBeamDescription_44 or TutorialStepEnum.MissionOpenSkillsPanel_51)
        {
            if (_panelDoMoveX.IsOpen()) PanelMove(false);
        }

        if (tutorialStepEnum == TutorialStepEnum.MissionEnergyBeamDescription_44)
        {
            if (!_panelDoMoveX.IsOpen()) PanelMove(false);
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnStartTutorialStep -= OpenOrClosePanel;
    }
}
