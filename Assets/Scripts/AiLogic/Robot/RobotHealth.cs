using System.Collections;
using UnityEngine;
using Zenject;

public class RobotHealth : BaseHealth
{
    [Inject] private readonly HealthCanvas _healthCanvas;
    [SerializeField] private GameObject _healthSliderPrefab;
    [SerializeField] private float _sliderHeightOffset;
    private RobotAnimator _robotAnimator;
    private RobotLevel _robotLevel;
    private BaseTakeDamageVFX _takeDamageVFX;
    public bool FullHealth() => CurrentHealth == MaxHealth;

    private void Awake()
    {
        _robotLevel = GetComponent<RobotLevel>();
        _takeDamageVFX = GetComponent<BaseTakeDamageVFX>();
        _robotAnimator = GetComponent<RobotAnimator>();
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

    public override void CalculateDamage(float damage, int knockBackPoints = 0)
    {
        base.CalculateDamage(damage, knockBackPoints);
        _takeDamageVFX.SpawnTakeDamageVFX();
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
        MaxHealth = _robotLevel.GetRobotInformation().Durability[_robotLevel.GetLevel()];
        CurrentHealth = MaxHealth;
        CreateHealthBar();
        UpdateSlider();
    }

    public override void TakeDamage(float damage, int knockBackPoints)
    {
        base.TakeDamage(damage, knockBackPoints);
    }

    public override void Death()
    {
        base.Death();
        _robotAnimator.DeathAnim();
        CustomEvents.FireRobotDie();

        StartCoroutine(FadeAndDestroy());
    }

    private IEnumerator FadeAndDestroy()
    {
        yield return new WaitForSeconds(WorldGameInfo.RobotDieDelay);

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
