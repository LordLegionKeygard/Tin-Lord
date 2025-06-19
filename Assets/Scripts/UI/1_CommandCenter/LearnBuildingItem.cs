using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LearnBuildingItem : MonoBehaviour
{
    [Inject] readonly CommandCenterSaveGame CommandCenterSaveGame;

    [Header("View")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _backImage;
    [SerializeField] private TextMeshProUGUI _priceText;

    [Header("Objects")]
    [SerializeField] private GameObject _priceObject;
    [SerializeField] private GameObject _nameObject;

    [Header("Other")]
    [SerializeField] private LearnBuildingItem _previousLearnBuildingItem;
    [SerializeField] private LearnBuildingInfoPanel _learnBuildingInfoPanel;
    [SerializeField] private Building _building;
    [SerializeField] private Button _button;
    [SerializeField] private MainResources _mainResources;
    private bool _isLearn;
    public bool IsLearn() => _isLearn;
    public Building GetBuilding() => _building;
    private bool _resourcesEnough;
    public bool IsResourcesEnough() => _resourcesEnough;

    private void Start()
    {
        CustomEvents.OnLearnBuilding += RefreshView;
    }

    public void SetupData(bool state)
    {
        _isLearn = state;
        RefreshView();
    }

    private void RefreshView()
    {
        var canLearn = _previousLearnBuildingItem == null || _previousLearnBuildingItem.IsLearn();

        _icon.sprite = _building.BuildingSprite;
        _nameText.text = canLearn ? _building.Name[Language.LanguageNumber] : "?";
        _priceText.text = _building.Price.ToString();

        _priceObject.SetActive(!_isLearn && canLearn);
        _button.interactable = canLearn;
        _resourcesEnough = _mainResources.ResourceEnough(ResourceEnum.MemoryFragment, _building.Price);
        _priceText.color = _resourcesEnough ? Colors.GreyEight : Colors.FadedYellow;
        _icon.color = _isLearn ? Color.white : Color.black;
        _backImage.color = _isLearn ? Color.white : Colors.GreyEight;
    }

    public void SelectItem()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        _learnBuildingInfoPanel.SetNewBuildingItem(this);
        _learnBuildingInfoPanel.RefreshInfo();
    }

    public void LearnBuilding()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.LearnBuilding], transform.position);
        _mainResources.ChangeResource(ResourceEnum.MemoryFragment, -_building.Price);
        _isLearn = true;
        RefreshView();

        CommandCenterSaveGame.SaveGameData(false);
        CustomEvents.FireLearnBuilding();
    }

    private void OnDestroy()
    {
        CustomEvents.OnLearnBuilding -= RefreshView;
    }
}
