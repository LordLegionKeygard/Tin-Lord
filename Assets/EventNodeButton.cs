using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventNodeButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Button _button;

    public void Setup(string text, System.Action callback)
    {
        _text.text = text;
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => callback?.Invoke());
    }
}
