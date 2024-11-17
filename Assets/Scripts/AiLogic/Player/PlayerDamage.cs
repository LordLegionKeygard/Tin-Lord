using UnityEngine;
using Zenject;

public class PlayerDamage : BaseDamage
{
    [Inject] readonly BulletsPool _bulletsPool;
    private PlayerLevel _playerLevel;
    [SerializeField] private BulletEnum _bulletType;
    [SerializeField] private Transform[] _firePoints;

    public override void Awake()
    {
        base.Awake();
        _playerLevel = GetComponent<PlayerLevel>();
    }

    public override void SetDamage()
    {
        Damage = _playerLevel.GetPlayerLevelInformation().PhysAttack[_playerLevel.GetLevel()];
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
