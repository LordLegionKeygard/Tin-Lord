using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class BuildingTile : MonoBehaviour
{
   [Inject] private readonly MissionHangarSystem _missionHangarSystem;
   [Inject] private DiContainer _diContainer;
   [Inject] private TutorialSystem _tutorialSystem;
   [Inject] private MissionResources _missionResources;
   [Inject] private TilesSystem _tilesSystem;
   [Inject] private LearnedBuildingsDataMission _learnedBuildingsDataMission;
   [SerializeField] private Transform _buildingParent;
   [SerializeField] private TileObject _tileObject;
   [SerializeField] private TileObject _isRealBaseTileObject; // этот тайл является экстра обьектом базы и хранит текущий тайл базы
   private BuildingHealth _buildingHealth;
   private BuildingTileWallGates _buildingTileWallGates;
   private BuildingTileTransform _buildingTileTransform;
   private BuildingLevels _buildingLevels;
   private GameObject _currentBuildingGameObject;
   private GameObject _constructionPrefab;
   private Tile _currentBuildingTile;
   private int _currentLevel;
   private bool _isConstructionNow;
   private bool _isUpgradeBase;
   private float _previousBaseBuildingHealth;
   private float _previousBuildingHealthPercent;
   private int _tacticCardIncreaseDamageLevel;

   public TileObject IsExtraBaseTileObject() => _isRealBaseTileObject;
   public BuildingTileTransform GetBuildingTileTransform() => _buildingTileTransform;
   public BuildingLevels GetBuildingLevels() => _buildingLevels;
   public BuildingTileWallGates CurrentBuildingTileProtective() => _buildingTileWallGates;
   private ConstructionBuildingView _constructionView;
   public bool IsConstructionNow() => _isConstructionNow;
   public bool IsUpgradeBase() => _isUpgradeBase;
   public float PreviousBaseBuildingHealth() => _previousBaseBuildingHealth;
   public bool IsHaveTile() => _currentBuildingTile != null;
   public Tile GetCurrentBuildingTile() => _currentBuildingTile;
   public GameObject CurrentBuildingGameObject() => _currentBuildingGameObject;
   public bool IsHaveBuildingGameObject() => _currentBuildingGameObject != null;
   public int GetCurrentBuildingLevel() => _currentLevel;
   public Building GetCurrentBuilding() => _currentBuildingTile.Buildings[_currentLevel - 1];
   public bool IsWallTile() => _currentBuildingTile == null ? false : _currentBuildingTile.BuildingTileView == BuildingTileViewEnum.Walls;
   public bool IsGateTile() => _currentBuildingTile == null ? false : _currentBuildingTile.BuildingTileView == BuildingTileViewEnum.Gates;
   public bool IsTrap() => _currentBuildingTile == null ? false : _currentBuildingTile.BuildingTileView == BuildingTileViewEnum.Traps;

   public bool IsWallOrGate() => _currentBuildingTile == null ? false : _currentBuildingTile.BuildingTileView is BuildingTileViewEnum.Walls or BuildingTileViewEnum.Gates;
   public bool IsEcologyBuilding() => _currentBuildingTile == null ? false : _currentBuildingTile.BuildingTileView == BuildingTileViewEnum.EcologyPurifier;
   public bool NeightbourTileIsWallOrGate(int number) => _tileObject.GetNeighbourBuildingTile(number) == null ? false : _tileObject.GetNeighbourBuildingTile(number).IsWallOrGate();

   public int GetTacticCardIncreaseDamageLevel() => _tacticCardIncreaseDamageLevel;

   public float GetRealTurretDamage()
   {
      var baseDamage = GetCurrentBuilding().Damage;
      var bonus = _missionHangarSystem.GetAimBotDamageBonus();
      var tacticCardIncreaseDamage = baseDamage * _tacticCardIncreaseDamageLevel * WorldGameInfo.TacticCardIncreaseDamageFactor;
      return (baseDamage + tacticCardIncreaseDamage) * bonus;
   }

   public void TacticCardIncreaseDamageLevel()
   {
      _tacticCardIncreaseDamageLevel += 1;
   }

   public bool IsCanUpgrade()
   {
      if (_currentBuildingTile != null)
      {
         return GetCurrentBuildingLevel() < _currentBuildingTile.Buildings.Length && _learnedBuildingsDataMission.IsHaveLearnedBuildingUpgradeInBuildingType(_currentBuildingTile, _currentLevel);
      }

      return false;
   }

   private void Awake()
   {
      _buildingHealth = GetComponent<BuildingHealth>();
      _buildingTileWallGates = GetComponent<BuildingTileWallGates>();
      _buildingTileTransform = GetComponent<BuildingTileTransform>();

      CustomEvents.OnCompleteLoadTiles += UpdateWallsAndGates;
   }

   /// <summary>
   /// Вызывается при смерти обьекта
   /// </summary>
   public void StopConstruction()
   {
      if (_isConstructionNow)
      {
         StopCoroutine(nameof(RunConstructionCoroutine));
         _isConstructionNow = false;
         Destroy(_constructionPrefab);
      }
   }

   public void BeginConstruction(Tile tile, int level, bool isLoad)
   {
      _currentBuildingTile = tile;
      _currentLevel = level;
      _buildingHealth.SetNewBuildingHealth(GetCurrentBuilding(), isConstruction: true);
      _buildingTileTransform.CachedRandomTransform(GetCurrentBuilding());
      _tileObject.ClearResourceProductionAndRequiredWhenBuildingConstruct();
      if (_currentBuildingTile.BuildingTileView == BuildingTileViewEnum.MachineProduction && !_tilesSystem.IsHaveMachineProduction()) _tilesSystem.SetIsHaveMachineProduction(true);
      if (!_tutorialSystem.IsCompleteMissionTutorial()) _tutorialSystem.SetCurrentStepInProccess(_currentBuildingTile.BuildingTileView);

      SpawnConstructionPrefab();
      StartCoroutine(RunConstructionCoroutine(
           onComplete: () =>
           {
              InstantiateCompletedBuilding();
              if (!isLoad) CustomEvents.FireObjectiveAmountChange(ObjectiveEnum.ConstructBuilding, 1);
           },
           onFail: () => _isConstructionNow = false
       ));
   }

   private void SpawnConstructionPrefab()
   {
      _constructionPrefab = Instantiate(GetCurrentBuilding().ConstructionPrefab, _buildingParent.position, Quaternion.identity);
      _constructionPrefab.transform.SetParent(_buildingParent);
      _buildingTileTransform.SetTransform(_constructionPrefab.transform, GetCurrentBuilding(), _tileObject);
   }

   private IEnumerator RunConstructionCoroutine(Action onComplete, Action onFail)
   {
      _isConstructionNow = true;
      _constructionView = _constructionPrefab.GetComponent<ConstructionBuildingView>();

      while (_buildingHealth.GetCurrentHealth() < _buildingHealth.GetMaxHealth())
      {
         if (_buildingHealth.IsDeath())
         {
            onFail?.Invoke();
            yield break;
         }

         var speed = _currentBuildingTile.BuildingTileView == BuildingTileViewEnum.Base ? _tutorialSystem.IsCompleteMissionTutorial() ? WorldGameInfo.FirstBaseConstructionSpeed : WorldGameInfo.TutorialBaseConstructionSpeed : WorldGameInfo.ConstructionSpeed;
         _buildingHealth.ConstructionIncreaseHealth(speed * Time.deltaTime);
         _constructionView.UpdateShaderByHealth(_buildingHealth.GetCurrentHealth(), _buildingHealth.GetMaxHealth());

         yield return null;
      }

      onComplete?.Invoke();
   }

   private void CheckIsExtrabaseTileObject()
   {
      if (_currentBuildingTile.BuildingTileView == BuildingTileViewEnum.Base)
      {
         _tileObject.GetNeighbourBuildingTile((int)TileDirectionEnum.East)._isRealBaseTileObject = _tileObject;
         _tileObject.GetNeighbourBuildingTile((int)TileDirectionEnum.North)._isRealBaseTileObject = _tileObject;
         _tileObject.GetNeighbourBuildingTile((int)TileDirectionEnum.NorthEast)._isRealBaseTileObject = _tileObject;
      }
   }

   public void InstantiateCompletedBuilding()
   {
      AudioManager.Instance.PlayerOneShot(_currentLevel == 0 ? FMODEvents.Instance.CompleteConstructBuilding : FMODEvents.Instance.CompleteUpgradeBuilding, transform.position);
      _isConstructionNow = false;

      if (_currentBuildingTile.BuildingTileView == BuildingTileViewEnum.Base) CustomEvents.FireSetBase(_currentLevel);

      _tutorialSystem.CompleteConstructionBuilding(_currentBuildingTile.BuildingTileView);
      CheckIsExtrabaseTileObject();

      _currentBuildingGameObject = _diContainer.InstantiatePrefab(_currentBuildingTile.TileObject, _buildingParent.position, Quaternion.identity, null);
      Destroy(_constructionPrefab);
      _currentBuildingGameObject.transform.SetParent(_buildingParent);
      _buildingTileTransform.SetTransform(_currentBuildingGameObject.transform, GetCurrentBuilding(), _tileObject);
      _buildingLevels = _currentBuildingGameObject.GetComponent<BuildingLevels>();
      _buildingLevels.SetBuildingLevelView(_currentLevel, _tileObject);
      _buildingLevels.SetBuildingProductionView();
      PrepareSetResourceRequired();
      CustomEvents.FireChangeEcology(_tileObject.TileEcology().GetEcology(GetEcologyEnum.Total), _tileObject.GetId(), false);
      if (GetCurrentBuilding().ResourcesProduction.Length != 0) _tileObject.SetNewResourceProductionAfterUpgradeBuilding(GetCurrentBuilding().ResourcesProduction);
      _buildingHealth.SetNewBuildingHealth(GetCurrentBuilding(), false);

      if (IsWallOrGate()) UpdateWallsAndGates();

      _tileObject.SetBuildingWork(true);
      _tileObject.CheckResourceRequired(true);
      _tileObject.SetGeneralRepairSelect(true);
      CustomEvents.FireChangeGeneralRepairTileObject(_tileObject);
   }

   public void UpgradeBaseBuilding(int newLevel, TileObject tileObject)
   {
      _isUpgradeBase = true;
      _currentLevel = newLevel;
      _previousBuildingHealthPercent = tileObject.BuildingHealth().GetCurrentHealthPercent();
      _buildingHealth.SetUpgradeBuildingHealth(GetCurrentBuilding(), isConstruction: true);
      _buildingLevels.DisableAllBuilding();
      _tileObject.ClearResourceProductionAndRequiredWhenBuildingConstruct();

      SpawnConstructionPrefab();

      StartCoroutine(RunBaseUpgradeCoroutine(
            onComplete: () =>
            {
               Destroy(_constructionPrefab);
               FinalizeBaseUpgrade(_currentLevel, _currentLevel - 1);
            },
            onFail: () =>
            {
               _isConstructionNow = false;
            }
        ));
   }

   private IEnumerator RunBaseUpgradeCoroutine(Action onComplete, Action onFail, float baseHealthOffset = 0f)
   {
      _previousBaseBuildingHealth = baseHealthOffset > 0f ? baseHealthOffset : _buildingHealth.GetCurrentHealth();
      _isConstructionNow = true;

      _constructionView = _constructionPrefab.GetComponent<ConstructionBuildingView>();

      while (_buildingHealth.GetCurrentHealth() < _buildingHealth.GetMaxHealth())
      {
         if (_buildingHealth.IsDeath())
         {
            onFail?.Invoke();
            yield break;
         }

         _buildingHealth.ConstructionIncreaseHealth(WorldGameInfo.ConstructionSpeed * Time.deltaTime);
         _constructionView.UpdateShaderByHealth(_buildingHealth.GetCurrentHealth() - _previousBaseBuildingHealth, _buildingHealth.GetMaxHealth() - _previousBaseBuildingHealth);

         yield return null;
      }
      onComplete?.Invoke();
   }

   public void FinalizeBaseUpgrade(int newLevel, int previousLevel)
   {
      AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.CompleteUpgradeBuilding, transform.position);
      _isConstructionNow = false;
      _currentLevel = newLevel;
      if (_currentBuildingTile.BuildingTileView == BuildingTileViewEnum.Base) CustomEvents.FireSetBase(_currentLevel);

      _buildingHealth.UpdateSliderColor(false);
      _buildingLevels.SetBuildingLevelView(_currentLevel, _tileObject);
      PrepareSetResourceRequired();
      CustomEvents.FireChangeEcology(_tileObject.TileEcology().GetEcology(GetEcologyEnum.Total), _tileObject.GetId(), false);
      _buildingLevels.SetBuildingProductionView();
      _tileObject.SetResourceProduction(_tileObject.CurrentResourceProduction(), _tileObject.CurrentResourceRecept());

      if (IsWallOrGate()) UpdateWallsAndGates();
      _tileObject.CheckResourceRequired(true);

      var previousBuilding = _tileObject.BuildingTileObject()._currentBuildingTile.Buildings[previousLevel - 1].ResourcesForBuild;

      _missionResources.AddResourcesAfterDestroyBuilding(previousBuilding, _previousBuildingHealthPercent); // возвращаем часть ресурсов за прошлое здание
   }

   public void UpgradeBaseAterLoad(Tile tile, int level)
   {
      _currentBuildingTile = tile;
      _currentLevel = level;
      _buildingHealth.SetNewBuildingHealth(GetCurrentBuilding(), isConstruction: true);
      _buildingTileTransform.CachedRandomTransform(GetCurrentBuilding());
      _tileObject.ClearResourceProductionAndRequiredWhenBuildingConstruct();

      SpawnConstructionPrefab();
      StartCoroutine(RunBaseUpgradeCoroutine(
            onComplete: () =>
            {
               Destroy(_constructionPrefab);
               InstantiateCompletedBuilding();
            },
            onFail: () =>
            {
               _isConstructionNow = false;
            },
            baseHealthOffset: _previousBaseBuildingHealth
        ));
   }

   private void UpdateWallsAndGates()
   {
      _buildingTileWallGates.PrepareWallsAndGates();

      RefreshNeighbourWallTiles();
   }

   private void RefreshNeighbourWallTiles()
   {
      for (int i = 0; i < 8; i++)
      {
         if (!IsNeedCheck(i, true)) continue;

         if (_tileObject.GetNeighbourBuildingTile(i).IsWallTile() || _tileObject.GetNeighbourBuildingTile(i).IsGateTile())
         {
            _tileObject.GetNeighbourBuildingTile(i).CurrentBuildingTileProtective().PrepareWallsAndGates();
         }
      }
   }

   public bool IsNeedCheck(int i, bool cross)
   {
      if (cross)
      {
         if (i is (int)TileDirectionEnum.NorthEast or (int)TileDirectionEnum.NorthWest or (int)TileDirectionEnum.SouthEast or (int)TileDirectionEnum.SouthWest) return false;
      }

      if (_tileObject.GetNeighbourBuildingTile(i) == null) return false;
      if (!_tileObject.GetNeighbourBuildingTile(i).IsHaveTile()) return false;

      return true;
   }

   public void AddResourcesAfterDestroyBuilding()
   {
      if (_buildingHealth.GetCurrentHealth() > 0)
      {
         if (_isConstructionNow) // если мы уничтожили строящееся нами здание, то возвращаем половину ресурсов
         {
            _missionResources.AddResourcesAfterDestroyBuilding(GetCurrentBuilding().ResourcesForBuild, 100);
         }
         else // если мы апгрейдим здание или уничтожаем обычное здание, то получаем только % ресурсов от прошлого здоровья здания
         {
            _missionResources.AddResourcesAfterDestroyBuilding(GetCurrentBuilding().ResourcesForBuild, _buildingHealth.GetCurrentHealthPercent());
         }
      }
   }

   /// <summary>
   /// Можно вызывать только при строительстве нового здания или после вызова смерти здания
   /// </summary>
   public void DestroyBuildingTile(bool isUpgrade)
   {
      if (_currentBuildingTile == null) return;
      StopConstruction();

      if (_currentBuildingTile.BuildingTileView == BuildingTileViewEnum.Walls)
      {
         _buildingTileWallGates.Reset();
         _currentBuildingTile = null; //иначе стена не туда повернет, так как соседа IsWall найдет в цикле

         RefreshNeighbourWallTiles();
      }
      if (!isUpgrade) _buildingHealth.DestroyHealthSlider(); // вызываем еще раз, так как есть ситуации, когда не вызывается уничтожение слайдера, например уничтожаем сами, а не через реальную смерть
      _currentBuildingTile = null;
      _currentLevel = 0;
      _tacticCardIncreaseDamageLevel = 0;
      CustomEvents.FireChangeEcology(_tileObject.TileEcology().GetEcology(GetEcologyEnum.Total), _tileObject.GetId(), false);

      var tileObjectsView = _tileObject.GroundTileObject().CurrentGroundTileObject().GetComponent<TileObjectsView>();
      if (tileObjectsView != null) tileObjectsView.RefreshObjects();
      Destroy(_currentBuildingGameObject);
   }

   public void PrepareSetResourceRequired()
   {
      var resourceRecept = GetCurrentBuilding().ResourcesProduction.Length == 0 ? null : GetCurrentBuilding().ResourcesProduction[0].ResourceRecept;
      var resourcesForWork = GetCurrentBuilding().ResourcesForWork;

      if (GetCurrentBuilding().ResourcesForWork.Length == 0)
      {
         _tileObject.SetResourceForWork(null, 0, resourceRecept);
      }
      else
      {
         _tileObject.SetNewResourceRequiredAfterUpgradeBuilding(resourcesForWork, resourceRecept);
      }
   }

   public void LoadResourceRequired(BuildingData data)
   {
      var resource = data.RequiredResource < 0 ? null : _missionResources.GetResourceForNumber(data.RequiredResource);
      var amount = data.RequiredResourceAmount;
      var recept = GetCurrentBuilding().ResourcesProduction.Length == 0 ? null : GetCurrentBuilding().ResourcesProduction[0].ResourceRecept;
      _tileObject.SetResourceForWork(resource, amount, recept);
   }

   public void LoadResourceProduction(BuildingData data)
   {
      if (GetCurrentBuilding().ResourcesProduction.Length == 0) return;

      var resource = _missionResources.GetResourceForNumber(data.ResourceProduction);
      ResourceRecept[] recept = null;

      for (int i = 0; i < GetCurrentBuilding().ResourcesProduction.Length; i++)
      {
         if (resource == GetCurrentBuilding().ResourcesProduction[i].ProductionResource)
         {
            recept = GetCurrentBuilding().ResourcesProduction[i].ResourceRecept;
         }
      }

      _tileObject.SetResourceProduction(resource, recept);
   }

   public void LoadBuildingTile(TileDataWrapper tileDataWrapper)
   {
      _currentBuildingTile = _tilesSystem.GetBuildingTileForId(tileDataWrapper.BuildingData.BuildingTileTypeId);
      _currentLevel = tileDataWrapper.BuildingData.BuildingTileLevel;
      _tacticCardIncreaseDamageLevel = tileDataWrapper.BuildingData.TacticCardIncreaseDamage;

      if (tileDataWrapper.BuildingData.IsConstructionNow)
      {
         if (tileDataWrapper.BuildingData.IsUpgradeBase)
         {
            _previousBaseBuildingHealth = tileDataWrapper.BuildingData.PreviousBaseBuildingHealth;
            UpgradeBaseAterLoad(_currentBuildingTile, _currentLevel);
            _buildingHealth.LoadBuildingHealth(GetCurrentBuilding(), tileDataWrapper.BuildingData.BuildingHealth, true);
         }
         else
         {
            BeginConstruction(_currentBuildingTile, _currentLevel, true);
            _buildingHealth.LoadBuildingHealth(GetCurrentBuilding(), tileDataWrapper.BuildingData.BuildingHealth, true);
         }
      }
      else
      {
         _currentBuildingGameObject = _diContainer.InstantiatePrefab(_currentBuildingTile.TileObject, _buildingParent.position, Quaternion.identity, null);
         _currentBuildingGameObject.transform.SetParent(_buildingParent);
         _buildingTileTransform.LoadTransform(tileDataWrapper.BuildingData);
         _buildingTileTransform.SetTransform(_currentBuildingGameObject.transform, GetCurrentBuilding(), _tileObject);
         _buildingLevels = _currentBuildingGameObject.GetComponent<BuildingLevels>();
         _buildingLevels.SetBuildingLevelView(_currentLevel, _tileObject);
         _buildingLevels.SetBuildingProductionView();
         LoadResourceRequired(tileDataWrapper.BuildingData);
         CustomEvents.FireChangeEcology(_tileObject.TileEcology().GetEcology(GetEcologyEnum.Total), _tileObject.GetId(), false);
         LoadResourceProduction(tileDataWrapper.BuildingData);
         CheckIsExtrabaseTileObject();


         _buildingHealth.LoadBuildingHealth(GetCurrentBuilding(), tileDataWrapper.BuildingData.BuildingHealth, false);

         _tileObject.SetBuildingWork(tileDataWrapper.BuildingData.IsBuildingWork);
         _tileObject.CheckResourceRequired(true);

         var rotationView = _tileObject.BuildingTileObject().CurrentBuildingGameObject().GetComponent<RotationView>();
         if (rotationView != null) rotationView.LoadRotate(tileDataWrapper.BuildingData.BuildingRotation);

         _tileObject.SetGeneralRepairSelect(tileDataWrapper.BuildingData.IsGeneralRepairSelect);
         CustomEvents.FireChangeGeneralRepairTileObject(_tileObject);
      }
   }

   private void OnDestroy()
   {
      CustomEvents.OnCompleteLoadTiles -= UpdateWallsAndGates;
   }
}