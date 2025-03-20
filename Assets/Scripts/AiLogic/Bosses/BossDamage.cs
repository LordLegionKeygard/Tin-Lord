using UnityEngine;
using Zenject;

public class BossDamage : BaseDamage
{
    [Inject] private DiContainer _diContainer;
    [Inject] readonly BulletsPool _bulletsPool;
    [SerializeField] private float _bossDamage;
    [SerializeField] private BulletFromPoolWrapper _bulletFromPoolWrapper;
    [SerializeField] private CreatureSkillsWrapper[] _creatureSkillsWrapper;


    public override void SetDamage()
    {
        Damage = _bossDamage;
    }

    public override void Shoot(int firePointNumber)
    {
        if (BaseAttackVFX != null) BaseAttackVFX.PlayVFX(firePointNumber);

        var currentPoint = _bulletFromPoolWrapper.FirePoints[firePointNumber];

        GameObject bullet = _bulletsPool.GetBullet(_bulletFromPoolWrapper.BulletType);
        bullet.transform.SetPositionAndRotation(currentPoint.position, currentPoint.rotation);

        if (bullet.TryGetComponent<Bullet>(out var bulletScript))
        {
            bulletScript.SetTarget(CurrentTargetBaseHealth, CurrentTargetTransform);
            bulletScript.SetDamage(Damage, 0);
            bulletScript.SetBulletPool(_bulletsPool, _bulletFromPoolWrapper.BulletType);
        }
    }

    public void UseSkill(int number)
    {
        var skillWrapper = _creatureSkillsWrapper[number];
        var skill = _diContainer.InstantiatePrefab(skillWrapper.SkillPrefab, skillWrapper.SkillPoint.position, Quaternion.identity, null);
    }
}

[System.Serializable]
public class BulletFromPoolWrapper
{
    public BulletEnum BulletType;
    public Transform[] FirePoints;
}

[System.Serializable]
public class CreatureSkillsWrapper
{
    public GameObject SkillPrefab;
    public Transform SkillPoint;
}

