using System.Collections;
using UnityEngine;
using Zenject;

public class BuildingHealth : BaseHealth
{
    [Inject] private readonly TutorialSystem _tutorialSystem;
    [Inject] private readonly MissionHangarSystem _missionHangarSystem;
    [Inject] private readonly DiContainer _diContainer;
    [Inject] private readonly AllSkills _allSkills;
    [Inject] private readonly TilesSystem _tilesSystem;
    [Inject] private readonly HealthCanvas _healthCanvas;
    [SerializeField] private GameObject _healthSliderPrefab;
    [SerializeField] private Transform _fourTileTransform;
    private BuildingTile _buildingTile;
    private TileObject _tileObject;
    private BuildingSliderWorkView _buildingSliderWorkView;
    private bool _isConstructionNow;

    public bool IsContructionNow() => _isConstructionNow;
    public override Tile BuildingTile() => _buildingTile.GetCurrentBuildingTile();
    public override Transform GetFoutTileTransform() => _fourTileTransform;
    public bool IsFullHealth() => _currentHealth == _maxHealth;
    public float GetCurrentHealthPercent() => _currentHealth / _maxHealth;
    public TileObject GetTileObject() => _tileObject;
    public BuildingSliderWorkView GetBuildingSliderWorkView() => _buildingSliderWorkView;

    public override bool IsDeath()
    {
        var state = _isDeath || !_buildingTile.IsHaveTile();
        return state;
    }

    private void Awake()
    {
        _buildingTile = GetComponent<BuildingTile>();
        _tileObject = GetComponent<TileObject>();
    }

    private void Start()
    {
        CustomEvents.OnDayEnd += BuidingDecay;
        CustomEvents.OnUpdateBuildingsMaxHealth += UpdateBuildingMaxHealth;
    }

    private void UpdateBuildingMaxHealth()
    {
        var buildingTile = _tileObject.BuildingTileObject();

        if (buildingTile.IsHaveTile())
        {
            _maxHealth = CalculateMaxHealth(buildingTile.GetCurrentBuilding());
            _healthSlider.UpdateMaxSliderValue(_maxHealth);
            UpdateSlider();
        }
    }

    private void BuidingDecay(int _)
    {
        if (_buildingTile.IsHaveTile() && !_buildingTile.IsConstructionNow() && _buildingTile.GetCurrentBuildingTile().BuildingTileView == BuildingTileViewEnum.Traps)
        {
            CalculateDamage(CalculateHealthFromPercent(5));
        }
    }

    public void FullRepair()
    {
        _currentHealth = _maxHealth;
        UpdateSlider();
    }

    public void PercentRepair(float percent)
    {
        var repairHealth = _maxHealth * percent;

        if ((_currentHealth + repairHealth) > _maxHealth) _currentHealth = _maxHealth;
        else _currentHealth += repairHealth;
        
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

    private void SetupBuildingSliderView()
    {
        // сетапим здание для отображения умений через получение событий
        var buildingSliderSkillView = _healthSliderObject.GetComponent<BuildingSliderSkillView>();
        buildingSliderSkillView.SetBuildingTile(_buildingTile.GetCurrentBuilding());

        // берем компонент BuildingWorkView для постоянного отображения не хватки ресурса и работы здания
        _buildingSliderWorkView = _healthSliderObject.GetComponent<BuildingSliderWorkView>();
    }

    public void SetNewBuildingHealth(Building building, bool isConstruction)
    {
        _isDeath = false;
        _isConstructionNow = isConstruction;
        _maxHealth = CalculateMaxHealth(building);
        _currentHealth = _isConstructionNow ? 1 : _maxHealth;

        CreateHealthSlider(isConstruction);
        SetupBuildingSliderView();
        UpdateSlider();
    }

    public void SetUpgradeBuildingHealth(Building building, bool isConstruction)
    {
        _isConstructionNow = isConstruction;
        _maxHealth = CalculateMaxHealth(building);
        UpdateSliderColor(isConstruction);
        _healthSlider.SetupMaxHealth(_maxHealth);
        UpdateSlider();
    }

    private float CalculateMaxHealth(Building building)
    {
        var tacticCardIncreaseHealth = building.BuildingHealth * _tileObject.BuildingTileObject().GetTacticCardIncreaseHealthLevel() * WorldGameInfo.TacticCardIncreaseHealthFactor;
        return (int)(building.BuildingHealth * _missionHangarSystem.GetTitanBuildingHealthBonus() + tacticCardIncreaseHealth);
    }

    public void UpdateSliderColor(bool isConstruction)
    {
        if (_healthSlider == null) return;
        _healthSlider.ChangeColor(isConstruction ? Colors.ConstructionBlue : _buildingTile.IsTrap() ? Colors.DecayYellow : Colors.GreySix);
    }

    public void LoadBuildingHealth(Building building, float currentHealth, bool isConstruction)
    {
        _isConstructionNow = isConstruction;
        _maxHealth = CalculateMaxHealth(building);
        _currentHealth = currentHealth;
        CreateHealthSlider(isConstruction);
        SetupBuildingSliderView();
        UpdateSlider();
    }

    public override void CalculateDamage(float damage, float knockBackPoints = 0)
    {
        if (!_buildingTile.IsHaveTile()) return;
        if (IsDeath()) return;

        var extraDamage = _isConstructionNow ? WorldGameInfo.ConstructionExtraDamage : 1;
        var fortification = _allSkills.GetSkill((int)SkillEnum.Fortification).IsActive() ? WorldGameInfo.FortificationSkillDamage : 1;
        var resultDamage = damage * extraDamage * fortification;
        TakeDamage(resultDamage, knockBackPoints);
        CustomEvents.FireBuildingTakeDamage(_tileObject.GetId());

        if (_tutorialSystem.GetTutorialStepEnum() == TutorialStepEnum.MissionBuildingTakeDamage_56)
        {
            CustomEvents.FireRunStepAfterWait(TutorialStepEnum.MissionBuildingTakeDamage_56);
        }
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
        if (!_buildingTile.IsHaveTile()) return;

        if (_buildingTile.GetCurrentBuildingTile().BuildingTileView == BuildingTileViewEnum.Base)
        {
            CustomEvents.FireMissionEnd(MissionEndEnum.Defeat);
        }
        else AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.DestructionBuilding, transform.position);

        if (_buildingTile.GetCurrentBuildingTile().BuildingTileView == BuildingTileViewEnum.MachineProduction)
        {
            _tilesSystem.SetIsHaveMachineProduction(false);
            CustomEvents.FireDestroyMachineProductionBuilding();
        }

        base.Death();
        _buildingTile.AddResourcesAfterDestroyBuilding(); // вызываем здесь, чтобы вызвать до StopConstruction
        _buildingTile.StopConstruction();
        _tileObject.ToggleIsBuildingDestroyedNow(true);
        CustomEvents.FireObjectiveAmountChange(ObjectiveEnum.ConstructBuilding, -1);
        CustomEvents.FireBuildingDestroyed(_tileObject.GetId());
        _tileObject.ClearBuildingInfoAndProduction();
        StartCoroutine(FadeAndDestroy());
    }

    private IEnumerator FadeAndDestroy()
    {
        var spawnPos = _buildingTile.GetCurrentBuildingTile().IsFourTile ? _fourTileTransform.position : transform.position;
        Instantiate(_buildingTile.GetCurrentBuilding().DestroyVFXPrefab, spawnPos, Quaternion.identity);

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

        _buildingTile.DestroyBuildingTile(false);
        _tileObject.ToggleIsBuildingDestroyedNow(false);
    }

    private void OnDestroy()
    {
        CustomEvents.OnDayEnd -= BuidingDecay;
        CustomEvents.OnUpdateBuildingsMaxHealth -= UpdateBuildingMaxHealth;
    }
}
