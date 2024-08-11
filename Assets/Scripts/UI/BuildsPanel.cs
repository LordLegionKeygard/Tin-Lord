using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;
using UnityEngine.UI;

public class BuildsPanel : MonoBehaviour
{
    [Inject] private DiContainer _diContainer;
    [SerializeField] private BuildingItem _buildingItem;
    [SerializeField] private Transform _content;
    private List<GameObject> _buildingsList = new List<GameObject>();
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private ScrollRect _scrollRect;

    public void SpawnBuildingItemsInScrollView(TileObject tileObject, SelectTilePanel selectTilePanel, Tile tile) //все здания в типе
    {
        ClearListObjects();

        var length = tile.UpgradeBuildingWrapper;
        _scrollRect.horizontal = length.Length > 3;
        _scrollRect.horizontalNormalizedPosition = 0f;

        for (int i = 0; i < length.Length; i++)
        {
            var item = _diContainer.InstantiatePrefab(_buildingItem, transform.position, Quaternion.identity, null);
            item.transform.SetParent(_content);
            item.GetComponent<BuildingItem>().SetBuildingInfo(tileObject, selectTilePanel, i + 1, tile, BuildingState.FirstBuild);
            _buildingsList.Add(item.gameObject);
        }
    }

    public void SpawnUpgradeItemsInScrollView(TileObject tileObject, SelectTilePanel selectTilePanel) //оставшиеся здания в типе
    {
        ClearListObjects(); 
        var tile = tileObject.BuildingTileObject().CurrentBuildingTile();
        var length = tile.UpgradeBuildingWrapper;
        _scrollRect.horizontal = length.Length > 3;
        _scrollRect.horizontalNormalizedPosition = 0f;
        var level = tileObject.BuildingTileObject().CurrentBuildingLevel();

        for (int i = level; i < length.Length; i++)
        {
            var item = _diContainer.InstantiatePrefab(_buildingItem, transform.position, Quaternion.identity, null);
            item.transform.SetParent(_content);
            item.GetComponent<BuildingItem>().SetBuildingInfo(tileObject, selectTilePanel, i + 1, tile, BuildingState.UpgradeBuilding);
            _buildingsList.Add(item.gameObject);
        }
    }

    public void ClearListObjects()
    {
        foreach (var item in _buildingsList)
        {
            Destroy(item);
        }
        _buildingsList.Clear();
    }
}
