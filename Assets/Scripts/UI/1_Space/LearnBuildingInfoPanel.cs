using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LearnBuildingInfoPanel : MonoBehaviour
{
    [SerializeField] private Tile[] _allBuildingTypes;
    [SerializeField] private BuildingsLearnPanel _learnPanel;
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
    private ResourcesViewSpace _resourcesView;

    [Header("ResourcesFoWork")]
    [SerializeField] private TextMeshProUGUI _resourceForWorkText;
    [SerializeField] private GameObject _resourceForWorkPanelObject;
    [SerializeField] private GameObject _resourceForWorkPanelLine;
    private ResourceForWorkPanelSpace _resourceForWorkPanel;
    private float _resourceForWorkAmount;

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

    [Header("BlockReason")]
    [SerializeField] private GameObject _blockReasonPanelObject;
    [SerializeField] private GameObject _blockReasonPanelLine;
    [SerializeField] private TextMeshProUGUI _blockReasonText;

    [Header("BlockReason → Go")]
    [SerializeField] private Button _goButton;          // сама кнопка GO
    [SerializeField] private ScrollRect _scrollRect;    // ScrollRect, где лежат LearnBuildingItem
    private LearnBuildingItem _targetItemForGo;         // куда прыгать

    [Header("Buttons")]
    [SerializeField] private GameObject _buttonsPanelObject;

    private void Awake()
    {
        _productionResourcePanel = GetComponent<BaseProductionResourcePanel>();
        _resourceForWorkPanel = GetComponent<ResourceForWorkPanelSpace>();
        _resourcesView = GetComponent<ResourcesViewSpace>();
        _receptPanel = GetComponent<ReceptPanelSpace>();
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
            var productionName = $"{Language.TextStatic[building.ResourcesProduction[_currentResourcesProduction].ProductionResource.NameNumber]}";
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
        var building = _currentLearnBuildingItem.GetBuilding();
        int currentBaseLevel = _learnPanel.GetCurrentBaseLevel();
        int requiredBase = building.RequiredBaseLevel;

        if (currentBaseLevel < requiredBase)
        {
            _targetItemForGo = _learnPanel.GetBaseItemByLevel(requiredBase);
            _blockReasonText.text = GetNeedOpenText(_targetItemForGo);

            PrepareGoButton();
            BlockReasonToggle(true);
            _buttonsPanelObject.SetActive(false);
            return;
        }

        bool depsOk = _learnPanel.TryGetBlockingResource(building, out ResourceEnum missing);

        if (!depsOk)
        {
            _targetItemForGo = _learnPanel.GetProducerOf(missing);
            _blockReasonText.text = GetNeedOpenText(_targetItemForGo);

            PrepareGoButton();
            BlockReasonToggle(true);
            _buttonsPanelObject.SetActive(false);
            return;
        }

        BlockReasonToggle(false);
        _buttonsPanelObject.SetActive(!_currentLearnBuildingItem.IsLearn());
    }

    private string GetNeedOpenText(LearnBuildingItem item)
    {
        string needOpen = Language.TextStatic[43];  // "Вам нужно открыть"
        string buildingNm = item.GetBuilding().Name[Language.LanguageNumber];
        string inText = Language.TextStatic[75];  // "в"
        string typeNm = _learnPanel.GetParentTileName(item, _allBuildingTypes);

        return $"{needOpen} \"{buildingNm}\" {inText} \"{typeNm}\"";
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


    private IEnumerator LateScroll()
    {
        yield return null; // ждём перерисовку LayoutGroup
        ScrollToItem(_targetItemForGo);
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
            if (building.ResourcesForWork[i].ResourceForWork == resource)
            {
                _resourceForWorkAmount = building.ResourcesForWork[i].ResourcesForWorkAmount;
            }
        }

        _resourceForWorkPanel.UpdateButtonsView(building, resource.ResourceEnum);
        _resourceForWorkText.text = $"{Language.TextStatic[14]}: {Language.TextStatic[resource.NameNumber]} {_resourceForWorkAmount}";
    }

    private string GetBlockReasonText(ResourceEnum res)
    {
        // Проходим по всем типам зданий по порядку (порядок открытия)
        for (int i = 0; i < _allBuildingTypes.Length; i++)
        {
            var tile = _allBuildingTypes[i];
            if (tile == null || tile.Buildings == null) continue;

            // Внутри типа — по всем зданиям
            for (int j = 0; j < tile.Buildings.Length; j++)
            {
                var b = tile.Buildings[j];
                if (b == null || b.ResourcesProduction == null) continue;

                // Проверяем, какие ресурсы здание производит
                foreach (var prod in b.ResourcesProduction)
                {
                    if (prod == null || prod.ProductionResource == null) continue;

                    if (prod.ProductionResource.ResourceEnum == res)
                    {
                        // Нашли первое здание-производитель ↴ формируем текст
                        string needOpen = Language.TextStatic[43]; // "Вам нужно открыть"
                        string buildingName = b.Name[Language.LanguageNumber];
                        string inText = Language.TextStatic[75]; // "в"
                        string typeName = tile.Name[Language.LanguageNumber];

                        return $"{needOpen} \"{buildingName}\" {inText} \"{typeName}\"";
                    }
                }
            }
        }

        return "Необходимо неизвестное здание";   // это явная ошибка
    }
}
