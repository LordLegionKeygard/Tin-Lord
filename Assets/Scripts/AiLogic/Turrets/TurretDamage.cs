using UnityEngine;
using Zenject;

public class TurretDamage : BaseDamage
{
    [Inject] private readonly WorldHangarSystem _worldHangarSystem;
    [Inject] private readonly BulletsPool _pool;
    private TurretBuilding _turretBuilding;
    [SerializeField] private TurretAttackState _turretAttackState;
    [SerializeField] private BulletEnum _bulletType;
    [SerializeField] private Transform[] _firePoints;

    public override void Awake()
    {
        base.Awake();
        _turretBuilding = GetComponent<TurretBuilding>();
    }

    private void Start()
    {
        SetDamage();
    }

    public override void SetDamage()
    {
        Damage = _turretBuilding.Building().Damage * _worldHangarSystem.GetAimBotDamageBonus();
    }

    public override void Shoot(int attackNumber)
    {
        if (_bulletType == BulletEnum.None) return;
        
        if (_turretAttackState.AttackOneByOne())
        {
            _turretAttackState.ChangeAttackIndex();
        }

        if (BaseAttackVFX != null) BaseAttackVFX.PlayVFX(attackNumber);

        var currentPoint = _firePoints.Length == 1 ? _firePoints[0] : _firePoints[attackNumber];

        GameObject bullet = _pool.GetBullet(_bulletType);
        bullet.transform.SetPositionAndRotation(currentPoint.position, currentPoint.rotation);

        if (bullet.TryGetComponent<Bullet>(out var bulletScript))
        {
            bulletScript.SetTarget(CurrentTargetBaseHealth, CurrentTargetTransform);
            bulletScript.SetDamage(Damage, _turretBuilding.Building().KnockbackPoints);
            bulletScript.SetBulletPool(_pool, _bulletType);
        }
    }
}
