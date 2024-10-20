using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _targetTransform;
    private float _speed = 80;
    private float _damage;
    private int _knockbackPoints;
    private BulletsPool _bulletsPool;
    private BulletEnum _bulletEnum;
    private BaseHealth _targetHealth;

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

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    private void HitTarget()
    {
        if (_targetHealth != null)
        {
            _targetHealth.CalculateDamage(_damage, _knockbackPoints);
        }

        _bulletsPool.ReturnBullet(_bulletEnum, gameObject);
    }
}



[System.Serializable]
public enum BulletEnum
{
    BasicX1 = 0,
    BasicX2 = 1,
    BallistaBolt = 2,
    CannonBall = 3,
}

