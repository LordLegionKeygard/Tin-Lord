using UnityEngine;
using Zenject;

public class RobotDamage : BaseDamage
{
    [Inject] readonly BulletsPool _bulletsPool;
    [SerializeField] private BulletEnum _bulletType;
    [SerializeField] private Transform[] _firePoints;

    public override void Attack(int attackNumber)
    {  
        if (CurrentTargetBaseHealth == null) return;
        CurrentTargetBaseHealth.CalculateDamage(RobotsDataWorld.Instance.GetCurrentMeleeDamage(), 0); 
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
            bulletScript.SetDamage(RobotsDataWorld.Instance.GetCurrentRangeDamage(), 0);
            bulletScript.SetBulletPool(_bulletsPool, _bulletType);
        }
    }
}
