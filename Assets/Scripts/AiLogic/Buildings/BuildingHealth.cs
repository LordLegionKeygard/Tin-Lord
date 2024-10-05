using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

public class BuildingHealth : BaseHealth
{
    [Inject] private readonly HealthCanvas _healthCanvas;
    [SerializeField] private GameObject _healthSliderPrefab;
    [SerializeField] private Transform _fourTileTransform;
    private BuildingTile _buildingTile;
    private TileObject _tileObject;
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
        _tileObject = GetComponent<TileObject>();
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

    public override void CalculateDamage(float damage, int knockBackPoints)
    {
        if (IsDeath()) return;
        TakeDamage(damage, knockBackPoints);
    }

    public override void Death()
    {
        if (_buildingTile.CurrentBuildingTile().BuildingTileView == BuildingTileViewEnum.Base)
        {
            CustomEvents.FireBaseDestroy();
        }

        base.Death();
        //тут логика для отключения всего
        _tileObject.ToggleIsBuildingDestroyedNow(true);
        CustomEvents.FireBuildingDestroyedNow(_tileObject.GetId());
        StartCoroutine(FadeAndDestroy());
    }

    private IEnumerator FadeAndDestroy()
    {
        var spawnPos = _buildingTile.CurrentBuildingTile().IsFourTile ? _fourTileTransform.position : transform.position;
        Instantiate(_buildingTile.CurrentBuilding().DestroyVFXPrefab, spawnPos, Quaternion.identity);
        float duration = 5f;
        float elapsedTime = 0f;
        Vector3 startPosition = _buildingTile.CurrentBuildingTileObject().transform.position;
        Vector3 targetPosition = new Vector3(startPosition.x, startPosition.y - 12, startPosition.z);

        while (elapsedTime < duration)
        {
            _buildingTile.CurrentBuildingTileObject().transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _buildingTile.DestroyBuildingTile(IsDeath());
        _tileObject.ToggleIsBuildingDestroyedNow(false);
    }
}
