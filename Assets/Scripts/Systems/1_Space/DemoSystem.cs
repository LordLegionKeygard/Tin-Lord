using System.Linq;
using UnityEngine;

public class DemoSystem : MonoBehaviour
{
    [SerializeField] private EventNodePanel _eventPanel;
    [SerializeField] private UIPanelsSpace _panels;
    [SerializeField] private DialogueSequence _demoDialogue;
    [SerializeField] private EscapePanelSpace _escapePanelSpace;

    public void LoadDemo(SpaceSaveData saveData)
    {
        if (!WorldGameInfo.IsDemo) return;

        var missionsCompleted = saveData.Map.Nodes.Count(n => n.IsCompleted && n.NodeType == NodeType.Mission);

        if (missionsCompleted >= 2)
        {
            _eventPanel.Open(_demoDialogue, OnDemoFinished);
            _panels.EventPanelOpen();
        }
    }

    private void OnDemoFinished()
    {
        _escapePanelSpace.MenuButton();
    }
}
