using TMPro;
using UnityEngine;

public class SelectSkill : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private TextMeshProUGUI _text;
    
    [Header("Tooltip Settings")]
    private float _maxWidth = 370f; // Максимальная ширина панели
    private float _padding = 30f;   // Отступ по краям текста

    private void Awake()
    {
        CustomEvents.OnUpdateToolTip += UpdateView;

        _text.enableWordWrapping = true;
        _text.overflowMode = TextOverflowModes.Overflow;

        _rectTransform.pivot = new Vector2(0f, 0f);

        _rectTransform.anchorMin = new Vector2(0f, 0f);
        _rectTransform.anchorMax = new Vector2(0f, 0f);
    }

    private void UpdateView(float x, float y, string text)
    {
        _text.text = text;

        Vector2 preferred = _text.GetPreferredValues(text, _maxWidth - _padding, 0f);
        float finalWidth  = Mathf.Min(preferred.x + _padding, _maxWidth);
        float finalHeight = preferred.y + _padding;

        _rectTransform.sizeDelta = new Vector2(finalWidth, finalHeight);
        
        transform.position = new Vector2(x, y);
    }

    private void OnDestroy()
    {
        CustomEvents.OnUpdateToolTip -= UpdateView;
    }
}
