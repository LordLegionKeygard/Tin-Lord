using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileBuildPanel : MonoBehaviour
{
    [Inject] private DiContainer _diContainer;
    [SerializeField] private TilesSystem _tileSystem;
    [SerializeField] private BuildingItem _buildingItem;
    [SerializeField] private Transform _content;
    [SerializeField] private BuildingsOnTileInfo _buildingsOnTileInfo;
    private List<GameObject> _buildingItemsList = new List<GameObject>();

    public void SpawnBuildingItemsInScrollView(TileObject tileObject, SelectTilePanel selectTilePanel)
    {
        var tiles = _buildingsOnTileInfo.BuildingsOnTileInfoWrapper[(int)tileObject.GroundTileObject().CurrentGroundTile().GroundTileView - 1].BuildingTiles;
        
        for (int i = 0; i < tiles.Length; i++)
        {
            var item = _diContainer.InstantiatePrefab(_buildingItem, transform.position, Quaternion.identity, null);
            item.transform.SetParent(_content);
            item.GetComponent<BuildingItem>().SetBuildingTile(tiles[i], tileObject, selectTilePanel);
            _buildingItemsList.Add(item.gameObject);
        }
    }

    public void SpawnUpgradeItemInScrollView(TileObject tileObject, SelectTilePanel selectTilePanel)
    {
        var item = _diContainer.InstantiatePrefab(_buildingItem, transform.position, Quaternion.identity, null);
        item.transform.SetParent(_content);
        item.GetComponent<BuildingItem>().SetUpgradeTile(tileObject, selectTilePanel);
        _buildingItemsList.Add(item.gameObject);
    }

    private void ClearListObjects()
    {
        foreach (var item in _buildingItemsList)
        {
            Destroy(item);
        }
        _buildingItemsList.Clear();
    }

    private void OnDisable()
    {
        ClearListObjects();
    }
}
