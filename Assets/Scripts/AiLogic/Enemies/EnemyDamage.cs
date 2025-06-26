using UnityEngine;
using Zenject;

public class EnemyDamage : BaseDamage
{
    [Inject] readonly BulletsPool _bulletsPool;
    [SerializeField] private BulletEnum _bulletType;
    [SerializeField] private Transform[] _firePoints;
    private EnemyLevel _enemyLevel;
    private EnemyInfo _enemyInfo;

    public override void Awake()
    {
        base.Awake();
        _enemyLevel = GetComponent<EnemyLevel>();
        _enemyInfo = GetComponent<EnemyInfo>();
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
}
