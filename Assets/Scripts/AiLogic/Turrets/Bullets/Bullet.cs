using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 80;
    public bool _needTimeAfterHit;
    private Transform _targetTransform;
    private float _damage;
    private int _knockbackPoints;
    private BulletsPool _bulletsPool;
    private BulletEnum _bulletEnum;
    private BaseHealth _targetHealth;
    private float _duration = 0.5f;
    private bool _isHitTarget;

    private void OnEnable()
    {
        _isHitTarget = false;
    }

    public void SetTarget(BaseHealth targetHealth)
    {
        _targetHealth = targetHealth;
        if (_targetHealth != null)
        {
            _targetTransform = _targetHealth.transform;
        }
    }

    public void SetDamage(float damageAmount, int knockback)
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

        Vector3 direction = _targetTransform.position - transform.position;
        float distanceThisFrame = _speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame && !_isHitTarget)
        {
            _isHitTarget = true;
            HitTarget();
            return;
        }

        if (_isHitTarget) return;

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(_targetTransform);
    }

    private void HitTarget()
    {
        if (_targetHealth != null)
        {
            _targetHealth.CalculateDamage(_damage, _knockbackPoints);
        }

        if (_needTimeAfterHit)
        {
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
    }
}



[System.Serializable]
public enum BulletEnum
{
    None = -1,
    BasicX1 = 0,
    BasicX2 = 1,
    BallistaBolt = 2,
    CannonBall = 3,
    Robot_SniperRiffle_Bullet = 4,
    HowitzerBullet = 5,
}

