using UnityEngine;
using Zenject;

public class EnemyDamage : BaseDamage
{
    [Inject] readonly BulletsPool _bulletsPool;
    [Inject] readonly DeathExplosionPool _explosionPool;
    [SerializeField] private BulletEnum _bulletType;
    [SerializeField] private DeathExplosionEnum _deathExplosionType;
    [SerializeField] private Transform[] _firePoints;
    private EnemyLevel _enemyLevel;
    private EnemyInfo _enemyInfo;
    private EnemyHealth _enemyHealth;

    public override void Awake()
    {
        base.Awake();
        _enemyLevel = GetComponent<EnemyLevel>();
        _enemyInfo = GetComponent<EnemyInfo>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    public override void SetDamage()
    {
        Damage = _enemyLevel.GetInformation().GetPhysAttack(_enemyLevel.GetLevel()) * _enemyInfo.GetDamageFactor();
    }

    public override void Shoot(int firePointNumber)
    {
        if (BaseAttackVFX != null) BaseAttackVFX.PlayVFX(firePointNumber);

        var currentPoint = _firePoints[firePointNumber];

        GameObject bullet = _bulletsPool.GetBullet(_bulletType);
        bullet.transform.SetPositionAndRotation(currentPoint.position, currentPoint.rotation);

        if (bullet.TryGetComponent<Bullet>(out var bulletScript))
        {
            bulletScript.SetTarget(CurrentTargetBaseHealth, CurrentTargetTransform);
            bulletScript.SetDamage(Damage, 0);
            bulletScript.SetBulletPool(_bulletsPool, _bulletType);
        }
    }

    public void Explosion(int firePointNumber)
    {
        var currentPoint = _firePoints[firePointNumber];

        var explosion = _explosionPool.GetDeathExplosion(_deathExplosionType);
        explosion.transform.SetPositionAndRotation(currentPoint.position, currentPoint.rotation);

        if (explosion.TryGetComponent<DeathExplosion>(out var deathExplosionScript))
        {
            deathExplosionScript.Setup(Damage * WorldGameInfo.ExplosionDamageFactor, 0, _explosionPool, _deathExplosionType, _enemyHealth);
        }
    }
}
