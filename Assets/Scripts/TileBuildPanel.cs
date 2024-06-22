using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBuildPanel : MonoBehaviour
{
    [SerializeField] private BuildingItem _buildingItem;
    [SerializeField] private Transform _content;
    [SerializeField] private BuildingsOnTileInfo _buildingsOnTileInfo;
    private List<GameObject> _buildingItemsList = new List<GameObject>();

    public void SpawnBuildingItemsInScrollView(TileObject tileObject, SelectTilePanel selectTilePanel)
    {
        var tiles = _buildingsOnTileInfo.BuildingsOnTileInfoWrapper[(int)tileObject.GroundTileObject().CurrentGroundTile().GroundTileView - 1].BuildingTiles;

        for (int i = 0; i < tiles.Length; i++)
        {
            var item = Instantiate(_buildingItem, transform.position, Quaternion.identity);
            item.transform.SetParent(_content);
            item.SetBuildingTile(tiles[i], tileObject, selectTilePanel);
            _buildingItemsList.Add(item.gameObject);
        }
    }

    public void SpawnUpgradeItemInScrollView(TileObject tileObject, SelectTilePanel selectTilePanel)
    {
        var item = Instantiate(_buildingItem, transform.position, Quaternion.identity);
        item.transform.SetParent(_content);
        item.SetUpgradeTile(tileObject, selectTilePanel);
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
