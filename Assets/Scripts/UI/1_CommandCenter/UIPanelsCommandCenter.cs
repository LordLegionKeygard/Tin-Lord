using UnityEngine;

public class UIPanelsCommandCenter : MonoBehaviour
{
    [SerializeField] private PanelDoMoveX _buildingsPanelDoMove;
    [SerializeField] private PanelDoMoveY _mapPanelDoMove;
    [SerializeField] private PanelDoMoveY _buildingInfoPanel;
    [SerializeField] private PanelDoMoveX _missionPanelDoMove;

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
    }

    public void MapPanelToggle()
    {
        if (_buildingsPanelDoMove.IsOpen())
        {
            LearnBuildingPanelToggle(false);
        }

        if (!_mapPanelDoMove.IsOpen())
        {
            _mapPanelDoMove.PanelMove();
        }
    }

    public void EscapeClick()
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
            _escapePanel.PanelViewToggle();
        }
    }

    public void MissionPanelToggle()
    {
        // закрываем карту
        if (_mapPanelDoMove.IsOpen())
            _mapPanelDoMove.PanelMove();

        // открываем миссию
        if (!_missionPanelDoMove.IsOpen())
            _missionPanelDoMove.PanelMove();
    }
}
