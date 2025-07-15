using System.Collections;
using Pathfinding;
using UnityEngine;

public class BossHealth : BaseHealth
{
    private EnemyAnimator _enemyAnimator;
    private EnemyKnockBack _creatureKnockBackController;
    private AIPath _aiPath;
    private CharacterController _characterController;
    private EnemyLevel _enemyLevel;
    private BaseTakeDamageVFX _takeDamageVFX;
    private EnemyCenterPoint _enemyCenterPoint;
    private EnemyInfo _enemyInfo;

    public override float CalculateHealthFromPercent(int percent)
    {
        return 1;
    }

    public override Transform GetTransform()
    {
        return _enemyCenterPoint.GetTransform();
    }

    private void Awake()
    {
        _enemyLevel = GetComponent<EnemyLevel>();
        _creatureKnockBackController = GetComponent<EnemyKnockBack>();
        _characterController = GetComponent<CharacterController>();
        _takeDamageVFX = GetComponent<BaseTakeDamageVFX>();
        _aiPath = GetComponent<AIPath>();
        _enemyAnimator = GetComponent<EnemyAnimator>();
        _enemyCenterPoint = GetComponent<EnemyCenterPoint>();
        _enemyInfo = GetComponent<EnemyInfo>();
    }

    public override void CalculateDamage(float damage, float knockBackPoints)
    {
        if (!BossHealthSlider.Instance.SliderIsActive() && !_isDeath) BossHealthSlider.Instance.ActivateSlider(true);
        base.CalculateDamage(damage, knockBackPoints);
        _takeDamageVFX.SpawnTakeDamageVFX();
    }

    public void SetHealth()
    {
        _isDeath = false;
        _maxHealth = _enemyLevel.GetInformation().GetHealth(_enemyLevel.GetLevel());
        _currentHealth = _maxHealth;
        BossHealthSlider.Instance.SetMaxHealth(_maxHealth);
        BossHealthSlider.Instance.UpdateSliders(_currentHealth);
    }

    public override void LoadHealth(float newHealth)
    {
        _isDeath = false;
        _maxHealth = _enemyLevel.GetInformation().GetHealth(_enemyLevel.GetLevel());
        _currentHealth = newHealth;
        BossHealthSlider.Instance.SetMaxHealth(_maxHealth);
        BossHealthSlider.Instance.UpdateSliders(_currentHealth);
        if (!BossHealthSlider.Instance.SliderIsActive() && !_isDeath) BossHealthSlider.Instance.ActivateSlider(true);
    }

    public override void TakeDamage(float damage, float knockBackPoints)
    {
        if (IsDeath()) return;
        _currentHealth -= damage;
        BossHealthSlider.Instance.UpdateSliders(_currentHealth);
        _creatureKnockBackController.TakeKnockbackPoints(knockBackPoints);
        CheckDeath();
    }

    public override void Death()
    {
        BossHealthSlider.Instance.ActivateSlider(false);
        _isDeath = true;
        _characterController.enabled = false;
        _aiPath.enabled = false;
        _enemyAnimator.DeathAnim();
        DeathSound();
        CustomEvents.FireEnemyDeath(_enemyInfo.GetEnemyNumber());

        StartCoroutine(nameof(BossDeathEvent));
    }

    private void DeathSound()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.Death[(int)_enemyInfo.GetEnemyEnum()], transform.position);
    }

    private IEnumerator BossDeathEvent()
    {
        yield return new WaitForSeconds(5f);
        CustomEvents.FireObjectiveAmountChange(ObjectiveEnum.KillBoss, 1);
    }
}
