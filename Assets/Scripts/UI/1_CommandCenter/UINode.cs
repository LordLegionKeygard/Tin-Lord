using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UINode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Graphics")]
    [SerializeField] private Image _icon;
    [SerializeField] private Image _availableOverlay;
    private NodeData _nodeData;
    private int _index;
    private MapSystem _mapSystem;
    private Color _defaultColor;

    // ---------------------------------------------------------------------    
    public void Setup(NodeData data, int index, MapSystem map)
    {
        _nodeData = data;
        _index = index;
        _mapSystem = map;

        _defaultColor = _nodeData.IconColor;
        _icon.sprite = _nodeData.Icon;
        _icon.color = _defaultColor;
        _icon.rectTransform.sizeDelta = new Vector2(_nodeData.IconWidth, _nodeData.IconHeight);

        SetAvailable(false);
        SetCompleted(false);
    }

    public void SelectNodeButton() => _mapSystem.TrySelectNode(_index);

    public void SetAvailable(bool value)
    {
        _availableOverlay.enabled = value;
        transform.localScale = value ? Vector3.one * 1.2f : Vector3.one;
    }

    public void SetCompleted(bool value)
    {
        _icon.color = value ? Color.green : _defaultColor;
    }

    public void OnPointerEnter(PointerEventData e)
    {
        _mapSystem.OnHoverNode(_index, true);
    }

    public void OnPointerExit(PointerEventData e)
    {
        _mapSystem.OnHoverNode(_index, false);
    }

    public void SetOnPointerColor(bool state)
    {
        _availableOverlay.color = state ? Color.green : Color.white;
    }
}
