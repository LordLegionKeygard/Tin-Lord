using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField] private ResourcesView _buildingResourcesView;
    [SerializeField] private ScrollToCard _scrollToCard;
    [SerializeField] private BuildTypesPanel _buildTypesPanel;

    public void SpawnBuildingItemsInScrollView(TileObject tileObject, SelectTilePanel selectTilePanel, Tile tile) //все здания в типе
    {
        ClearListObjects();
        _buildingResourcesView.ResetCells();

        var length = tile.Buildings;
        _scrollRect.horizontal = length.Length > 3;
        _scrollRect.horizontalNormalizedPosition = 0f;

        for (int i = 0; i < length.Length; i++)
        {
            Spawn(tileObject, selectTilePanel, i + 1, tile, BuildingState.FirstBuild);
        }
    }

    private void Spawn(TileObject tileObject, SelectTilePanel selectTilePanel, int index, Tile tile, BuildingState buildingState)
    {
        var item = _diContainer.InstantiatePrefab(_buildingItem, transform.position, Quaternion.identity, null);
        item.transform.SetParent(_content);

        var buildingItem = item.GetComponent<BuildingItem>();
        buildingItem.SetBuildingInfo(tileObject, selectTilePanel, index, tile, buildingState, _buildingResourcesView, this);
        _buildingsList.Add(buildingItem);
    }

    public void SpawnUpgradeItemsInScrollView(TileObject tileObject, SelectTilePanel selectTilePanel) //оставшиеся здания в типе
    {
        ClearListObjects();
        _buildingResourcesView.ResetCells();

        var tile = tileObject.BuildingTileObject().CurrentBuildingTile();
        var length = tile.Buildings;
        _scrollRect.horizontal = length.Length > 3;
        _scrollRect.horizontalNormalizedPosition = 0f;
        var isFullHealth = tileObject.BuildingHealth().IsFullHealth();
        var level = tileObject.BuildingTileObject().CurrentBuildingLevel();

        if (!isFullHealth)
        {
            Spawn(tileObject, selectTilePanel, level, tile, BuildingState.Repair);
        }

        for (int i = level; i < length.Length; i++)
        {
            Spawn(tileObject, selectTilePanel, i + 1, tile, BuildingState.UpgradeBuilding);
        }
    }

    public void PlyerInputBuildItemButton(int number)
    {
        if (_buildingsList.Count == 0 || _buildingsList.Count <= number - 1) return;

        var foundBuilding = _buildingsList[number - 1];

        if (foundBuilding != null)
        {            
            if (foundBuilding.IsSelect())
            {
                foundBuilding.BuildOrUpgrade();
            }
            else
            {
                _scrollToCard.SelectCard(number - 1, _buildingsList.Count - 1);
                foundBuilding.SelectView();
            }
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

    private void OnDisable()
    {
        ClearListObjects();
        _buildTypesPanel.UnselectAllTypes();
    }
}
