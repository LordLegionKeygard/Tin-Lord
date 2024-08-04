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
   public UpgradeBuildingWrapper CurrentUpgradeBuildingWrapper() => _currentTile.UpgradeBuildingWrapper[_buildingLevels.CurrentBuildingLevel() - 1];
   public bool IsCanUpgrade() => _currentTile != null ? CurrentBuildingLevel() < _currentTile.UpgradeBuildingWrapper.Length : false;

   public void SpawnBuildingTile(Tile tile, int level, TileObject tileObject)
   {
      _currentTile = tile;
      _tileObject = tileObject;

      _currentBuildingTileObject = _diContainer.InstantiatePrefab(_currentTile.TileObject, _buildingParent.position, Quaternion.identity, null);

      _currentBuildingTileObject.transform.SetParent(_buildingParent);

      _buildingLevels = _currentBuildingTileObject.GetComponent<BuildingLevels>();

      _buildingLevels.SetBuildingLevelView(level, tileObject);

      CustomEvents.FireChangeEcology(tileObject.TileEcology().GetEcology(GetEcologyEnum.Total), tileObject.GetId(), false);
      tileObject.BuildingResourcesRequired().SetResourceRequiredAfterSpawnOrUpgradeBuilding(tileObject, CurrentUpgradeBuildingWrapper().ResourceRequiredEnum);
      _buildingLevels.CheckBuildingProductionView();
   }

   public void UpgradeBuildingTile(int level, TileObject tileObject)
   {
      _tileObject = tileObject;
      _buildingLevels.SetBuildingLevelView(level, tileObject);

      CustomEvents.FireChangeEcology(tileObject.TileEcology().GetEcology(GetEcologyEnum.Total), tileObject.GetId(), false);
      tileObject.BuildingResourcesRequired().SetResourceRequiredAfterSpawnOrUpgradeBuilding(tileObject, CurrentUpgradeBuildingWrapper().ResourceRequiredEnum);
      _buildingLevels.CheckBuildingProductionView();
   }

   public void DestroyBuildingTile()
   {
      _playerResources.AddResourcesFromDestroyBuilding(CurrentUpgradeBuildingWrapper().ResourcesForBuild);
      _currentTile = null;
      _tileObject.ClearBuildingInfo();
      CustomEvents.FireChangeEcology(_tileObject.TileEcology().GetEcology(GetEcologyEnum.Total), _tileObject.GetId(), false);

      var tileObjectsView = _tileObject.GroundTileObject().CurrentGroundTileObject().GetComponent<TileObjectsView>();
      if (tileObjectsView != null) tileObjectsView.RefreshObjects();
      Destroy(_currentBuildingTileObject);

   }
}
