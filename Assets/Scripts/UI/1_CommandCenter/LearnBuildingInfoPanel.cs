using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LearnBuildingInfoPanel : MonoBehaviour
{
    [SerializeField] private RectTransform _objectTransform;
    private LearnBuildingItem _currentLearnBuildingItem;

    [Header("Main")]
    [SerializeField] private TextMeshProUGUI _buildingNameText;
    [SerializeField] private TextMeshProUGUI _buildingHealthText;
    [SerializeField] private TextMeshProUGUI _buildingEcologyText;
    [SerializeField] private TextMeshProUGUI _buildingLevelText;

    [Header("Production")]
    [SerializeField] private GameObject _productionResourcePanelObject;
    [SerializeField] private GameObject _productionResourcePanelLine;
    [SerializeField] private TextMeshProUGUI _productionResourceText;
    private BaseProductionResourcePanel _productionResourcePanel;
    private int _currentResourcesProduction = 0;

    [Header("Turret")]
    [SerializeField] private GameObject _turretPanelObject;
    [SerializeField] private GameObject _turretPanelLine;
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _attackSpeedText;
    [SerializeField] private TextMeshProUGUI _attackRadiusText;
    [SerializeField] private TextMeshProUGUI _rotationSpeedText;


    [Header("Buttons")]
    [SerializeField] private Button _learnButton;
    [SerializeField] private GameObject _buttonsPanelObject;

    private void Awake()
    {
        _productionResourcePanel = GetComponent<BaseProductionResourcePanel>();
    }

    public void PanelViewToggle(bool state)
    {
        if (state)
        {
            _objectTransform.DOAnchorPosY(0, 0.3f).SetUpdate(true);
        }
        else
        {
            _objectTransform.DOAnchorPosY(-599, 0.3f).SetUpdate(true);

            ResetPanels();
            Clear();
        }
    }

    public void SetNewBuildingItem(LearnBuildingItem learnBuildingItem)
    {
        _currentLearnBuildingItem = learnBuildingItem;
        _currentResourcesProduction = 0;
    }

    public void RefreshInfo()
    {
        var building = _currentLearnBuildingItem.GetBuilding();
        SetMainPanel(building);
        SetProductionPanel(building);
        SetTurretPanel(building);
        SetButtonPanel();
        PanelViewToggle(true);
    }

    private void SetMainPanel(Building building)
    {
        _buildingNameText.text = building.Name[Language.LanguageNumber];
        _buildingHealthText.text = $"{Language.TextStatic[97]}: {building.BuildingHealth}";
        _buildingEcologyText.text = $"{Language.TextStatic[16]}: {building.BuildingEcology}";
        _buildingLevelText.text = $"{Language.TextStatic[3]}: {building.BuildingLevel}";
    }

    private void SetProductionPanel(Building building)
    {
        if (building.ResourcesProduction.Length == 0)
        {
            _productionResourcePanelObject.SetActive(false);
            _productionResourcePanelLine.SetActive(false);
        }
        else
        {
            _productionResourcePanel.SetButtonView(building, building.ResourcesProduction[_currentResourcesProduction].ProductionResource);
            var productionName = $"{building.ResourcesProduction[_currentResourcesProduction].ProductionResource.Name[Language.LanguageNumber]}";
            string productionAmount;
            productionAmount = building.ResourceExtractedAmount.ToString();
            var productionText = $"{Language.TextStatic[6]}: {productionName} {productionAmount}";
            _productionResourceText.text = productionText;

            _productionResourcePanelObject.SetActive(true);
            _productionResourcePanelLine.SetActive(true);
        }
    }

    private void SetTurretPanel(Building building)
    {
        if (building.Damage == 0)
        {
            _turretPanelObject.SetActive(false);
            _turretPanelLine.SetActive(false);
        }
        else
        {
            _damageText.text = $"{Language.TextStatic[98]}: {building.Damage}";
            _attackSpeedText.text = $"{Language.TextStatic[99]}: {building.AttackSpeed}";
            _attackRadiusText.text = $"{Language.TextStatic[100]}: {building.AttackRadius}";
            _rotationSpeedText.text = $"{Language.TextStatic[101]}: {building.RotationSpeed}";

            _turretPanelObject.SetActive(true);
            _turretPanelLine.SetActive(true);
        }
    }

    private void SetButtonPanel()
    {
        _buttonsPanelObject.SetActive(!_currentLearnBuildingItem.IsLearn());

        _learnButton.interactable = _currentLearnBuildingItem.IsResourcesEnough();
    }

    public void ChangeResourceProduction(int resourceNumber)
    {
        _currentResourcesProduction = resourceNumber;
        RefreshInfo();
    }

    public void LearnButton()
    {
        _currentLearnBuildingItem.LearnBuilding();
        SetButtonPanel();
        CustomEvents.FireCloseTooltips();
    }

    public void ResetPanels()
    {
        _productionResourcePanelObject.SetActive(false);
        _productionResourcePanelLine.SetActive(false);
        _turretPanelObject.SetActive(false);
        _turretPanelLine.SetActive(false);
        _buttonsPanelObject.SetActive(false);
    }
    private void Clear()
    {
        _currentLearnBuildingItem = null;
    }
}
