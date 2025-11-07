using UnityEngine;

public class TextViewSpawner : MonoBehaviour
{
    [SerializeField] private ResourceView _resourceView;
    [SerializeField] private TextView _textView;
    [SerializeField] private Canvas _canvas;


    public void ShowAddResourceView(Vector3 pos, Sprite sprite, int amount)
    {
        var item = Instantiate(_resourceView, pos, Quaternion.identity);
        item.gameObject.transform.SetParent(_canvas.transform);
        item.Initialize(pos, sprite, amount);
    }

    public void ShowTextView(Vector3 pos, string text, Color color)
    {
        var item = Instantiate(_textView, pos, Quaternion.identity);
        item.gameObject.transform.SetParent(_canvas.transform);
        item.Initialize(pos, text, color);
    }
}
