using System.Collections;
using UnityEngine;
using Zenject;

public class BossDamage : BaseDamage
{
    [Inject] private readonly DiContainer _diContainer;
    [Inject] readonly BulletsPool _bulletsPool;
    [SerializeField] private float _defaultDamage;
    [SerializeField] private float _shootDamage;
    [SerializeField] private BulletFromPoolWrapper _bulletFromPoolWrapper;
    [SerializeField] private CreatureSkillsWrapper[] _creatureSkillsWrapper;
    private EnemyAttacks _attacks;
    private EnemyStateChanger _stateChanger;

    public override void Awake()
    {
        base.Awake();
        _attacks = GetComponent<EnemyAttacks>();
        _stateChanger = GetComponent<EnemyStateChanger>();
    }

    public override void UpdateDamage()
    {
        Damage = _defaultDamage;
    }

    public override void Attack(int firePointNumber)
    {
        if (_stateChanger.DistanceToTarget() > _attacks.MaxAttackRange())
        {
            //иногда босс делает несколько атак в 1 анимации и после уничтожения здания сразу меняет цель
            //делаем возврат если дистанция до нового здания больше дистанция атаки
            return;
        }
        base.Attack(firePointNumber);
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
            bulletScript.SetDamage(_shootDamage, 0);
            bulletScript.SetBulletPool(_bulletsPool, _bulletFromPoolWrapper.BulletType);
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

[System.Serializable]
public class BulletFromPoolWrapper
{
    public BulletEnum BulletType;
    public Transform[] FirePoints;
}

[System.Serializable]
public class CreatureSkillsWrapper
{
    public float TriggerStayDamageFactor;
    public GameObject SkillPrefab;
    public Transform SkillPoint;
    public bool InFirePoint;
    public ParticleSystem[] ParticleSystems;
    public float ParticlesTimer;
}

