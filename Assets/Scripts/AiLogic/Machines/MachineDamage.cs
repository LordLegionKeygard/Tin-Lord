using UnityEngine;
using Zenject;

public class MachineDamage : BaseDamage
{
    [Inject] readonly BulletsPool _bulletsPool;
    [SerializeField] private BulletEnum[] _bulletTypes;
    [SerializeField] private Transform[] _firePoints;

    public override void Attack(int attackNumber)
    {  
        if (CurrentTargetBaseHealth == null) return;
        CurrentTargetBaseHealth.CalculateDamage(MachinesDataMission.Instance.GetCurrentMeleeDamage(), 0); 
    }

    public override void Shoot(int fireNumber)
    {
        if (BaseAttackVFX != null) BaseAttackVFX.PlayAttackVFX(fireNumber);

        var currentPoint = _firePoints[fireNumber];

        GameObject bullet = _bulletsPool.GetBullet(_bulletTypes[fireNumber]);
        bullet.transform.SetPositionAndRotation(currentPoint.position, currentPoint.rotation);

        if (bullet.TryGetComponent<Bullet>(out var bulletScript))
        {
            bulletScript.SetTarget(CurrentTargetBaseHealth, CurrentTargetTransform);
            bulletScript.SetDamage(MachinesDataMission.Instance.GetCurrentRangeDamage(), 5);
            bulletScript.SetBulletPool(_bulletsPool, _bulletTypes[fireNumber]);
        }
    }
}
