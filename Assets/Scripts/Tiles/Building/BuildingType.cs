using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingType : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Tile _currentBuildingTypeTile;
    [SerializeField] private TileObject _currentTileObject;
    private SelectTilePanel _selectTilePanel;
    private BuildsPanel _buildsPanel;

    public void SetBuildingType(Tile buildingTypeTile, TileObject tileObject, SelectTilePanel selectTilePanel, BuildsPanel buildsPanel)
    {
        _selectTilePanel = selectTilePanel;
        _currentTileObject = tileObject;
        _currentBuildingTypeTile = buildingTypeTile;
        _buildsPanel = buildsPanel;

        _nameText.text = _currentBuildingTypeTile.Name[Language.LanguageNumber];
    }

    public void SelectTypeButton()
    {
        _buildsPanel.gameObject.SetActive(true);
        _buildsPanel.SpawnBuildingItemsInScrollView(_currentTileObject, _selectTilePanel, _currentBuildingTypeTile); //спавним список зданий этого типа
    }
}


