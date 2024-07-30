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
    [SerializeField] private TextMeshProUGUI _tileEcologyText;
    [SerializeField] private RectTransform _objectTransform;
    [SerializeField] private GameObject _buildButton;
    [SerializeField] private GameObject _upgradeButton;
    [SerializeField] private GameObject _tileInfoPanelObject;
    [SerializeField] private GameObject _tileBuildPanelObject;
    [SerializeField] private BuildTypesPanel _buildTypesPanel;
    [SerializeField] private BuildsPanel _buildsPanel;
    private TileObject _currentTileObject;


    public void PanelViewToggle(bool state)
    {
        if (state)
        {
            _tileInfoPanelObject.SetActive(true);
            _tileBuildPanelObject.SetActive(false);
            _objectTransform.DOAnchorPosY(110, 0.3f).SetUpdate(true);
        }
        else
        {
            _objectTransform.DOAnchorPosY(-130, 0.3f).SetUpdate(true);
            _buildsPanel.PanelViewToggle(false);
        }
    }

    public void ShowInfo(TileObject tileObject)
    {
        _currentTileObject = tileObject;

        PanelViewToggle(true);

        var buildingTile = tileObject.BuildingTileObject().CurrentBuildingTile();
        var haveTile = tileObject.BuildingTileObject().HaveTile();
        var buildingLevel = haveTile ? tileObject.BuildingTileObject().CurrentBuildingLevel() : 0;

        _groundTileNameText.text = tileObject.GroundTileObject().CurrentGroundTile().Name[Language.LanguageNumber];
        _buildingNameText.text = haveTile ? Language.TextStatic[2] + ": " + buildingTile.UpgradeBuildingWrapper[buildingLevel - 1].Name[Language.LanguageNumber] : Language.TextStatic[2] + ": -";
        _buildingLevelText.text = haveTile ? Language.TextStatic[3] + ": " + buildingLevel.ToString() : Language.TextStatic[3] + ": -";
        _productionModifierText.text = haveTile && buildingTile.Resource != null ? Language.TextStatic[11] + ": " + "x" + StaticMethods.GetResourceModifier(tileObject) : Language.TextStatic[11] + ": -";
        _totalProductionText.text = haveTile && buildingTile.Resource != null ? Language.TextStatic[6] + ": " + buildingTile.Resource.Name[Language.LanguageNumber] + " " + (buildingTile.UpgradeBuildingWrapper[buildingLevel - 1].ResourceExtractedAmount * StaticMethods.GetResourceModifier(tileObject)) : Language.TextStatic[6] + ": -";
        _tileEcologyText.text = Language.TextStatic[1] + ": " + tileObject.GetEcology().ToString();

        _buildButton.SetActive(!haveTile);
        _upgradeButton.SetActive(tileObject.BuildingTileObject().IsCanUpgrade());
    }

    public void OpenPanelForBuild()
    {
        _tileInfoPanelObject.SetActive(false);
        _tileBuildPanelObject.SetActive(true);
        _buildTypesPanel.SpawnBuildingTypesInScrollView(_currentTileObject, this);
    }

    public void OpenPanelForUpgrade()
    {
        _buildsPanel.SpawnUpgradeItemsInScrollView(_currentTileObject, this);
    }

    public void ClosePanelAndRefreshInfo()
    {
        _buildsPanel.PanelViewToggle(false);
        _tileInfoPanelObject.SetActive(true);
        _tileBuildPanelObject.SetActive(false);
        ShowInfo(_currentTileObject);
    }
}
