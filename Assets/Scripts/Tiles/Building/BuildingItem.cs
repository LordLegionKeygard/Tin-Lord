using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;
using UnityEngine.UI;

public class BuildingItem : MonoBehaviour
{
    [Inject] private TilesSystem _tilesSystem;
    [Inject] private PlayerResources _playerResources;
    [SerializeField] private Tile _currentTile;
    [SerializeField] private TileObject _currentTileObject;

    [Header("View")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;

    [Header("Other")]
    private SelectTilePanel _selectTilePanel;
    private BuildingState _currentBuildingState;
    private int _upgradeToLevel;

    public void SetBuildingInfo(TileObject tileObject, SelectTilePanel selectTilePanel, int level, Tile tile, BuildingState buildingState)
    {
        _currentBuildingState = buildingState;
        _selectTilePanel = selectTilePanel;
        _currentTileObject = tileObject;
        _currentTile = tile;
        _upgradeToLevel = level;

        UpdateView();
    }

    private void UpdateView()
    {
        var building = _currentTile.UpgradeBuildingWrapper[_upgradeToLevel - 1];

        _nameText.text = building.Name[Language.LanguageNumber];
        _icon.sprite = building.BuildingSprite;


        var resourcesEnough = _playerResources.ResourcesForBuildEnough(_currentTile.UpgradeBuildingWrapper[_upgradeToLevel - 1].ResourcesForBuild);
        _button.enabled = resourcesEnough;

        _nameText.color = resourcesEnough ? Color.white : Colors.BuildingItemTextDisabled;
        _icon.color = resourcesEnough ? Color.white : Colors.BuildingItemIconDisabled;
    }

    public void BuildOrUpgrade()
    {
        _playerResources.UseResourcesFromBuild(_currentTile.UpgradeBuildingWrapper[_upgradeToLevel - 1].ResourcesForBuild);
        switch (_currentBuildingState)
        {
            case BuildingState.FirstBuild:
                if (_currentTile.BuildingTileView == BuildingTileViewEnum.Base) _tilesSystem.IsHaveBase = true;
                _currentTileObject.BuildingTileObject().SpawnBuildingTile(_currentTile, _upgradeToLevel, _currentTileObject); //спавним впервые здание на тайле определенного лвла
                _selectTilePanel.ClosePanelAndRefreshInfo();
                break;
            case BuildingState.UpgradeBuilding:
                _currentTileObject.BuildingTileObject().UpgradeBuildingTile(_upgradeToLevel, _currentTileObject); //улучшаем здание
                _selectTilePanel.ClosePanelAndRefreshInfo();
                break;
        }
    }
}

public enum BuildingState
{
    FirstBuild = 0,
    UpgradeBuilding = 1,
}
