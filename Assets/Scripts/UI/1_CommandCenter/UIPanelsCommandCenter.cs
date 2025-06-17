using UnityEngine;

public class UIPanelsCommandCenter : MonoBehaviour
{
    [SerializeField] private MapSystem _mapSystem;
    [SerializeField] private PanelDoMoveX _buildingsPanelDoMove;
    [SerializeField] private PanelDoMoveY _mapPanelDoMove;
    [SerializeField] private PanelDoMoveY _buildingInfoPanel;
    [SerializeField] private PanelDoMoveX _missionPanelDoMove;
    [SerializeField] private GameObject _eventPanel;
    [SerializeField] private EscapePanelCommandCenter _escapePanel;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private LearnBuildingInfoPanel _learnBuildingInfoPanel;
    [SerializeField] private BuildingsLearnPanel _buildingsLearnPanel;

    public void LearnBuildingPanelToggle(bool needSound = true)
    {
        _buildingsPanelDoMove.PanelMove();
        _buildingInfoPanel.PanelMove(false);

        if (_buildingsPanelDoMove.IsOpen())
        {
            _buildingsLearnPanel.ResetScrollPosition();
        }

        if (!_buildingInfoPanel.IsOpen())
        {
            _learnBuildingInfoPanel.Reset();
        }

        if (_missionPanelDoMove.IsOpen())
        {
            _missionPanelDoMove.PanelMove();
        }
    }

    public void MapPanelOpen()
    {
        if (_buildingsPanelDoMove.IsOpen())
        {
            LearnBuildingPanelToggle(false);
        }

        if (_missionPanelDoMove.IsOpen())
        {
            _missionPanelDoMove.PanelMove();
        }

        if (!_mapPanelDoMove.IsOpen())
        {
            _mapSystem.FocusOnCurrentNode();
            _mapPanelDoMove.PanelMove();
        }
    }

    public void EscapeClick(bool emptyEscapeClick)
    {
        if (_canvasGroup.interactable == false) return;

        CustomEvents.FireTooltipToggle(false, 0);
        if (_buildingsPanelDoMove.IsOpen())
        {
            LearnBuildingPanelToggle();
        }
        else if (_mapPanelDoMove.IsOpen())
        {
            _mapPanelDoMove.PanelMove();
        }
        else
        {
            if (emptyEscapeClick) return;

            _escapePanel.PanelViewToggle();
        }
    }

    public void EventPanelOpen()
    {
        // закрываем карту
        if (_mapPanelDoMove.IsOpen()) _mapPanelDoMove.PanelMove();

        _eventPanel.SetActive(true);
    }

    public void MissionPanelOpen()
    {
        // закрываем карту
        if (_mapPanelDoMove.IsOpen()) _mapPanelDoMove.PanelMove();

        // открываем миссию
        if (!_missionPanelDoMove.IsOpen()) _missionPanelDoMove.PanelMove();
    }
}
