using UnityEngine;
using Zenject;

public class CityRobotDamage : BaseDamage
{
    [Inject] private readonly BulletsPool _pool;
    [SerializeField] private BulletEnum _bulletType;
    [SerializeField] private Transform _firePoint;
    private CityRobotInformation _cityRobotInformation;

    public override void Awake()
    {
        base.Awake();
        _cityRobotInformation = GetComponent<CityRobotInformation>();
    }

    private void Start()
    {
        UpdateDamage();
    }

    public override void UpdateDamage()
    {
        Damage = _cityRobotInformation.GetCityRobotInfo().Damage;
    }

    public override void Shoot(int attackNumber)
    {
        if (_bulletType == BulletEnum.None) return;

        if (BaseAttackVFX != null) BaseAttackVFX.PlayAttackVFX(attackNumber);

        GameObject bullet = _pool.GetBullet(_bulletType);
        bullet.transform.SetPositionAndRotation(_firePoint.position, _firePoint.rotation);

        if (bullet.TryGetComponent<Bullet>(out var bulletScript))
        {
            bulletScript.SetTarget(CurrentTargetBaseHealth, CurrentTargetTransform);
            bulletScript.SetDamage(Damage, _cityRobotInformation.GetCityRobotInfo().KnockbackPoints);
            bulletScript.SetBulletPool(_pool, _bulletType);
        }
    }
}
