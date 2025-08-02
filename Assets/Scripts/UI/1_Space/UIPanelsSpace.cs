using UnityEngine;
using Zenject;

public class UIPanelsSpace : MonoBehaviour
{
    [Inject] private readonly TutorialSystem _tutorialSystem;

    [Header("DoMove")]
    [SerializeField] private PanelDoMoveX _buildingsPanelDoMove;
    [SerializeField] private PanelDoMoveY _mapPanelDoMove;
    [SerializeField] private PanelDoMoveY _buildingInfoPanelDoMove;
    [SerializeField] private PanelDoMoveX _missionPanelDoMove;
    [SerializeField] private PanelDoMoveX _mainResourcesPanelDoMove;
    [SerializeField] private PanelDoMoveY _resourceTraderPanelDoMove;
    [SerializeField] private PanelDoMoveY _skillTraderPanelDoMove;

    [Header("Other")]
    [SerializeField] private ResourceTraderPanel _resourceTraderPanel;
    [SerializeField] private SkillTraderPanel _skillTraderPanel;
    [SerializeField] private MapSystem _mapSystem;
    [SerializeField] private GameObject _eventPanel;
    [SerializeField] private EscapePanelSpace _escapePanel;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private LearnBuildingInfoPanel _learnBuildingInfoPanel;
    [SerializeField] private BuildingsLearnPanel _buildingsLearnPanel;

    public void LearnBuildingPanelToggle()
    {
        if (_tutorialSystem.GetTutorialStepEnum() < TutorialStepEnum.SpaceOpenLearningPanel) return;

        _buildingsPanelDoMove.PanelMove();
        _buildingInfoPanelDoMove.PanelMove(false);
        _buildingsLearnPanel.UnselectAllBuildingItems();

        if (_buildingsPanelDoMove.IsOpen()) _buildingsLearnPanel.ResetScrollPosition();
        if (_mainResourcesPanelDoMove.IsOpen()) _mainResourcesPanelDoMove.PanelMove();
        if (_missionPanelDoMove.IsOpen()) _missionPanelDoMove.PanelMove();
        if (_resourceTraderPanelDoMove.IsOpen()) _resourceTraderPanelDoMove.PanelMove();
        if (_skillTraderPanelDoMove.IsOpen()) _skillTraderPanelDoMove.PanelMove();
        if (!_buildingInfoPanelDoMove.IsOpen()) _learnBuildingInfoPanel.Reset();
    }

    public void MapPanelOpen()
    {
        if (_tutorialSystem.GetTutorialStepEnum() < TutorialStepEnum.SpaceOpenMap_5) return;
        if (_tutorialSystem.GetTutorialStepEnum() == TutorialStepEnum.SpaceStartMission_8 && _tutorialSystem.PanelIsActive()) return;

        if (_buildingsPanelDoMove.IsOpen()) LearnBuildingPanelToggle();
        if (_missionPanelDoMove.IsOpen()) _missionPanelDoMove.PanelMove();
        if (_mainResourcesPanelDoMove.IsOpen()) _mainResourcesPanelDoMove.PanelMove();
        if (_resourceTraderPanelDoMove.IsOpen()) _resourceTraderPanelDoMove.PanelMove();
        if (_skillTraderPanelDoMove.IsOpen()) _skillTraderPanelDoMove.PanelMove();
        if (!_mapPanelDoMove.IsOpen())
        {
            CustomEvents.FireCompleteTutorialStep(TutorialStepEnum.SpaceOpenMap_5);
            _mapSystem.FocusOnCurrentNode();
            _mapPanelDoMove.PanelMove();
        }
    }

    public void OpenTraderPanel(TraderKind traderKind)
    {
        if (_mapPanelDoMove.IsOpen()) _mapPanelDoMove.PanelMove(false);

        switch (traderKind)
        {
            case TraderKind.Resource:
                if (!_resourceTraderPanelDoMove.IsOpen())
                {
                    _resourceTraderPanel.ResetTraderPanel();
                    _resourceTraderPanelDoMove.PanelMove();
                }
                break;
            case TraderKind.Skill:
                if (!_skillTraderPanelDoMove.IsOpen())
                {
                    _skillTraderPanel.ResetTraderPanel();
                    _skillTraderPanelDoMove.PanelMove();
                }
                break;
        }
    }

    public void EscapeClick(bool emptyEscapeClick)
    {
        if (_canvasGroup.interactable == false) return;

        var tutorialStep = _tutorialSystem.GetTutorialStepEnum();

        CustomEvents.FireTooltipToggle(false, 0);
        if (_buildingsPanelDoMove.IsOpen()) LearnBuildingPanelToggle();
        else if (_mapPanelDoMove.IsOpen() && tutorialStep is not TutorialStepEnum.SpaceMapDescription_6 or TutorialStepEnum.SpaceSelectNode_7) _mapPanelDoMove.PanelMove();
        else if (_mainResourcesPanelDoMove.IsOpen()) _mainResourcesPanelDoMove.PanelMove();
        else if (_resourceTraderPanelDoMove.IsOpen()) _resourceTraderPanelDoMove.PanelMove();
        else if (_skillTraderPanelDoMove.IsOpen()) _skillTraderPanelDoMove.PanelMove();
        else
        {
            if (emptyEscapeClick) return;
            _escapePanel.PanelViewToggle();
        }
    }

    public void EventPanelOpen()
    {
        if (_mapPanelDoMove.IsOpen()) _mapPanelDoMove.PanelMove(false);
        _eventPanel.SetActive(true);
    }

    public void MissionPanelOpen(bool needSound)
    {
        if (_mapPanelDoMove.IsOpen()) _mapPanelDoMove.PanelMove(needSound);
        if (!_missionPanelDoMove.IsOpen()) _missionPanelDoMove.PanelMove(needSound);
    }
}
