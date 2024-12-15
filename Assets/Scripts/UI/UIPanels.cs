using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanels : MonoBehaviour
{
    [SerializeField] private CardHolderSystem _cardHolderSystem;
    [SerializeField] private TileDetector _tileDetector;

    [Header("SelectTilePanels & Lines")]
    [SerializeField] private GameObject[] _panels;
    [Header("Panel Logic")]
    [SerializeField] private SelectTilePanel _selectTilePanel;
    [SerializeField] private RobotPanel _robotPanel;

    public void MainPanelsViewToggle(bool selectTilePanel, bool robotPanel)
    {
        _selectTilePanel.PanelViewToggle(selectTilePanel);
        _robotPanel.PanelViewToggle(robotPanel);
    }

    public bool ActiveInHierarchy(UIPanelsEnum panelEnum) => _panels[(int)panelEnum].activeInHierarchy;

    public void EscapeClick()
    {
        if (_panels[0].activeInHierarchy)
        {
            _panels[0].SetActive(false);
        }
        else if (_panels[1].activeInHierarchy)
        {
            SetBuildTypesPanelAndLineVisibility(false);
        }
        else if (_cardHolderSystem.IsHaveCurrentSelectedCardObject() || _tileDetector.IsHaveCurrentSelectedTileObject())
        {
            ClearAndCancelCardHolderAndTileDetector();
        }
        else
        {
            //Включение EscapePanel
        }
    }

    public void ClearAndCancelCardHolderAndTileDetector()
    {
        _tileDetector.ClearTileDetector();
        _cardHolderSystem.CancelSelectCard();
    }

    public void TogglePanel(UIPanelsEnum panelEnum, bool state)
    {
        _panels[(int)panelEnum].SetActive(state);
    }

    public void SetRequiredResourcePanelVisibility(bool haveBuildingTile, Building building)
    {
        var state = haveBuildingTile && building.ResourcesForWork.Length != 0;
        _panels[3].SetActive(state);
        _panels[4].SetActive(state);
    }

    public void SetProductionResourcePanelVisibility(bool haveBuildingTile, Building building)
    {
        var state = haveBuildingTile && building.ResourcesProduction.Length != 0;
        _panels[5].SetActive(state);
        _panels[6].SetActive(state);
    }

    public void SetReceptPanelVisibility(bool haveBuildingTile, ResourceRecept[] resourceRecept)
    {
        var state = haveBuildingTile && resourceRecept != null && resourceRecept.Length != 0;
        _panels[7].SetActive(state);
        _panels[8].SetActive(state);
    }

    public void SetBuildTypesPanelAndLineVisibility(bool state)
    {
        _panels[1].SetActive(state);
        _panels[2].SetActive(state);
    }

    public void SetButtonsPanelVisibility(bool state)
    {
        _panels[10].SetActive(state);
        _panels[9].SetActive(state);
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
}
