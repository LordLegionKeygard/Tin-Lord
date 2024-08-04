using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class SelectTilePanel : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private BuildTypesPanel _buildTypesPanel;
    [SerializeField] private BuildsPanel _buildsPanel;

    [Header("Objects")]
    [SerializeField] private GameObject _buildButton;
    [SerializeField] private GameObject _onOffButton;
    [SerializeField] private GameObject _tileInfoPanelObject;
    [SerializeField] private GameObject _tileBuildPanelObject;
    [SerializeField] private GameObject _tileBuildPanelLine;
    [SerializeField] private GameObject _requiredResourcePanelObject;
    [SerializeField] private GameObject _requiredResourcePanelLine;
    [SerializeField] private RectTransform _objectTransform;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _groundTileNameText;
    [SerializeField] private TextMeshProUGUI _buildingNameText;
    [SerializeField] private TextMeshProUGUI _buildingLevelText;
    [SerializeField] private TextMeshProUGUI _productionModifierText;
    [SerializeField] private TextMeshProUGUI _totalProductionText;
    [SerializeField] private TextMeshProUGUI _requiredResources;
    [SerializeField] private TextMeshProUGUI _groundEcologyText;
    [SerializeField] private TextMeshProUGUI _buildingEcologyText;

    [Header("Other")]
    private TileObject _currentTileObject;
    private RequiredResourcePanel _requiredResourcePanel;

    private void Awake()
    {
        _requiredResourcePanel = GetComponent<RequiredResourcePanel>();
    }

    public void RefreshShowInfo(int tileId)
    {
        if (_currentTileObject == null || _currentTileObject.GetId() != tileId) return;
        Debug.Log("RefreshShowInfo - CheckCount");
        SetInfo(_currentTileObject);
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
        _tileInfoPanelObject.SetActive(true);
        _tileBuildPanelObject.SetActive(false);
        _objectTransform.DOAnchorPosY(0, 0.3f).SetUpdate(true);
    }

    private void HideInfoPanel()
    {
        _objectTransform.DOAnchorPosY(-600, 0.3f).SetUpdate(true);
        _buildsPanel.gameObject.SetActive(false);
        _tileBuildPanelLine.SetActive(false);
    }

    public void SetInfo(TileObject tileObject)
    {
        _currentTileObject = tileObject;

        var buildingTile = tileObject.BuildingTileObject().CurrentBuildingTile();
        var haveBuildingTile = tileObject.BuildingTileObject().HaveTile();
        var buildingWrapper = haveBuildingTile ? tileObject.BuildingTileObject().CurrentUpgradeBuildingWrapper() : null;

        SetTextFields(tileObject, buildingTile, haveBuildingTile, buildingWrapper);
        SetProductionText(tileObject, buildingTile, haveBuildingTile, buildingWrapper);
        SetEcologyTexts(tileObject);
        SetPanelVisibility(haveBuildingTile, buildingWrapper);

        if (haveBuildingTile && buildingTile.Resource != null && tileObject.CurrentResourceRequired() != null)
        {
            _requiredResourcePanel.UpdateButtonsView(_currentTileObject.CurrentResourceRequired().ResourceEnum, _currentTileObject.BuildingTileObject().CurrentUpgradeBuildingWrapper().ResourceRequiredEnum);
        }

        SetButtonStates(tileObject, haveBuildingTile);
    }

    private void SetTextFields(TileObject tileObject, Tile buildingTile, bool haveBuildingTile, UpgradeBuildingWrapper buildingWrapper)
    {
        _groundTileNameText.text = tileObject.GroundTileObject().CurrentGroundTile().Name[Language.LanguageNumber];
        _buildingNameText.text = haveBuildingTile ? $"{Language.TextStatic[2]}: {buildingWrapper.Name[Language.LanguageNumber]}" : $"{Language.TextStatic[2]}: -";
        _buildingLevelText.text = haveBuildingTile ? $"{Language.TextStatic[3]}: {tileObject.BuildingTileObject().CurrentBuildingLevel()}" : $"{Language.TextStatic[3]}: -";
        _productionModifierText.text = haveBuildingTile && buildingTile.Resource != null ? $"{Language.TextStatic[11]}: x{tileObject.CurrentModifier()}" : $"{Language.TextStatic[11]}: -";
    }

    private void SetProductionText(TileObject tileObject, Tile buildingTile, bool haveBuildingTile, UpgradeBuildingWrapper buildingWrapper)
    {
        if (haveBuildingTile && buildingTile.Resource != null)
        {
            var isUseRources = _currentTileObject.BuildingTileObject().CurrentUpgradeBuildingWrapper().ResourceRequiredEnum != ResourceRequiredEnum.None;
            var productionText = $"{Language.TextStatic[6]}: {buildingTile.Resource.Name[Language.LanguageNumber]} ";

            if (isUseRources)
            {
                productionText += tileObject.IsHaveRequiredResource() ? (buildingWrapper.ResourceExtractedAmount * tileObject.CurrentModifier()).ToString() : "0";

            }
            else
            {
                productionText += (buildingWrapper.ResourceExtractedAmount * tileObject.CurrentModifier()).ToString();
            }
            _totalProductionText.text = productionText;

        }
        else
        {
            _totalProductionText.text = $"{Language.TextStatic[6]}: -";
        }

        _requiredResources.text = $"{Language.TextStatic[14]}: {(haveBuildingTile && buildingTile.Resource != null && tileObject.CurrentResourceRequired() != null ? $"{tileObject.CurrentResourceRequired().Name[Language.LanguageNumber]} {tileObject.CurrentResourceRequiredAmount()}" : "-")}";
    }

    private void SetEcologyTexts(TileObject tileObject)
    {
        _groundEcologyText.text = $"{Language.TextStatic[15]}{tileObject.TileEcology().GetEcology(GetEcologyEnum.Ground)}";
        _buildingEcologyText.text = $"{Language.TextStatic[16]}{tileObject.TileEcology().GetEcology(GetEcologyEnum.Building)}";
    }

    private void SetPanelVisibility(bool haveBuildingTile, UpgradeBuildingWrapper buildingWrapper)
    {
        var showRequiredResourcePanel = haveBuildingTile && buildingWrapper.ResourceRequiredEnum != ResourceRequiredEnum.None;
        _requiredResourcePanelObject.SetActive(showRequiredResourcePanel);
        _requiredResourcePanelLine.SetActive(showRequiredResourcePanel);
    }

    private void SetButtonStates(TileObject tileObject, bool haveBuildingTile)
    {
        _onOffButton.SetActive(haveBuildingTile);
        _buildButton.SetActive(!haveBuildingTile || tileObject.BuildingTileObject().IsCanUpgrade()); ;
    }

    public void BuildButton()
    {
        if (!_currentTileObject.BuildingTileObject().HaveTile() && !_tileBuildPanelObject.activeInHierarchy)
        {
            _tileBuildPanelObject.SetActive(true);
            _tileBuildPanelLine.SetActive(true);
            _buildTypesPanel.SpawnBuildingTypesInScrollView(_currentTileObject, this);
        }
        else if (_currentTileObject.BuildingTileObject().HaveTile() && !_buildsPanel.gameObject.activeInHierarchy)
        {
            _buildsPanel.gameObject.SetActive(true);
            _buildsPanel.SpawnUpgradeItemsInScrollView(_currentTileObject, this);
        }
    }

    public void DestroyButton()
    {
        if (!_currentTileObject.BuildingTileObject().HaveTile())
        {
            _currentTileObject.GroundTileObject().DestroyGroundTile();
            PanelViewToggle(false);            
        }
    }

    public void ClosePanelAndRefreshInfo()
    {
        _buildsPanel.gameObject.SetActive(false);
        _tileBuildPanelObject.SetActive(false);
        _tileBuildPanelLine.SetActive(false);
        PanelViewToggle(true);
        SetInfo(_currentTileObject);
    }

    public void ChangeResourceRequired(Resource resource)
    {
        _currentTileObject.BuildingResourcesRequired().ChangeResourceRequired(_currentTileObject, resource);
        _requiredResourcePanel.UpdateButtonsView(_currentTileObject.CurrentResourceRequired().ResourceEnum, _currentTileObject.BuildingTileObject().CurrentUpgradeBuildingWrapper().ResourceRequiredEnum);
    }
}
