using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Tile _currentTile;
    [SerializeField] private TileObject _currentTileObject;
    private SelectTilePanel _selectTilePanel;
    private bool _isBuild;

    public void SetBuildingTile(Tile tile, TileObject tileObject, SelectTilePanel selectTilePanel)
    {
        _isBuild = true;
        _selectTilePanel = selectTilePanel;
        _currentTileObject = tileObject;
        _currentTile = tile;
        _nameText.text = _currentTile.Name[Language.LanguageNumber];
    }

    public void SetUpgradeTile(TileObject tileObject, SelectTilePanel selectTilePanel)
    {
        _isBuild = false;
        _selectTilePanel = selectTilePanel;
        _currentTileObject = tileObject;

        var tile = tileObject.BuildingTileObject();
        _nameText.text = tile.CurrentBuildingTile().UpgradeBuildingWrapper[tile.CurrentBuildingLevel()].Name[Language.LanguageNumber];
    }

    public void BuildOrUpgrade()
    {
        if (_isBuild)
        {
            _currentTileObject.BuildingTileObject().SpawnBuildingTile(_currentTile);
        }
        else
        {
            _currentTileObject.BuildingTileObject().UpgradeBuildingTile();
        }
        _selectTilePanel.ClosePanelAndRefreshInfo();
    }
}
