using System.Collections;
using UnityEngine;
using Zenject;
using DG.Tweening;

public class BuildingHealth : BaseHealth
{
    [Inject] private readonly HealthCanvas _healthCanvas;
    [SerializeField] private GameObject _healthSliderPrefab;
    [SerializeField] private Transform _fourTileTransform;
    private BuildingTile _buildingTile;
    private TileObject _tileObject;
    private bool _isConstructionNow;
    public bool IsContructionNow() => _isConstructionNow;
    public override Tile BuildingTile() => _buildingTile.CurrentBuildingTile();
    public override Transform GetFoutTileTransform() => _fourTileTransform;
    public bool IsFullHealth() => CurrentHealth == MaxHealth;
    public float CalculateHealthFromPercent(int percent) => MaxHealth * percent / 100;
    public float GetCurrentHealthPercent() => CurrentHealth / MaxHealth;
    public TileObject GetTileObject() => _tileObject;

    public override bool IsDeath()
    {
        var state = _isDeath || !_buildingTile.HaveTile();
        return state;
    }

    private void Awake()
    {
        _buildingTile = GetComponent<BuildingTile>();
        _tileObject = GetComponent<TileObject>();
    }

    public void FullRepair()
    {
        CurrentHealth = MaxHealth;
        UpdateSlider();
    }

    public void SlowTimeRepair(float repairRate)
    {
        if (CurrentHealth < MaxHealth)
        {
            CurrentHealth += Time.deltaTime * repairRate; // RepairRate — скорость ремонта
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
            UpdateSlider();
        }
    }

    private void CreateHealthSlider()
    {
        if (_healthSliderObject == null)
        {
            _healthSliderObject = Instantiate(_healthSliderPrefab, _healthCanvas.transform);
            _healthSlider = _healthSliderObject.GetComponent<BaseSlider>();
            _healthSlider.SetHeightOffset(-3.5f);
            _healthSlider.SetObjectTransform(transform);
        }

        _healthSlider.SetupAllHealthValue(MaxHealth);
    }

    public void SetNewBuildingHealth(Building building, bool isConstruction)
    {
        _isDeath = false;
        _isConstructionNow = isConstruction;
        MaxHealth = building.BuildingHealth;
        CurrentHealth = _isConstructionNow ? 1 : MaxHealth;

        CreateHealthSlider();
        UpdateSlider();
    }

    public void SetUpgradeBuildingHealth(Building building, bool isConstruction)
    {
        _isConstructionNow = isConstruction;
        MaxHealth = building.BuildingHealth;
        _healthSlider.SetupMaxHealth(MaxHealth);
        UpdateSlider();
    }

    public void LoadBuildingHealth(Building building, float currentHealth, bool isConstruction)
    {
        _isConstructionNow = isConstruction;
        MaxHealth = building.BuildingHealth;
        CurrentHealth = currentHealth;
        CreateHealthSlider();
        UpdateSlider();
    }

    public override void CalculateDamage(float damage, float knockBackPoints = 0)
    {
        if (!_buildingTile.HaveTile()) return;
        if (IsDeath()) return;

        var extraDamage = _isConstructionNow ? 3 : 1;
        TakeDamage(damage * extraDamage, knockBackPoints);
        CustomEvents.FireBuildingTakeDamage(_tileObject.GetId());
    }

    public void ConstructionIncreaseHealth(float amount)
    {
        if (IsDeath()) return;

        CurrentHealth += amount;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        UpdateSlider();
    }

    public override void Death()
    {
        if(!_buildingTile.HaveTile()) return;

        if (_buildingTile.CurrentBuildingTile().BuildingTileView == BuildingTileViewEnum.Base)
        {
            CustomEvents.FireBaseDestroy();
        }

        base.Death();
        _tileObject.ToggleIsBuildingDestroyedNow(true);
        CustomEvents.FireBuildingDestroyed(_tileObject.GetId());
        _tileObject.ClearBuildingInfoAndProduction();
        StartCoroutine(FadeAndDestroy());
    }

    private IEnumerator FadeAndDestroy()
    {
        var spawnPos = _buildingTile.CurrentBuildingTile().IsFourTile ? _fourTileTransform.position : transform.position;
        Instantiate(_buildingTile.CurrentBuilding().DestroyVFXPrefab, spawnPos, Quaternion.identity);

        if (!_isConstructionNow)
        {
            float duration = 5f;
            float elapsedTime = 0f;
            Vector3 startPosition = _buildingTile.CurrentBuildingGameObject().transform.position;
            Vector3 targetPosition = new Vector3(startPosition.x, startPosition.y - 12, startPosition.z);

            while (elapsedTime < duration)
            {
                _buildingTile.CurrentBuildingGameObject().transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        _buildingTile.DestroyBuildingTile(CurrentHealth > 0);
        _tileObject.ToggleIsBuildingDestroyedNow(false);
    }
}
