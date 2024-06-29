using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TileBuildPanel : MonoBehaviour
{
    [Inject] private DiContainer _diContainer;
    [SerializeField] private TilesSystem _tileSystem;
    [SerializeField] private BuildingItem _buildingItem;
    [SerializeField] private Transform _content;
    [SerializeField] private BuildingsOnTileInfo _buildingsOnTileInfo;
    private List<GameObject> _buildingItemsList = new List<GameObject>();
    [SerializeField] private ScrollRect _scrollRect;

    private void OnEnable()
    {
        _scrollRect.verticalNormalizedPosition = 1;
    }

    public void SpawnBuildingTypesInScrollView(TileObject tileObject, SelectTilePanel selectTilePanel) //тип зданий
    {
        var tiles = _buildingsOnTileInfo.BuildingsOnTileInfoWrapper[(int)tileObject.GroundTileObject().CurrentGroundTile().GroundTileView - 1].BuildingTiles;

        for (int i = 0; i < tiles.Length; i++)
        {
            var item = _diContainer.InstantiatePrefab(_buildingItem, transform.position, Quaternion.identity, null);
            item.transform.SetParent(_content);
            item.GetComponent<BuildingItem>().SetBuildingType(tiles[i], tileObject, selectTilePanel, this);
            _buildingItemsList.Add(item.gameObject);
        }
    }

    public void SpawnBuildingItemsInScrollView(TileObject tileObject, SelectTilePanel selectTilePanel, Tile tile) //все здания в типе
    {
        ClearListObjects(); // очищаем предыдущий список типов зданий

        var length = tile.UpgradeBuildingWrapper;

        for (int i = 0; i < length.Length; i++)
        {
            var item = _diContainer.InstantiatePrefab(_buildingItem, transform.position, Quaternion.identity, null);
            item.transform.SetParent(_content);
            item.GetComponent<BuildingItem>().SetSpawnFirstBuilding(tileObject, selectTilePanel, i + 1, tile);
            _buildingItemsList.Add(item.gameObject);
        }
    }

    public void SpawnUpgradeItemsInScrollView(TileObject tileObject, SelectTilePanel selectTilePanel) //оставшиеся здания в типе
    {
        var tile = tileObject.BuildingTileObject().CurrentBuildingTile();
        var length = tile.UpgradeBuildingWrapper;
        var level = tileObject.BuildingTileObject().CurrentBuildingLevel();

        for (int i = level; i < length.Length; i++)
        {
            var item = _diContainer.InstantiatePrefab(_buildingItem, transform.position, Quaternion.identity, null);
            item.transform.SetParent(_content);
            item.GetComponent<BuildingItem>().SetUpgradeBuilding(tileObject, selectTilePanel, i + 1, tile);
            _buildingItemsList.Add(item.gameObject);
        }
    }

    public void ClearListObjects()
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
