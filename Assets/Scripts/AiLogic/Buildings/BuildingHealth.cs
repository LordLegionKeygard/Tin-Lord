using System.Collections;
using UnityEngine;
using Zenject;

public class BuildingHealth : BaseHealth
{
    [Inject] private readonly MissionHangarSystem _missionHangarSystem;
    [Inject] private readonly DiContainer _diContainer;
    [Inject] private readonly AllSkills _allSkills;
    [Inject] private readonly TilesSystem _tilesSystem;
    [Inject] private readonly HealthCanvas _healthCanvas;
    [SerializeField] private GameObject _healthSliderPrefab;
    [SerializeField] private Transform _fourTileTransform;
    private BuildingTile _buildingTile;
    private TileObject _tileObject;
    private bool _isConstructionNow;
    public bool IsContructionNow() => _isConstructionNow;
    public override Tile BuildingTile() => _buildingTile.CurrentBuildingTile();
    public override Transform GetFoutTileTransform() => _fourTileTransform;
    public bool IsFullHealth() => _currentHealth == _maxHealth;
    public float CalculateHealthFromPercent(int percent) => _maxHealth * percent / 100;
    public float GetCurrentHealthPercent() => _currentHealth / _maxHealth;
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
        _currentHealth = _maxHealth;
        UpdateSlider();
    }

    public void SlowTimeRepair(float repairRate)
    {
        if (_currentHealth < _maxHealth)
        {
            _currentHealth += Time.deltaTime * repairRate; // RepairRate — скорость ремонта
            if (_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
            UpdateSlider();
        }
    }

    private void CreateHealthSlider(bool isConstruction)
    {
        if (_healthSliderObject == null)
        {
            _healthSliderObject = _diContainer.InstantiatePrefab(_healthSliderPrefab, _healthCanvas.transform);
            _healthSlider = _healthSliderObject.GetComponent<BaseSlider>();
            _healthSlider.SetHeightOffset(-3.5f);
            _healthSlider.SetObjectTransform(transform);
        }

        UpdateSliderColor(isConstruction);
        _healthSlider.SetupAllHealthValue(_maxHealth);
    }

    private void SetBuildingSkillView()
    {
        var buildingSkillView = _healthSliderObject.GetComponent<BuildingSkillView>();
        buildingSkillView.SetBuildingTile(_buildingTile.CurrentBuilding());
    }

    public void SetNewBuildingHealth(Building building, bool isConstruction)
    {
        _isDeath = false;
        _isConstructionNow = isConstruction;
        _maxHealth = (int)(building.BuildingHealth * _missionHangarSystem.GetTitanBuildingHealthBonus());
        _currentHealth = _isConstructionNow ? 1 : _maxHealth;

        CreateHealthSlider(isConstruction);
        SetBuildingSkillView();
        UpdateSlider();
    }

    public void SetUpgradeBuildingHealth(Building building, bool isConstruction)
    {
        _isConstructionNow = isConstruction;
        _maxHealth = (int)(building.BuildingHealth * _missionHangarSystem.GetTitanBuildingHealthBonus());
        UpdateSliderColor(isConstruction);
        _healthSlider.SetupMaxHealth(_maxHealth);
        UpdateSlider();
    }

    public void UpdateSliderColor(bool isConstruction)
    {
        if (_healthSlider == null) return;
        _healthSlider.ChangeColor(isConstruction ? Colors.ConstructionBlue : Colors.GreySix);
    }

    public void LoadBuildingHealth(Building building, float currentHealth, bool isConstruction)
    {
        _isConstructionNow = isConstruction;
        _maxHealth = (int)(building.BuildingHealth * _missionHangarSystem.GetTitanBuildingHealthBonus());
        _currentHealth = currentHealth;
        CreateHealthSlider(isConstruction);
        SetBuildingSkillView();
        UpdateSlider();
    }

    public override void CalculateDamage(float damage, float knockBackPoints = 0)
    {
        if (!_buildingTile.HaveTile()) return;
        if (IsDeath()) return;

        var extraDamage = _isConstructionNow ? WorldGameInfo.ConstructionExtraDamage : 1;
        var fortification = _allSkills.GetSkill((int)SkillEnum.Fortification).IsActive() ? WorldGameInfo.FortificationSkillDamage : 1;
        var resultDamage = damage * extraDamage * fortification;
        TakeDamage(resultDamage, knockBackPoints);
        CustomEvents.FireBuildingTakeDamage(_tileObject.GetId());
    }

    public void ConstructionIncreaseHealth(float amount)
    {
        if (IsDeath()) return;

        _currentHealth += amount;
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        UpdateSlider();
    }

    public override void Death()
    {
        if (!_buildingTile.HaveTile()) return;

        if (_buildingTile.CurrentBuildingTile().BuildingTileView == BuildingTileViewEnum.Base)
        {
            CustomEvents.FireMissionEnd(MissionEndEnum.Defeat);
        }
        else AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.DestructionBuilding, transform.position);

        if (_buildingTile.CurrentBuildingTile().BuildingTileView == BuildingTileViewEnum.MachineProduction)
        {
            _tilesSystem.SetIsHaveMachineProduction(false);
            CustomEvents.FireDestroyMachineProduction();
        }

        base.Death();
        _buildingTile.StopConstruction();
        _tileObject.ToggleIsBuildingDestroyedNow(true);
        CustomEvents.FireObjectiveAmountChange(ObjectiveEnum.ConstructBuilding, -1);
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

        _buildingTile.DestroyBuildingTile(_currentHealth > 0);
        _tileObject.ToggleIsBuildingDestroyedNow(false);
    }
}
