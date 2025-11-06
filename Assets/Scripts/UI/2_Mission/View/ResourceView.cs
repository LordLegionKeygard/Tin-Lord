using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResourceView : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private CanvasGroup _canvasGroup; // можно не указывать — добавится автоматически

    [Header("Anim")]
    [SerializeField] private float _duration = 1.5f;
    [SerializeField] private float _riseDistance = 80f;

    private Camera _camera;
    private ResourceViewPool _pool;
    private Vector3 _worldPos;
    private RectTransform _rect;

    private Sequence _seq;
    private bool _returned;

    private void Awake()
    {
        _camera = Camera.main;
        _rect = GetComponent<RectTransform>();
    }

    private void OnDisable()
    {
        if (_seq != null && _seq.IsActive()) _seq.Kill();
        _seq = null;
        _returned = false;
    }

    public void Initialize(ResourceViewPool pool, Vector3 worldPos, Sprite sprite, int amount)
    {
        _pool = pool;
        _worldPos = worldPos;
        _icon.sprite = sprite;
        _text.text = $"+{amount}";

        gameObject.SetActive(true);

        UpdatePosition();

        _rect.localScale = Vector3.one;
        _canvasGroup.alpha = 1f;

        var start = _rect.anchoredPosition;
        var end = start + new Vector2(0, _riseDistance);

        _seq = DOTween.Sequence()
            .Append(_rect.DOAnchorPos(end, _duration).SetEase(Ease.OutCubic))
            .Join(_rect.DOScale(0f, _duration).SetEase(Ease.InBack))
            .Join(_canvasGroup.DOFade(0f, _duration * 0.9f).SetEase(Ease.InSine))
            .OnComplete(Despawn).SetUpdate(true);
    }

    private void UpdatePosition()
    {
        if (_camera == null) return;
        Vector2 screen = _camera.WorldToScreenPoint(_worldPos);
        _rect.position = screen;
    }

    private void Despawn()
    {
        if (_returned) return;
        _returned = true;
        _pool.Return(this);
    }
}
