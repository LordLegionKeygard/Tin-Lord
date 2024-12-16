using System.Collections;
using UnityEngine;
using Zenject;

public class BuildingTile : MonoBehaviour
{
   [Inject] private DiContainer _diContainer;
   [Inject] private PlayerResources _playerResources;
   [SerializeField] private Transform _buildingParent;
   private BuildingLevels _buildingLevels;
   private GameObject _currentBuildingTileGameObject;
   private Tile _currentBuildingTile;
   private BuildingHealth _buildingHealth;
   private BuildingTileProtective _buildingTileProtective;
   public BuildingTileProtective CurrentBuildingTileProtective() => _buildingTileProtective;
   private TileObject _currentTileObject;
   private int _currentLevel;
   private bool _isConstructionNow;
   private float _previousBuildingHealthPercent;
   public bool ConstructionNow() => _isConstructionNow;
   public bool HaveTile() => _currentBuildingTile != null;
   public Tile CurrentBuildingTile() => _currentBuildingTile;
   public GameObject CurrentBuildingTileGameObject() => _currentBuildingTileGameObject;
   public bool HaveBuildingTileGameObject() => _currentBuildingTileGameObject != null;
   public int CurrentBuildingLevel() => _currentLevel;
   public Building CurrentBuilding() => _currentBuildingTile.Buildings[_currentLevel - 1];
   public bool IsProtectiveTile() => _currentBuildingTile == null ? false : _currentBuildingTile.BuildingTileView == BuildingTileViewEnum.ProtectiveStructures;
   public bool IsEcologyBuilding() => _currentBuildingTile == null ? false : _currentBuildingTile.BuildingTileView == BuildingTileViewEnum.EcologyPurifier;
   public bool NeightbourTileIsProtective(int number) => _currentTileObject.GetNeighbourBuildingTile(number) == null ? false : _currentTileObject.GetNeighbourBuildingTile(number).IsProtectiveTile();
   public bool IsCanUpgrade() => _currentBuildingTile != null ? CurrentBuildingLevel() < _currentBuildingTile.Buildings.Length : false;

   private void Awake()
   {
      _buildingHealth = GetComponent<BuildingHealth>();
      _buildingTileProtective = GetComponent<BuildingTileProtective>();
   }

   public void FirstConstructingBuilding(Tile tile, int level, TileObject tileObject)
   {
      _isConstructionNow = true;
      _currentBuildingTile = tile;
      _currentTileObject = tileObject;
      _currentLevel = level;
      float constructionTime = _currentBuildingTile.Buildings[_currentLevel - 1].ConstructionTime;

      _buildingHealth.SetNewBuildingHealth(CurrentBuilding(), isConstruction: true);
      StartCoroutine(BuildingTimer(constructionTime));
   }

   private IEnumerator BuildingTimer(float constructionTime)
   {
      float elapsed = 0f;

      while (elapsed < constructionTime)
      {
         if (_buildingHealth.IsDeath())
         {
            _isConstructionNow = false;
            yield break;
         }

         elapsed += Time.deltaTime;
         float healthIncrement = _buildingHealth.MaxHealth / constructionTime * Time.deltaTime;
         _buildingHealth.ConstructionIncreaseHealth(healthIncrement);
         yield return null;
      }

      _isConstructionNow = false;
      FirstSpawnBuilding();
   }

   public void FirstSpawnBuilding()
   {
      if (_currentBuildingTile.BuildingTileView == BuildingTileViewEnum.Base) CustomEvents.FireSetBase();

      _currentBuildingTileGameObject = _diContainer.InstantiatePrefab(_currentBuildingTile.TileObject, _buildingParent.position, Quaternion.identity, null);
      _currentBuildingTileGameObject.transform.SetParent(_buildingParent);
      _buildingLevels = _currentBuildingTileGameObject.GetComponent<BuildingLevels>();
      _buildingLevels.SetBuildingLevelView(_currentLevel, _currentTileObject);
      SetResourceRequiredAfterSpawnOrUpgradeBuilding();
      CustomEvents.FireChangeEcology(_currentTileObject.TileEcology().GetEcology(GetEcologyEnum.Total), _currentTileObject.GetId(), false);
      _buildingLevels.SetBuildingProductionView();
      if (CurrentBuilding().ResourcesProduction.Length != 0) _currentTileObject.SetResourceProduction(CurrentBuilding().ResourcesProduction[0].ProductionResource, CurrentBuilding().ResourcesProduction[0].ResourceRecept);

      _buildingHealth.SetNewBuildingHealth(CurrentBuilding(), false);

      if (IsProtectiveTile()) UpdateProtectiveTiles();

      _currentTileObject.SetBuildingWork(true);
      _currentTileObject.CheckResourceRequired(true);
   }

   public void UpgradeConstructingBuilding(int newLevel, TileObject tileObject)
   {
      _isConstructionNow = true;
      _currentTileObject = tileObject;
      float constructionTime = _currentBuildingTile.Buildings[newLevel - 1].ConstructionTime;
      _previousBuildingHealthPercent = tileObject.BuildingHealth().GetCurrentHealthPercent();

      _buildingHealth.SetNewBuildingHealth(CurrentBuilding(), isConstruction: true);
      StartCoroutine(UpgradeBuildingTimer(constructionTime, newLevel));
   }

   private IEnumerator UpgradeBuildingTimer(float constructionTime, int newLevel)
   {
      float elapsed = 0f;

      while (elapsed < constructionTime)
      {
         if (_buildingHealth.IsDeath())
         {
            _isConstructionNow = false;
            yield break;
         }

         elapsed += Time.deltaTime;
         float healthIncrement = _buildingHealth.MaxHealth / constructionTime * Time.deltaTime;
         _buildingHealth.ConstructionIncreaseHealth(healthIncrement);
         yield return null;
      }

      _isConstructionNow = false;
      UpgradeBuilding(newLevel, _currentLevel);
   }

   public void UpgradeBuilding(int newLevel, int previousLevel)
   {
      _currentLevel = newLevel;

      _buildingLevels.SetBuildingLevelView(_currentLevel, _currentTileObject);
      SetResourceRequiredAfterSpawnOrUpgradeBuilding();
      CustomEvents.FireChangeEcology(_currentTileObject.TileEcology().GetEcology(GetEcologyEnum.Total), _currentTileObject.GetId(), false);
      _buildingLevels.SetBuildingProductionView();
      if (CurrentBuilding().ResourcesProduction.Length != 0) _currentTileObject.SetResourceProduction(_currentTileObject.CurrentResourceProduction(), _currentTileObject.CurrentResourceRecept());

      _buildingHealth.SetNewBuildingHealth(CurrentBuilding(), false);

      if (IsProtectiveTile()) UpdateProtectiveTiles();
      _currentTileObject.CheckResourceRequired(true);

      var previousBuilding = _currentTileObject.BuildingTileObject()._currentBuildingTile.Buildings[previousLevel - 1].ResourcesForBuild;

      _playerResources.AddResourcesAfterDestroyBuilding(previousBuilding, _previousBuildingHealthPercent); // возвращаем часть ресурсов за прошлое здание
   }

   private void UpdateProtectiveTiles()
   {
      _buildingTileProtective.PrepareProtective();

      RefreshProtectiveNeighbourTiles();
   }

   private void RefreshProtectiveNeighbourTiles()
   {
      for (int i = 0; i < _currentTileObject.GetNeighbourBuildingTilesArray().Length; i++)
      {
         if (!IsNeedCheck(i, true)) continue;

         if (_currentTileObject.GetNeighbourBuildingTile(i).IsProtectiveTile())
         {
            _currentTileObject.GetNeighbourBuildingTile(i).CurrentBuildingTileProtective().PrepareProtective();
         }
      }
   }

   public bool IsNeedCheck(int i, bool cross)
   {
      if (cross)
      {
         if (i is (int)TileDirectionEnum.NorthEast or (int)TileDirectionEnum.NorthWest or (int)TileDirectionEnum.SouthEast or (int)TileDirectionEnum.SouthWest) return false;
      }

      if (_currentTileObject.GetNeighbourBuildingTile(i) == null) return false;
      if (!_currentTileObject.GetNeighbourBuildingTile(i).HaveTile()) return false;

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
      CustomEvents.FireChangeEcology(_currentTileObject.TileEcology().GetEcology(GetEcologyEnum.Total), _currentTileObject.GetId(), false);

      var tileObjectsView = _currentTileObject.GroundTileObject().CurrentGroundTileObject().GetComponent<TileObjectsView>();
      if (tileObjectsView != null) tileObjectsView.RefreshObjects();
      Destroy(_currentBuildingTileGameObject);
   }

   public void SetResourceRequiredAfterSpawnOrUpgradeBuilding()
   {
      if (CurrentBuilding().ResourcesForWork.Length == 0)
      {
         if (CurrentBuilding().ResourcesProduction.Length == 0) _currentTileObject.SetResourceRequied(null, 0, null);
         else _currentTileObject.SetResourceRequied(null, 0, CurrentBuilding().ResourcesProduction[0].ResourceRecept);
      }
      else
      {
         var resourceRecept = CurrentBuilding().ResourcesProduction.Length == 0 ? null : CurrentBuilding().ResourcesProduction[0].ResourceRecept; //при спавне здания ставим 0 ресурс из массива
         var resourcesForWork = CurrentBuilding().ResourcesForWork[0];
         _currentTileObject.SetResourceRequied(resourcesForWork.ResourceForWork, resourcesForWork.ResourcesForWorkAmount, resourceRecept);
      }
   }
}
