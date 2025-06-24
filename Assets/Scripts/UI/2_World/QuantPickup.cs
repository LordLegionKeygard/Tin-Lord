using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Zenject;
using DG.Tweening;

public class QuantPickup : MonoBehaviour, IPointerClickHandler
{
    [Inject] private WorldQuantSystem _quantSystem;

    [SerializeField] private Image _ring;
    [SerializeField] private float _amount = 0.25f;
    [SerializeField] private float _lifeTime = 6f;

    private Camera _camera;
    private QuantPickupPool _pool;
    private Vector3 _worldPos;
    private RectTransform _rect;
    private Tween _lifeTween;
    private Tween _spawnTween;

    public float GetTimeLeft() => _ring.fillAmount * _lifeTime;
    public Vector3 GetWorldPos() => _worldPos;

    private void Awake()
    {
        _camera = Camera.main;
        _rect = GetComponent<RectTransform>();
    }

    public void Initialize(QuantPickupPool pool, Vector3 worldPos, float timeLeft = -1f)
    {
        _pool = pool;
        _worldPos = worldPos;

        float remain = timeLeft < 0f ? _lifeTime : timeLeft;
        _ring.fillAmount = Mathf.Clamp01(remain / _lifeTime);

        gameObject.SetActive(true);
        UpdatePosition();

        // анимация «жизни» кольца
        _lifeTween?.Kill();
        _lifeTween = DOTween.To(() => _ring.fillAmount, x => _ring.fillAmount = x, 0f, remain)
                            .SetEase(Ease.Linear)
                            .SetLink(gameObject, LinkBehaviour.KillOnDisable)
                            .OnComplete(Despawn);

        // анимация «вылета» — ТОЛЬКО для новых монет
        if (timeLeft < 0f)
            PlaySpawnMotion();
    }

    private void PlaySpawnMotion()
    {
        _spawnTween?.Kill();

        Vector3 origin = _worldPos;

        // ─ горизонтальный разлёт: ± 3…6.6 ед.
        float dir = Random.value < 0.5f ? -1f : 1f;
        float distX = Random.Range(3f, 6.6f);
        float dx = dir * distX;
        Vector3 landing = origin + new Vector3(dx, -0.4f, 0f);

        // ─ высота дуги 4.5…8.4 ед.
        float arcHeight = Random.Range(4.5f, 8.4f);

        // ─ длительность: 0.9…1.5 с (чем дальше — тем дольше)
        float duration = Mathf.Lerp(0.9f, 1.5f,
                           Mathf.InverseLerp(3f, 6.6f, distX));

        _spawnTween = DOTween.To(() => 0f, t =>
        {
            float newX = Mathf.Lerp(origin.x, landing.x, t);

            // парабола 4t(1−t) × высота
            float newY = Mathf.Lerp(origin.y, landing.y, t) +
                         arcHeight * 4f * t * (1f - t);

            _worldPos = new Vector3(newX, newY, origin.z);
        },
        1f, duration).SetEase(Ease.Linear).SetLink(gameObject, LinkBehaviour.KillOnDisable);

        UpdatePosition();
    }

    private void Update() => UpdatePosition();

    private void UpdatePosition()
    {
        if (_camera == null) return;
        Vector2 screen = _camera.WorldToScreenPoint(_worldPos);
        _rect.position = screen;
    }

    public void OnPointerClick(PointerEventData _)
    {
        _lifeTween?.Kill();
        _spawnTween?.Kill();
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        _quantSystem.Add(_amount);
        Despawn();
    }

    private void Despawn()
    {
        _lifeTween?.Kill();
        _spawnTween?.Kill();
        _pool.Return(this);
    }

    private void OnDisable()
    {
        _lifeTween?.Kill();
        _spawnTween?.Kill();
    }
}
