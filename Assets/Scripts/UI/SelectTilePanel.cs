using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class SelectTilePanel : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private BuildTypesPanel _buildTypesPanel;
    [SerializeField] private BuildsPanel _buildsPanel;
    [SerializeField] private UIPanels _uiPanels;

    [Header("Objects")]
    [SerializeField] private GameObject _buildButton;
    [SerializeField] private GameObject _workButton;
    [SerializeField] private GameObject _rotateButton;
    [SerializeField] private GameObject _destroyButton;
    [SerializeField] private RectTransform _objectTransform;
    [SerializeField] private Image _onOffImage;

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
        _uiPanels.TogglePanel(UIPanelsEnum.BuildTypesPanel, false);
        _objectTransform.DOAnchorPosY(0, 0.3f).SetUpdate(true);
    }

    private void HideInfoPanel()
    {
        _objectTransform.DOAnchorPosY(-700, 0.3f).SetUpdate(true);

        _uiPanels.SetBuildTypesPanelAndLineVisibility(false);
        _uiPanels.TogglePanel(UIPanelsEnum.BuildsPanel, false);

        Clear();
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
        _uiPanels.SetRequiredResourcePanelVisibility(haveBuildingTile, currentBuilding);
        _uiPanels.SetProductionResourcePanelVisibility(haveBuildingTile, currentBuilding);
        _uiPanels.SetReceptPanelVisibility(haveBuildingTile, tileObject.CurrentResourceRecept());
        _uiPanels.SetBuildTypesPanelAndLineVisibility(false);
        SetOnOffButtonColor();

        if (haveBuildingTile)
        {
            if (tileObject.CurrentResourceRequired() != null)
            {
                _requiredResourcePanel.UpdateButtonsView(_tileObject);
            }

            if (buildingTile.IsHaveProductionResources())
            {
                _productionResourcePanel.SetButtonView(tileObject.BuildingTileObject().CurrentBuilding(), tileObject.CurrentResourceProduction());
                _receptPanel.UpdateReceptView(tileObject.CurrentResourceRecept());
            }
        }

        SetButtonStates(tileObject, haveBuildingTile);
    }


    private void SetOnOffButtonColor()
    {
        _onOffImage.color = _tileObject.IsBuildingWork() ? Colors.Grey : Color.black;
    }

    private void SetTextFields(TileObject tileObject, Tile tile, bool haveBuildingTile, Building building)
    {
        _groundTileNameText.text = tileObject.GroundTileObject().CurrentGroundTile().Name[Language.LanguageNumber];
        _buildingNameText.text = haveBuildingTile ? $"{Language.TextStatic[2]}: {building.Name[Language.LanguageNumber]}" : $"{Language.TextStatic[2]}: -";
        _buildingLevelText.text = haveBuildingTile ? $"{Language.TextStatic[3]}: {tileObject.BuildingTileObject().CurrentBuildingLevel()}" : $"{Language.TextStatic[3]}: -";

        _productionModifierText.text = haveBuildingTile && tile.IsHaveProductionResources()
     ? $"{Language.TextStatic[11]}: <color={(tileObject.CurrentModifier() == 0 ? Colors.HexColorWarningYellow : Colors.HexColorWhite)}>x{tileObject.CurrentModifier()}</color>"
     : $"{Language.TextStatic[11]}: -";
    }

    private void SetProductionText(TileObject tileObject, Tile tile, bool haveBuildingTile, Building buildings)
    {
        if (haveBuildingTile && tile.IsHaveProductionResources())
        {
            var isUseRources = _tileObject.BuildingTileObject().CurrentBuilding().ResourcesForWork.Length != 0;
            var productionName = $"{tileObject.CurrentResourceProduction().Name[Language.LanguageNumber]} ";
            string productionAmount;

            if (isUseRources)
            {
                productionAmount = tileObject.IsHaveRequiredResource() && tileObject.IsBuildingWork()
                    ? (buildings.ResourceExtractedAmount * tileObject.CurrentModifier()).ToString()
                    : "0";
            }
            else
            {
                productionAmount = tileObject.IsBuildingWork()
                    ? (buildings.ResourceExtractedAmount * tileObject.CurrentModifier()).ToString()
                    : "0";
            }

            var productionColor = (productionAmount == "0") ? Colors.HexColorWarningYellow : Colors.HexColorWhite;

            var productionText = $"{Language.TextStatic[6]}: <color={productionColor}>{productionName}{productionAmount}</color>";

            _productionResourceText.text = productionText;
        }
        else
        {
            _productionResourceText.text = $"{Language.TextStatic[6]}: -";
        }

    }

    private void SetRequiredText(TileObject tileObject, Tile tile, bool haveBuildingTile)
    {
        string textColor;

        if (haveBuildingTile && tileObject.CurrentResourceRequired() != null && tileObject.IsHaveRequiredResource() &&
            (tile.IsHaveProductionResources() || tile.BuildingTileView == BuildingTileViewEnum.EcologyPurifier))
        {
            textColor = Colors.HexColorWhite;
        }
        else
        {
            textColor = Colors.HexColorWarningYellow;
        }

        _requiredResources.text = $"{Language.TextStatic[14]}: <color={textColor}>{(haveBuildingTile && (tile.IsHaveProductionResources() || tile.BuildingTileView == BuildingTileViewEnum.EcologyPurifier) && tileObject.CurrentResourceRequired() != null ? $"{tileObject.CurrentResourceRequired().Name[Language.LanguageNumber]} {tileObject.CurrentResourceRequiredAmount()}" : "-")}</color>";
    }

    private void SetEcologyTexts(TileObject tileObject)
    {
        _groundEcologyText.text = $"{Language.TextStatic[15]}{tileObject.TileEcology().GetEcology(GetEcologyEnum.Ground)}";
        _buildingEcologyText.text = $"{Language.TextStatic[16]}{tileObject.TileEcology().GetEcology(GetEcologyEnum.Building)}";
    }

    private void SetButtonStates(TileObject tileObject, bool haveBuildingNow)
    {
        var currentGroundTile = tileObject.GroundTileObject().CurrentGroundTile();
        var currentBuildingTile = haveBuildingNow ? tileObject.BuildingTileObject().CurrentBuildingTile() : null;

        var isRoad = currentGroundTile.GroundTileView == GroundTileViewEnum.Road;
        var isForwardRoad = tileObject.GroundTileObject().IsForwardRoad();
        var isBase = currentGroundTile.GroundTileView == GroundTileViewEnum.BaseFoundation;
        var isWater = currentGroundTile.IsWater;
        var haveProdictionResources = currentBuildingTile?.IsHaveProductionResources() ?? false;
        var canUpgrade = tileObject.BuildingTileObject().IsCanUpgrade();
        var isLastRiverTile = tileObject.GroundTileObject().GetLastRiverTile();
        var canRepair = !tileObject.BuildingHealth().IsFullHealth();
        var groundHaveBuildings = currentGroundTile.BuildingTypes.Length > 0;

        bool canRepairOrUpgrade = !haveBuildingNow || canUpgrade || canRepair;
        bool notRoadOrForwardRoad = !isRoad || isForwardRoad;

        var haveRotationViewGround = _tileObject.GroundTileObject().CurrentGroundTileObject().GetComponent<RotationView>() != null;
        var haveRotationViewBuilding = _tileObject.BuildingTileObject().HaveTile() && _tileObject.BuildingTileObject().CurrentBuildingTileObject().GetComponent<RotationView>() != null;

        var onOffButtonState = haveBuildingNow && (haveProdictionResources || tileObject.BuildingTileObject().IsEcologyBuilding());
        var buildButtonState = canRepairOrUpgrade && notRoadOrForwardRoad && groundHaveBuildings;
        var rotateButtonState = haveRotationViewGround || haveRotationViewBuilding;
        var destroyButtonState = (haveBuildingNow || (!isRoad && (!isWater || isLastRiverTile))) && !isBase;

        _workButton.SetActive(onOffButtonState);
        _buildButton.SetActive(buildButtonState);
        _rotateButton.SetActive(rotateButtonState);
        _destroyButton.SetActive(destroyButtonState);

        _uiPanels.SetButtonsPanelVisibility(onOffButtonState || buildButtonState || destroyButtonState);
    }

    public void BuildOnTile()
    {
        if (!_buildButton.activeInHierarchy || _tileObject == null) return;

        if (!_tileObject.BuildingTileObject().HaveTile() && !_uiPanels.ActiveInHierarchy(UIPanelsEnum.BuildTypesPanel))
        {
            _uiPanels.SetBuildTypesPanelAndLineVisibility(true);
            _buildTypesPanel.SpawnBuildingTypesInScrollView(_tileObject, this);
        }
        else if (_tileObject.BuildingTileObject().HaveTile() && !_uiPanels.ActiveInHierarchy(UIPanelsEnum.BuildsPanel))
        {
            _uiPanels.TogglePanel(UIPanelsEnum.BuildsPanel, true);
            _buildsPanel.SpawnUpgradeItemsInScrollView(_tileObject, this);
        }
    }

    public void ToggleBuildingWork()
    {
        if (!_workButton.activeInHierarchy || _tileObject == null) return;

        if (_tileObject.BuildingTileObject().IsEcologyBuilding() && !_tileObject.IsHaveRequiredResource()) return;

        _tileObject.SetBuildingWork(!_tileObject.IsBuildingWork());
        SetOnOffButtonColor();

        CustomEvents.FireChangeEcology(_tileObject.TileEcology().GetEcology(GetEcologyEnum.Total), _tileObject.GetId(), false);

        _tileObject.ChangeResourceProduction();
        CustomEvents.FireChangeResourceRequired(_tileObject, _tileObject.CurrentResourceRequired(), _tileObject.IsBuildingWork() ? _tileObject.CurrentResourceRequiredAmount() : 0, _tileObject.CurrentResourceRecept());
    }

    public void RotateTile()
    {
        if (!_rotateButton.activeInHierarchy || _tileObject == null) return;

        var rotationViewGround = _tileObject.GroundTileObject().CurrentGroundTileObject().GetComponent<RotationView>();
        var rotationViewBuilding = _tileObject.BuildingTileObject().HaveTile() ? _tileObject.BuildingTileObject().CurrentBuildingTileObject().GetComponent<RotationView>() : null;

        if (rotationViewGround != null) rotationViewGround.Rotate();
        if (rotationViewBuilding != null) rotationViewBuilding.Rotate();
    }

    public void DestroyTile()
    {
        if (!_destroyButton.activeInHierarchy || _tileObject == null) return;

        if (!_tileObject.BuildingTileObject().HaveTile())
        {
            _tileObject.GroundTileObject().DestroyGroundTile();
            PanelViewToggle(false);
        }
        else
        {
            _tileObject.BuildingTileObject().DestroyBuildingTile(false);
            CloseBuildPanelAndRefreshInfo();
            SetInfo(_tileObject);
        }
    }

    public void CloseBuildPanelAndRefreshInfo()
    {
        _uiPanels.TogglePanel(UIPanelsEnum.BuildsPanel, false);
        _uiPanels.SetBuildTypesPanelAndLineVisibility(false);
        PanelViewToggle(true);
        SetInfo(_tileObject);
    }

    public void ChangeResourceRequired(Resource resource)
    {
        var resourcesForWork = _tileObject.BuildingTileObject().CurrentBuilding().ResourcesForWork;

        for (int i = 0; i < resourcesForWork.Length; i++)
        {
            if (resource == resourcesForWork[i].ResourceForWork)
            {
                _tileObject.SetResourceRequied(resource, resourcesForWork[i].ResourcesForWorkAmount, _tileObject.CurrentResourceRecept());
            }
        }

        _requiredResourcePanel.UpdateButtonsView(_tileObject);
    }

    public void ChangeResourceProduction(Resource resource, ResourceRecept[] resourceRecept)
    {
        _tileObject.SetResourceProduction(resource, resourceRecept);
    }

    private void Clear()
    {
        _tileObject = null;
    }
}
