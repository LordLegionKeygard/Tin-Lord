using UnityEngine;

public class UIPanelsCommandCenter : MonoBehaviour
{
    [SerializeField] private PanelDoMoveX _buildingsPanelDoMove;
    [SerializeField] private PanelDoMoveX _missionPanelDoMove;
    [SerializeField] private PanelDoMoveY _buildingInfoPanel;

    [SerializeField] private EscapePanelCommandCenter _escapePanel;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private LearnBuildingInfoPanel _learnBuildingInfoPanel;

    public void Click()
    {
        _buildingsPanelDoMove.PanelMove();
        _missionPanelDoMove.PanelMove();
        _buildingInfoPanel.PanelMove();

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
