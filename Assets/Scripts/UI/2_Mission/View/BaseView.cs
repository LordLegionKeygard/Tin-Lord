using DG.Tweening;
using TMPro;
using UnityEngine;


public class BaseView : MonoBehaviour
{
    [Header("Refs")]
    public TextMeshProUGUI Text;
    public CanvasGroup CanvasGroup;
    public RectTransform Content;

    [Header("Anim")]
    private protected float _duration = 1.5f;
    private protected float _riseDistance = 80f;

    private protected Camera _mainCamera;
    private protected Vector3 _worldPos;
    private protected RectTransform _rect;
    private protected Sequence _seq;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _rect = GetComponent<RectTransform>();
    }

    private void OnDisable()
    {
        if (_seq != null && _seq.IsActive()) _seq.Kill();
        _seq = null;
    }

    private void LateUpdate()
    {
        if (_mainCamera == null) return;

        Vector3 screenPosition = _mainCamera.WorldToScreenPoint(_worldPos);
        _rect.position = screenPosition;
    }


    public void UpdatePosition()
    {
        if (_mainCamera == null) return;
        Vector2 screen = _mainCamera.WorldToScreenPoint(_worldPos);
        _rect.position = screen;
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }
}
