using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject _model;
    [SerializeField] private float _speed = 80;
    public bool _needTimeAfterHit;
    protected float _damage;
    protected float _knockbackPoints;
    private BulletsPool _bulletsPool;
    private BulletEnum _bulletEnum;
    protected BaseHealth _targetHealth;
    private Transform _targetTransform;
    private float _duration = 0.5f;
    private bool _isHitTarget;
    private Camera _mainCamera;
    private Vector3 _cameraForwardNormalized;

    private void Start()
    {
        // Кэшируем камеру и её направление
        _mainCamera = Camera.main;
        _cameraForwardNormalized = _mainCamera.transform.forward;
        _cameraForwardNormalized.y = 0; // Убираем вертикальную составляющую
        _cameraForwardNormalized.Normalize();
    }

    private void OnEnable()
    {
        _isHitTarget = false;
    }

    public void SetTarget(BaseHealth targetHealth, Transform newTransform)
    {
        _targetHealth = targetHealth;
        if (_targetHealth != null)
        {
            _targetTransform = newTransform;
        }
    }

    public void SetDamage(float damageAmount, float knockback)
    {
        _damage = damageAmount;
        _knockbackPoints = knockback;
    }

    public void SetBulletPool(BulletsPool poolManager, BulletEnum type)
    {
        _bulletsPool = poolManager;
        _bulletEnum = type;
    }

    private void Update()
    {
        if (_targetTransform == null)
        {
            _bulletsPool.ReturnBullet(_bulletEnum, gameObject);
            return;
        }

        // Рассчитываем скорректированную позицию цели
        Vector3 adjustedTargetPosition = AdjustTargetPosition(_targetTransform.position);

        Vector3 direction = adjustedTargetPosition - transform.position;
        float distanceThisFrame = _speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame && !_isHitTarget)
        {
            _isHitTarget = true;
            HitTarget();
            return;
        }

        if (_isHitTarget) return;

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(adjustedTargetPosition);
    }

    private Vector3 AdjustTargetPosition(Vector3 targetPosition)
    {
        // Смещение по высоте
        targetPosition.y += WorldGameInfo.BulletHeightOffset;

        // Горизонтальное смещение на основе нормализованного направления камеры
        targetPosition += _cameraForwardNormalized * WorldGameInfo.BulletLateralOffset;

        return targetPosition;
    }

    public virtual void HitTarget()
    {
        if (_targetHealth != null)
        {
            _targetHealth.CalculateDamage(_damage, _knockbackPoints);
        }

        if (_needTimeAfterHit)
        {
            if (_model != null) _model.SetActive(false);
            StartCoroutine(nameof(ReturnBulletCoroutine));
        }
        else _bulletsPool.ReturnBullet(_bulletEnum, gameObject);
    }

    private IEnumerator ReturnBulletCoroutine()
    {
        float elapsedTime = 0;

        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _bulletsPool.ReturnBullet(_bulletEnum, gameObject);
        if (_model != null) _model.SetActive(true);
    }
}


[System.Serializable]
public enum BulletEnum
{
    None = -1,
    TurretGunBullet = 0,
    BattleShipTowerBullet = 1,
    BallistaBolt = 2,
    CannonBall = 3,
    SniperRiffleBullet = 4,
    HowitzerBullet = 5,
    Rocket = 6,
    RobotTankBullet = 7,
    BeetleProjectile = 8,
    IceCrystal = 9,

}

