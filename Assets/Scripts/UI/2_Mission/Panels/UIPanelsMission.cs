using UnityEngine;
using Zenject;

public class UIPanelsMission : MonoBehaviour
{
    [Inject] private readonly EscapePanelMission _escapePanel;
    [Inject] private readonly TutorialSystem _tutorialSystem;
    [SerializeField] private CardHolderSystem _cardHolderSystem;
    [SerializeField] private TileDetector _tileDetector;
    [SerializeField] private SkillTargetSystem _skillTargetSystem;

    [Header("Panels")]
    [SerializeField] private GameObject[] _selectTilePanels;
    [SerializeField] private GameObject[] _mainPanels;
    [SerializeField] private GameObject _settingsPanelObject;
    [SerializeField] private GameObject _tutorialCanvas;

    [Header("Panel Logic")]
    [SerializeField] private SelectTilePanel _selectTilePanel;
    [SerializeField] private MachinePanel _machinePanel;
    [SerializeField] private SettingsPanels _settingsPanel;
    [SerializeField] private MissionResourcePanel _missionResourcePanel;
    [SerializeField] private MissionHolderPanel _missionHolderPanel;

    public void MainPanelsViewToggle(bool selectTilePanel, bool machinePanel)
    {
        _selectTilePanel.PanelViewToggle(selectTilePanel);
        _machinePanel.PanelViewToggle(machinePanel);
    }

    public bool ActiveInHierarchy(UIPanelsEnum panelEnum) => _selectTilePanels[(int)panelEnum].activeInHierarchy;

    public void EscapeClick()
    {
        _skillTargetSystem.CancelSkillCircle();
        if (_selectTilePanels[0].activeInHierarchy)
        {
            _selectTilePanels[0].SetActive(false);
        }
        else if (_selectTilePanels[1].activeInHierarchy)
        {
            SetBuildTypesPanelAndLineVisibility(false);
        }
        else if (_selectTilePanels[11].activeInHierarchy)
        {
            _selectTilePanel.DestroyPanelToggleAndRefreshButtonColor(false);
        }
        else if (_cardHolderSystem.IsHaveCurrentSelectedCardObject() || _tileDetector.IsHaveCurrentSelectedTileObject())
        {
            ClearAndCancelCardHolderAndTileDetector();
        }
        else if (_settingsPanelObject.activeInHierarchy)
        {
            _settingsPanel.CloseButton();
        }
        else
        {
            EscapeButton();
        }
    }
    
    public void EscapeButton()
    {
        _escapePanel.PanelViewToggle(true);
        _tutorialCanvas.SetActive(!_escapePanel.IsEscapeMode());
    }

    public void PreparePanelsToShipMode()
    {
        _skillTargetSystem.CancelSkillCircle();
        _selectTilePanel.PanelViewToggle(false);
        _missionResourcePanel.PanelClose();
        _missionHolderPanel.PanelClose();
        ClearAndCancelCardHolderAndTileDetector();
    }

    public void ClearAndCancelCardHolderAndTileDetector()
    {
        CustomEvents.FireTooltipToggle(false, 0);
        if (_tutorialSystem.CanClearTileDetector())
        {
            _tileDetector.ClearTileDetector();
        }
        if (_tutorialSystem.CanCancelSeletCard())
        {
            _cardHolderSystem.CancelSelectCard();
        }
    }

    public void TogglePanel(UIPanelsEnum panelEnum, bool state)
    {
        _selectTilePanels[(int)panelEnum].SetActive(state);
    }

    public void SetRequiredResourcePanelVisibility(bool haveBuildingTile, Building building)
    {
        var state = haveBuildingTile && building.ResourcesForWork.Length != 0;
        _selectTilePanels[3].SetActive(state);
        _selectTilePanels[4].SetActive(state);
    }

    public void SetProductionResourcePanelVisibility(bool haveBuildingTile, Building building)
    {
        var state = haveBuildingTile && building.ResourcesProduction.Length != 0;
        _selectTilePanels[5].SetActive(state);
        _selectTilePanels[6].SetActive(state);
    }

    public void SetReceptPanelVisibility(bool haveBuildingTile, ResourceRecept[] resourceRecept)
    {
        var state = haveBuildingTile && resourceRecept != null && resourceRecept.Length != 0;
        _selectTilePanels[7].SetActive(state);
        _selectTilePanels[8].SetActive(state);
    }

    public void SetBuildTypesPanelAndLineVisibility(bool state)
    {
        _selectTilePanels[1].SetActive(state);
        _selectTilePanels[2].SetActive(state);
    }

    public void SetButtonsPanelVisibility(bool state)
    {
        _selectTilePanels[10].SetActive(state);
        _selectTilePanels[9].SetActive(state);
    }

    public void CloseAllSelectTilePanels()
    {
        _selectTilePanels[0].SetActive(false);
        _selectTilePanels[1].SetActive(false);
        _selectTilePanels[2].SetActive(false);
        _selectTilePanels[3].SetActive(false);
        _selectTilePanels[4].SetActive(false);
        _selectTilePanels[5].SetActive(false);
        _selectTilePanels[6].SetActive(false);
        _selectTilePanels[7].SetActive(false);
        _selectTilePanels[8].SetActive(false);
        _selectTilePanels[12].SetActive(false);
        _selectTilePanels[13].SetActive(false);
    }

    public void UnactiveAllPanelsAfterEndMission()
    {
        foreach (var item in _mainPanels)
        {
            item.SetActive(false);
        }
    }

    public void CloseAllBuildsPanels()
    {
        SetBuildTypesPanelAndLineVisibility(false);
        TogglePanel(UIPanelsEnum.BuildsPanel, false);
    }

    public void CloseAllBuildsAndDestroyPanel()
    {
        CloseAllBuildsPanels();
        TogglePanel(UIPanelsEnum.DestroyPanel, false);
    }

    public void InputDestroyButton()
    {
        if (_escapePanel.IsEscapeMode()) return;

        if (_selectTilePanel.PanelActive()) _selectTilePanel.DestroyButton();
        if (_machinePanel.PanelActive()) _machinePanel.DestroyMachineButton();
    }
}

public enum UIPanelsEnum
{

    BuildsPanel = 0,
    BuildTypesPanel = 1,
    BuildTypesPanelLine = 2,
    RequiredResourcePanel = 3,
    RequiredResourcePanelLine = 4,
    ProductionResourcePanel = 5,
    ProductionResourcePanelLine = 6,
    ReceptPanel = 7,
    ReceptPanelLine = 8,
    ButtonsPanel = 9,
    BuildingLine = 10,
    DestroyPanel = 11,
    TurretPanel = 12,
    TurretPanelLine = 13,
}
