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
    private BuildsPanel _buildsPanel;

    public void SetBuildingType(Tile tile, TileObject tileObject, SelectTilePanel selectTilePanel, BuildsPanel buildsPanel)
    {
        _selectTilePanel = selectTilePanel;
        _currentTileObject = tileObject;
        _currentTile = tile;
        _buildsPanel = buildsPanel;

        _nameText.text = _currentTile.Name[Language.LanguageNumber];
    }

    public void SelectTypeButton()
    {
        _buildsPanel.SpawnBuildingItemsInScrollView(_currentTileObject, _selectTilePanel, _currentTile); //спавним список зданий этого типа
    }
}


