using UnityEngine;
using Zenject;

public class TurretDamage : BaseDamage
{
    [Inject] readonly BulletsPool _bulletsPool;
    private TurretBuilding _turretBuilding;
    [SerializeField] private TurretAttackState _turretAttackState;
    [SerializeField] private BulletEnum _bulletType;
    [SerializeField] private Transform[] _firePoints;

    public override void Awake()
    {
        base.Awake();
        _turretBuilding = GetComponent<TurretBuilding>();
    }

    public override void Attack(int attackNumber)
    {
        if (_turretAttackState.AttackOneByOne())
        {
            _turretAttackState.ChangeAttackIndex();
        }

        if (BaseAttackVFX != null) BaseAttackVFX.PlayVFX(attackNumber);

        var currentPoint = _firePoints.Length == 1 ? _firePoints[0] : _firePoints[attackNumber];

        GameObject bullet = _bulletsPool.GetBullet(_bulletType);
        bullet.transform.SetPositionAndRotation(currentPoint.position, currentPoint.rotation);

        if (bullet.TryGetComponent<Bullet>(out var bulletScript))
        {
            bulletScript.SetTarget(CurrentTargetBaseHealth);
            bulletScript.SetDamage(Damage, _turretBuilding.Building().KnockbackPoints);
            bulletScript.SetBulletPool(_bulletsPool, _bulletType);
        }
    }
}
