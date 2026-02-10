using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LearnBuildingInfoPanel : MonoBehaviour
{
    [SerializeField] private Tile[] _allBuildingTypes;
    [SerializeField] private BuildingsLearnPanel _buildingLearnPanel;
    [SerializeField] private PanelDoMoveY _panelDoMoveY;
    private LearnBuildingItem _currentLearnBuildingItem;

    [Header("Main")]
    [SerializeField] private TextMeshProUGUI _buildingNameText;
    [SerializeField] private TextMeshProUGUI _buildingEcologyText;
    [SerializeField] private TextMeshProUGUI _buildingHealthText;
    [SerializeField] private TextMeshProUGUI _buildingLevelText;

    [Header("MainPlus")]
    [SerializeField] private TextMeshProUGUI _ecologicalRestorationText;

    [Header("BuildingResources")]
    [SerializeField] private TextMeshProUGUI _buildingResourcesText;
    [SerializeField] private GameObject _buildingResourcesPanelObject;
    [SerializeField] private GameObject _buildingResourcesPanelLine;
    [SerializeField] private ResourcesViewSpace _buildingResourcesView;

    [Header("ResourcesFoWork")]
    [SerializeField] private TextMeshProUGUI _resourceForWorkText;
    [SerializeField] private GameObject _resourceForWorkPanelObject;
    [SerializeField] private GameObject _resourceForWorkPanelLine;
    private ResourceForWorkPanelSpace _resourceForWorkPanel;
    private float _resourceForWorkAmount;
    private Resource _currentResourceForWork;
    public Resource GetCurrentResourceForWork() => _currentResourceForWork;

    [Header("Recept")]
    [SerializeField] private TextMeshProUGUI _receptText;
    [SerializeField] private GameObject _receptPanelObject;
    [SerializeField] private GameObject _receptPanelLine;
    private ReceptPanelSpace _receptPanel;

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

    [Header("Machine")]
    [SerializeField] private TextMeshProUGUI _machineText;
    [SerializeField] private TextMeshProUGUI _machineResourcesText;
    [SerializeField] private GameObject _machinePanelObject;
    [SerializeField] private GameObject _machinePanelLine;
    [SerializeField] private ResourcesViewSpace _machineResourcesView;

    [Header("BlockReason")]
    [SerializeField] private GameObject _blockReasonPanelObject;
    [SerializeField] private GameObject _blockReasonPanelLine;
    [SerializeField] private TextMeshProUGUI _blockReasonText;

    [Header("BlockReason → Go")]
    [SerializeField] private Button _goButton;
    [SerializeField] private ScrollRect _scrollRect;
    private LearnBuildingItem _targetItemForGo;

    [Header("Buttons")]
    [SerializeField] private GameObject _buttonsPanelObject;

    private void Awake()
    {
        _productionResourcePanel = GetComponent<BaseProductionResourcePanel>();
        _resourceForWorkPanel = GetComponent<ResourceForWorkPanelSpace>();
        _receptPanel = GetComponent<ReceptPanelSpace>();
    }

    private void Start()
    {
        var buildingEcologyText = $"<color={Colors.HexGreySeven}>{Language.TextStatic[16]}:</color>";
        var buildingHealthText = $"<color={Colors.HexGreySeven}>{Language.TextStatic[97]}:</color>";
        var buildindLevelText = $"<color={Colors.HexGreySeven}>{Language.TextStatic[3]}:</color>";

        _buildingEcologyText.text = $"{buildingEcologyText} -";
        _buildingHealthText.text = $"{buildingHealthText} -";
        _buildingLevelText.text = $"{buildindLevelText} -";
        _ecologicalRestorationText.gameObject.SetActive(false);
    }

    public void SetNewBuildingItem(LearnBuildingItem learnBuildingItem)
    {
        _buildingLearnPanel.UnselectAllBuildingItems();
        _currentLearnBuildingItem = learnBuildingItem;
        _currentResourcesProduction = 0;
        _currentResourceForWork = null;
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
        SetMachine(building);
        SetButtonPanel();

        if (!_panelDoMoveY.IsOpen()) _panelDoMoveY.IsOpen();
    }

    private void SetMainPanel(Building building)
    {
        _ecologicalRestorationText.gameObject.SetActive(building.BuildingEcologicalRestoration > 0);
        _buildingNameText.text = Language.TextStatic[building.NameLanguageNumber];
        var buildingEcologyText = $"<color={Colors.HexGreySeven}>{Language.TextStatic[16]}:</color>";
        var buildingHealthText = $"<color={Colors.HexGreySeven}>{Language.TextStatic[97]}:</color>";
        var buildindLevelText = $"<color={Colors.HexGreySeven}>{Language.TextStatic[3]}:</color>";
        var ecologicalRestorationText = $"<color={Colors.HexGreySeven}>{Language.TextStatic[245]}:</color>";

        _buildingEcologyText.text = $"{buildingEcologyText} {building.BuildingEcology}";
        _buildingHealthText.text = $"{buildingHealthText} {building.BuildingHealth}";
        _buildingLevelText.text = $"{buildindLevelText} {building.BuildingLevel}";
        _ecologicalRestorationText.text = $"{ecologicalRestorationText} {building.BuildingEcologicalRestoration}";
    }

    private void SetBuildingResourcesPanel(Building building)
    {
        _buildingResourcesText.text = Language.TextStatic[152];
        _buildingResourcesPanelObject.SetActive(true);
        _buildingResourcesPanelLine.SetActive(true);
        _buildingResourcesView.SetResourcesView(building.ResourcesForBuild);
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
            var productionName = $"{Language.TextStatic[building.ResourcesProduction[_currentResourcesProduction].ProductionResource.NameNumber]}";
            string productionAmount;
            productionAmount = building.ResourceExtractedAmount.ToString();
            var productionResourceText = $"<color={Colors.HexGreySeven}>{Language.TextStatic[6]}:</color>";
            _productionResourceText.text = $"{productionResourceText} {productionName} {productionAmount}";

            _productionResourcePanelObject.SetActive(true);
            _productionResourcePanelLine.SetActive(true);
        }
    }

    public void ChangeResourceForWorkPanel(Resource resource)
    {
        if (_currentResourceForWork == resource) return;
        _currentResourceForWork = resource;
        SetResourceForWorkAndText();
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        SetButtonPanel();
    }

    private void SetResourceForWorkPanel(Building building)
    {
        if (building.ResourcesForWork.Length != 0)
        {
            var resourceForWork = _currentResourceForWork ?? building.ResourcesForWork[0].ResourceForWork;
            _currentResourceForWork = resourceForWork;
            SetResourceForWorkAndText();
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
            var damageText = $"<color={Colors.HexGreySeven}>{Language.TextStatic[98]}:</color>";
            var attackSpeedText = $"<color={Colors.HexGreySeven}>{Language.TextStatic[99]}:</color>";
            var attackRadiusText = $"<color={Colors.HexGreySeven}>{Language.TextStatic[100]}:</color>";
            var rotationSpeedText = $"<color={Colors.HexGreySeven}>{Language.TextStatic[101]}:</color>";

            _damageText.text = $"{damageText} {building.Damage}";
            _attackSpeedText.text = $"{attackSpeedText} {building.AttackSpeed}";
            _attackRadiusText.text = $"{attackRadiusText} {building.AttackRadius}";
            _rotationSpeedText.text = $"{rotationSpeedText} {building.RotationSpeed}";

            _turretPanelObject.SetActive(true);
            _turretPanelLine.SetActive(true);
        }
    }

    private void SetMachine(Building building)
    {
        if (building.MachineInfo == null)
        {
            _machinePanelObject.SetActive(false);
            _machinePanelLine.SetActive(false);
        }
        else
        {
            var machineText = $"<color={Colors.HexGreySeven}>{Language.TextStatic[243]}:</color>";
            _machineText.text = $"{machineText} {Language.TextStatic[building.MachineInfo.NameLanguageNumber]}";
            _machineResourcesText.text = Language.TextStatic[244];
            _machinePanelObject.SetActive(true);
            _machinePanelLine.SetActive(true);
            _machineResourcesView.SetResourcesView(building.MachineInfo.ResourcesForBuild);
        }
    }

    private void SetButtonPanel()
    {
        var building = _currentLearnBuildingItem.GetBuilding();
        int currentBaseLevel = _buildingLearnPanel.GetCurrentBaseLevel();
        int requiredBase = building.RequiredBaseLevel;

        if (currentBaseLevel < requiredBase)
        {
            _targetItemForGo = _buildingLearnPanel.GetBaseItemByLevel(requiredBase);
            _blockReasonText.text = GetNeedOpenText(_targetItemForGo);

            PrepareGoButton();
            BlockReasonToggle(true);
            _buttonsPanelObject.SetActive(false);
            return;
        }

        bool isHaveAllResourcesForBuilding = _buildingLearnPanel.TryGetBlockingResourceForBuilding(building, out ResourceEnum missing);

        if (!isHaveAllResourcesForBuilding)
        {
            SetBlockReason(missing);
            return;
        }

        if (building.MachineInfo != null)
        {
            bool isHaveAllResourcesForMachine = _buildingLearnPanel.TryGetBlockingResourceForMachine(building.MachineInfo, out ResourceEnum missingMachine);

            if (!isHaveAllResourcesForMachine)
            {
                SetBlockReason(missingMachine);
                return;
            }
        }

        BlockReasonToggle(false);
        _buttonsPanelObject.SetActive(!_currentLearnBuildingItem.IsLearn());
    }

    private void SetBlockReason(ResourceEnum missing)
    {
        _targetItemForGo = _buildingLearnPanel.GetProducerOf(missing);
        _blockReasonText.text = GetNeedOpenText(_targetItemForGo);

        PrepareGoButton();
        BlockReasonToggle(true);
        _buttonsPanelObject.SetActive(false);
    }

    private string GetNeedOpenText(LearnBuildingItem item)
    {
        string needOpen = Language.TextStatic[43];  // "Вам нужно открыть"
        string buildingName = Language.TextStatic[item.GetBuilding().NameLanguageNumber];
        string typeNumber = _buildingLearnPanel.GetParentTileName(item, _allBuildingTypes);

        return $"{needOpen} \"{buildingName}\" => \"{typeNumber}\"";
    }

    private void PrepareGoButton()
    {
        _goButton.onClick.RemoveAllListeners();
        LearnBuildingItem target = _targetItemForGo;

        _goButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
            CustomEvents.FireCloseTooltips();
            target.SelectItem();
            StartCoroutine(LateScroll(target));
        });
    }

    private IEnumerator LateScroll(LearnBuildingItem item)
    {
        yield return null; // ждём, пока перестроится лэйаут
        ScrollToItem(item); // крутимся к той же сохранённой цели
    }

    private void ScrollToItem(LearnBuildingItem item)
    {
        if (item == null) return;

        const float block = 260f;
        const float space = 12f;

        int index = item.GetOrder();
        float offsetDown = index * (block + space);

        float scrollable = _scrollRect.content.rect.height - _scrollRect.viewport.rect.height;
        if (scrollable <= 0) return;

        float normalized = 1f - Mathf.Clamp01(offsetDown / scrollable);

        _scrollRect.verticalNormalizedPosition = normalized;
    }

    private void BlockReasonToggle(bool state)
    {
        _blockReasonPanelObject.SetActive(state);
        _blockReasonPanelLine.SetActive(state);
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
        _resourceForWorkPanelObject.SetActive(false);
        _resourceForWorkPanelLine.SetActive(false);
        _buildingResourcesPanelObject.SetActive(false);
        _buildingResourcesPanelLine.SetActive(false);
        _receptPanelObject.SetActive(false);
        _receptPanelLine.SetActive(false);
        _productionResourcePanelObject.SetActive(false);
        _productionResourcePanelLine.SetActive(false);
        _turretPanelObject.SetActive(false);
        _turretPanelLine.SetActive(false);
        _buttonsPanelObject.SetActive(false);
        _blockReasonPanelObject.SetActive(false);
        _blockReasonPanelLine.SetActive(false);
        _machinePanelObject.SetActive(false);
        _machinePanelLine.SetActive(false);
        _ecologicalRestorationText.gameObject.SetActive(false);

        _currentLearnBuildingItem = null;
    }

    private void SetResourceForWorkAndText()
    {
        var building = _currentLearnBuildingItem.GetBuilding();

        for (int i = 0; i < building.ResourcesForWork.Length; i++)
        {
            if (building.ResourcesForWork[i].ResourceForWork == _currentResourceForWork)
            {
                _resourceForWorkAmount = building.ResourcesForWork[i].ResourcesForWorkAmount;
            }
        }

        _resourceForWorkPanel.UpdateButtonsView(building, _currentResourceForWork.ResourceEnum);
        var resourceForWorkText = $"<color={Colors.HexGreySeven}>{Language.TextStatic[14]}:</color>";
        _resourceForWorkText.text = $"{resourceForWorkText} {Language.TextStatic[_currentResourceForWork.NameNumber]} {_resourceForWorkAmount}";
    }
}
