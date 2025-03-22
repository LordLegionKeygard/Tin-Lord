using UnityEngine;
using Zenject;

public class MachineDamage : BaseDamage
{
    [Inject] readonly BulletsPool _bulletsPool;
    [SerializeField] private BulletEnum _bulletType;
    [SerializeField] private Transform[] _firePoints;

    public override void Attack(int attackNumber)
    {  
        if (CurrentTargetBaseHealth == null) return;
        CurrentTargetBaseHealth.CalculateDamage(MachinesDataWorld.Instance.GetCurrentMeleeDamage(), 0); 
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
            bulletScript.SetDamage(MachinesDataWorld.Instance.GetCurrentRangeDamage(), 0);
            bulletScript.SetBulletPool(_bulletsPool, _bulletType);
        }
    }
}
