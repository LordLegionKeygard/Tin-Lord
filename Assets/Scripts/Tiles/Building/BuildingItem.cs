using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;
using UnityEngine.UI;

public class BuildingItem : MonoBehaviour
{
    [Inject] private PlayerResources _playerResources;
    [SerializeField] private Tile _currentTile;
    [SerializeField] private TileObject _currentTileObject;

    [Header("View")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;
    [SerializeField] private Image _backImage;

    [Header("Other")]
    private SelectTilePanel _selectTilePanel;
    private BuildingState _currentBuildingState;
    private BuildingResourcesView _buildingResourcesView;
    private BuildsPanel _buildsPanel;
    private int _upgradeToLevel;
    private bool _isSelect;

    private void Start()
    {
        CustomEvents.OnTimeTickAfterResourcesChanged += SetTextColor;
    }

    public void SetBuildingInfo(TileObject tileObject, SelectTilePanel selectTilePanel, int level, Tile tile, BuildingState buildingState, BuildingResourcesView buildingResourcesView, BuildsPanel buildsPanel)
    {
        _currentBuildingState = buildingState;
        _selectTilePanel = selectTilePanel;
        _currentTileObject = tileObject;
        _currentTile = tile;
        _upgradeToLevel = level;
        _buildingResourcesView = buildingResourcesView;
        _buildsPanel = buildsPanel;

        UpdateView();
    }

    private void UpdateView()
    {
        var building = _currentTile.Buildings[_upgradeToLevel - 1];

        _nameText.text = building.Name[Language.LanguageNumber];
        _icon.sprite = building.BuildingSprite;
    }

    private void SetTextColor()
    {
        var resourcesEnough = _playerResources.ResourcesForBuildEnough(_currentTile.Buildings[_upgradeToLevel - 1].ResourcesForBuild);
        _button.enabled = resourcesEnough;
        _nameText.color = !_isSelect ? Colors.LightGrey : resourcesEnough ? Color.white : Colors.WarningYellow;
        _icon.color = _isSelect ? Color.white : Colors.LightGrey;
        _backImage.color = _isSelect ? Color.white : Colors.LightGrey;
        if (_isSelect) _buildingResourcesView.SetBuildingResourcesView(_currentTile.Buildings[_upgradeToLevel - 1]);
    }

    public void SelectToggleState(bool state)
    {
        _isSelect = state;
        SetTextColor();
    }

    public void SetView()
    {
        _buildsPanel.UnselectAllBuildings();
        SelectToggleState(true);
        _buildingResourcesView.SetBuildingResourcesView(_currentTile.Buildings[_upgradeToLevel - 1]);
    }

    public void BuildOrUpgrade()
    {
        _buildingResourcesView.ResetCells();
        _playerResources.UseResourcesFromBuilding(_currentTile.Buildings[_upgradeToLevel - 1].ResourcesForBuild);
        switch (_currentBuildingState)
        {
            case BuildingState.FirstBuild:
                if (_currentTile.BuildingTileView == BuildingTileViewEnum.Base) CustomEvents.FireSetBase();

                _currentTileObject.BuildingTileObject().SpawnBuildingTile(_currentTile, _upgradeToLevel, _currentTileObject); //спавним впервые здание на тайле определенного лвла
                _selectTilePanel.CloseBuildPanelAndRefreshInfo();
                break;
            case BuildingState.UpgradeBuilding:
                _playerResources.AddResourcesFromDestroyBuilding(_currentTileObject.BuildingTileObject().CurrentBuilding().ResourcesForBuild); // возвращаем часть ресурсов за прошлое здание
                _currentTileObject.BuildingTileObject().UpgradeBuildingTile(_upgradeToLevel, _currentTileObject); //улучшаем здание
                _selectTilePanel.CloseBuildPanelAndRefreshInfo();
                break;
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnTimeTickAfterResourcesChanged -= SetTextColor;
    }
}

public enum BuildingState
{
    FirstBuild = 0,
    UpgradeBuilding = 1,
}
