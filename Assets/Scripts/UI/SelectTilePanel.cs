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
    [SerializeField] private TextMeshProUGUI _tileEcologyText;
    [SerializeField] private RectTransform _objectTransform;
    [SerializeField] private GameObject _buildButton;
    [SerializeField] private GameObject _upgradeButton;
    [SerializeField] private GameObject _tileInfoPanelObject;
    [SerializeField] private GameObject _tileBuildPanelObject;
    [SerializeField] private TileBuildPanel _tileBuildPanel;
    private TileObject _currentTileObject;


    public void PanelViewToggle(bool state)
    {
        if (state)
        {
            _tileInfoPanelObject.SetActive(true);
            _tileBuildPanelObject.SetActive(false);
            _objectTransform.DOAnchorPosY(125, 0.3f);
        }
        else
        {
            _objectTransform.DOAnchorPosY(-130, 0.3f);
        }
    }

    public void ShowInfo(TileObject tileObject)
    {
        _currentTileObject = tileObject;

        PanelViewToggle(true);

        _groundTileNameText.text = tileObject.GroundTileObject().CurrentGroundTile().Name[Language.LanguageNumber];
        _buildingNameText.text = tileObject.BuildingTileObject().HaveTile() ? Language.TextStatic[2] + ": " + tileObject.BuildingTileObject().CurrentBuildingTile().UpgradeBuildingWrapper[tileObject.BuildingTileObject().CurrentBuildingLevel() - 1].Name[Language.LanguageNumber] : Language.TextStatic[2] + ": -";
        _buildingLevelText.text = tileObject.BuildingTileObject().HaveTile() ? Language.TextStatic[3] + ": " + tileObject.BuildingTileObject().CurrentBuildingLevel().ToString() : Language.TextStatic[3] + ": -";

        var groundEcology = tileObject.GroundTileObject().CurrentGroundTile().GroundEcology;
        var buildingEcology = tileObject.BuildingTileObject().HaveTile() ? tileObject.BuildingTileObject().CurrentBuildingTile().UpgradeBuildingWrapper[tileObject.BuildingTileObject().CurrentBuildingLevel() - 1].BuildingEcology : 0;
        _tileEcologyText.text = Language.TextStatic[1] + ": " + (groundEcology + buildingEcology).ToString();

        _buildButton.SetActive(!tileObject.BuildingTileObject().HaveTile());
        _upgradeButton.SetActive(tileObject.BuildingTileObject().IsCanUpgrade());
    }

    public void OpenPanelForBuild()
    {
        _tileInfoPanelObject.SetActive(false);
        _tileBuildPanelObject.SetActive(true);
        _tileBuildPanel.SpawnBuildingTypesInScrollView(_currentTileObject, this);
    }

    public void OpenPanelForUpgrade()
    {
        _tileInfoPanelObject.SetActive(false);
        _tileBuildPanelObject.SetActive(true);
        _tileBuildPanel.SpawnUpgradeItemsInScrollView(_currentTileObject, this);
    }

    public void ClosePanelAndRefreshInfo()
    {
        _tileInfoPanelObject.SetActive(true);
        _tileBuildPanelObject.SetActive(false);
        ShowInfo(_currentTileObject);
    }
}
