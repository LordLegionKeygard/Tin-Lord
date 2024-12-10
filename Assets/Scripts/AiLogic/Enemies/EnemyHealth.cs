using UnityEngine;
using Pathfinding;
using Zenject;
using System.Collections;


public class EnemyHealth : BaseHealth
{
    [Inject] private readonly HealthCanvas _healthCanvas;
    [SerializeField] private GameObject _healthSliderPrefab;
    [SerializeField] private float _sliderHeightOffset;
    private EnemyAnimator _enemyAnimator;
    private EnemyKnockBack _creatureKnockBackController;
    private AIPath _aiPath;
    private CharacterController _characterController;
    private EnemyLevel _enemyLevel;
    private BaseTakeDamageVFX _takeDamageVFX;
    private EnemyCenterPoint _enemyCenterPoint;

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
    }

    public void Start()
    {
        SetStartStats();
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
            _healthSliderObject = Instantiate(_healthSliderPrefab, _healthCanvas.transform);
            _healthSlider = _healthSliderObject.GetComponent<BaseSlider>();
            _healthSlider.SetupHealth(MaxHealth);
            _healthSlider.SetHeightOffset(_sliderHeightOffset);
            _healthSlider.SetObjectTransform(transform);
        }
    }

    private void SetStartStats()
    {
        _isDeath = false;
        MaxHealth = _enemyLevel.GetInformation().Health[_enemyLevel.GetLevel()];
        CurrentHealth = MaxHealth;
        CreateHealthBar();
        UpdateSlider();
    }

    public override void TakeDamage(float damage, float knockBackPoints)
    {
        base.TakeDamage(damage, knockBackPoints);
        _creatureKnockBackController.TakeKnockbackPoints(knockBackPoints);
    }

    public override void Death()
    {
        base.Death();
        _characterController.enabled = false;
        _aiPath.enabled = false;
        _enemyAnimator.DeathAnim();

        CustomEvents.FireChangeExperience(_enemyLevel.GetExperience());

        StartCoroutine(FadeAndDestroy());
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
