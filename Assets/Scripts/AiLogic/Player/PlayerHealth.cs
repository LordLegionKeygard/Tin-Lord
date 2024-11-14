using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerHealth : BaseHealth
{
    [Inject] private readonly HealthCanvas _healthCanvas;
    [SerializeField] private GameObject _healthSliderPrefab;
    [SerializeField] private float _sliderHeightOffset;
    private PlayerAnimator _playerAnimator;
    private PlayerLevel _playerLevel;
    private BaseTakeDamageVFX _takeDamageVFX;

    private void Awake()
    {
        _playerLevel = GetComponent<PlayerLevel>();
        _takeDamageVFX = GetComponent<BaseTakeDamageVFX>();
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    public void Start()
    {
        SetStartStats();
    }

    public override void CalculateDamage(float damage, int knockBackPoints)
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
        MaxHealth = _playerLevel.GetPlayerLevelInformation().Health[_playerLevel.GetLevel()];
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
        _playerAnimator.DeathAnim();

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
