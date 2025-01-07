using System.Collections;
using UnityEngine;
using Zenject;

public class BuildingTile : MonoBehaviour
{
   [Inject] private DiContainer _diContainer;
   [Inject] private PlayerResources _playerResources;
   [Inject] private TilesSystem _tilesSystem;
   [SerializeField] private Transform _buildingParent;
   private BuildingLevels _buildingLevels;
   private GameObject _currentBuildingGameObject;
   private Tile _currentBuildingTile;
   private BuildingHealth _buildingHealth;
   private BuildingTileProtective _buildingTileProtective;
   private BuildingTileTransform _buildingTileTransform;
   public BuildingTileTransform BuildingTileTransform() => _buildingTileTransform;
   public BuildingTileProtective CurrentBuildingTileProtective() => _buildingTileProtective;
   [SerializeField] private TileObject _tileObject;
   private int _currentLevel;
   private bool _isConstructionNow;
   private float _previousBuildingHealthPercent;
   private GameObject _constructionPrefab;
   private ConstructionBuildingView _constructionView;
   public bool ConstructionNow() => _isConstructionNow;
   public bool HaveTile() => _currentBuildingTile != null;
   public Tile CurrentBuildingTile() => _currentBuildingTile;
   public GameObject CurrentBuildingGameObject() => _currentBuildingGameObject;
   public bool HaveBuildingGameObject() => _currentBuildingGameObject != null;
   public int CurrentBuildingLevel() => _currentLevel;
   public Building CurrentBuilding() => _currentBuildingTile.Buildings[_currentLevel - 1];
   public bool IsProtectiveTile() => _currentBuildingTile == null ? false : _currentBuildingTile.BuildingTileView == BuildingTileViewEnum.ProtectiveStructures;
   public bool IsEcologyBuilding() => _currentBuildingTile == null ? false : _currentBuildingTile.BuildingTileView == BuildingTileViewEnum.EcologyPurifier;
   public bool NeightbourTileIsProtective(int number) => _tileObject.GetNeighbourBuildingTile(number) == null ? false : _tileObject.GetNeighbourBuildingTile(number).IsProtectiveTile();
   public bool IsCanUpgrade() => _currentBuildingTile != null ? CurrentBuildingLevel() < _currentBuildingTile.Buildings.Length : false;


   private void Awake()
   {
      _buildingHealth = GetComponent<BuildingHealth>();
      _buildingTileProtective = GetComponent<BuildingTileProtective>();
      _buildingTileTransform = GetComponent<BuildingTileTransform>();
   }

   public void SpawnDestroyVFX()
   {
      var spawnPos = CurrentBuildingTile().IsFourTile ? new Vector3(transform.position.x + 5, transform.position.y, transform.position.z + 5) : transform.position;
      Instantiate(CurrentBuilding().DestroyVFXPrefab, spawnPos, Quaternion.identity);
   }

   public void ConstructingBuilding(Tile tile, int level)
   {
      _currentBuildingTile = tile;
      _currentLevel = level;
      _buildingHealth.SetNewBuildingHealth(CurrentBuilding(), isConstruction: true);
      _buildingTileTransform.CachedRandomTransform(CurrentBuilding());

      SpawnConstructionPrefab();
      StartCoroutine(BuildingTimer());
   }

   private void SpawnConstructionPrefab()
   {
      _constructionPrefab = Instantiate(CurrentBuilding().ConstructionPrefab, _buildingParent.position, Quaternion.identity);
      _constructionPrefab.transform.SetParent(_buildingParent);
      _buildingTileTransform.SetTransform(_constructionPrefab.transform, CurrentBuilding(), _tileObject);
   }

   private IEnumerator BuildingTimer()
   {
      _isConstructionNow = true;

      _constructionView = _constructionPrefab.GetComponent<ConstructionBuildingView>();

      while (_buildingHealth.CurrentHealth < _buildingHealth.MaxHealth)
      {
         if (_buildingHealth.IsDeath())
         {
            _isConstructionNow = false;
            yield break;
         }

         _buildingHealth.ConstructionIncreaseHealth(WorldGameInfo.ConstructionSpeed * Time.deltaTime);
         _constructionView.UpdateShaderByHealth(_buildingHealth.CurrentHealth, _buildingHealth.MaxHealth);

         yield return null;
      }

      _isConstructionNow = false;
      SpawnBuilding();
   }

   public void SpawnBuilding()
   {
      if (_currentBuildingTile.BuildingTileView == BuildingTileViewEnum.Base) CustomEvents.FireSetBase();

      _currentBuildingGameObject = _diContainer.InstantiatePrefab(_currentBuildingTile.TileObject, _buildingParent.position, Quaternion.identity, null);
      Destroy(_constructionPrefab);
      _currentBuildingGameObject.transform.SetParent(_buildingParent);
      _buildingTileTransform.SetTransform(_currentBuildingGameObject.transform, CurrentBuilding(), _tileObject);
      _buildingLevels = _currentBuildingGameObject.GetComponent<BuildingLevels>();
      _buildingLevels.SetBuildingLevelView(_currentLevel, _tileObject);
      _buildingLevels.SetBuildingProductionView();
      SetResourceRequiredAfterSpawnOrUpgradeBuilding();
      CustomEvents.FireChangeEcology(_tileObject.TileEcology().GetEcology(GetEcologyEnum.Total), _tileObject.GetId(), false);
      if (CurrentBuilding().ResourcesProduction.Length != 0) _tileObject.SetResourceProduction(CurrentBuilding().ResourcesProduction[0].ProductionResource, CurrentBuilding().ResourcesProduction[0].ResourceRecept);

      _buildingHealth.SetNewBuildingHealth(CurrentBuilding(), false);

      if (IsProtectiveTile()) UpdateProtectiveTiles();

      _tileObject.SetBuildingWork(true);
      _tileObject.CheckResourceRequired(true);
   }

   public void UpgradeBaseBuilding(int newLevel, TileObject tileObject)
   {
      _currentLevel = newLevel;
      _previousBuildingHealthPercent = tileObject.BuildingHealth().GetCurrentHealthPercent();
      _buildingHealth.SetUpgradeBuildingHealth(CurrentBuilding(), isConstruction: true);
      _buildingLevels.DisableAllBuilding();
      SpawnConstructionPrefab();
      StartCoroutine(UpgradeBaseTimer(newLevel));
   }

   private IEnumerator UpgradeBaseTimer(int newLevel)
   {
      var previousHealth = _buildingHealth.CurrentHealth;
      _isConstructionNow = true;

      _constructionView = _constructionPrefab.GetComponent<ConstructionBuildingView>();

      while (_buildingHealth.CurrentHealth < _buildingHealth.MaxHealth)
      {
         if (_buildingHealth.IsDeath())
         {
            _isConstructionNow = false;
            yield break;
         }

         _buildingHealth.ConstructionIncreaseHealth(WorldGameInfo.ConstructionSpeed * Time.deltaTime);
         _constructionView.UpdateShaderByHealth(_buildingHealth.CurrentHealth - previousHealth, _buildingHealth.MaxHealth - previousHealth);

         yield return null;
      }

      _isConstructionNow = false;
      Destroy(_constructionPrefab);

      UpgradeBuilding(newLevel, _currentLevel);
   }


   public void UpgradeBuilding(int newLevel, int previousLevel)
   {
      _currentLevel = newLevel;

      _buildingLevels.SetBuildingLevelView(_currentLevel, _tileObject);
      SetResourceRequiredAfterSpawnOrUpgradeBuilding();
      CustomEvents.FireChangeEcology(_tileObject.TileEcology().GetEcology(GetEcologyEnum.Total), _tileObject.GetId(), false);
      _buildingLevels.SetBuildingProductionView();
      if (CurrentBuilding().ResourcesProduction.Length != 0) _tileObject.SetResourceProduction(_tileObject.CurrentResourceProduction(), _tileObject.CurrentResourceRecept());

      if (IsProtectiveTile()) UpdateProtectiveTiles();
      _tileObject.CheckResourceRequired(true);

      var previousBuilding = _tileObject.BuildingTileObject()._currentBuildingTile.Buildings[previousLevel - 1].ResourcesForBuild;

      _playerResources.AddResourcesAfterDestroyBuilding(previousBuilding, _previousBuildingHealthPercent); // возвращаем часть ресурсов за прошлое здание
   }

   private void UpdateProtectiveTiles()
   {
      _buildingTileProtective.PrepareProtective();

      RefreshProtectiveNeighbourTiles();
   }

   private void RefreshProtectiveNeighbourTiles()
   {
      for (int i = 0; i < _tileObject.GetNeighbourBuildingTilesArray().Length; i++)
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

   public void DestroyBuildingTile(bool isUpgrade)
   {
      if (_currentBuildingTile == null) return;

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

   public void SetResourceRequiredAfterSpawnOrUpgradeBuilding()
   {
      if (CurrentBuilding().ResourcesForWork.Length == 0)
      {
         if (CurrentBuilding().ResourcesProduction.Length == 0) _tileObject.SetResourceRequied(null, 0, null);
         else _tileObject.SetResourceRequied(null, 0, CurrentBuilding().ResourcesProduction[0].ResourceRecept);
      }
      else
      {
         var resourceRecept = CurrentBuilding().ResourcesProduction.Length == 0 ? null : CurrentBuilding().ResourcesProduction[0].ResourceRecept; //при спавне здания ставим 0 ресурс из массива
         var resourcesForWork = CurrentBuilding().ResourcesForWork[0];
         _tileObject.SetResourceRequied(resourcesForWork.ResourceForWork, resourcesForWork.ResourcesForWorkAmount, resourceRecept);
      }
   }

   public void LoadBuildingTile(TileDataWrapper tileDataWrapper)
   {
      _currentBuildingTile = _tilesSystem.GetBuildingTileForNumber(tileDataWrapper.BuildingData.BuildingTileTypeId);
      _currentLevel = tileDataWrapper.BuildingData.BuildingTileLevel;

      _currentBuildingGameObject = _diContainer.InstantiatePrefab(_currentBuildingTile.TileObject, _buildingParent.position, Quaternion.identity, null);
      // Destroy(_constructionPrefab);
      _currentBuildingGameObject.transform.SetParent(_buildingParent);
      _buildingTileTransform.LoadTransform(tileDataWrapper.BuildingData);
      _buildingTileTransform.SetTransform(_currentBuildingGameObject.transform, CurrentBuilding(), _tileObject);
      _buildingLevels = _currentBuildingGameObject.GetComponent<BuildingLevels>();
      _buildingLevels.SetBuildingLevelView(_currentLevel, _tileObject);
      _buildingLevels.SetBuildingProductionView();
      SetResourceRequiredAfterSpawnOrUpgradeBuilding();
      CustomEvents.FireChangeEcology(_tileObject.TileEcology().GetEcology(GetEcologyEnum.Total), _tileObject.GetId(), false);
      if (CurrentBuilding().ResourcesProduction.Length != 0) _tileObject.SetResourceProduction(CurrentBuilding().ResourcesProduction[0].ProductionResource, CurrentBuilding().ResourcesProduction[0].ResourceRecept);

      _buildingHealth.LoadBuildingHealth(CurrentBuilding(), tileDataWrapper.BuildingData.BuildingHealth);

      if (IsProtectiveTile()) UpdateProtectiveTiles();

      _tileObject.SetBuildingWork(tileDataWrapper.BuildingData.IsBuildingWork);
      _tileObject.CheckResourceRequired(true);

      var rotationView = _tileObject.BuildingTileObject().CurrentBuildingGameObject().GetComponent<RotationView>();
      if (rotationView != null) rotationView.LoadRotate(tileDataWrapper.BuildingData.BuildingRotation);
   }
}