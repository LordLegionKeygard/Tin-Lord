using UnityEngine;

public class UIPanelsCommandCenter : MonoBehaviour
{
    [SerializeField] private PanelDoMove _panelDoMove;
    [SerializeField] private EscapePanelCommandCenter _escapePanel;
    [SerializeField] private CanvasGroup _canvasGroup;

    public void EscapeClick()
    {
        if(_canvasGroup.interactable == false) return;
        
        if (_panelDoMove.IsOpen())
        {
            _panelDoMove.PanelClose();
        }
        else
        {
            _escapePanel.PanelViewToggle();
        }
    }
}
