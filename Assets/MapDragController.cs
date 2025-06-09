using UnityEngine;
using UnityEngine.EventSystems;

public class MapDragController : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    [SerializeField] private RectTransform _contentTransform;
    [SerializeField] private float _minX = 0f;
    [SerializeField] private float _maxX = 2000f; // тут задаешь максимально вправо

    private Vector2 lastMousePosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastMousePosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.position - lastMousePosition;
        Vector2 newPosition = _contentTransform.anchoredPosition + new Vector2(delta.x, 0);
        newPosition.x = Mathf.Clamp(newPosition.x, -_maxX, _minX);
        _contentTransform.anchoredPosition = newPosition;
        lastMousePosition = eventData.position;
    }
}
