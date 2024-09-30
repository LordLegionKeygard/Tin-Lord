using UnityEngine;
using Pathfinding;
using Zenject;


public class CreatureHealth : BaseHealth
{
    [Inject] private readonly HealthCanvas _healthCanvas;
    [SerializeField] private GameObject _healthSliderPrefab;
    [SerializeField] private float _sliderHeightOffset;
    private GameObject _healthSliderObject;
    private HealthSlider _healthSlider;
    private CreatureKnockBack _creatureKnockBackController;
    private AIPath _aiPath;
    private CharacterController _characterController;
    private CreatureLevel _creatureLevel;
    private CreatureTakeDamageVFX _creatureTakeDamageVFX; //TO DO

    private void Awake()
    {
        _creatureLevel = GetComponent<CreatureLevel>();
        _creatureKnockBackController = GetComponent<CreatureKnockBack>();
        _characterController = GetComponent<CharacterController>();
        _creatureTakeDamageVFX = GetComponent<CreatureTakeDamageVFX>();
        _aiPath = GetComponent<AIPath>();
    }

    public void Start()
    {
        SetStartStats();
    }

    private void CreateHealthBar()
    {
        if (_healthSliderObject == null)
        {
            _healthSliderObject = Instantiate(_healthSliderPrefab, _healthCanvas.transform);
            _healthSlider = _healthSliderObject.GetComponent<HealthSlider>();
            _healthSlider.SetMaxHealth(MaxHealth);
            _healthSlider.SetHeightOffset(_sliderHeightOffset);
            _healthSlider.SetObjectTransform(transform);
        }
    }

    private void SetStartStats()
    {
        _isDeath = false;
        MaxHealth = _creatureLevel.GetEnemiesInformation().Health[_creatureLevel.GetLevel()];
        CurrentHealth = MaxHealth;
        CreateHealthBar();
        UpdateSlider();
    }

    public override void CalculateDamage(float damage, KnockBackType knockBackType)
    {
        if (IsDeath()) return;

        TakeDamage(damage, knockBackType);
    }

    private void TakeDamage(float damage, KnockBackType knockBackType)
    {
        _creatureKnockBackController.TakeKnockbackPoints(knockBackType);
        CurrentHealth -= damage;
        UpdateSlider();
    }


    private void UpdateSlider()
    {
        if (_isDeath) return;
        _healthSlider.SetHealth(CurrentHealth);
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (CurrentHealth <= 0 && !IsDeath()) Death();
    }

    private void Death()
    {
        Destroy(_healthSliderObject);
        _isDeath = true;
        _characterController.enabled = false;
        _aiPath.enabled = false;
    }
}
