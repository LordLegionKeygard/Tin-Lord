using UnityEngine;
using TMPro;
using Zenject;
using UnityEngine.UI;
using System.Linq;

public class BuildingItem : MonoBehaviour
{
    [Inject] private readonly TutorialSystem _tutorialSystem;
    [Inject] private readonly TilesSystem _tilesSystem;
    [Inject] private readonly MissionHangarSystem _missionHangarSystem;
    [Inject] private readonly MissionResources _missionResources;
    [SerializeField] private Tile _currentTile;
    private TileObject _currentTileObject;

    [Header("View")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _backImage;
    [SerializeField] private GameObject _tutorialView;

    [Header("Other")]
    private SelectTilePanel _selectTilePanel;
    private BuildingState _currentBuildingState;
    private ResourcesViewMission _buildingResourcesViewMission;
    private BuildsPanel _buildsPanel;
    private int _buildingIndex;
    private bool _isSelect;
    public bool IsSelect() => _isSelect;
    private bool _resourcesEnough;
    private bool _haveRequiredLevel;

    private void Start()
    {
        CustomEvents.OnTimeTick += RefreshView;
        CustomEvents.OnStartTutorialStep += SelectTutorialBuildingItem;
        RefreshView();

        if (_tutorialSystem.GetTutorialStepEnum() == TutorialStepEnum.CompleteMissionTutorial) return;
        SelectTutorialBuildingItem(TutorialStepEnum.None);
    }

    private void SelectTutorialBuildingItem(TutorialStepEnum _)
    {
        var building = _currentTile.Buildings[_buildingIndex - 1];
        switch (_tutorialSystem.GetTutorialStepEnum())
        {
            case TutorialStepEnum.MissionSelectSettlementBuildingItem_18 or TutorialStepEnum.MissionStartConstructSettlement_20:
                if (_tutorialSystem.GetTutorialBuilding(0).Id == building.Id)
                {
                    _tutorialView.SetActive(true);
                }
                break;
            case TutorialStepEnum.MissionStartConstructionManualWoodMining_37:
                if (_tutorialSystem.GetTutorialBuilding(1).Id == building.Id)
                {
                    _tutorialView.SetActive(true);
                }
                break;
        }
    }

    public void SetBuildingInfo(TileObject tileObject, SelectTilePanel selectTilePanel, int index, Tile tile, BuildingState buildingState, ResourcesViewMission buildingResourcesViewMission, BuildsPanel buildsPanel)
    {
        _currentBuildingState = buildingState;
        _selectTilePanel = selectTilePanel;
        _currentTileObject = tileObject;
        _currentTile = tile;
        _buildingIndex = index;
        _buildingResourcesViewMission = buildingResourcesViewMission;
        _buildsPanel = buildsPanel;

        UpdateView();
    }

    private void UpdateView()
    {
        var requiredLevel = _currentTile.Buildings[_buildingIndex - 1].RequiredBaseLevel;
        _haveRequiredLevel = requiredLevel <= _tilesSystem.GetBaseLevel();
        var building = _currentTile.Buildings[_buildingIndex - 1];
        _nameText.text = _currentBuildingState == BuildingState.Repair ? _missionHangarSystem.GetRepairText() : _haveRequiredLevel ? building.Name[Language.LanguageNumber] : $"{string.Format(Language.TextStatic[297], requiredLevel)}"; ;
        _icon.sprite = building.BuildingSprite;
    }

    private void RefreshView()
    {
        _haveRequiredLevel = _currentTile.Buildings[_buildingIndex - 1].RequiredBaseLevel <= _tilesSystem.GetBaseLevel();
        _resourcesEnough = _missionResources.ResourcesEnough(GetResources());
        _nameText.color = _resourcesEnough && _haveRequiredLevel ? _isSelect ? Color.white : Colors.GreyEight : _isSelect ? Colors.WarningYellow : Colors.FadedYellow;
        _icon.color = _isSelect ? Color.white : Colors.GreyEight;
        _backImage.color = _isSelect ? Color.white : Colors.GreyEight;
        if (_isSelect) _buildingResourcesViewMission.SetResourcesView(GetResources());
    }

    public void SelectToggleState(bool state)
    {
        _isSelect = state;
        RefreshView();
    }

    public void SelectView()
    {
        _tutorialSystem.SelectBuildingItem(_currentTile.Buildings[_buildingIndex - 1]);
        _buildsPanel.UnselectAllBuildings();
        SelectToggleState(true);
        _buildingResourcesViewMission.SetResourcesView(GetResources());

        if (_tutorialSystem.GetTutorialStepEnum() == TutorialStepEnum.MissionOpenResourcePanel_19)
        {
            _tutorialView.SetActive(false);
        }
        CustomEvents.FireCompleteTutorialStep(TutorialStepEnum.MissionSelectSettlementBuildingItem_18);
    }

    public void BuildOrUpgrade()
    {
        if (!_tutorialSystem.CanBuildOrUpgrade()) return;

        if (!_resourcesEnough || !_haveRequiredLevel)
        {
            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Error], transform.position);
            return;
        }

        if (_tutorialSystem.GetTutorialStepEnum() == TutorialStepEnum.MissionStartConstructSettlement_20) _tutorialView.SetActive(false);
        _tutorialSystem.StartConstructBuilding(_currentTile.Buildings[_buildingIndex - 1]);
        _buildingResourcesViewMission.ResetCells();
        _missionResources.UseResourcesForBuilding(GetResources());

        var buildingTile = _currentTileObject.BuildingTileObject();

        switch (_currentBuildingState)
        {
            case BuildingState.FirstBuild:
                AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Card], transform.position);
                buildingTile.BeginConstruction(_currentTile, _buildingIndex, false);
                break;
            case BuildingState.UpgradeBuilding:
                AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Card], transform.position);
                if (_currentTileObject.GroundTileObject().CheckTileView(GroundTileViewEnum.BaseFoundation))
                {
                    buildingTile.UpgradeBaseBuilding(_buildingIndex, _currentTileObject);
                }
                else
                {
                    buildingTile.AddResourcesAfterDestroyBuilding();
                    buildingTile.DestroyBuildingTile(true); // получается при апгрейде здания, мы сначала уничтожаем прошлое здание и получаем такое кол-во ресурсов, какой процент хп у него остался
                    buildingTile.BeginConstruction(_currentTile, _buildingIndex, false);
                }
                break;
            case BuildingState.Repair:
                AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Repair], transform.position);
                _currentTileObject.BuildingHealth().FullRepair();
                break;
        }

        _selectTilePanel.CloseBuildPanelAndRefreshInfo();
    }

    public ResourceWrapper[] GetResources()
    {
        var building = _currentTile.Buildings[_buildingIndex - 1];

        if (_currentBuildingState == BuildingState.Repair)
        {
            var buildingHealth = _currentTileObject.BuildingHealth();
            float healthPercentage = (float)(buildingHealth.GetMaxHealth() - buildingHealth.GetCurrentHealth()) / buildingHealth.GetMaxHealth();

            return building.ResourcesForBuild.Select(resource => new ResourceWrapper
            {
                ResourceEnum = resource.ResourceEnum,
                RecourceAmount = Mathf.CeilToInt(resource.RecourceAmount * healthPercentage * _missionHangarSystem.GetPatchRepairBonus())
            }).ToArray();
        }

        return building.ResourcesForBuild;
    }


    private void OnDestroy()
    {
        CustomEvents.OnTimeTick -= RefreshView;
    }
}

public enum BuildingState
{
    FirstBuild = 0,
    UpgradeBuilding = 1,
    Repair = 2,
}
