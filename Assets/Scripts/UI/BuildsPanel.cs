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

    public void PanelViewToggle(bool state)
    {
        if (state)
        {
            _rectTransform.DOAnchorPosX(-248, 0.3f).SetUpdate(true);
        }
        else
        {
            _rectTransform.DOAnchorPosX(276, 0.3f).SetUpdate(true);
            ClearListObjects();
        }
    }

    public void SpawnBuildingItemsInScrollView(TileObject tileObject, SelectTilePanel selectTilePanel, Tile tile) //все здания в типе
    {
        ClearListObjects();
        PanelViewToggle(true);

        var length = tile.UpgradeBuildingWrapper;
        _scrollRect.horizontal = length.Length > 3;

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
        PanelViewToggle(true);
        var tile = tileObject.BuildingTileObject().CurrentBuildingTile();
        var length = tile.UpgradeBuildingWrapper;
        _scrollRect.horizontal = length.Length > 3;
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
