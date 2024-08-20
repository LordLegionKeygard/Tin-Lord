using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class SelectTilePanel : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private BuildTypesPanel _buildTypesPanel;
    [SerializeField] private BuildsPanel _buildsPanel;

    [Header("Objects")]
    [SerializeField] private GameObject _buildButton;
    [SerializeField] private GameObject _onOffButton;
    [SerializeField] private GameObject _destroyButton;
    [SerializeField] private RectTransform _objectTransform;
    [SerializeField] private Image _onOffImage;

    [Header("Panels & Lines")]
    [SerializeField] private GameObject _buildTypesPanelObject;
    [SerializeField] private GameObject _buildTypesPanelLine;
    [SerializeField] private GameObject _requiredResourcePanelObject;
    [SerializeField] private GameObject _requiredResourcePanelLine;
    [SerializeField] private GameObject _productionResourcePanelObject;
    [SerializeField] private GameObject _productionResourcePanelLine;
    [SerializeField] private GameObject _receptPanelObject;
    [SerializeField] private GameObject _receptPanelLine;
    [SerializeField] private GameObject _buttonsPanel;
    [SerializeField] private GameObject _buildingLine;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _groundTileNameText;
    [SerializeField] private TextMeshProUGUI _buildingNameText;
    [SerializeField] private TextMeshProUGUI _buildingLevelText;
    [SerializeField] private TextMeshProUGUI _productionModifierText;
    [SerializeField] private TextMeshProUGUI _productionResourceText;
    [SerializeField] private TextMeshProUGUI _requiredResources;
    [SerializeField] private TextMeshProUGUI _groundEcologyText;
    [SerializeField] private TextMeshProUGUI _buildingEcologyText;

    [Header("Other")]
    private TileObject _tileObject;
    private RequiredResourcePanel _requiredResourcePanel;
    private ProductionResourcePanel _productionResourcePanel;
    private ReceptPanel _receptPanel;

    private void Awake()
    {
        _requiredResourcePanel = GetComponent<RequiredResourcePanel>();
        _productionResourcePanel = GetComponent<ProductionResourcePanel>();
        _receptPanel = GetComponent<ReceptPanel>();
    }

    public void RefreshShowInfo(int tileId)
    {
        if (_tileObject == null || _tileObject.GetId() != tileId) return;
        // Debug.Log("RefreshShowInfo - CheckCount");
        SetInfo(_tileObject);
    }

    public void PanelViewToggle(bool state)
    {
        if (state)
        {
            ShowInfoPanel();
        }
        else
        {
            HideInfoPanel();
        }
    }

    private void ShowInfoPanel()
    {
        _buildTypesPanelObject.SetActive(false);
        _objectTransform.DOAnchorPosY(0, 0.3f).SetUpdate(true);
    }

    private void HideInfoPanel()
    {
        _objectTransform.DOAnchorPosY(-700, 0.3f).SetUpdate(true);
        _buildsPanel.gameObject.SetActive(false);
        _buildTypesPanelLine.SetActive(false);
    }

    public void SetInfo(TileObject tileObject)
    {
        _tileObject = tileObject;

        var buildingTile = tileObject.BuildingTileObject().CurrentBuildingTile();
        var haveBuildingTile = tileObject.BuildingTileObject().HaveTile();
        var currentBuilding = haveBuildingTile ? tileObject.BuildingTileObject().CurrentBuilding() : null;

        SetTextFields(tileObject, buildingTile, haveBuildingTile, currentBuilding);
        SetProductionText(tileObject, buildingTile, haveBuildingTile, currentBuilding);
        SetRequiredText(tileObject, buildingTile, haveBuildingTile);
        SetEcologyTexts(tileObject);
        SetRequiredResourcePanelVisibility(haveBuildingTile, currentBuilding);
        SetProductionResourcePanelVisibility(haveBuildingTile, currentBuilding);
        SetReceptPanelVisibility(haveBuildingTile, tileObject.CurrentResourceRecept());
        SetButtonsVisibility(tileObject.GroundTileObject().CurrentGroundTile().GroundTileView);
        SetBuildTypesPanelVisibility(false);
        SetOnOffButtonColor();

        if (haveBuildingTile && buildingTile.IsHaveProdictionResources())
        {
            _productionResourcePanel.SetButtonView(tileObject.BuildingTileObject().CurrentBuilding(), tileObject.CurrentResourceProduction());
            if (tileObject.CurrentResourceRequired() != null)
            {
                _requiredResourcePanel.UpdateButtonsView(_tileObject.CurrentResourceRequired().ResourceEnum, _tileObject.BuildingTileObject().CurrentBuilding().ResourcesForWork);
            }
            _receptPanel.UpdateReceptView(tileObject.CurrentResourceRecept());
        }

        SetButtonStates(tileObject, haveBuildingTile);
    }

    private void SetButtonsVisibility(GroundTileViewEnum groundTileViewEnum)
    {
        var state = groundTileViewEnum != GroundTileViewEnum.Road;
        _buildingLine.SetActive(state);
        _buttonsPanel.SetActive(state);
    }

    private void SetOnOffButtonColor()
    {
        _onOffImage.color = _tileObject.IsBuildingWork ? Colors.TextGrey : Color.black;
    }

    private void SetTextFields(TileObject tileObject, Tile buildingTile, bool haveBuildingTile, Building building)
    {
        _groundTileNameText.text = tileObject.GroundTileObject().CurrentGroundTile().Name[Language.LanguageNumber];
        _buildingNameText.text = haveBuildingTile ? $"{Language.TextStatic[2]}: {building.Name[Language.LanguageNumber]}" : $"{Language.TextStatic[2]}: -";
        _buildingLevelText.text = haveBuildingTile ? $"{Language.TextStatic[3]}: {tileObject.BuildingTileObject().CurrentBuildingLevel()}" : $"{Language.TextStatic[3]}: -";

        _productionModifierText.text = haveBuildingTile && buildingTile.IsHaveProdictionResources()
     ? $"{Language.TextStatic[11]}: <color={(tileObject.CurrentModifier() == 0 ? Colors.HexColorYellow : Colors.HexColorWhite)}>x{tileObject.CurrentModifier()}</color>"
     : $"{Language.TextStatic[11]}: -";
    }

    private void SetProductionText(TileObject tileObject, Tile buildingTile, bool haveBuildingTile, Building buildings)
    {
        if (haveBuildingTile && buildingTile.IsHaveProdictionResources())
        {
            var isUseRources = _tileObject.BuildingTileObject().CurrentBuilding().ResourcesForWork.Length != 0;
            var productionName = $"{tileObject.CurrentResourceProduction().Name[Language.LanguageNumber]} ";
            string productionAmount;

            if (isUseRources)
            {
                productionAmount = tileObject.IsHaveRequiredResource() && tileObject.IsBuildingWork
                    ? (buildings.ResourceExtractedAmount * tileObject.CurrentModifier()).ToString()
                    : "0";
            }
            else
            {
                productionAmount = tileObject.IsBuildingWork
                    ? (buildings.ResourceExtractedAmount * tileObject.CurrentModifier()).ToString()
                    : "0";
            }

            // Определяем цвет текста в зависимости от значения производства ресурсов
            var productionColor = (productionAmount == "0") ? Colors.HexColorYellow : Colors.HexColorWhite;

            // Формируем итоговую строку с цветовым форматированием
            var productionText = $"{Language.TextStatic[6]}: <color={productionColor}>{productionName}{productionAmount}</color>";

            _productionResourceText.text = productionText;
        }
        else
        {
            _productionResourceText.text = $"{Language.TextStatic[6]}: -";
        }

    }

    private void SetRequiredText(TileObject tileObject, Tile buildingTile, bool haveBuildingTile)
    {
        string textColor;

        if (haveBuildingTile && buildingTile.IsHaveProdictionResources() && tileObject.CurrentResourceRequired() != null && tileObject.IsHaveRequiredResource())
        {
            textColor = Colors.HexColorWhite;
        }
        else
        {
            textColor = Colors.HexColorYellow;
        }

        _requiredResources.text = $"{Language.TextStatic[14]}: <color={textColor}>{(haveBuildingTile && buildingTile.IsHaveProdictionResources() && tileObject.CurrentResourceRequired() != null ? $"{tileObject.CurrentResourceRequired().Name[Language.LanguageNumber]} {tileObject.CurrentResourceRequiredAmount()}" : "-")}</color>";
    }

    private void SetEcologyTexts(TileObject tileObject)
    {
        _groundEcologyText.text = $"{Language.TextStatic[15]}{tileObject.TileEcology().GetEcology(GetEcologyEnum.Ground)}";
        _buildingEcologyText.text = $"{Language.TextStatic[16]}{tileObject.TileEcology().GetEcology(GetEcologyEnum.Building)}";
    }

    private void SetRequiredResourcePanelVisibility(bool haveBuildingTile, Building building)
    {
        var state = haveBuildingTile && building.ResourcesForWork.Length != 0;
        _requiredResourcePanelObject.SetActive(state);
        _requiredResourcePanelLine.SetActive(state);
    }

    private void SetProductionResourcePanelVisibility(bool haveBuildingTile, Building building)
    {
        var state = haveBuildingTile && building.ResourcesProduction.Length != 0;
        _productionResourcePanelObject.SetActive(state);
        _productionResourcePanelLine.SetActive(state);
    }

    private void SetReceptPanelVisibility(bool haveBuildingTile, ResourceRecept[] resourceRecept)
    {
        var state = haveBuildingTile && resourceRecept != null && resourceRecept.Length != 0;
        _receptPanelObject.SetActive(state);
        _receptPanelLine.SetActive(state);
    }

    private void SetBuildTypesPanelVisibility(bool state)
    {
        _buildTypesPanelObject.SetActive(state);
        _buildTypesPanelLine.SetActive(state);
    }

    private void SetButtonStates(TileObject tileObject, bool haveBuildingTile)
    {
        var isRoad = tileObject.GroundTileObject().CurrentGroundTile().GroundTileView == GroundTileViewEnum.Road;
        var isBase = tileObject.GroundTileObject().CurrentGroundTile().GroundTileView == GroundTileViewEnum.BaseFoundation;

        _onOffButton.SetActive(haveBuildingTile && tileObject.BuildingTileObject().CurrentBuildingTile().IsHaveProdictionResources());
        _buildButton.SetActive((!haveBuildingTile || tileObject.BuildingTileObject().IsCanUpgrade()) && !isRoad);
        _destroyButton.SetActive(!isRoad && !isBase);
    }

    public void BuildButton()
    {
        if (!_tileObject.BuildingTileObject().HaveTile() && !_buildTypesPanelObject.activeInHierarchy)
        {
            SetBuildTypesPanelVisibility(true);
            _buildTypesPanel.SpawnBuildingTypesInScrollView(_tileObject, this);
        }
        else if (_tileObject.BuildingTileObject().HaveTile() && !_buildsPanel.gameObject.activeInHierarchy)
        {
            _buildsPanel.gameObject.SetActive(true);
            _buildsPanel.SpawnUpgradeItemsInScrollView(_tileObject, this);
        }
    }

    public void DestroyButton()
    {
        if (!_tileObject.BuildingTileObject().HaveTile())
        {
            _tileObject.GroundTileObject().DestroyGroundTile();
            PanelViewToggle(false);
        }
        else
        {
            _tileObject.BuildingTileObject().DestroyBuildingTile();
            CloseBuildPanelAndRefreshInfo();
            SetInfo(_tileObject);
        }
    }

    public void CloseBuildPanelAndRefreshInfo()
    {
        _buildsPanel.gameObject.SetActive(false);
        SetBuildTypesPanelVisibility(false);
        PanelViewToggle(true);
        SetInfo(_tileObject);
    }

    public void ToggleBuildingWork()
    {
        _tileObject.IsBuildingWork = !_tileObject.IsBuildingWork;
        SetOnOffButtonColor();

        CustomEvents.FireChangeEcology(_tileObject.TileEcology().GetEcology(GetEcologyEnum.Total), _tileObject.GetId(), false);
        if (!_tileObject.IsHaveRequiredResource()) return;

        _tileObject.ChangeResourceProduction();
        CustomEvents.FireChangeResourceRequired(_tileObject, _tileObject.CurrentResourceRequired(), _tileObject.IsBuildingWork ? _tileObject.CurrentResourceRequiredAmount() : 0, _tileObject.CurrentResourceRecept());
    }

    public void ChangeResourceRequired(Resource resource) // для работы
    {
        var resourcesForWork = _tileObject.BuildingTileObject().CurrentBuilding().ResourcesForWork;

        for (int i = 0; i < resourcesForWork.Length; i++)
        {
            if (resource == resourcesForWork[i].ResourceForWork)
            {
                _tileObject.SetResourceRequied(resource, resourcesForWork[i].ResourcesForWorkAmount, _tileObject.CurrentResourceRecept());
            }
        }

        _requiredResourcePanel.UpdateButtonsView(resource.ResourceEnum, resourcesForWork);
    }

    public void ChangeResourceProduction(Resource resource, ResourceRecept[] resourceRecept)
    {
        _tileObject.SetResourceProduction(resource, resourceRecept);
    }
}
