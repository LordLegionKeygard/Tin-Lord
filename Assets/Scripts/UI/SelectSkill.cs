using TMPro;
using UnityEngine;

public class SelectSkill : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private TextMeshProUGUI _text;


    private void Awake()
    {
        CustomEvents.OnUpdateSkillToolTip += UpdateView;

        _text.enableWordWrapping = true;
        _text.overflowMode = TextOverflowModes.Overflow;

        _rectTransform.pivot = new Vector2(0f, 0f);

        _rectTransform.anchorMin = new Vector2(0f, 0f);
        _rectTransform.anchorMax = new Vector2(0f, 0f);
    }

    private void UpdateView(float x, float y, int maxWidth, int padding, string text)
    {
        _text.text = text;

        Vector2 preferred = _text.GetPreferredValues(text, maxWidth - padding, 0f);
        float finalWidth  = Mathf.Min(preferred.x + padding, maxWidth);
        float finalHeight = preferred.y + padding;

        _rectTransform.sizeDelta = new Vector2(finalWidth, finalHeight);
        
        transform.position = new Vector2(x, y);
    }

    private void OnDestroy()
    {
        CustomEvents.OnUpdateSkillToolTip -= UpdateView;
    }
}
