using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuildingType : MonoBehaviour
{

    [SerializeField] private Image _image;
    private Tile _currentBuildingTypeTile;
    private TileObject _currentTileObject;
    private SelectTilePanel _selectTilePanel;
    private BuildsPanel _buildsPanel;
    private BuildTypesPanel _buildTypesPanel;

    public void SetBuildingType(Tile buildingTypeTile, TileObject tileObject, SelectTilePanel selectTilePanel, BuildsPanel buildsPanel, BuildTypesPanel buildTypesPanel)
    {
        _selectTilePanel = selectTilePanel;
        _currentTileObject = tileObject;
        _currentBuildingTypeTile = buildingTypeTile;
        _buildsPanel = buildsPanel;
        _buildTypesPanel = buildTypesPanel;
    }

    public void SelectTypeButton()
    {
        _buildsPanel.gameObject.SetActive(true);
        _buildsPanel.SpawnBuildingItemsInScrollView(_currentTileObject, _selectTilePanel, _currentBuildingTypeTile); //спавним список зданий этого типа
        _buildTypesPanel.UnselectAllTypes();
        ToggleSelectView(true);
        _buildTypesPanel.SetBuildingTypeText(_currentBuildingTypeTile.Name[Language.LanguageNumber]);
    }

    public void ToggleSelectView(bool state)
    {
        _image.color = state ? Color.white : Colors.Grey;
    }
}


