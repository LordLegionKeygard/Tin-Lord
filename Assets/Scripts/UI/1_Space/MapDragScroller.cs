using UnityEngine;
using UnityEngine.EventSystems;

public class MapDragScroller : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private RectTransform _viewport;   // подсвиживаемый контейнер
    [SerializeField] private RectTransform _maskRect;   // рамка-маска

    public float MinX { get; private set; }
    public float MaxX { get; private set; }

    // где внутри маски мы взяли точку касания
    private Vector2 _localPointerStart;
    // стартовая позиция viewport
    private Vector2 _viewportStartAnchPos;

    private void Awake()
    {
        if (_maskRect == null) _maskRect = (RectTransform)transform;
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

    // хук начала драга
    public void OnBeginDrag(PointerEventData e)
    {
        if (e.button != PointerEventData.InputButton.Left) return;

        // преобразуем экранную позицию в локальную внутри маски
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _maskRect, e.position, e.pressEventCamera, out _localPointerStart);

        _viewportStartAnchPos = _viewport.anchoredPosition;
    }

    // хук самого драга
    public void OnDrag(PointerEventData e)
    {
        if (e.button != PointerEventData.InputButton.Left) return;

        // текущая локальная позиция курсора внутри маски
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _maskRect, e.position, e.pressEventCamera, out Vector2 localPointerPos);

        // дельта локального смещения по X
        float deltaX = localPointerPos.x - _localPointerStart.x;

        // новая позиция viewport = стартовая + дельта, клэмпим
        float newX = Mathf.Clamp(_viewportStartAnchPos.x + deltaX, MinX, MaxX);
        SetViewportPos(newX);
    }

    private void SetViewportPos(float x)
    {
        _viewport.anchoredPosition = new Vector2(x, 0f);
    }

    public void JumpTo(RectTransform nodeRect)
    {
        if (nodeRect == null) return;
        RecalculateBounds();
        float targetX = Mathf.Clamp(-nodeRect.anchoredPosition.x, MinX, MaxX);
        SetViewportPos(targetX);
    }
}
