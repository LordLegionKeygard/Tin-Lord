using System.Collections;
using UnityEngine;
using Zenject;

public class RobotHealth : BaseHealth
{
    [Inject] private readonly HealthCanvas _healthCanvas;
    [SerializeField] private GameObject _healthSliderPrefab;
    [SerializeField] private float _sliderHeightOffset;
    private BaseTakeDamageVFX _takeDamageVFX;
    private AnimationToRagdoll _animationToRagdoll;
    private CapsuleCollider _capsuleCollider;
    public bool FullHealth() => CurrentHealth == MaxHealth;

    private void Awake()
    {
        _takeDamageVFX = GetComponent<BaseTakeDamageVFX>();
        _animationToRagdoll = GetComponent<AnimationToRagdoll>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public void Start()
    {
        SetStartStats();

        CustomEvents.OnRepairRobot += Repair;
    }

    private void Repair()
    {
        CurrentHealth = MaxHealth;
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
            _healthSlider.SetupAllHealthValue(MaxHealth);
            _healthSlider.SetHeightOffset(_sliderHeightOffset);
            _healthSlider.SetObjectTransform(transform);
        }
    }

    private void SetStartStats()
    {
        _isDeath = false;
        MaxHealth = RobotsData.Instance.GetCurrentDurability();
        CurrentHealth = MaxHealth;
        CreateHealthBar();
        UpdateSlider();
    }

    public override void TakeDamage(float damage, float knockBackPoints)
    {
        base.TakeDamage(damage, knockBackPoints);
        CustomEvents.FireRobotTakeDamage();
    }

    public override void Death()
    {
        base.Death();
        _capsuleCollider.enabled = false;
        _animationToRagdoll.RagdollOn();
        CustomEvents.FireRobotDie();

        StartCoroutine(FadeAndDestroy());
    }

    private IEnumerator FadeAndDestroy()
    {
        yield return new WaitForSeconds(WorldGameInfo.RobotDieDelay);

        _animationToRagdoll.KinematicToggle(true);

        float duration = WorldGameInfo.RobotDieDuration;
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
        CustomEvents.OnRepairRobot -= Repair;
    }
}
