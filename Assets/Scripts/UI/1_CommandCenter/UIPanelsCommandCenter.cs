using UnityEngine;

public class UIPanelsCommandCenter : MonoBehaviour
{
    [SerializeField] private PanelDoMove _panelDoMove;
    [SerializeField] private EscapePanelCommandCenter _escapePanel;

    public void EscapeClick()
    {
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
