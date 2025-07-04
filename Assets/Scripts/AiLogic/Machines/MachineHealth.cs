using System.Collections;
using UnityEngine;
using Zenject;

public class MachineHealth : BaseHealth
{
    [Inject] private readonly HealthCanvas _healthCanvas;
    [SerializeField] private MachineType _machineType;
    [SerializeField] private GameObject _healthSliderPrefab;
    [SerializeField] private float _sliderHeightOffset;
    private BaseTakeDamageVFX _takeDamageVFX;
    private AnimationToRagdoll _animationToRagdoll;
    private CapsuleCollider _capsuleCollider;
    public bool FullHealth() => _currentHealth == _maxHealth;

    private void Awake()
    {
        _takeDamageVFX = GetComponent<BaseTakeDamageVFX>();
        _animationToRagdoll = GetComponent<AnimationToRagdoll>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public void Start()
    {
        CustomEvents.OnRepairMachine += Repair;
    }

    private void Repair()
    {
        _currentHealth = _maxHealth;
        UpdateSlider();
    }

    public override void CalculateDamage(float damage, float knockBackPoints = 0)
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
            _healthSlider.SetupAllHealthValue(_maxHealth);
            _healthSlider.SetHeightOffset(_sliderHeightOffset);
            _healthSlider.SetObjectTransform(transform);
        }
    }

    public void SetHealth()
    {
        _isDeath = false;
        _maxHealth = MachinesDataMission.Instance.GetCurrentDurability();
        _currentHealth = _maxHealth;
        CreateHealthBar();
        UpdateSlider();
    }

    public override void LoadHealth(float newHealth)
    {
        _isDeath = false;
        _maxHealth = MachinesDataMission.Instance.GetCurrentDurability();
        _currentHealth = newHealth;
        CreateHealthBar();
        UpdateSlider();
    }

    public override void TakeDamage(float damage, float knockBackPoints)
    {
        base.TakeDamage(damage, knockBackPoints);
        CustomEvents.FireMachineTakeDamage();
    }

    public override void Death()
    {
        base.Death();
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.MachinesDeath[(int)_machineType], transform.position);
        _capsuleCollider.enabled = false;
        _animationToRagdoll.ActiveRagdoll();
        CustomEvents.FireMachineDie();
        StartCoroutine(FadeAndDestroy());
    }

    private IEnumerator FadeAndDestroy()
    {
        yield return new WaitForSeconds(WorldGameInfo.MachineDieDelay);

        _animationToRagdoll.KinematicToggle(true);

        float duration = WorldGameInfo.MachineDieDuration;
        float elapsedTime = 0;
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

    private void OnDestroy()
    {
        CustomEvents.OnRepairMachine -= Repair;
    }
}
