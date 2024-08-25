using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;
using UnityEngine.UI;

public class BuildsPanel : MonoBehaviour
{
    [Inject] private DiContainer _diContainer;
    private List<BuildingItem> _buildingsList = new List<BuildingItem>();
    [SerializeField] private BuildingItem _buildingItem;
    [SerializeField] private Transform _content;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private BuildingResourcesView _buildingResourcesView;

    public void SpawnBuildingItemsInScrollView(TileObject tileObject, SelectTilePanel selectTilePanel, Tile tile) //все здания в типе
    {
        ClearListObjects();

        var length = tile.Buildings;
        _scrollRect.horizontal = length.Length > 3;
        _scrollRect.horizontalNormalizedPosition = 0f;

        for (int i = 0; i < length.Length; i++)
        {
            var item = _diContainer.InstantiatePrefab(_buildingItem, transform.position, Quaternion.identity, null);
            item.transform.SetParent(_content);

            var buildingItem = item.GetComponent<BuildingItem>();
            buildingItem.SetBuildingInfo(tileObject, selectTilePanel, i + 1, tile, BuildingState.FirstBuild, _buildingResourcesView, this);
            _buildingsList.Add(buildingItem);
        }
    }

    public void SpawnUpgradeItemsInScrollView(TileObject tileObject, SelectTilePanel selectTilePanel) //оставшиеся здания в типе
    {
        ClearListObjects(); 
        var tile = tileObject.BuildingTileObject().CurrentBuildingTile();
        var length = tile.Buildings;
        _scrollRect.horizontal = length.Length > 3;
        _scrollRect.horizontalNormalizedPosition = 0f;
        var level = tileObject.BuildingTileObject().CurrentBuildingLevel();

        for (int i = level; i < length.Length; i++)
        {
            var item = _diContainer.InstantiatePrefab(_buildingItem, transform.position, Quaternion.identity, null);
            item.transform.SetParent(_content);

            var buildingItem = item.GetComponent<BuildingItem>();
            buildingItem.SetBuildingInfo(tileObject, selectTilePanel, i + 1, tile, BuildingState.UpgradeBuilding, _buildingResourcesView, this);
            _buildingsList.Add(buildingItem);
        }
    }

    public void UnselectAllBuildings()
    {
        for (int i = 0; i < _buildingsList.Count; i++)
        {
            _buildingsList[i].SelectToggleState(false);
        }
    }

    public void ClearListObjects()
    {
        foreach (var item in _buildingsList)
        {
            Destroy(item.gameObject);
        }
        _buildingsList.Clear();
    }
}
