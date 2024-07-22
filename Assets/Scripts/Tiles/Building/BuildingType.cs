using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingType : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Tile _currentTile;
    [SerializeField] private TileObject _currentTileObject;
    private SelectTilePanel _selectTilePanel;
    private TileBuildPanel _tileBuildPanel;

    public void SetBuildingType(Tile tile, TileObject tileObject, SelectTilePanel selectTilePanel, TileBuildPanel tileBuildPanel)
    {
        _selectTilePanel = selectTilePanel;
        _currentTileObject = tileObject;
        _currentTile = tile;
        _tileBuildPanel = tileBuildPanel;

        _nameText.text = _currentTile.Name[Language.LanguageNumber];
    }

    public void SelectTypeButton()
    {
        _tileBuildPanel.SpawnBuildingItemsInScrollView(_currentTileObject, _selectTilePanel, _currentTile); //возвращаемся заспавнить список зданий этого типа

    }
}


