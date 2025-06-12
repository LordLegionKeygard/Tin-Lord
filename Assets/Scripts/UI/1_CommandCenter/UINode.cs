using UnityEngine;
using UnityEngine.UI;

public class UINode : MonoBehaviour
{
    [SerializeField] private Image _image;
    private NodeData _nodeData;

    public void Setup(NodeData data)
    {
        var rt = _image.rectTransform;

        _nodeData = data;
        _image.sprite = data.Icon;
        _image.color = data.IconColor;
        rt.sizeDelta = new Vector2(data.IconWidth, data.IconHeight);
    }
}
