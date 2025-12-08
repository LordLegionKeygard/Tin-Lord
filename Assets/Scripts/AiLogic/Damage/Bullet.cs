using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _model;
    [SerializeField] private float _destroyTimeAfterHit = 1.5f;
    protected float _damage;
    protected float _knockbackPoints;
    private BulletsPool _bulletsPool;
    private BulletEnum _bulletEnum;
    protected BaseHealth _targetHealth;
    protected Transform _targetTransform;
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

    public void SetBulletPool(BulletsPool bulletsPool, BulletEnum type)
    {
        _bulletsPool = bulletsPool;
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

        if (_destroyTimeAfterHit > 0)
        {
            if (_model != null) _model.SetActive(false);
            StartCoroutine(nameof(ReturnBulletCoroutine));
        }
        else _bulletsPool.ReturnBullet(_bulletEnum, gameObject);
    }

    private IEnumerator ReturnBulletCoroutine()
    {
        float elapsedTime = 0;

        while (elapsedTime < _destroyTimeAfterHit)
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
    Any_GunBullet = 0,
    Any_BattleShipTowerBullet = 1,
    Any_BallistaBolt = 2,
    Any_CannonBall = 3,
    CityRobot_SniperBullet = 4,
    Any_HowitzerBullet = 5,
    Turret_Rocket = 6,
    CityRobot_TankBullet = 7,
    Any_BeetleBullet = 8,
    Any_IceCrystalBullet = 9,
    Machine_WarBallistaBolt = 10,
    Machine_TankBullet = 11,
    Machine_MechaRocket = 12,
    ShipWeapon_SteelRiffleBullet = 13,
    ShipWeapon_TitaniumRocketLauncherBullet = 14,
    Enemy_GiantRocket = 15,
    Enemy_MediumRocket = 16,
    Any_BigIceCrystalBullet = 17,
    Any_SporesBullet = 18,
    CityRobot_ArbalesterBolt = 19,
    ShipWeapon_ScatterShotgunBullet = 20,
    ShipWeapon_LongshotRailgun = 21,
    ShipWeapon_BreakshotMinigunBullet = 22,
}

