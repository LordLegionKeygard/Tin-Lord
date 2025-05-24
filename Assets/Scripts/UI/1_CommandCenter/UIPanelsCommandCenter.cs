using UnityEngine;

public class UIPanelsCommandCenter : MonoBehaviour
{
    [SerializeField] private PanelDoMoveX _buildingsPanelDoMove;
    [SerializeField] private PanelDoMoveX _missionPanelDoMove;
    [SerializeField] private PanelDoMoveY _buildingInfoPanel;

    [SerializeField] private EscapePanelCommandCenter _escapePanel;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private LearnBuildingInfoPanel _learnBuildingInfoPanel;
    [SerializeField] private BuildingsLearnPanel _buildingsLearnPanel;

    public void Click()
    {
        _buildingsPanelDoMove.PanelMove(true);
        _missionPanelDoMove.PanelMove(false);
        _buildingInfoPanel.PanelMove(false);

        if(_buildingsPanelDoMove.IsOpen())
        {
            _buildingsLearnPanel.ResetScrollPosition();
        }

        if(!_buildingInfoPanel.IsOpen())
        {
            _learnBuildingInfoPanel.Reset();
        }
    }

    public void EscapeClick()
    {
        if(_canvasGroup.interactable == false) return;
        
        CustomEvents.FireTooltipToggle(false, 0);
        if (_buildingsPanelDoMove.IsOpen())
        {
            Click();
        }
        else
        {
            _escapePanel.PanelViewToggle();
        }
    }
}
