using UnityEngine;
using Pathfinding;
using Zenject;
using System.Collections;


public class EnemyHealth : BaseHealth
{
    [Inject] private readonly DiContainer _diContainer;
    [Inject] private readonly EnemyDefenceSystem _enemyDefenceSystem;
    [Inject] private readonly HealthCanvas _healthCanvas;
    // [Inject] private readonly QuantPickupPool _quantPool;
    [Inject] private MissionQuantSystem _quantSystem;
    [SerializeField] private GameObject _healthSliderPrefab;
    [SerializeField] private float _sliderHeightOffset;
    private EnemyAnimator _enemyAnimator;
    private EnemyKnockBack _creatureKnockBackController;
    private AIPath _aiPath;
    private CharacterController _characterController;
    private EnemyLevel _enemyLevel;
    private BaseTakeDamageVFX _takeDamageVFX;
    private EnemyCenterPoint _enemyCenterPoint;
    private EnemyInfo _enemyInfo;
    private AnimationToRagdoll _animationToRagdoll;

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
        _animationToRagdoll = GetComponent<AnimationToRagdoll>();
    }

    public override void CalculateDamage(float damage, float knockBackPoints)
    {
        base.CalculateDamage(damage, knockBackPoints);
        _takeDamageVFX.SpawnTakeDamageVFX();
    }

    private void CreateHealthBar()
    {
        if (_healthSliderObject == null)
        {
            var isMiniBoss = _enemyInfo.GetHealthFactor() > 1 || _enemyInfo.GetDamageFactor() > 1;
            var sliderHeightOfsset = isMiniBoss ? _sliderHeightOffset * WorldGameInfo.MiniBossScale : _sliderHeightOffset;
            _healthSliderObject = _diContainer.InstantiatePrefab(_healthSliderPrefab, _healthCanvas.transform);
            _healthSlider = _healthSliderObject.GetComponent<EnemySlider>();
            _healthSlider.SetupAllHealthValue(_maxHealth);
            _healthSlider.SetHeightOffset(sliderHeightOfsset);
            _healthSlider.SetObjectTransform(transform);
            _healthSlider.SetLevel(_enemyLevel.GetLevel().ToString());
            _healthSlider.SetEnemySliderView(isMiniBoss);
        }
    }

    public void SetHealth()
    {
        _isDeath = false;
        _maxHealth = _enemyLevel.GetInformation().GetHealth(_enemyLevel.GetLevel()) * _enemyInfo.GetHealthFactor();
        _currentHealth = _maxHealth;
        CreateHealthBar();
        UpdateSlider();
    }

    public override void LoadHealth(float newHealth)
    {
        _isDeath = false;
        _maxHealth = _enemyLevel.GetInformation().GetHealth(_enemyLevel.GetLevel()) * _enemyInfo.GetHealthFactor();
        _currentHealth = newHealth;
        CreateHealthBar();
        UpdateSlider();
    }

    public override void TakeDamage(float damage, float knockBackPoints)
    {
        if (!_isCanTarget) return;

        var totalDamage = _enemyDefenceSystem.GetDefencePercent() * damage;
        _currentHealth -= totalDamage;
        UpdateSlider();

        _creatureKnockBackController.TakeKnockbackPoints(knockBackPoints);
    }

    private void DropQuant()
    {
        if (Random.value > WorldGameInfo.QuantDropChance) return;

        // Vector3 pos = transform.position + Vector3.up * 0.3f;
        // _quantPool.ActiveQuantPickup(pos);
        _quantSystem.ChangeQuants(1);
    }

    public override void Death()
    {
        base.Death();
        _characterController.enabled = false;
        _aiPath.enabled = false;
        _enemyAnimator.DeathAnim();
        _animationToRagdoll?.ActiveRagdoll();
        DeathSound();
        DropQuant();

        CustomEvents.FireEnemyDeath(_enemyInfo.GetEnemyNumber());
        CustomEvents.FireObjectiveAmountChange(ObjectiveEnum.KillEnemies, 1);

        StartCoroutine(FadeAndDestroy());
    }

    public virtual void DeathSound()
    {
        var rnd = Random.Range(0, 100);
        if (WorldGameInfo.EnemiesDeathSoundChance < rnd && !_enemyInfo.IsMiniBoss()) return;
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.Death[(int)_enemyInfo.GetEnemyEnum()], transform.position);
    }

    private IEnumerator FadeAndDestroy()
    {
        yield return new WaitForSeconds(5f);

        float duration = 3f;
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = new Vector3(startPosition.x, startPosition.y - 4, startPosition.z);

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
