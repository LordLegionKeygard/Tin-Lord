using UnityEngine;
using UnityEngine.UI;

public class UINode : MonoBehaviour
{
    [Header("Graphics")]
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _availableOverlay;   // «можно перейти»
    //  _completedOverlay  БОЛЬШЕ НЕ НУЖЕН → поле удаляем

    private NodeData _nodeData;
    private int _index;
    private MapSystem _mapSystem;

    private Color _defaultColor;     // чтобы вернуть исходный цвет

    // ---------------------------------------------------------------------    
    public void Setup(NodeData data, int index, MapSystem map)
    {
        _nodeData = data;
        _index = index;
        _mapSystem = map;

        _defaultColor = data.IconColor;
        _icon.sprite = data.Icon;
        _icon.color = _defaultColor;
        _icon.rectTransform.sizeDelta =
            new Vector2(data.IconWidth, data.IconHeight);

        SetAvailable(false);
        SetCompleted(false);   // ← теперь просто вернёт базовый цвет
    }

    public void SelectNode() => _mapSystem.TrySelectNode(_index);

    // ---------------------------------------------------------------------
    public void SetAvailable(bool value)
    {
        _availableOverlay.SetActive(value);
        transform.localScale = value ? Vector3.one * 1.2f : Vector3.one;
    }

    /// <summary> Узел помечаем завершённым — красим саму иконку. </summary>
    public void SetCompleted(bool value)
    {
        _icon.color = value ? Color.green : _defaultColor;
        // ничего больше показывать не нужно
    }
}
