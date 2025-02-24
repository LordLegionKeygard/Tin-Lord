using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LearnBuildingInfoPanel : MonoBehaviour
{
    [SerializeField] private RectTransform _objectTransform;
    [SerializeField] private Button _learnButton;
    private BaseProductionResourcePanel _productionResourcePanel;
    private LearnBuildingItem _currentLearnBuildingItem;
    private int _currentResourcesProduction = 0;

    [Header("Panels")]
    [SerializeField] private GameObject _productionResourcePanelObject;
    [SerializeField] private GameObject _productionResourcePanelLine;
    [SerializeField] private GameObject _buttonsPanelObject;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _buildingNameText;
    [SerializeField] private TextMeshProUGUI _buildingEcologyText;
    [SerializeField] private TextMeshProUGUI _buildingLevelText;
    [SerializeField] private TextMeshProUGUI _productionResourceText;

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
        var builing = _currentLearnBuildingItem.GetBuilding();
        _buildingNameText.text = builing.Name[Language.LanguageNumber];
        _buildingEcologyText.text = $"{Language.TextStatic[16]}{builing.BuildingEcology}";
        _buildingLevelText.text = $"{Language.TextStatic[3]}: {builing.BuildingLevel}";
        SetProductionPanel(builing);
        SetButtonPanel();

        PanelViewToggle(true);
    }

    private void SetButtonPanel()
    {
        _buttonsPanelObject.SetActive(!_currentLearnBuildingItem.IsLearn());

        _learnButton.interactable = _currentLearnBuildingItem.IsResourcesEnough();
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
        _buttonsPanelObject.SetActive(false);
    }
    private void Clear()
    {
        _currentLearnBuildingItem = null;
    }
}
