using UnityEngine;
using Zenject;

public class BuildingHealth : BaseHealth
{
    [Inject] private readonly HealthCanvas _healthCanvas;
    [SerializeField] private GameObject _healthSliderPrefab;
    [SerializeField] private Transform _fourTileTransform;
    private GameObject _healthSliderObject;
    private BuildingTile _buildingTile;
    private HealthSlider _healthSlider;
    public override Tile BuildingTile() => _buildingTile.CurrentBuildingTile();
    public override Transform GetFoutTileTransform() => _fourTileTransform;

    public override bool IsDeath()
    {
        var state = _isDeath || !_buildingTile.HaveTile();
        return state;
    }

    private void Awake()
    {
        _buildingTile = GetComponent<BuildingTile>();
    }

    private void CreateHealthBar()
    {
        if (_healthSliderObject == null)
        {
            _healthSliderObject = Instantiate(_healthSliderPrefab, _healthCanvas.transform);
            _healthSlider = _healthSliderObject.GetComponent<HealthSlider>();
            _healthSlider.SetMaxHealth(MaxHealth);
            _healthSlider.SetHeightOffset(-3.5f);
            _healthSlider.SetObjectTransform(transform);
        }
    }

    public void SetNewBuildingHealth(Building building)
    {
        _isDeath = false;
        MaxHealth = building.BuildingHealth;
        CurrentHealth = MaxHealth;

        CreateHealthBar();
        UpdateSlider();
    }

    public override void CalculateDamage(float damage, KnockBackType knockBackType)
    {
        if (IsDeath()) return;

        TakeDamage(damage);
    }

    private void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        UpdateSlider();
    }

    private void UpdateSlider()
    {
        if (IsDeath()) return;
        _healthSlider.SetHealth(CurrentHealth);
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (CurrentHealth <= 0 && !IsDeath()) Death();
    }

    private void Death()
    {
        if (_buildingTile.CurrentBuildingTile().BuildingTileView == BuildingTileViewEnum.Base)
        {
            CustomEvents.FireBaseDestroy();
        }

        Destroy(_healthSliderObject);
        _isDeath = true;
        _buildingTile.DestroyBuildingTile(IsDeath());

    }
}
