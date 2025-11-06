using System.Collections;
using UnityEngine;
using Zenject;

public class EnemyDamage : BaseDamage
{
    [Inject] private readonly DiContainer _diContainer;
    [Inject] readonly BulletsPool _bulletsPool;
    [Inject] readonly DeathExplosionPool _explosionPool;
    [SerializeField] private BulletEnum _bulletType;
    [SerializeField] private DeathExplosionEnum _deathExplosionType;
    [SerializeField] private Transform[] _firePoints;
    [SerializeField] private CreatureSkillsWrapper[] _creatureSkillsWrapper;
    private EnemyLevel _enemyLevel;
    private EnemyInfo _enemyInfo;
    private EnemyHealth _enemyHealth;

    public override void Awake()
    {
        base.Awake();
        _enemyLevel = GetComponent<EnemyLevel>();
        _enemyInfo = GetComponent<EnemyInfo>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    public override void UpdateDamage()
    {
        Damage = _enemyLevel.GetInformation().GetPhysAttack(_enemyLevel.GetLevel()) * _enemyInfo.GetDamageFactor();
    }

    public override void Shoot(int firePointNumber)
    {
        if (BaseAttackVFX != null) BaseAttackVFX.PlayAttackVFX(firePointNumber);

        var currentPoint = _firePoints[firePointNumber];

        GameObject bullet = _bulletsPool.GetBullet(_bulletType);
        bullet.transform.SetPositionAndRotation(currentPoint.position, currentPoint.rotation);

        if (bullet.TryGetComponent<Bullet>(out var bulletScript))
        {
            bulletScript.SetTarget(CurrentTargetBaseHealth, CurrentTargetTransform);
            bulletScript.SetDamage(Damage, 0);
            bulletScript.SetBulletPool(_bulletsPool, _bulletType);
        }
    }

    public void Explosion(int firePointNumber)
    {
        var currentPoint = _firePoints[firePointNumber];

        var explosion = _explosionPool.GetDeathExplosion(_deathExplosionType);
        explosion.transform.SetPositionAndRotation(currentPoint.position, currentPoint.rotation);

        if (explosion.TryGetComponent<DeathExplosion>(out var deathExplosionScript))
        {
            deathExplosionScript.Setup(Damage * WorldGameInfo.ExplosionDamageFactor, 0, _explosionPool, _deathExplosionType, _enemyHealth);
        }
    }

    public void UseSkill(int number)
    {
        var skillWrapper = _creatureSkillsWrapper[number];
        if (skillWrapper.ParticleSystems.Length != 0) ActiveParticles(skillWrapper);
        var skill = _diContainer.InstantiatePrefab(skillWrapper.SkillPrefab, skillWrapper.SkillPoint.position, Quaternion.identity, null);
        if (skillWrapper.InFirePoint)
        {
            skill.transform.SetParent(skillWrapper.SkillPoint);
            skill.transform.rotation = skillWrapper.SkillPoint.rotation;
        }
        var bossSkillTriggerStayDamage = skill.GetComponent<BaseSkillTriggerStayDamage>();
        if (bossSkillTriggerStayDamage != null) bossSkillTriggerStayDamage.SetDamage(Damage * skillWrapper.TriggerStayDamageFactor);
    }

    private void ActiveParticles(CreatureSkillsWrapper wrapper)
    {
        ChangeMaxParticles(wrapper, 1000);
        StartCoroutine(UnactiveParticles(wrapper));
    }

    private IEnumerator UnactiveParticles(CreatureSkillsWrapper wrapper)
    {
        yield return new WaitForSeconds(wrapper.ParticlesTimer);
        ChangeMaxParticles(wrapper, 0);
    }

    private void ChangeMaxParticles(CreatureSkillsWrapper wrapper, int number)
    {
        for (int i = 0; i < wrapper.ParticleSystems.Length; i++)
        {
            var main = wrapper.ParticleSystems[i].main;
            main.maxParticles = number;
        }
    }
}
