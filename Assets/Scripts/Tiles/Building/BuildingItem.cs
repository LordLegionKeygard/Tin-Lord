using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;

public class BuildingItem : MonoBehaviour
{
    [Inject] private TilesSystem _tilesSystem;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Tile _currentTile;
    [SerializeField] private TileObject _currentTileObject;
    private SelectTilePanel _selectTilePanel;
    private TileBuildPanel _tileBuildPanel;
    private BuildingState _currentBuildingState;
    private int _upgradeToLevel;

    public void SetBuildingType(Tile tile, TileObject tileObject, SelectTilePanel selectTilePanel, TileBuildPanel tileBuildPanel)
    {
        _currentBuildingState = BuildingState.BuildingType;
        _selectTilePanel = selectTilePanel;
        _currentTileObject = tileObject;
        _currentTile = tile;
        _tileBuildPanel = tileBuildPanel;

        _nameText.text = _currentTile.Name[Language.LanguageNumber];
    }

    public void SetSpawnFirstBuilding(TileObject tileObject, SelectTilePanel selectTilePanel, int level, Tile tile)
    {
        _currentBuildingState = BuildingState.FirstBuild;
        _selectTilePanel = selectTilePanel;
        _currentTileObject = tileObject;
        _currentTile = tile;
        _upgradeToLevel = level;

        _nameText.text = tile.UpgradeBuildingWrapper[_upgradeToLevel - 1].Name[Language.LanguageNumber];
    }

    public void SetUpgradeBuilding(TileObject tileObject, SelectTilePanel selectTilePanel, int level, Tile tile)
    {
        _currentBuildingState = BuildingState.UpgradeBuilding;
        _selectTilePanel = selectTilePanel;
        _currentTileObject = tileObject;
        _upgradeToLevel = level;

        _nameText.text = tile.UpgradeBuildingWrapper[_upgradeToLevel - 1].Name[Language.LanguageNumber];
    }

    public void BuildOrUpgrade()
    {
        switch (_currentBuildingState)
        {
            case BuildingState.BuildingType:
                _tileBuildPanel.SpawnBuildingItemsInScrollView(_currentTileObject, _selectTilePanel, _currentTile); //возвращаемся заспавнить список зданий этого типа
                break;
            case BuildingState.FirstBuild:
                if (_currentTile.BuildingTileView == BuildingTileViewEnum.Base) _tilesSystem.IsHaveBase = true;
                _currentTileObject.BuildingTileObject().SpawnBuildingTile(_currentTile, _upgradeToLevel); //спавним впервые здание на тайле определенного лвла
                _selectTilePanel.ClosePanelAndRefreshInfo();
                break;
            case BuildingState.UpgradeBuilding:
                _currentTileObject.BuildingTileObject().UpgradeBuildingTile(_upgradeToLevel); //улучшаем здание
                _selectTilePanel.ClosePanelAndRefreshInfo();
                break;
        }
    }
}

public enum BuildingState
{
    BuildingType = 0,
    FirstBuild = 1,
    UpgradeBuilding = 2,
}
