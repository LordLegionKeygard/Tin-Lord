using UnityEngine;

public class EndActSystem : MonoBehaviour
{
    [SerializeField] private EventNodePanel _eventPanel;
    [SerializeField] private UIPanelsSpace _panels;
    [SerializeField] private DialogueSequence[] _newActDialogues;

    public void PrepareOpenStartNewActDialoguePanel(int act)
    {
        if (act == 0) return; // это начало, там работает пролог систем


        _eventPanel.Open(_newActDialogues[act - 1]);
        _panels.EventPanelOpen();
    }
}
