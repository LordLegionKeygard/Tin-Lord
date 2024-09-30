using UnityEngine;
using Zenject;

public class BuildingHealth : BaseHealth
{
    [Inject] private readonly HealthCanvas _healthCanvas;
    [SerializeField] private GameObject _healthSliderPrefab;
    [SerializeField] private Transform _fourTileTransform;
    private BuildingTile _buildingTile;
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
        TakeDamage(damage, knockBackType);
    }

    public override void Death()
    {
        if (_buildingTile.CurrentBuildingTile().BuildingTileView == BuildingTileViewEnum.Base)
        {
            CustomEvents.FireBaseDestroy();
        }

        base.Death();
        _buildingTile.DestroyBuildingTile(IsDeath());

    }
}
