using UnityEngine;
using Zenject;

public class RobotDamage : BaseDamage
{
    [Inject] readonly BulletsPool _bulletsPool;
    private RobotLevel _robotLevel;
    [SerializeField] private BulletEnum _bulletType;
    [SerializeField] private Transform[] _firePoints;

    public override void Awake()
    {
        base.Awake();
        _robotLevel = GetComponent<RobotLevel>();
    }

    public override void SetDamage()
    {
        Damage = _robotLevel.GetRobotInformation().PhysAttack[_robotLevel.GetLevel()];
    }

    public override void Shoot(int attackNumber)
    {
        if (BaseAttackVFX != null) BaseAttackVFX.PlayVFX(attackNumber);

        var currentPoint = _firePoints.Length == 1 ? _firePoints[0] : _firePoints[attackNumber];

        GameObject bullet = _bulletsPool.GetBullet(_bulletType);
        bullet.transform.SetPositionAndRotation(currentPoint.position, currentPoint.rotation);

        if (bullet.TryGetComponent<Bullet>(out var bulletScript))
        {
            bulletScript.SetTarget(CurrentTargetBaseHealth);
            bulletScript.SetDamage(Damage, 0);
            bulletScript.SetBulletPool(_bulletsPool, _bulletType);
        }
    }
}
