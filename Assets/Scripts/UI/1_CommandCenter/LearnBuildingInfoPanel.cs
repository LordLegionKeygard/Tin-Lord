using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LearnBuildingInfoPanel : MonoBehaviour
{
    [SerializeField] private PanelDoMoveY _panelDoMoveY;
    private LearnBuildingItem _currentLearnBuildingItem;

    [Header("Main")]
    [SerializeField] private TextMeshProUGUI _buildingNameText;
    [SerializeField] private TextMeshProUGUI _buildingHealthText;
    [SerializeField] private TextMeshProUGUI _buildingEcologyText;
    [SerializeField] private TextMeshProUGUI _buildingLevelText;

    [Header("BuildingResources")]
    [SerializeField] private TextMeshProUGUI _buildingResourcesText;
    [SerializeField] private GameObject _buildingResourcesPanelObject;
    [SerializeField] private GameObject _buildingResourcesPanelLine;
    private ResourcesViewCommandCenter _resourcesView;

    [Header("ResourcesFoWork")]
    [SerializeField] private TextMeshProUGUI _resourceForWorkText;
    [SerializeField] private GameObject _resourceForWorkPanelObject;
    [SerializeField] private GameObject _resourceForWorkPanelLine;
    private ResourceForWorkPanelCommandCenter _resourceForWorkPanel;
    private float _resourceForWorkAmount;

    [Header("Recept")]
    [SerializeField] private TextMeshProUGUI _receptText;
    [SerializeField] private GameObject _receptPanelObject;
    [SerializeField] private GameObject _receptPanelLine;
    private ReceptPanelCommandCenter _receptPanel;

    [Header("Production")]
    [SerializeField] private GameObject _productionResourcePanelObject;
    [SerializeField] private GameObject _productionResourcePanelLine;
    [SerializeField] private TextMeshProUGUI _productionResourceText;
    private BaseProductionResourcePanel _productionResourcePanel;
    private int _currentResourcesProduction = 0;

    [Header("Turret")]
    [SerializeField] private GameObject _turretPanelObject;
    [SerializeField] private GameObject _turretPanelLine;
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _attackSpeedText;
    [SerializeField] private TextMeshProUGUI _attackRadiusText;
    [SerializeField] private TextMeshProUGUI _rotationSpeedText;


    [Header("Buttons")]
    [SerializeField] private Button _learnButton;
    [SerializeField] private GameObject _buttonsPanelObject;

    private void Awake()
    {
        _productionResourcePanel = GetComponent<BaseProductionResourcePanel>();
        _resourceForWorkPanel = GetComponent<ResourceForWorkPanelCommandCenter>();
        _resourcesView = GetComponent<ResourcesViewCommandCenter>();
        _receptPanel = GetComponent<ReceptPanelCommandCenter>();
    }

    public void SetNewBuildingItem(LearnBuildingItem learnBuildingItem)
    {
        _currentLearnBuildingItem = learnBuildingItem;
        _currentResourcesProduction = 0;
    }

    public void RefreshInfo()
    {
        var building = _currentLearnBuildingItem.GetBuilding();
        SetMainPanel(building);
        SetBuildingResourcesPanel(building);
        SetProductionPanel(building);
        SetResourceForWorkPanel(building);
        SetReceptPanel(building);
        SetTurretPanel(building);
        SetButtonPanel();

        if (!_panelDoMoveY.IsOpen()) _panelDoMoveY.IsOpen();
    }

    private void SetMainPanel(Building building)
    {
        _buildingNameText.text = building.Name[Language.LanguageNumber];
        _buildingHealthText.text = $"{Language.TextStatic[97]}: {building.BuildingHealth}";
        _buildingEcologyText.text = $"{Language.TextStatic[16]}: {building.BuildingEcology}";
        _buildingLevelText.text = $"{Language.TextStatic[3]}: {building.BuildingLevel}";
    }

    private void SetBuildingResourcesPanel(Building building)
    {
        _buildingResourcesText.text = Language.TextStatic[152];
        _buildingResourcesPanelObject.SetActive(true);
        _buildingResourcesPanelLine.SetActive(true);
        _resourcesView.SetResourcesView(building.ResourcesForBuild);
    }

    private void SetProductionPanel(Building building)
    {
        if (building.ResourcesProduction.Length == 0)
        {
            _productionResourcePanelObject.SetActive(false);
            _productionResourcePanelLine.SetActive(false);
        }
        else
        {
            _productionResourcePanel.SetButtonView(building, building.ResourcesProduction[_currentResourcesProduction].ProductionResource);
            var productionName = $"{building.ResourcesProduction[_currentResourcesProduction].ProductionResource.Name[Language.LanguageNumber]}";
            string productionAmount;
            productionAmount = building.ResourceExtractedAmount.ToString();
            var productionText = $"{Language.TextStatic[6]}: {productionName} {productionAmount}";
            _productionResourceText.text = productionText;

            _productionResourcePanelObject.SetActive(true);
            _productionResourcePanelLine.SetActive(true);
        }
    }

    private void SetResourceForWorkPanel(Building building)
    {
        if (building.ResourcesForWork.Length != 0)
        {
            SetResourceForWorkAndText(building.ResourcesForWork[0].ResourceForWork);
            _resourceForWorkPanelObject.SetActive(true);
            _resourceForWorkPanelLine.SetActive(true);
        }
        else
        {
            _resourceForWorkPanelObject.SetActive(false);
            _resourceForWorkPanelLine.SetActive(false);
        }
    }

    private void SetReceptPanel(Building building)
    {
        _receptText.text = Language.TextStatic[1];
        if (building.ResourcesProduction.Length == 0 || building.ResourcesProduction[_currentResourcesProduction].ResourceRecept.Length == 0)
        {
            _receptPanelObject.SetActive(false);
            _receptPanelLine.SetActive(false);
        }
        else
        {
            _receptPanel.UpdateReceptView(building.ResourcesProduction[_currentResourcesProduction].ResourceRecept);
            _receptPanelObject.SetActive(true);
            _receptPanelLine.SetActive(true);
        }
    }

    private void SetTurretPanel(Building building)
    {
        if (building.Damage == 0)
        {
            _turretPanelObject.SetActive(false);
            _turretPanelLine.SetActive(false);
        }
        else
        {
            _damageText.text = $"{Language.TextStatic[98]}: {building.Damage}";
            _attackSpeedText.text = $"{Language.TextStatic[99]}: {building.AttackSpeed}";
            _attackRadiusText.text = $"{Language.TextStatic[100]}: {building.AttackRadius}";
            _rotationSpeedText.text = $"{Language.TextStatic[101]}: {building.RotationSpeed}";

            _turretPanelObject.SetActive(true);
            _turretPanelLine.SetActive(true);
        }
    }

    private void SetButtonPanel()
    {
        _buttonsPanelObject.SetActive(!_currentLearnBuildingItem.IsLearn());

        _learnButton.interactable = _currentLearnBuildingItem.IsResourcesEnough();
    }

    public void ChangeResourceProduction(int resourceNumber)
    {
        _currentResourcesProduction = resourceNumber;
        RefreshInfo();
    }

    public void LearnButton()
    {
        _currentLearnBuildingItem.LearnBuilding();
        SetButtonPanel();
        CustomEvents.FireCloseTooltips();
    }

    public void Reset()
    {
        _buildingResourcesPanelObject.SetActive(false);
        _buildingResourcesPanelLine.SetActive(false);
        _receptPanelObject.SetActive(false);
        _receptPanelLine.SetActive(false);
        _productionResourcePanelObject.SetActive(false);
        _productionResourcePanelLine.SetActive(false);
        _turretPanelObject.SetActive(false);
        _turretPanelLine.SetActive(false);
        _buttonsPanelObject.SetActive(false);

        _currentLearnBuildingItem = null;
    }

    public void ChangeResourceForWork(Resource resource)
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        SetResourceForWorkAndText(resource);
    }

    private void SetResourceForWorkAndText(Resource resource)
    {
        var building = _currentLearnBuildingItem.GetBuilding();

        for (int i = 0; i < building.ResourcesForWork.Length; i++)
        {
            if(building.ResourcesForWork[i].ResourceForWork == resource)
            {
                _resourceForWorkAmount = building.ResourcesForWork[i].ResourcesForWorkAmount;
            }
        }

        _resourceForWorkPanel.UpdateButtonsView(building, resource.ResourceEnum);
        _resourceForWorkText.text = $"{Language.TextStatic[14]}: {resource.Name[Language.LanguageNumber]} {_resourceForWorkAmount}";
    }
}
