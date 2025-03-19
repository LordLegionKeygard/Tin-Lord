using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyDamage : BaseDamage
{
    [Inject] readonly BulletsPool _bulletsPool;
    private EnemyLevel _enemyLevel;
    [SerializeField] private BulletEnum _bulletType;
    [SerializeField] private Transform[] _firePoints;

    public override void Awake()
    {
        base.Awake();
        _enemyLevel = GetComponent<EnemyLevel>();
    }

    public override void SetDamage()
    {
        Damage = _enemyLevel.GetInformation().GetPhysAttack(_enemyLevel.GetLevel());
    }

    public override void Shoot(int attackNumber)
    {
        if (BaseAttackVFX != null) BaseAttackVFX.PlayVFX(attackNumber);

        var currentPoint = _firePoints[attackNumber];

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
