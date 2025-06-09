using UnityEngine;

[CreateAssetMenu(fileName = "NodeData", menuName = "TinLord/Nodes/NodeData")]
public class NodeData : ScriptableObject
{
    public NodeType NodeType;
    public Sprite Icon;
}

[System.Serializable]
public enum NodeType
{
    Mission,
    Event,
    Trader,
    Boss
}
