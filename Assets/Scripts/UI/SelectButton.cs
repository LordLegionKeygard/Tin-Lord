using TMPro;
using UnityEngine;

public class SelectButton : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private TextMeshProUGUI _text;

    private void Awake()
    {
        CustomEvents.OnUpdateButtonToolTip += UpdateView;
    }

    public void UpdateView(float x, float y, string text)
    {
        transform.position = new Vector2(x, y);
        _text.text = text;

        // Получаем предпочтительную ширину текста
        Vector2 preferredValues = _text.GetPreferredValues();
        float preferredWidth = preferredValues.x;

        // Устанавливаем ширину RectTransform, сохраняя текущую высоту
        _rectTransform.sizeDelta = new Vector2(preferredWidth + 20, _rectTransform.sizeDelta.y);
    }

    private void OnDestroy()
    {
        CustomEvents.OnUpdateButtonToolTip -= UpdateView;
    }
}
