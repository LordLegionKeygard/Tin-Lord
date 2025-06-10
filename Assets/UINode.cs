using UnityEngine;
using UnityEngine.UI;

public class UINode : MonoBehaviour
{
    [SerializeField] private Image _image;
    private NodeData _nodeData;

    public void Setup(NodeData data)
    {
        _nodeData = data;
        _image.sprite = data.Icon;
    }

    public void OnClick()
    {
        Debug.Log($"Клик по ноду: {_nodeData.NodeType}");

        // Тут потом вставим переход к миссии или ивенту
        if (_nodeData is MissionNode missionNode)
        {
            Debug.Log("Миссия: " + missionNode.Landscape.MissionId);
        }
        else if (_nodeData is BossNode bossNode)
        {
            Debug.Log("Босс миссия: " + bossNode.Mission.MissionId);
        }
    }
}
