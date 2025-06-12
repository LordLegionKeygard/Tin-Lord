using UnityEngine;

[CreateAssetMenu(fileName = "NodeData", menuName = "TinLord/Nodes/NodeData")]
public class NodeData : ScriptableObject
{
    public NodeType NodeType;
    public Sprite Icon;
    public Color IconColor;
    public int IconWidth;
    public int IconHeight;
}
