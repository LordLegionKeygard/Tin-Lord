using UnityEngine;
using Zenject;

public class BuildingTile : MonoBehaviour
{
   [Inject] private DiContainer _diContainer;
   [Inject] private PlayerResources _playerResources;
   [SerializeField] private Tile _currentBuildingTile;
   [SerializeField] private Transform _buildingParent;
   [SerializeField] private GameObject _currentBuildingTileObject;
   [SerializeField] private BuildingLevels _buildingLevels;
   private BuildingHealth _buildingHealth;
   private BuildingTileProtective _buildingTileProtective;

   public BuildingTileProtective CurrentBuildingTileProtective() => _buildingTileProtective;

   private TileObject _tileObject;
   public bool HaveTile() => _currentBuildingTile != null;
   public Tile CurrentBuildingTile() => _currentBuildingTile;
   public GameObject CurrentBuildingTileObject() => _currentBuildingTileObject;
   public int CurrentBuildingLevel() => _buildingLevels.CurrentBuildingLevel();
   public Building CurrentBuilding() => _currentBuildingTile.Buildings[_buildingLevels.CurrentBuildingLevel() - 1];

   public bool IsProtectiveTile() => _currentBuildingTile == null ? false : _currentBuildingTile.IsProtective;
   public bool NeightbourTileIsProtective(int number) => _tileObject.GetNeighbourBuildingTile(number) == null ? false : _tileObject.GetNeighbourBuildingTile(number).IsProtectiveTile();

   public bool IsCanUpgrade() => _currentBuildingTile != null ? CurrentBuildingLevel() < _currentBuildingTile.Buildings.Length : false;

   private void Awake()
   {
      _buildingHealth = GetComponent<BuildingHealth>();
      _buildingTileProtective = GetComponent<BuildingTileProtective>();
   }

   public void SpawnBuildingTile(Tile tile, int level, TileObject tileObject)
   {
      _currentBuildingTile = tile;
      _tileObject = tileObject;

      _currentBuildingTileObject = _diContainer.InstantiatePrefab(_currentBuildingTile.TileObject, _buildingParent.position, Quaternion.identity, null);
      _currentBuildingTileObject.transform.SetParent(_buildingParent);
      _buildingLevels = _currentBuildingTileObject.GetComponent<BuildingLevels>();
      _buildingLevels.SetBuildingLevelView(level, tileObject);
      tileObject.IsBuildingWork = true;
      CustomEvents.FireChangeEcology(tileObject.TileEcology().GetEcology(GetEcologyEnum.Total), tileObject.GetId(), false);
      SetResourceRequiredAfterSpawnOrUpgradeBuilding();
      _buildingLevels.CheckBuildingProductionView();
      if (CurrentBuilding().ResourcesProduction.Length != 0) tileObject.SetResourceProduction(CurrentBuilding().ResourcesProduction[0].ProductionResource, CurrentBuilding().ResourcesProduction[0].ResourceRecept);

      _buildingHealth.SetNewBuildingHealth(CurrentBuilding());

      if (IsProtectiveTile()) UpdateProtectiveTiles();
   }

   public void UpgradeBuildingTile(int level, TileObject tileObject)
   {
      _tileObject = tileObject;

      _buildingLevels.SetBuildingLevelView(level, tileObject);
      CustomEvents.FireChangeEcology(tileObject.TileEcology().GetEcology(GetEcologyEnum.Total), tileObject.GetId(), false);
      SetResourceRequiredAfterSpawnOrUpgradeBuilding();
      _buildingLevels.CheckBuildingProductionView();
      if (CurrentBuilding().ResourcesProduction.Length != 0) tileObject.SetResourceProduction(tileObject.CurrentResourceProduction(), tileObject.CurrentResourceRecept());

      _buildingHealth.SetNewBuildingHealth(CurrentBuilding());

      if (IsProtectiveTile()) UpdateProtectiveTiles();
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

   public void DestroyBuildingTile(bool isDeath)
   {
      if (!isDeath) _playerResources.AddResourcesAfterDestroyBuilding(CurrentBuilding().ResourcesForBuild);

      if (_currentBuildingTile.BuildingTileView == BuildingTileViewEnum.PretectiveStructures)
      {
         _buildingTileProtective.Reset();
         _currentBuildingTile = null; //иначе стена не туда повернет, так как соседа IsWall найдет в цикле

         RefreshProtectiveNeighbourTiles();
      }
      _buildingHealth.DestroyHealthSlider();
      _currentBuildingTile = null;
      _tileObject.ClearBuildingInfo();
      CustomEvents.FireChangeEcology(_tileObject.TileEcology().GetEcology(GetEcologyEnum.Total), _tileObject.GetId(), false);

      var tileObjectsView = _tileObject.GroundTileObject().CurrentGroundTileObject().GetComponent<TileObjectsView>();
      if (tileObjectsView != null) tileObjectsView.RefreshObjects();
      Destroy(_currentBuildingTileObject);
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
         _tileObject.SetResourceRequied(CurrentBuilding().ResourcesForWork[0].ResourceForWork, CurrentBuilding().ResourcesForWork[0].ResourcesForWorkAmount, CurrentBuilding().ResourcesProduction[0].ResourceRecept); //ставим 0 ресурс из массива
      }
   }
}
