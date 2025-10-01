using System.Collections;
using UnityEngine;
using Zenject;

public class BossDamage : BaseDamage
{
    [Inject] private DiContainer _diContainer;
    [Inject] readonly BulletsPool _bulletsPool;
    [SerializeField] private BulletFromPoolWrapper _bulletFromPoolWrapper;
    [SerializeField] private CreatureSkillsWrapper[] _creatureSkillsWrapper;
    private EnemyLevel _enemyLevel;

    public override void Awake()
    {
        base.Awake();
        _enemyLevel = GetComponent<EnemyLevel>();
    }

    public override void SetDamage()
    {
        Damage = _enemyLevel.GetInformation().GetPhysAttack(_enemyLevel.GetLevel());
    }

    public override void Shoot(int firePointNumber)
    {
        if (BaseAttackVFX != null) BaseAttackVFX.PlayAttackVFX(firePointNumber);

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
        if (skillWrapper.ParticleSystems.Length != 0) ActiveParticles(skillWrapper);
        var skill = _diContainer.InstantiatePrefab(skillWrapper.SkillPrefab, skillWrapper.SkillPoint.position, Quaternion.identity, null);
        var bossSkillTriggerStayDamage = skill.GetComponent<BossSkillTriggerStayDamage>();
        if (bossSkillTriggerStayDamage != null) bossSkillTriggerStayDamage.SetDamage(Damage / WorldGameInfo.BossTriggetStayDamageFactor);
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
    public ParticleSystem[] ParticleSystems;
    public float ParticlesTimer;
}

