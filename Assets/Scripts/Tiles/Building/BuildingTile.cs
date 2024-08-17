using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BuildingTile : MonoBehaviour
{
   [Inject] private DiContainer _diContainer;
   [Inject] private PlayerResources _playerResources;
   [SerializeField] private Tile _currentTile;
   [SerializeField] private Transform _buildingParent;
   [SerializeField] private GameObject _currentBuildingTileObject;
   [SerializeField] private BuildingLevels _buildingLevels;
   private TileObject _tileObject;

   public bool HaveTile() => _currentTile != null;
   public Tile CurrentBuildingTile() => _currentTile;
   public int CurrentBuildingLevel() => _buildingLevels.CurrentBuildingLevel();
   public Building CurrentBuilding() => _currentTile.Buildings[_buildingLevels.CurrentBuildingLevel() - 1];

   public bool IsCanUpgrade() => _currentTile != null ? CurrentBuildingLevel() < _currentTile.Buildings.Length : false;

   public void SpawnBuildingTile(Tile tile, int level, TileObject tileObject)
   {
      _currentTile = tile;
      _tileObject = tileObject;

      _currentBuildingTileObject = _diContainer.InstantiatePrefab(_currentTile.TileObject, _buildingParent.position, Quaternion.identity, null);
      _currentBuildingTileObject.transform.SetParent(_buildingParent);
      _buildingLevels = _currentBuildingTileObject.GetComponent<BuildingLevels>();
      _buildingLevels.SetBuildingLevelView(level, tileObject);
      tileObject.IsBuildingWork = true;
      CustomEvents.FireChangeEcology(tileObject.TileEcology().GetEcology(GetEcologyEnum.Total), tileObject.GetId(), false);
      SetResourceRequiredAfterSpawnOrUpgradeBuilding();
      _buildingLevels.CheckBuildingProductionView();
      if (CurrentBuilding().ResourcesProduction.Length != 0) tileObject.SetResourceProduction(CurrentBuilding().ResourcesProduction[0].ProductionResource, CurrentBuilding().ResourcesProduction[0].ResourceRecept);
   }

   public void UpgradeBuildingTile(int level, TileObject tileObject)
   {
      _tileObject = tileObject;

      _buildingLevels.SetBuildingLevelView(level, tileObject);
      CustomEvents.FireChangeEcology(tileObject.TileEcology().GetEcology(GetEcologyEnum.Total), tileObject.GetId(), false);
      SetResourceRequiredAfterSpawnOrUpgradeBuilding();
      _buildingLevels.CheckBuildingProductionView();
      if (CurrentBuilding().ResourcesProduction.Length != 0) tileObject.SetResourceProduction(CurrentBuilding().ResourcesProduction[0].ProductionResource, CurrentBuilding().ResourcesProduction[0].ResourceRecept);
   }

   public void DestroyBuildingTile()
   {
      _playerResources.AddResourcesFromDestroyBuilding(CurrentBuilding().ResourcesForBuild);
      _currentTile = null;
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
