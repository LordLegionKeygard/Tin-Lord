using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using Zenject;
using System.Collections;

public class SelectTilePanel : MonoBehaviour
{
    [Inject] private readonly WorldHangarSystem _worldHangarSystem;
    [Inject] private readonly MissionResources _missionResources;

    [Header("Panels")]
    [SerializeField] private MachinePanel _machinePanel;
    [SerializeField] private BuildTypesPanel _buildTypesPanel;
    [SerializeField] private BuildsPanel _buildsPanel;
    [SerializeField] private UIPanelsWorld _uiPanels;
    [SerializeField] private DestroyPanel _destroyPanel;

    [Header("Objects")]
    [SerializeField] private GameObject _buildButton;
    [SerializeField] private GameObject _workButton;
    [SerializeField] private GameObject _generalRepairButton;

    [SerializeField] private GameObject _rotateButton;
    [SerializeField] private GameObject _destroyButton;
    [SerializeField] private GameObject _machineButton;
    [SerializeField] private RectTransform _objectTransform;

    [Header("Button Icons")]
    [SerializeField] private Image _workButtonIcon;
    [SerializeField] private Image _generalRepairButtonIcon;
    [SerializeField] private Image _destroyButtonIcon;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _groundTileNameText;
    [SerializeField] private TextMeshProUGUI _buildingNameText;
    [SerializeField] private TextMeshProUGUI _buildingHealthText;
    [SerializeField] private TextMeshProUGUI _buildingLevelText;
    [SerializeField] private TextMeshProUGUI _productionModifierText;
    [SerializeField] private TextMeshProUGUI _productionResourceText;
    [SerializeField] private TextMeshProUGUI _resourceForWorkText;
    [SerializeField] private TextMeshProUGUI _groundEcologyText;
    [SerializeField] private TextMeshProUGUI _buildingEcologyText;

    [Header("Turret")]
    [SerializeField] private GameObject _turretPanelObject;
    [SerializeField] private GameObject _turretPanelLine;
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _attackSpeedText;
    [SerializeField] private TextMeshProUGUI _attackRadiusText;
    [SerializeField] private TextMeshProUGUI _rotationSpeedText;

    [Header("Other")]
    [SerializeField] private MachineSpawnerSystem _machineSpawnerSystem;
    private TileObject _tileObject;
    private ResourceForWorkPanel _resourceForWorkPanel;
    private BaseProductionResourcePanel _productionResourcePanel;
    private ReceptPanel _receptPanel;
    private Coroutine _tagsCoroutine;
    private bool _isOpen;
    public bool PanelActive() => _isOpen;

    private void Awake()
    {
        _resourceForWorkPanel = GetComponent<ResourceForWorkPanel>();
        _productionResourcePanel = GetComponent<BaseProductionResourcePanel>();
        _receptPanel = GetComponent<ReceptPanel>();
    }

    private void Start()
    {
        CustomEvents.OnBuildingTakeDamage += RefreshInfoAfterTakeDamage;
        CustomEvents.OnRobotFullRepairBuilding += CheckBuildingFullRepairFromRobot;
    }

    public void RefreshInfoAfterTakeDamage(int tileId)
    {
        if (_tileObject == null || _tileObject.GetId() != tileId) return;
        RefreshInfo();
        RefreshDestroyPanelInfoAfterBuildingTakeDamage();
    }

    public void PanelViewToggle(bool state)
    {
        _isOpen = state;

        CustomEvents.FireTooltipToggle(false, 0);
        if (state)
        {
            _objectTransform.DOAnchorPosY(0, 0.3f).SetUpdate(true);
        }
        else
        {
            _objectTransform.DOAnchorPosY(-410, 0.3f).SetUpdate(true);

            ResetPanels();
            Clear();
        }
    }

    public void ResetPanels()
    {
        _uiPanels.CloseAllSelectTilePanels();
        DestroyPanelToggleAndRefreshButtonColor(false);
    }

    public void SetTile(TileObject tileObject)
    {
        _tileObject = tileObject;
    }

    public void RefreshInfo()
    {
        if (_tileObject == null) return;

        var buildingTileObject = _tileObject.BuildingTileObject();
        var buildingTile = buildingTileObject.CurrentBuildingTile();
        var haveBuildingTile = buildingTileObject.HaveTile() && buildingTileObject.HaveBuildingGameObject() && !buildingTileObject.IsConstructionNow();
        var building = haveBuildingTile ? buildingTileObject.CurrentBuilding() : null;

        SetTextFields(_tileObject, buildingTile, haveBuildingTile, building);
        SetProductionText(_tileObject, buildingTile, haveBuildingTile, building);
        SetWorkResourcesText(_tileObject, buildingTile, haveBuildingTile);
        SetEcologyTexts(_tileObject);
        SetTurretPanel(building);
        _uiPanels.SetRequiredResourcePanelVisibility(haveBuildingTile, building);
        _uiPanels.SetProductionResourcePanelVisibility(haveBuildingTile, building);
        _uiPanels.SetReceptPanelVisibility(haveBuildingTile, _tileObject.CurrentResourceRecept());
        WorkButtonIconChangeColor();
        GeneralRepairIconChangeColor();
        DestroyButtonChangeColor();

        if (haveBuildingTile)
        {
            if (_tileObject.CurrentResourceForWork() != null)
            {
                _resourceForWorkPanel.UpdateButtonsView(_tileObject, true);
            }

            if (buildingTile.IsHaveProductionResources())
            {
                _productionResourcePanel.SetButtonView(buildingTileObject.CurrentBuilding(), _tileObject.CurrentResourceProduction());
                _receptPanel.UpdateReceptView(_tileObject.CurrentResourceRecept());
            }
        }

        SetButtonStates(haveBuildingTile);
    }


    private void WorkButtonIconChangeColor()
    {
        _workButtonIcon.color = _tileObject.IsBuildingWork() ? Colors.GreySeven : Color.black;
    }

    private void GeneralRepairIconChangeColor()
    {
        _generalRepairButtonIcon.color = _tileObject.IsGeneralRepairSelect() ? Colors.GreySeven : Color.black;
    }

    private void SetTextFields(TileObject tileObject, Tile tile, bool haveBuildingTile, Building building)
    {
        var buildingFullHealth = haveBuildingTile ? _worldHangarSystem.GetTitanBuildingHealthBonus() > 1 ? $"<color={Colors.HexColorLightGreen}>{tileObject.BuildingHealth().GetMaxHealth()}</color>" : building.BuildingHealth.ToString() : "";
        _groundTileNameText.text = tileObject.GroundTileObject().CurrentGroundTile().Name[Language.LanguageNumber];
        _buildingNameText.text = haveBuildingTile ? $"{Language.TextStatic[2]}: {building.Name[Language.LanguageNumber]}" : $"{Language.TextStatic[2]}: -";
        _buildingHealthText.text = haveBuildingTile ? $"{Language.TextStatic[97]}: {tileObject.BuildingHealth().GetCurrentHealth()}/{buildingFullHealth}" : $"{Language.TextStatic[97]}: -";
        _buildingLevelText.text = haveBuildingTile ? $"{Language.TextStatic[3]}: {tileObject.BuildingTileObject().CurrentBuildingLevel()}" : $"{Language.TextStatic[3]}: -";

        _productionModifierText.text = haveBuildingTile && tile.IsHaveProductionResources()
         ? $"{Language.TextStatic[11]}: <color={(tileObject.CurrentModifier() == 0 ? Colors.HexColorWarningYellow : Colors.HexColorWhite)}>x{tileObject.CurrentModifier()}</color>" : $"{Language.TextStatic[11]}: -";
    }

    private void SetProductionText(TileObject tileObject, Tile tile, bool haveBuildingTile, Building buildings)
    {
        if (haveBuildingTile && tile.IsHaveProductionResources())
        {
            var isUseRources = _tileObject.BuildingTileObject().CurrentBuilding().ResourcesForWork.Length != 0;
            var productionName = $"{Language.TextStatic[tileObject.CurrentResourceProduction().NameNumber]}";
            string productionAmount;

            if (isUseRources)
            {
                productionAmount = tileObject.IsHaveRequiredResource() && tileObject.IsBuildingWork()
                    ? (buildings.ResourceExtractedAmount * tileObject.CurrentModifier()).ToString()
                    : "0";
            }
            else
            {
                productionAmount = tileObject.IsBuildingWork()
                    ? (buildings.ResourceExtractedAmount * tileObject.CurrentModifier()).ToString()
                    : "0";
            }

            var productionColor = (productionAmount == "0") ? Colors.HexColorWarningYellow : Colors.HexColorWhite;

            var productionText = $"{Language.TextStatic[6]}: <color={productionColor}>{productionName} {productionAmount}</color>";

            _productionResourceText.text = productionText;
        }
        else
        {
            _productionResourceText.text = $"{Language.TextStatic[6]}: -";
        }

    }

    private void SetWorkResourcesText(TileObject tileObject, Tile tile, bool haveBuildingTile)
    {
        string textColor;

        if (haveBuildingTile && tileObject.CurrentResourceForWork() != null && tileObject.IsHaveRequiredResource() &&
            (tile.IsHaveProductionResources() || tile.BuildingTileView == BuildingTileViewEnum.EcologyPurifier))
        {
            textColor = Colors.HexColorWhite;
        }
        else
        {
            textColor = Colors.HexColorWarningYellow;
        }

        _resourceForWorkText.text = $"{Language.TextStatic[14]}: <color={textColor}>{(haveBuildingTile && (tile.IsHaveProductionResources() || tile.BuildingTileView == BuildingTileViewEnum.EcologyPurifier) && tileObject.CurrentResourceForWork() != null ? $"{Language.TextStatic[tileObject.CurrentResourceForWork().NameNumber]} {tileObject.CurrentResourceForWorkAmount()}" : "-")}</color>";
    }

    private void SetEcologyTexts(TileObject tileObject)
    {
        _groundEcologyText.text = $"{Language.TextStatic[15]}: {tileObject.TileEcology().GetEcology(GetEcologyEnum.Ground)}";
        _buildingEcologyText.text = $"{Language.TextStatic[16]}: {tileObject.TileEcology().GetEcology(GetEcologyEnum.Building)}";
    }

    private void SetButtonStates(bool haveBuildingNow)
    {
        var currentGroundTile = _tileObject.GroundTileObject().CurrentGroundTile();
        var buildingTile = _tileObject.BuildingTileObject();
        var currentBuildingTile = haveBuildingNow ? buildingTile.CurrentBuildingTile() : null;

        var isRoad = currentGroundTile.GroundTileView == GroundTileViewEnum.Road;
        var isForwardRoad = _tileObject.GroundTileObject().IsForwardRoad();
        var isBase = currentGroundTile.GroundTileView == GroundTileViewEnum.BaseFoundation;
        var isMachineProduction = buildingTile.HaveTile() && buildingTile.HaveBuildingGameObject() && buildingTile.CurrentBuildingTile().BuildingTileView == BuildingTileViewEnum.MachineProduction;
        var isWater = currentGroundTile.IsWater;
        var haveProdictionResources = currentBuildingTile?.IsHaveProductionResources() ?? false;
        var canUpgrade = buildingTile.IsCanUpgrade();
        var isLastRiverTile = _tileObject.GroundTileObject().GetLastRiverTile();
        var canRepair = !_tileObject.BuildingHealth().IsFullHealth();
        var groundHaveBuildings = currentGroundTile.BuildingTypes.Length > 0;
        var isConstructionNow = buildingTile.IsConstructionNow();

        bool canRepairOrUpgrade = !haveBuildingNow || canUpgrade || canRepair;
        bool notRoadOrForwardRoad = !isRoad || isForwardRoad;

        var haveRotationViewGround = _tileObject.GroundTileObject().CurrentGroundTileObject().GetComponent<RotationView>() != null;
        var canRotateBuilding = buildingTile.HaveTile() && buildingTile.HaveBuildingGameObject() && buildingTile.CurrentBuilding().CanRotateBuilding;

        var buildButtonState = canRepairOrUpgrade && notRoadOrForwardRoad && groundHaveBuildings && !isConstructionNow;
        var workButtonState = haveBuildingNow && (haveProdictionResources || buildingTile.IsEcologyBuilding());
        var generalRepairButtonState = haveBuildingNow;
        var rotateButtonState = haveRotationViewGround || canRotateBuilding;
        var machineButtonState = haveBuildingNow && isMachineProduction;
        var destroyButtonState = (haveBuildingNow || (!isRoad && (!isWater || isLastRiverTile))) && !isBase && !isConstructionNow;

        _buildButton.SetActive(buildButtonState);
        _workButton.SetActive(workButtonState);
        _generalRepairButton.SetActive(generalRepairButtonState);
        _rotateButton.SetActive(rotateButtonState);
        _machineButton.SetActive(machineButtonState);
        _destroyButton.SetActive(destroyButtonState);

        _uiPanels.SetButtonsPanelVisibility(workButtonState || buildButtonState || destroyButtonState);
    }

    private void SetTurretPanel(Building building)
    {
        if (building == null || building.Damage == 0)
        {
            _turretPanelObject.SetActive(false);
            _turretPanelLine.SetActive(false);
        }
        else
        {
            var bonus = _worldHangarSystem.GetAimBotDamageBonus();
            string damageText = bonus != 0 ? $"<color={Colors.HexColorLightGreen}>{building.Damage * bonus}</color>" : building.Damage.ToString();
            _damageText.text = $"{Language.TextStatic[98]}: {damageText}";
            _attackSpeedText.text = $"{Language.TextStatic[99]}: {building.AttackSpeed}";
            _attackRadiusText.text = $"{Language.TextStatic[100]}: {building.AttackRadius}";
            _rotationSpeedText.text = $"{Language.TextStatic[101]}: {building.RotationSpeed}";

            _turretPanelObject.SetActive(true);
            _turretPanelLine.SetActive(true);
        }
    }

    private void CheckBuildingFullRepairFromRobot(int tileId)
    {
        if (_tileObject == null) return;

        if (_tileObject.GetId() == tileId)
        {
            CloseBuildPanelAndRefreshInfo();
            RefreshInfo();
        }
    }

    public void BuildButton()
    {
        if (!_buildButton.activeInHierarchy || _tileObject == null) return;
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);

        _uiPanels.TogglePanel(UIPanelsEnum.DestroyPanel, false);
        DestroyButtonChangeColor();

        if (!_tileObject.BuildingTileObject().HaveTile() && !_uiPanels.ActiveInHierarchy(UIPanelsEnum.BuildTypesPanel))
        {
            _uiPanels.SetBuildTypesPanelAndLineVisibility(true);
            _buildTypesPanel.SpawnBuildingTypesInScrollView(_tileObject, this);
        }
        else if (_tileObject.BuildingTileObject().HaveTile() && !_uiPanels.ActiveInHierarchy(UIPanelsEnum.BuildsPanel))
        {
            _uiPanels.TogglePanel(UIPanelsEnum.BuildsPanel, true);
            _buildsPanel.SpawnUpgradeItemsInScrollView(_tileObject, this);
        }
        CustomEvents.FireTooltipToggle(false, 0);
    }

    public void ToggleBuildingWorkButton()
    {
        if (!_workButton.activeInHierarchy || _tileObject == null) return;
        if (_tileObject.BuildingTileObject().IsEcologyBuilding() && !_tileObject.IsHaveRequiredResource()) return;
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Work], transform.position);

        _tileObject.SetBuildingWork(!_tileObject.IsBuildingWork());
        WorkButtonIconChangeColor();

        CustomEvents.FireChangeEcology(_tileObject.TileEcology().GetEcology(GetEcologyEnum.Total), _tileObject.GetId(), false);

        _tileObject.ChangeResourceProduction();
        CustomEvents.FireChangeResourceForWork(_tileObject, _tileObject.CurrentResourceForWork(), _tileObject.IsBuildingWork() ? _tileObject.CurrentResourceForWorkAmount() : 0, _tileObject.CurrentResourceRecept());
    }

    public void ToggleGeneralRepairButton()
    {
        if (!_generalRepairButton.activeInHierarchy || _tileObject == null) return;
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);

        _tileObject.SetGeneralRepairSelect(!_tileObject.IsGeneralRepairSelect());
        GeneralRepairIconChangeColor();

        CustomEvents.FireChangeGeneralRepairTileObject(_tileObject);
    }

    public void RotateButton()
    {
        if (!_rotateButton.activeInHierarchy || _tileObject == null) return;

        CustomEvents.FireToggleCheckTags(false);
        if (_tagsCoroutine != null) StopCoroutine(_tagsCoroutine);

        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Rotate], transform.position);
        var rotationViewGround = _tileObject.GroundTileObject().CurrentGroundTileObject().GetComponent<RotationView>();
        var rotationViewBuilding = _tileObject.BuildingTileObject().HaveTile() ? _tileObject.BuildingTileObject().CurrentBuildingGameObject().GetComponent<RotationView>() : null;

        if (rotationViewGround != null) rotationViewGround.Rotate();
        if (rotationViewBuilding != null) rotationViewBuilding.Rotate();

        _tagsCoroutine = StartCoroutine(nameof(ToggleTagsDisableCoroutine));
    }

    private IEnumerator ToggleTagsDisableCoroutine()
    {
        yield return new WaitForSeconds(1f);
        CustomEvents.FireToggleCheckTags(true);
    }

    public void DestroyButton()
    {
        if (!_destroyButton.activeInHierarchy || _tileObject == null) return;
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        _uiPanels.CloseAllBuildsPanels();

        if (!_destroyPanel.gameObject.activeInHierarchy)
        {
            DestroyPanelToggleAndRefreshButtonColor(true);
            _destroyPanel.SetInfo(_tileObject.BuildingTileObject().HaveTile(), _tileObject);
            return;
        }

        var groundTileObject = _tileObject.GroundTileObject();

        if (_tileObject.BuildingTileObject().HaveTile())
        {
            _tileObject.BuildingHealth().Death();
        }
        else if (!_tileObject.BuildingTileObject().HaveTile() && _missionResources.ResourceEnough(ResourceEnum.BeamEnergy, groundTileObject.CurrentGroundTile().GetEnergyBeam()))
        {
            _missionResources.ChangeResource(ResourceEnum.BeamEnergy, -groundTileObject.CurrentGroundTile().GetEnergyBeam());
            groundTileObject.DestroyGroundTile();
            PanelViewToggle(false);
        }
        else // недостаточно ресурсов для уничтожения тайла земли
        {
            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Error], transform.position);
            DestroyPanelToggleAndRefreshButtonColor(false);
        }
    }

    private void DestroyButtonChangeColor()
    {
        _destroyButtonIcon.color = _destroyPanel.gameObject.activeInHierarchy ? Colors.WarningYellow : Colors.GreySeven;
    }

    private void RefreshDestroyPanelInfoAfterBuildingTakeDamage()
    {
        if (_destroyPanel.gameObject.activeInHierarchy && _tileObject.BuildingTileObject().HaveTile())
        {
            _destroyPanel.SetInfo(_tileObject.BuildingTileObject().HaveTile(), _tileObject);
        }
    }

    public void MachinePanelButton()
    {
        if (!_machineButton.activeInHierarchy || _tileObject == null) return;
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        _machineSpawnerSystem.SetTileObject(_tileObject);
        _machinePanel.ActiveMacnineItems(_tileObject.BuildingTileObject().CurrentBuildingLevel());
        _machinePanel.PanelViewToggle(true);
        PanelViewToggle(false);
    }

    public void CloseBuildPanelAndRefreshInfo()
    {
        _uiPanels.CloseAllBuildsPanels();
        PanelViewToggle(true);
        RefreshInfo();
    }

    public void DestroyPanelToggleAndRefreshButtonColor(bool state)
    {
        _uiPanels.TogglePanel(UIPanelsEnum.DestroyPanel, state);
        DestroyButtonChangeColor();
    }

    public void ChangeResourceForWork(Resource resource)
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        var resourcesForWork = _tileObject.BuildingTileObject().CurrentBuilding().ResourcesForWork;

        for (int i = 0; i < resourcesForWork.Length; i++)
        {
            if (resource == resourcesForWork[i].ResourceForWork)
            {
                _tileObject.SetResourceForWork(resource, resourcesForWork[i].ResourcesForWorkAmount, _tileObject.CurrentResourceRecept());
            }
        }

        _resourceForWorkPanel.UpdateButtonsView(_tileObject, false);
    }

    public void ChangeResourceProduction(Resource resource, ResourceRecept[] resourceRecept)
    {
        _tileObject.SetResourceProduction(resource, resourceRecept);
    }

    private void Clear()
    {
        _tileObject = null;
    }

    private void OnDestroy()
    {
        CustomEvents.OnBuildingTakeDamage -= RefreshInfoAfterTakeDamage;
        CustomEvents.OnRobotFullRepairBuilding -= CheckBuildingFullRepairFromRobot;
    }
}
