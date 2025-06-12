using UnityEngine;
using UnityEngine.UI;

public class UINode : MonoBehaviour
{
    [Header("Graphics")]
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _availableOverlay;   // «можно перейти»
    [SerializeField] private GameObject _completedOverlay;   // «пройдено»

    private NodeData _nodeData;
    private int _index;
    private MapSystem _mapSystem;

    public void Setup(NodeData data, int index, MapSystem map)
    {
        _nodeData = data;
        _index = index;
        _mapSystem = map;

        _icon.sprite = data.Icon;
        _icon.color = data.IconColor;
        _icon.rectTransform.sizeDelta =
            new Vector2(data.IconWidth, data.IconHeight);

        SetAvailable(false);
        SetCompleted(false);
    }

    public void SelectNode()
    {
        _mapSystem.TrySelectNode(_index);
    }

    // --- визуальные состояния -------------------------------------------------
    public void SetAvailable(bool value)
    {
        _availableOverlay.SetActive(value);
        transform.localScale = value ? Vector3.one * 1.2f : Vector3.one;
    }

    public void SetCompleted(bool value)
    {
        _completedOverlay.SetActive(value);
    }
}
