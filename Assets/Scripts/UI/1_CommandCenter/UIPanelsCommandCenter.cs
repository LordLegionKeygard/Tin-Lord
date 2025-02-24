using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelsCommandCenter : MonoBehaviour
{
    [SerializeField] private PanelDoMoveX _buildingsPanelDoMove;
    [SerializeField] private PanelDoMoveX _missionPanelDoMove;
    [SerializeField] private PanelDoMoveY _buildingInfoPanel;

    [SerializeField] private EscapePanelCommandCenter _escapePanel;
    [SerializeField] private CanvasGroup _canvasGroup;

    public void Click()
    {
        _buildingsPanelDoMove.PanelMove();
        _missionPanelDoMove.PanelMove();
        _buildingInfoPanel.PanelMove();
    }

    public void EscapeClick()
    {
        if(_canvasGroup.interactable == false) return;
        
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
