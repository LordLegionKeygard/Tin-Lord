using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class BuildingTile : MonoBehaviour
{
   [Inject] private DiContainer _diContainer;
   [Inject] private PlayerResources _playerResources;
   [Inject] private TilesSystem _tilesSystem;
   [Inject] private LearnedBuildingsDataWorld _learnedBuildingsDataWorld;

   [SerializeField] private Transform _buildingParent;
   [SerializeField] private TileObject _tileObject;

   private BuildingHealth _buildingHealth;
   private BuildingTileProtective _buildingTileProtective;
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

   [SerializeField] private TileObject _isRealBaseTileObject; // этот тайл является экстра обьектом базы и хранит текущий тайл базы

   #region Публичные геттеры
   public TileObject IsExtraBaseTileObject() => _isRealBaseTileObject;
   public BuildingTileTransform GetBuildingTileTransform() => _buildingTileTransform;
   public BuildingLevels GetBuildingLevels() => _buildingLevels;
   public BuildingTileProtective CurrentBuildingTileProtective() => _buildingTileProtective;
   private ConstructionBuildingView _constructionView;
   public bool IsConstructionNow() => _isConstructionNow;
   public bool IsUpgradeBase() => _isUpgradeBase;
   public float PreviousBaseBuildingHealth() => _previousBaseBuildingHealth;
   public bool HaveTile() => _currentBuildingTile != null;
   public Tile CurrentBuildingTile() => _currentBuildingTile;
   public GameObject CurrentBuildingGameObject() => _currentBuildingGameObject;
   public bool HaveBuildingGameObject() => _currentBuildingGameObject != null;
   public int CurrentBuildingLevel() => _currentLevel;
   public Building CurrentBuilding() => _currentBuildingTile.Buildings[_currentLevel - 1];
   public bool IsProtectiveTile() => _currentBuildingTile == null ? false : _currentBuildingTile.BuildingTileView == BuildingTileViewEnum.ProtectiveStructures;
   public bool IsEcologyBuilding() => _currentBuildingTile == null ? false : _currentBuildingTile.BuildingTileView == BuildingTileViewEnum.EcologyPurifier;
   public bool NeightbourTileIsProtective(int number) => _tileObject.GetNeighbourBuildingTile(number) == null ? false : _tileObject.GetNeighbourBuildingTile(number).IsProtectiveTile();

   public bool IsCanUpgrade()
   {
      if (_currentBuildingTile != null)
      {
         return CurrentBuildingLevel() < _currentBuildingTile.Buildings.Length && _learnedBuildingsDataWorld.IsHaveLearnedBuildingUpgradeInBuildingType(_currentBuildingTile, _currentLevel);
      }

      return false;
   }

   #endregion

   private void Awake()
   {
      _buildingHealth = GetComponent<BuildingHealth>();
      _buildingTileProtective = GetComponent<BuildingTileProtective>();
      _buildingTileTransform = GetComponent<BuildingTileTransform>();

      CustomEvents.OnCompleteLoadTiles += UpdateProtectiveTiles;
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
      _buildingHealth.SetNewBuildingHealth(CurrentBuilding(), isConstruction: true);
      _buildingTileTransform.CachedRandomTransform(CurrentBuilding());
      _tileObject.ClearResourceProductionAndRequiredWhenBuildingConstruct();
      if (_currentBuildingTile.BuildingTileView == BuildingTileViewEnum.MachineProduction && !_tilesSystem.IsHaveMachineProduction()) _tilesSystem.SetIsHaveMachineProduction(true);

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
      _constructionPrefab = Instantiate(CurrentBuilding().ConstructionPrefab, _buildingParent.position, Quaternion.identity);
      _constructionPrefab.transform.SetParent(_buildingParent);
      _buildingTileTransform.SetTransform(_constructionPrefab.transform, CurrentBuilding(), _tileObject);
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

         var speed = _currentBuildingTile.BuildingTileView == BuildingTileViewEnum.Base ? WorldGameInfo.FirstBaseConstructionSpeed : WorldGameInfo.ConstructionSpeed;
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

      if (_currentBuildingTile.BuildingTileView == BuildingTileViewEnum.Base && !_tilesSystem.IsHaveBase()) CustomEvents.FireSetBase();
      CheckIsExtrabaseTileObject();

      _currentBuildingGameObject = _diContainer.InstantiatePrefab(_currentBuildingTile.TileObject, _buildingParent.position, Quaternion.identity, null);
      Destroy(_constructionPrefab);
      _currentBuildingGameObject.transform.SetParent(_buildingParent);
      _buildingTileTransform.SetTransform(_currentBuildingGameObject.transform, CurrentBuilding(), _tileObject);
      _buildingLevels = _currentBuildingGameObject.GetComponent<BuildingLevels>();
      _buildingLevels.SetBuildingLevelView(_currentLevel, _tileObject);
      _buildingLevels.SetBuildingProductionView();
      PrepareSetResourceRequired();
      CustomEvents.FireChangeEcology(_tileObject.TileEcology().GetEcology(GetEcologyEnum.Total), _tileObject.GetId(), false);
      if (CurrentBuilding().ResourcesProduction.Length != 0) _tileObject.SetNewResourceProductionAfterUpgradeBuilding(CurrentBuilding().ResourcesProduction);
      _buildingHealth.SetNewBuildingHealth(CurrentBuilding(), false);

      if (IsProtectiveTile()) UpdateProtectiveTiles();

      _tileObject.SetBuildingWork(true);
      _tileObject.CheckResourceRequired(true);
   }

   public void UpgradeBaseBuilding(int newLevel, TileObject tileObject)
   {
      _isUpgradeBase = true;
      _currentLevel = newLevel;
      _previousBuildingHealthPercent = tileObject.BuildingHealth().GetCurrentHealthPercent();
      _buildingHealth.SetUpgradeBuildingHealth(CurrentBuilding(), isConstruction: true);
      _buildingLevels.DisableAllBuilding();
      _tileObject.ClearResourceProductionAndRequiredWhenBuildingConstruct();

      SpawnConstructionPrefab();

      StartCoroutine(RunBaseUpgradeCoroutine(
            onComplete: () =>
            {
               Destroy(_constructionPrefab);
               FinalizeBaseUpgrade(newLevel, _currentLevel);
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

      _buildingLevels.SetBuildingLevelView(_currentLevel, _tileObject);
      PrepareSetResourceRequired();
      CustomEvents.FireChangeEcology(_tileObject.TileEcology().GetEcology(GetEcologyEnum.Total), _tileObject.GetId(), false);
      _buildingLevels.SetBuildingProductionView();
      _tileObject.SetResourceProduction(_tileObject.CurrentResourceProduction(), _tileObject.CurrentResourceRecept());

      if (IsProtectiveTile()) UpdateProtectiveTiles();
      _tileObject.CheckResourceRequired(true);

      var previousBuilding = _tileObject.BuildingTileObject()._currentBuildingTile.Buildings[previousLevel - 1].ResourcesForBuild;

      _playerResources.AddResourcesAfterDestroyBuilding(previousBuilding, _previousBuildingHealthPercent); // возвращаем часть ресурсов за прошлое здание
   }

   public void UpgradeBaseAterLoad(Tile tile, int level)
   {
      _currentBuildingTile = tile;
      _currentLevel = level;
      _buildingHealth.SetNewBuildingHealth(CurrentBuilding(), isConstruction: true);
      _buildingTileTransform.CachedRandomTransform(CurrentBuilding());
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

   private void UpdateProtectiveTiles()
   {
      _buildingTileProtective.PrepareProtective();

      RefreshProtectiveNeighbourTiles();
   }

   private void RefreshProtectiveNeighbourTiles()
   {
      for (int i = 0; i < 8; i++)
      {
         if (!IsNeedCheck(i, true)) continue;

         if (_tileObject.GetNeighbourBuildingTile(i).IsProtectiveTile())
         {
            _tileObject.GetNeighbourBuildingTile(i).CurrentBuildingTileProtective().PrepareProtective();
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
      if (!_tileObject.GetNeighbourBuildingTile(i).HaveTile()) return false;

      return true;
   }

   /// <summary>
   /// Можно вызывать только при строительстве нового здания или после вызова смерти здания
   /// </summary>
   public void DestroyBuildingTile(bool isUpgrade)
   {
      if (_currentBuildingTile == null) return;
      StopConstruction();

      if (isUpgrade) _playerResources.AddResourcesAfterDestroyBuilding(CurrentBuilding().ResourcesForBuild, _buildingHealth.GetCurrentHealthPercent());

      if (_currentBuildingTile.BuildingTileView == BuildingTileViewEnum.ProtectiveStructures)
      {
         _buildingTileProtective.Reset();
         _currentBuildingTile = null; //иначе стена не туда повернет, так как соседа IsWall найдет в цикле

         RefreshProtectiveNeighbourTiles();
      }
      if (!isUpgrade) _buildingHealth.DestroyHealthSlider();
      _currentBuildingTile = null;
      _currentLevel = 0;
      CustomEvents.FireChangeEcology(_tileObject.TileEcology().GetEcology(GetEcologyEnum.Total), _tileObject.GetId(), false);

      var tileObjectsView = _tileObject.GroundTileObject().CurrentGroundTileObject().GetComponent<TileObjectsView>();
      if (tileObjectsView != null) tileObjectsView.RefreshObjects();
      Destroy(_currentBuildingGameObject);
   }

   public void PrepareSetResourceRequired()
   {
      var resourceRecept = CurrentBuilding().ResourcesProduction.Length == 0 ? null : CurrentBuilding().ResourcesProduction[0].ResourceRecept;
      var resourcesForWork = CurrentBuilding().ResourcesForWork;

      if (CurrentBuilding().ResourcesForWork.Length == 0)
      {
         _tileObject.SetResourceRequied(null, 0, resourceRecept);
      }
      else
      {
         _tileObject.SetNewResourceRequiredAfterUpgradeBuilding(resourcesForWork, resourceRecept);
      }
   }

   public void LoadResourceRequired(BuildingData data)
   {
      var resource = data.RequiredResource < 0 ? null : _playerResources.GetResourceForNumber(data.RequiredResource);
      var amount = data.RequiredResourceAmount;
      var recept = CurrentBuilding().ResourcesProduction.Length == 0 ? null : CurrentBuilding().ResourcesProduction[0].ResourceRecept;
      _tileObject.SetResourceRequied(resource, amount, recept);
   }

   public void LoadResourceProduction(BuildingData data)
   {
      if (CurrentBuilding().ResourcesProduction.Length == 0) return;

      var resource = _playerResources.GetResourceForNumber(data.ResourceProduction);
      ResourceRecept[] recept = null;

      for (int i = 0; i < CurrentBuilding().ResourcesProduction.Length; i++)
      {
         if (resource == CurrentBuilding().ResourcesProduction[i].ProductionResource)
         {
            recept = CurrentBuilding().ResourcesProduction[i].ResourceRecept;
         }
      }

      _tileObject.SetResourceProduction(resource, recept);
   }

   public void LoadBuildingTile(TileDataWrapper tileDataWrapper)
   {
      _currentBuildingTile = _tilesSystem.GetBuildingTileForNumber(tileDataWrapper.BuildingData.BuildingTileTypeId);
      _currentLevel = tileDataWrapper.BuildingData.BuildingTileLevel;

      if (tileDataWrapper.BuildingData.IsConstructionNow)
      {
         if (tileDataWrapper.BuildingData.IsUpgradeBase)
         {
            _previousBaseBuildingHealth = tileDataWrapper.BuildingData.PreviousBaseBuildingHealth;
            UpgradeBaseAterLoad(_currentBuildingTile, _currentLevel);
            _buildingHealth.LoadBuildingHealth(CurrentBuilding(), tileDataWrapper.BuildingData.BuildingHealth, true);
         }
         else
         {
            BeginConstruction(_currentBuildingTile, _currentLevel, true);
            _buildingHealth.LoadBuildingHealth(CurrentBuilding(), tileDataWrapper.BuildingData.BuildingHealth, true);
         }
      }
      else
      {
         _currentBuildingGameObject = _diContainer.InstantiatePrefab(_currentBuildingTile.TileObject, _buildingParent.position, Quaternion.identity, null);
         _currentBuildingGameObject.transform.SetParent(_buildingParent);
         _buildingTileTransform.LoadTransform(tileDataWrapper.BuildingData);
         _buildingTileTransform.SetTransform(_currentBuildingGameObject.transform, CurrentBuilding(), _tileObject);
         _buildingLevels = _currentBuildingGameObject.GetComponent<BuildingLevels>();
         _buildingLevels.SetBuildingLevelView(_currentLevel, _tileObject);
         _buildingLevels.SetBuildingProductionView();
         LoadResourceRequired(tileDataWrapper.BuildingData);
         CustomEvents.FireChangeEcology(_tileObject.TileEcology().GetEcology(GetEcologyEnum.Total), _tileObject.GetId(), false);
         LoadResourceProduction(tileDataWrapper.BuildingData);
         CheckIsExtrabaseTileObject();

         _buildingHealth.LoadBuildingHealth(CurrentBuilding(), tileDataWrapper.BuildingData.BuildingHealth, false);

         _tileObject.SetBuildingWork(tileDataWrapper.BuildingData.IsBuildingWork);
         _tileObject.CheckResourceRequired(true);

         var rotationView = _tileObject.BuildingTileObject().CurrentBuildingGameObject().GetComponent<RotationView>();
         if (rotationView != null) rotationView.LoadRotate(tileDataWrapper.BuildingData.BuildingRotation);
      }
   }

   private void OnDestroy()
   {
      CustomEvents.OnCompleteLoadTiles -= UpdateProtectiveTiles;
   }
}