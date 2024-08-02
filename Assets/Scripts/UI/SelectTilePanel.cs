using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class SelectTilePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _groundTileNameText;
    [SerializeField] private TextMeshProUGUI _buildingNameText;
    [SerializeField] private TextMeshProUGUI _buildingLevelText;
    [SerializeField] private TextMeshProUGUI _productionModifierText;
    [SerializeField] private TextMeshProUGUI _totalProductionText;
    [SerializeField] private TextMeshProUGUI _requiredResources;
    [SerializeField] private TextMeshProUGUI _groundEcologyText;
    [SerializeField] private TextMeshProUGUI _buildingEcologyText;
    [SerializeField] private RectTransform _objectTransform;
    [SerializeField] private GameObject _buildButton;
    [SerializeField] private GameObject _upgradeButton;
    [SerializeField] private GameObject _tileInfoPanelObject;
    [SerializeField] private GameObject _tileBuildPanelObject;
    [SerializeField] private GameObject _requiredResourcePanel;
    [SerializeField] private GameObject _requiredResourcePanelLine;
    [SerializeField] private BuildTypesPanel _buildTypesPanel;
    [SerializeField] private BuildsPanel _buildsPanel;
    private TileObject _currentTileObject;
    private RequiredResourcePanel _requiredResourcePanelScript;

    private void Awake()
    {
        CustomEvents.OnRefreshShowInfo += RefreshShowInfo;
        _requiredResourcePanelScript = GetComponent<RequiredResourcePanel>();

    }

    private void RefreshShowInfo(int tileId)
    {
        Debug.Log("RefreshShowInfo - NeedOptimize");
        if (_currentTileObject == null) return;
        if (_currentTileObject.GetId() != tileId) return;
        SetInfo(_currentTileObject);
    }


    public void PanelViewToggle(bool state)
    {
        if (state)
        {
            _tileInfoPanelObject.SetActive(true);
            _tileBuildPanelObject.SetActive(false);
            _objectTransform.DOAnchorPosY(0, 0.3f).SetUpdate(true);
        }
        else
        {
            _objectTransform.DOAnchorPosY(-600, 0.3f).SetUpdate(true);
            _buildsPanel.gameObject.SetActive(false);
        }
    }

    public void SetInfo(TileObject tileObject)
    {
        _currentTileObject = tileObject;

        var buildingTile = tileObject.BuildingTileObject().CurrentBuildingTile();
        var haveBuildingTile = tileObject.BuildingTileObject().HaveTile();
        var buildingWrapper = haveBuildingTile ? tileObject.BuildingTileObject().CurrentUpgradeBuildingWrapper() : null;


        _groundTileNameText.text = tileObject.GroundTileObject().CurrentGroundTile().Name[Language.LanguageNumber];
        _buildingNameText.text = haveBuildingTile ? Language.TextStatic[2] + ": " + buildingWrapper.Name[Language.LanguageNumber] : Language.TextStatic[2] + ": -";
        _buildingLevelText.text = haveBuildingTile ? Language.TextStatic[3] + ": " + tileObject.BuildingTileObject().CurrentBuildingLevel().ToString() : Language.TextStatic[3] + ": -";
        _productionModifierText.text = haveBuildingTile && buildingTile.Resource != null ? Language.TextStatic[11] + ": " + "x" + tileObject.CurrentModifier : Language.TextStatic[11] + ": -";

        if (haveBuildingTile && buildingTile.Resource != null)
        {
            if (_currentTileObject.BuildingTileObject().CurrentUpgradeBuildingWrapper().ResourceRequiredEnum == ResourceRequiredEnum.None && buildingTile.Resource != null)
            {
                _totalProductionText.text = Language.TextStatic[6] + ": " + buildingTile.Resource.Name[Language.LanguageNumber] + " " + buildingWrapper.ResourceExtractedAmount * tileObject.CurrentModifier;
            }
            else
            {
                _totalProductionText.text = tileObject.IsHaveRequiredResource() ? Language.TextStatic[6] + ": " + buildingTile.Resource.Name[Language.LanguageNumber] + " " + buildingWrapper.ResourceExtractedAmount * tileObject.CurrentModifier
                : Language.TextStatic[6] + ": " + buildingTile.Resource.Name[Language.LanguageNumber] + " " + 0;
            }
        }
        else
        {
            _totalProductionText.text = Language.TextStatic[6] + ": -";
        }

        _requiredResources.text = Language.TextStatic[14] + ": " + (haveBuildingTile && buildingTile.Resource != null && tileObject.CurrentResourceRequired != null ? tileObject.CurrentResourceRequired.Name[Language.LanguageNumber] + " " + tileObject.CurrentResourceRequiredAmount : "-");

        _groundEcologyText.text = Language.TextStatic[15] + tileObject.TileEcology().GetEcology(GetEcologyEnum.Ground).ToString();
        _buildingEcologyText.text = Language.TextStatic[16] + tileObject.TileEcology().GetEcology(GetEcologyEnum.Building).ToString();


        _requiredResourcePanel.SetActive(haveBuildingTile && buildingWrapper.ResourceRequiredEnum!= ResourceRequiredEnum.None);
        _requiredResourcePanelLine.SetActive(haveBuildingTile && buildingWrapper.ResourceRequiredEnum!= ResourceRequiredEnum.None);

        _buildButton.SetActive(!haveBuildingTile);
        _upgradeButton.SetActive(tileObject.BuildingTileObject().IsCanUpgrade());
        if(haveBuildingTile && buildingTile.Resource != null && tileObject.CurrentResourceRequired != null) _requiredResourcePanelScript.UpdateButtonsView(_currentTileObject.CurrentResourceRequired.ResourceEnum);
    }

    public void OpenPanelForBuild()
    {
        _tileBuildPanelObject.SetActive(true);
        _buildTypesPanel.SpawnBuildingTypesInScrollView(_currentTileObject, this);
    }

    public void OpenPanelForUpgrade()
    {
        _buildsPanel.gameObject.SetActive(true);
        _buildsPanel.SpawnUpgradeItemsInScrollView(_currentTileObject, this);
    }

    public void ClosePanelAndRefreshInfo()
    {
        _buildsPanel.gameObject.SetActive(false);
        _tileBuildPanelObject.SetActive(false);
        PanelViewToggle(true);
        SetInfo(_currentTileObject);
    }

    public void ChangeResourceRequired(Resource resource)
    {
        _currentTileObject.BuildingResourcesRequired().ChangeResourceRequired(_currentTileObject, resource);
        _requiredResourcePanelScript.UpdateButtonsView(resource.ResourceEnum);
    }

    private void OnDestroy()
    {
        CustomEvents.OnRefreshShowInfo -= RefreshShowInfo;
    }
}
