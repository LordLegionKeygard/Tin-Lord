using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BuildingTile : MonoBehaviour
{
   [Inject] private DiContainer _diContainer;
   [SerializeField] private Tile _currentBuildingTile;
   [SerializeField] private Transform _buildingParent;
   [SerializeField] private GameObject _currentBuildingTileObject;
   [SerializeField] private BuildingLevels _buildingLevels;

   public bool HaveTile() => _currentBuildingTile != null;
   public Tile CurrentBuildingTile() => _currentBuildingTile;
   public int CurrentBuildingLevel() => _buildingLevels.CurrentBuildingLevel();
   public bool IsCanUpgrade() => _currentBuildingTile != null ? CurrentBuildingLevel() < _currentBuildingTile.UpgradeBuildingWrapper.Length : false;

   public void SpawnBuildingTile(Tile tile)
   {
      _currentBuildingTile = tile;

      _currentBuildingTileObject = _diContainer.InstantiatePrefab(_currentBuildingTile.TileObject, _buildingParent.position, Quaternion.identity, null);

      _currentBuildingTileObject.transform.SetParent(_buildingParent);

      _buildingLevels = _currentBuildingTileObject.GetComponent<BuildingLevels>();

      _buildingLevels.SetBuildingView();
   }

   public void UpgradeBuildingTile()
   {
      _buildingLevels.SetBuildingView();
   }
}
