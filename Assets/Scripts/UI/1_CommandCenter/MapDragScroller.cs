using UnityEngine;
using UnityEngine.EventSystems;

public class MapDragScroller : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private RectTransform _viewport;
    [SerializeField] private RectTransform _maskRect;
    public float MinX { get; private set; }
    public float MaxX { get; private set; }

    private Vector2 _dragStartPointer;
    private Vector2 _dragStartAnchPos;

    private void Awake()
    {
        RecalculateBounds();
        SetViewportPos(MinX);
    }


    public void RecalculateBounds()
    {
        float halfView = _viewport.rect.width * 0.5f;
        float halfMask = _maskRect.rect.width * 0.5f;

        MaxX = halfView - halfMask;
        MinX = -MaxX;
    }

    public void OnBeginDrag(PointerEventData e)
    {
        if (e.button != PointerEventData.InputButton.Left) return;

        _dragStartPointer = e.position;
        _dragStartAnchPos = _viewport.anchoredPosition;
    }

    public void OnDrag(PointerEventData e)
    {
        if (e.button != PointerEventData.InputButton.Left) return;

        float deltaX = e.position.x - _dragStartPointer.x;
        float newX = Mathf.Clamp(_dragStartAnchPos.x + deltaX, MinX, MaxX);

        SetViewportPos(newX);
    }

    private void SetViewportPos(float x)
    {
        _viewport.anchoredPosition = new Vector2(x, 0f);
    }

    public void JumpTo(RectTransform nodeRect)
    {
        if (nodeRect == null) return;

        float nodeLocalX = nodeRect.anchoredPosition.x;

        RecalculateBounds();
        float targetX = Mathf.Clamp(-nodeLocalX, MinX, MaxX);

        _viewport.anchoredPosition = new Vector2(targetX, 0f);
    }
}
