using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventNodeButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private Button _button;

    private void Awake() => _button = GetComponent<Button>();

    public void Setup(string text, System.Action callback)
    {
        _text.text = text;
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => callback?.Invoke());
    }
}
