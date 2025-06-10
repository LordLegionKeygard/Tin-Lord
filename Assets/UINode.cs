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
}
