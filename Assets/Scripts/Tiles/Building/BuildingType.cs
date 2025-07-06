using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BuildingType : MonoBehaviour
{
    [Inject] private LearnedBuildingsDataMission _learnedBuildingsDataMission;
    [SerializeField] private Image _image;
    [SerializeField] private Image _icon;
    private Tile _currentBuildingTypeTile;
    private TileObject _currentTileObject;
    private SelectTilePanel _selectTilePanel;
    private BuildsPanel _buildsPanel;
    private BuildTypesPanel _buildTypesPanel;
    [SerializeField] private Button _button;
    private bool _canSelect = false;

    public Tile CurrentTile() => _currentBuildingTypeTile;

    public void SetBuildingType(Tile buildingTypeTile, TileObject tileObject, SelectTilePanel selectTilePanel, BuildsPanel buildsPanel, BuildTypesPanel buildTypesPanel)
    {
        _selectTilePanel = selectTilePanel;
        _currentTileObject = tileObject;
        _currentBuildingTypeTile = buildingTypeTile;
        _buildsPanel = buildsPanel;
        _buildTypesPanel = buildTypesPanel;
        _icon.sprite = buildingTypeTile.Icon;

        CheckButton(buildingTypeTile);
    }

    private void CheckButton(Tile buildingTypeTile)
    {
        _canSelect = _learnedBuildingsDataMission.IsHaveOneLearnedBuildingInBuildingType(buildingTypeTile);

        _button.interactable = _canSelect;
        _icon.color = _canSelect ? Color.white : Colors.AlphaGreyFive;
    }

    public void SelectTypeButton()
    {
        if (!_canSelect) return;
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);

        _buildsPanel.gameObject.SetActive(true);
        _buildsPanel.SpawnBuildingItemsInScrollView(_currentTileObject, _selectTilePanel, _currentBuildingTypeTile); //спавним список зданий этого типа
        _buildTypesPanel.UnselectAllTypes();
        ToggleSelectView(true);
        _buildTypesPanel.SetBuildingTypeText(_currentBuildingTypeTile.Name[Language.LanguageNumber]);
    }

    public void ToggleSelectView(bool state)
    {
        _image.color = state ? Color.white : Colors.GreySeven;
    }
}


