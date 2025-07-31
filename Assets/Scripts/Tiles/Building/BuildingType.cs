using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BuildingType : MonoBehaviour
{
    [Inject] private TutorialSystem _tutorialSystem;
    [Inject] private LearnedBuildingsDataMission _learnedBuildingsDataMission;
    [SerializeField] private Image _image;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _tutorialView;
    private Tile _currentBuildingTypeTile;
    private TileObject _currentTileObject;
    private SelectTilePanel _selectTilePanel;
    private BuildsPanel _buildsPanel;
    private BuildTypesPanel _buildTypesPanel;
    private bool _canSelect = false;
    public Tile CurrentTile() => _currentBuildingTypeTile;

    private void Start()
    {
        if (_tutorialSystem.GetTutorialStepEnum() == TutorialStepEnum.CompleteMissionTutorial) return;
        SelectTutorialBuildingType();
    }

    private void SelectTutorialBuildingType()
    {
        switch (_tutorialSystem.GetTutorialStepEnum())
        {
            case TutorialStepEnum.MissionSelectBaseTypeButton_17:
                if (_currentBuildingTypeTile.BuildingTileView == BuildingTileViewEnum.Base)
                {
                    _tutorialView.SetActive(true);
                }
                break;
        }
    }

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
        _tutorialSystem.SelectBuildingType(_currentBuildingTypeTile.BuildingTileView);
        _tutorialView.SetActive(false);
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


