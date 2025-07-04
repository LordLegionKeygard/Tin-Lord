using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class EventNodeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Button _button;

    public void Setup(string text, System.Action callback)
    {
        _text.text = text;
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => callback?.Invoke());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_button.interactable) _text.color = Colors.WarningYellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_button.interactable) _text.color = Colors.GreySix;
    }

    public void SetInteractable(bool value)
    {
        _button.interactable = value;
        _text.color = value ? Colors.GreySix : Color.black;
    }
}
