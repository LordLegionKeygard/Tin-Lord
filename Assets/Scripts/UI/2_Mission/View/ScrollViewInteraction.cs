using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollViewInteraction : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private bool _isScrolling;

    public bool IsScrolling() => _isScrolling;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isScrolling = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isScrolling = false;
    }
}
