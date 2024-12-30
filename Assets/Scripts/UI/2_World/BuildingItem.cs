using UnityEngine;
using TMPro;
using Zenject;
using UnityEngine.UI;
using System.Linq;

public class BuildingItem : MonoBehaviour
{
    [Inject] private PlayerResources _playerResources;
    [SerializeField] private Tile _currentTile;
    public int GetBuildingLevel() => _currentTile.Buildings[_buildingIndex - 1].BuildingLevel;
    private TileObject _currentTileObject;

    [Header("View")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;
    [SerializeField] private Image _backImage;

    [Header("Other")]
    private SelectTilePanel _selectTilePanel;
    private BuildingState _currentBuildingState;
    private ResourcesView _buildingResourcesView;
    private BuildsPanel _buildsPanel;
    private int _buildingIndex;
    private bool _isSelect;
    public bool IsSelect() => _isSelect;
    private bool _resourcesEnough;

    private void Start()
    {
        CustomEvents.OnTimeTickAfterResourcesChanged += RefreshView;
    }

    public void SetBuildingInfo(TileObject tileObject, SelectTilePanel selectTilePanel, int index, Tile tile, BuildingState buildingState, ResourcesView buildingResourcesView, BuildsPanel buildsPanel)
    {
        _currentBuildingState = buildingState;
        _selectTilePanel = selectTilePanel;
        _currentTileObject = tileObject;
        _currentTile = tile;
        _buildingIndex = index;
        _buildingResourcesView = buildingResourcesView;
        _buildsPanel = buildsPanel;

        UpdateView();
    }

    private void UpdateView()
    {
        var building = _currentTile.Buildings[_buildingIndex - 1];

        _nameText.text = _currentBuildingState == BuildingState.Repair ? Language.TextStatic[4] : building.Name[Language.LanguageNumber];
        _icon.sprite = building.BuildingSprite;
    }

    private void RefreshView()
    {
        _resourcesEnough = _playerResources.ResourcesEnough(GetResources());
        _button.enabled = _resourcesEnough;
        _nameText.color = _resourcesEnough ? _isSelect ? Color.white : Colors.GreyEight : _isSelect ? Colors.WarningYellow : Colors.FadedYellow;
        _icon.color = _isSelect ? Color.white : Colors.GreyEight;
        _backImage.color = _isSelect ? Color.white : Colors.GreyEight;
        if (_isSelect) _buildingResourcesView.SetResourcesView(GetResources());
    }

    public void SelectToggleState(bool state)
    {
        _isSelect = state;
        RefreshView();
    }

    public void SelectView()
    {
        _buildsPanel.UnselectAllBuildings();
        SelectToggleState(true);
        _buildingResourcesView.SetResourcesView(GetResources());
    }

    public void BuildOrUpgrade()
    {
        _buildingResourcesView.ResetCells();
        _playerResources.UseResourcesForBuilding(GetResources());

        switch (_currentBuildingState)
        {
            case BuildingState.FirstBuild:
                _currentTileObject.BuildingTileObject().ConstructingBuilding(_currentTile, _buildingIndex, _currentTileObject);
                break;
            case BuildingState.UpgradeBuilding:
                if (_currentTileObject.GroundTileObject().CheckTileView(GroundTileViewEnum.BaseFoundation))
                {
                    _currentTileObject.BuildingTileObject().UpgradeBaseBuilding(_buildingIndex, _currentTileObject);
                }
                else
                {
                    _currentTileObject.BuildingTileObject().DestroyBuildingTile(true);
                    _currentTileObject.BuildingTileObject().ConstructingBuilding(_currentTile, _buildingIndex, _currentTileObject);
                }
                break;
            case BuildingState.Repair:
                _currentTileObject.BuildingHealth().FullRepair();
                break;
        }

        _selectTilePanel.CloseBuildPanelAndRefreshInfo();
    }

    public ResourcesForBuildWrapper[] GetResources()
    {
        var building = _currentTile.Buildings[_buildingIndex - 1];

        if (_currentBuildingState == BuildingState.Repair)
        {
            // Получаем здоровье здания
            var buildingHealth = _currentTileObject.BuildingHealth();
            float healthPercentage = (float)(buildingHealth.MaxHealth - buildingHealth.CurrentHealth) / buildingHealth.MaxHealth;

            // Пропорционально рассчитываем ресурсы для ремонта
            return building.ResourcesForBuild
                .Select(resource => new ResourcesForBuildWrapper
                {
                    ResourcesForBuild = resource.ResourcesForBuild,
                    RecourcesForBuildAmount = Mathf.CeilToInt(resource.RecourcesForBuildAmount * healthPercentage)
                })
                .ToArray();
        }

        // Возвращаем ресурсы для строительства
        return building.ResourcesForBuild;
    }


    private void OnDestroy()
    {
        CustomEvents.OnTimeTickAfterResourcesChanged -= RefreshView;
    }
}

public enum BuildingState
{
    FirstBuild = 0,
    UpgradeBuilding = 1,
    Repair = 2,
}
