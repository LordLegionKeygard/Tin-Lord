using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using Zenject;
using System.Collections;

public class SelectTilePanel : MonoBehaviour
{
    [Inject] private readonly EscapePanelMission _escapePanel;
    [Inject] private readonly TileViewSystem _tileViewSystem;
    [Inject] private readonly TutorialSystem _tutorialSystem;
    [Inject] private readonly MissionHangarSystem _missionHangarSystem;
    [Inject] private readonly MissionResources _missionResources;

    [Header("Panels")]
    [SerializeField] private MachinePanel _machinePanel;
    [SerializeField] private BuildTypesPanel _buildTypesPanel;
    [SerializeField] private BuildsPanel _buildsPanel;
    [SerializeField] private UIPanelsMission _uiPanels;
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
    [SerializeField] private TextMeshProUGUI _groundEcologyText;
    [SerializeField] private TextMeshProUGUI _buildingEcologyText;
    [SerializeField] private TextMeshProUGUI _buildingNameText;
    [SerializeField] private TextMeshProUGUI _buildingHealthText;
    [SerializeField] private TextMeshProUGUI _buildingLevelText;
    [SerializeField] private TextMeshProUGUI _productionModifierText;
    [SerializeField] private TextMeshProUGUI _productionResourceText;
    [SerializeField] private TextMeshProUGUI _resourceForWorkText;

    [Header("Turret")]
    [SerializeField] private GameObject _turretPanelObject;
    [SerializeField] private GameObject _turretPanelLine;
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _attackSpeedText;
    [SerializeField] private TextMeshProUGUI _attackRadiusText;
    [SerializeField] private TextMeshProUGUI _rotationSpeedText;

    [Header("Other")]
    [SerializeField] private MachineSpawnerSystem _machineSpawnerSystem;
    [SerializeField] private AllSkills _allSkills;
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
        _tileViewSystem.UnactiveRadius();
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
        var buildingTile = buildingTileObject.GetCurrentBuildingTile();
        var haveBuildingTile = buildingTileObject.IsHaveTile() && buildingTileObject.IsHaveBuildingGameObject() && !buildingTileObject.IsConstructionNow();
        var building = haveBuildingTile ? buildingTileObject.GetCurrentBuilding() : null;

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
                _productionResourcePanel.SetButtonView(buildingTileObject.GetCurrentBuilding(), _tileObject.GetCurrentResourceProduction());
                _receptPanel.UpdateReceptView(_tileObject.CurrentResourceRecept());
            }

            if (buildingTile.BuildingTileView == BuildingTileViewEnum.AttackingStructures)
            {
                _tileViewSystem.ActivateRadius(_tileObject.transform, building.AttackRadius);
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
        var bonus = _missionHangarSystem.GetTitanBuildingHealthBonus();
        var tacticCardIncreaseHealthLevel = _tileObject.BuildingTileObject().GetTacticCardIncreaseHealthLevel();
        var maxHealthColor = bonus > 1 || tacticCardIncreaseHealthLevel > 0 ? Colors.HexLightGreen : Colors.HexWhite;
        var buildingMaxHealth = haveBuildingTile ? $"<color={maxHealthColor}>{tileObject.BuildingHealth().GetMaxHealth()}</color>" : "";

        var buildindText = $"<color={Colors.HexGreySeven}>{Language.TextStatic[2]}:</color>";
        var buildingHealthText = $"<color={Colors.HexGreySeven}>{Language.TextStatic[97]}:</color>";
        var buildindLevelText = $"<color={Colors.HexGreySeven}>{Language.TextStatic[3]}:</color>";
        var productionModifierText = $"<color={Colors.HexGreySeven}>{Language.TextStatic[11]}:</color>";

        _groundTileNameText.text = tileObject.GroundTileObject().CurrentGroundTile().Name[Language.LanguageNumber];
        _groundTileNameText.color = Colors.GetRarityColor(tileObject.GetRarity());
        _buildingNameText.text = haveBuildingTile ? $"{buildindText} {building.Name[Language.LanguageNumber]}" : $"{buildindText} -";
        _buildingHealthText.text = haveBuildingTile ? $"{buildingHealthText} {tileObject.BuildingHealth().GetCurrentHealth()}/{buildingMaxHealth}" : $"{buildingHealthText} -";
        _buildingLevelText.text = haveBuildingTile ? $"{buildindLevelText} {tileObject.BuildingTileObject().GetCurrentBuildingLevel()}" : $"{buildindLevelText} -";



        var color = Colors.GetSelectTilePanelProductionModifierColor(tileObject.GetBaseModifier());
        var haveProduction = haveBuildingTile && tile.IsHaveProductionResources();

        var rarity = tileObject.GetRarity();
        var rarityModifier = _tileObject.CurrentModifier() - _tileObject.GetBaseModifier();

        var productionModifier = rarity == 1 ? $"<color={color}>x{tileObject.GetBaseModifier()}</color>" : $"<color={color}>x{tileObject.GetBaseModifier()}</color> <color={Colors.GetRarityHexColor(rarity)}>+ {rarityModifier}</color>";

        _productionModifierText.text = haveProduction ? $"{productionModifierText} {productionModifier}" : $"{productionModifierText} -";
    }



    private void SetProductionText(TileObject tileObject, Tile tile, bool haveBuildingTile, Building buildings)
    {
        var productionResourceText = $"<color={Colors.HexGreySeven}>{Language.TextStatic[6]}:</color>";

        if (haveBuildingTile && tile.IsHaveProductionResources())
        {
            var isUseRources = _tileObject.BuildingTileObject().GetCurrentBuilding().ResourcesForWork.Length != 0;
            var productionName = $"{Language.TextStatic[tileObject.GetCurrentResourceProduction().NameNumber]}";
            float productionAmount;

            if (isUseRources)
            {
                productionAmount = tileObject.IsHaveRequiredResource() && tileObject.IsBuildingWork()
                    ? (buildings.ResourceExtractedAmount * tileObject.CurrentModifier())
                    : 0;
            }
            else
            {
                productionAmount = tileObject.IsBuildingWork()
                    ? (buildings.ResourceExtractedAmount * tileObject.CurrentModifier())
                    : 0;
            }

            var productionColor = Colors.GetSelectTilePanelProductionColor(productionAmount);
            _productionResourceText.text = $"{productionResourceText} <color={productionColor}>{productionName} {productionAmount}</color>";
        }
        else
        {
            _productionResourceText.text = $"{productionResourceText} -";
        }

    }

    private void SetWorkResourcesText(TileObject tileObject, Tile tile, bool haveBuildingTile)
    {
        string textColor;

        if (haveBuildingTile && tileObject.CurrentResourceForWork() != null && tileObject.IsHaveRequiredResource() &&
            (tile.IsHaveProductionResources() || tile.BuildingTileView == BuildingTileViewEnum.EcologyPurifier))
        {
            textColor = Colors.HexWhite;
        }
        else
        {
            textColor = Colors.HexWarningYellow;
        }


        var resourceForWorkText = $"<color={Colors.HexGreySeven}>{Language.TextStatic[14]}:</color>";
        var haveResourceForWork = haveBuildingTile && (tile.IsHaveProductionResources() || tile.BuildingTileView == BuildingTileViewEnum.EcologyPurifier) && tileObject.CurrentResourceForWork() != null;

        _resourceForWorkText.text = $"{resourceForWorkText} <color={textColor}>{(haveResourceForWork ? $"{Language.TextStatic[tileObject.CurrentResourceForWork().NameNumber]} {tileObject.CurrentResourceForWorkAmount()}" : "-")}</color>";
    }

    private void SetEcologyTexts(TileObject tileObject)
    {
        var rarity = tileObject.GetRarity();
        var groundEcology = tileObject.TileEcology().GetEcology(GetEcologyEnum.Ground);
        var buildingEcology = tileObject.TileEcology().GetEcology(GetEcologyEnum.Building);

        var groundColor = Colors.GetSelectTilePanelEcologyColor(groundEcology);
        var buildingColor = Colors.GetSelectTilePanelEcologyColor(buildingEcology);

        var groundEcologyText = $"<color={Colors.HexGreySeven}>{Language.TextStatic[15]}:</color>";
        var buildingEcologyText = $"<color={Colors.HexGreySeven}>{Language.TextStatic[16]}:</color>";

        var baseGroundEcologytext = $"<color={groundColor}>{groundEcology}</color>";
        var groundText = rarity == 1 ? $"{baseGroundEcologytext}" : $"{baseGroundEcologytext} <color={Colors.GetRarityHexColor(rarity)}>+ {rarity - 1}</color>";
        _groundEcologyText.text = $"{groundEcologyText} {groundText}";
        _buildingEcologyText.text = $"{buildingEcologyText} <color={buildingColor}>{buildingEcology}</color>";
    }

    private void SetButtonStates(bool haveBuildingNow)
    {
        var currentGroundTile = _tileObject.GroundTileObject().CurrentGroundTile();
        var buildingTile = _tileObject.BuildingTileObject();
        var currentBuildingTile = haveBuildingNow ? buildingTile.GetCurrentBuildingTile() : null;

        var isRoad = currentGroundTile.GroundTileView == GroundTileViewEnum.Road;
        var isBase = currentGroundTile.GroundTileView == GroundTileViewEnum.BaseFoundation;
        var isMachineProduction = buildingTile.IsHaveTile() && buildingTile.IsHaveBuildingGameObject() && buildingTile.GetCurrentBuildingTile().BuildingTileView == BuildingTileViewEnum.MachineProduction;
        var isWater = currentGroundTile.IsWater;
        var haveProdictionResources = currentBuildingTile?.IsHaveProductionResources() ?? false;
        var canUpgrade = buildingTile.IsCanUpgrade();
        var isLastRiverTile = _tileObject.GroundTileObject().GetLastRiverTile();
        var canRepair = !_tileObject.BuildingHealth().IsFullHealth();
        var groundHaveBuildings = currentGroundTile.BuildingTypes.Length > 0;
        var isConstructionNow = buildingTile.IsConstructionNow();

        bool canRepairOrUpgrade = !haveBuildingNow || canUpgrade || canRepair;

        var haveRotationViewGround = _tileObject.GroundTileObject().CurrentGroundTileObject().GetComponent<RotationView>() != null;
        var canRotateBuilding = buildingTile.IsHaveTile() && buildingTile.IsHaveBuildingGameObject() && buildingTile.GetCurrentBuilding().CanRotateBuilding;

        var buildButtonState = canRepairOrUpgrade && groundHaveBuildings && !isConstructionNow;
        var workButtonState = haveBuildingNow && (haveProdictionResources || buildingTile.IsEcologyBuilding());
        var generalRepairButtonState = haveBuildingNow && _allSkills.IsSkillOpen(SkillEnum.GeneralRepair);
        var rotateButtonState = (haveRotationViewGround || canRotateBuilding) && !isConstructionNow;
        var machineButtonState = haveBuildingNow && isMachineProduction;
        var destroyButtonState = (isConstructionNow || haveBuildingNow || (!isRoad && (!isWater || isLastRiverTile))) && !isBase;

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
            var damageText = $"<color={Colors.HexGreySeven}>{Language.TextStatic[98]}:</color>";
            var attackSpeedText = $"<color={Colors.HexGreySeven}>{Language.TextStatic[99]}:</color>";
            var attackRadiusText = $"<color={Colors.HexGreySeven}>{Language.TextStatic[100]}:</color>";
            var rotationSpeedText = $"<color={Colors.HexGreySeven}>{Language.TextStatic[101]}:</color>";

            var bonus = _missionHangarSystem.GetAimBotDamageBonus();
            var baseBuildingDamageWithRobotBonus = bonus != 1 ? $"<color={Colors.HexLightGreen}>{building.Damage * bonus}</color>" : building.Damage.ToString();

            var tacticCardIncreaseDamageLevel = _tileObject.BuildingTileObject().GetTacticCardIncreaseDamageLevel();
            var realDamageText = tacticCardIncreaseDamageLevel == 0 ? baseBuildingDamageWithRobotBonus : $"{baseBuildingDamageWithRobotBonus} <color={Colors.HexLightGreen}>+ {_tileObject.BuildingTileObject().GetTacticCardIncreaseDamage()}</color>";

            _damageText.text = $"{damageText} {realDamageText}";
            _attackSpeedText.text = $"{attackSpeedText} {building.AttackSpeed}";
            _attackRadiusText.text = $"{attackRadiusText} {building.AttackRadius}";
            _rotationSpeedText.text = $"{rotationSpeedText} {building.RotationSpeed}";

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
        if (!_buildButton.activeInHierarchy || _tileObject == null || !_tutorialSystem.CanClickBuildButton() || _escapePanel.IsEscapeMode()) return;

        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);

        _uiPanels.TogglePanel(UIPanelsEnum.DestroyPanel, false);
        DestroyButtonChangeColor();

        if (!_tileObject.BuildingTileObject().IsHaveTile() && !_uiPanels.ActiveInHierarchy(UIPanelsEnum.BuildTypesPanel))
        {
            _uiPanels.SetBuildTypesPanelAndLineVisibility(true);
            _buildTypesPanel.SpawnBuildingTypesInScrollView(_tileObject, this);
        }
        else if (_tileObject.BuildingTileObject().IsHaveTile() && !_uiPanels.ActiveInHierarchy(UIPanelsEnum.BuildsPanel))
        {
            _uiPanels.TogglePanel(UIPanelsEnum.BuildsPanel, true);
            _buildsPanel.SpawnUpgradeItemsInScrollView(_tileObject, this);
        }
        CustomEvents.FireTooltipToggle(false, 0);
    }

    public void ToggleBuildingWorkButton()
    {
        if (!_workButton.activeInHierarchy || _tileObject == null || !_tutorialSystem.CanClickBuildingWorkButton() || _escapePanel.IsEscapeMode()) return;
        if (_tileObject.BuildingTileObject().IsEcologyBuilding() && !_tileObject.IsHaveRequiredResource()) return;

        _tutorialSystem.ClickToggleBuildingWork(_tileObject.IsBuildingWork());
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Work], transform.position);

        _tileObject.SetBuildingWork(!_tileObject.IsBuildingWork());
        WorkButtonIconChangeColor();

        CustomEvents.FireChangeEcology(_tileObject.TileEcology().GetEcology(GetEcologyEnum.Total), _tileObject.GetId(), false);

        _tileObject.ChangeResourceProduction();
        CustomEvents.FireChangeResourceForWork(_tileObject, _tileObject.CurrentResourceForWork(), _tileObject.IsBuildingWork() ? _tileObject.CurrentResourceForWorkAmount() : 0, _tileObject.CurrentResourceRecept());
    }

    public void ToggleGeneralRepairButton()
    {
        if (!_tutorialSystem.IsCompleteMissionTutorial() || _escapePanel.IsEscapeMode()) return;

        if (!_generalRepairButton.activeInHierarchy || _tileObject == null) return;
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);

        _tileObject.SetGeneralRepairSelect(!_tileObject.IsGeneralRepairSelect());
        GeneralRepairIconChangeColor();

        CustomEvents.FireChangeGeneralRepairTileObject(_tileObject);
    }

    public void RotateButton()
    {
        if (!_rotateButton.activeInHierarchy || _tileObject == null || _escapePanel.IsEscapeMode()) return;

        CustomEvents.FireToggleCheckTags(false);
        if (_tagsCoroutine != null) StopCoroutine(_tagsCoroutine);

        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Rotate], transform.position);
        var rotationViewGround = _tileObject.GroundTileObject().CurrentGroundTileObject().GetComponent<RotationView>();
        var rotationViewBuilding = _tileObject.BuildingTileObject().IsHaveTile() && _tileObject.BuildingTileObject().IsHaveBuildingGameObject() ? _tileObject.BuildingTileObject().CurrentBuildingGameObject().GetComponent<RotationView>() : null;

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
        if (!_destroyButton.activeInHierarchy || _tileObject == null || !_tutorialSystem.IsCompleteMissionTutorial()) return;
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        _uiPanels.CloseAllBuildsPanels();

        if (!_destroyPanel.gameObject.activeInHierarchy)
        {
            DestroyPanelToggleAndRefreshButtonColor(true);
            _destroyPanel.SetInfo(_tileObject.BuildingTileObject().IsHaveTile(), _tileObject);
            return;
        }

        var groundTileObject = _tileObject.GroundTileObject();

        if (_tileObject.BuildingTileObject().IsHaveTile())
        {
            _tileObject.BuildingHealth().Death();
        }
        else if (!_tileObject.BuildingTileObject().IsHaveTile() && _missionResources.ResourceEnough(ResourceEnum.BeamEnergy, groundTileObject.CurrentGroundTile().GetEnergyBeam()))
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
        if (_destroyPanel.gameObject.activeInHierarchy && _tileObject.BuildingTileObject().IsHaveTile())
        {
            _destroyPanel.SetInfo(_tileObject.BuildingTileObject().IsHaveTile(), _tileObject);
        }
    }

    public void MachinePanelButton()
    {
        if (!_machineButton.activeInHierarchy || _tileObject == null || _escapePanel.IsEscapeMode()) return;
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        _machineSpawnerSystem.SetTileObject(_tileObject);
        _machinePanel.ActiveMacnineItems(_tileObject.BuildingTileObject().GetCurrentBuildingLevel());
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
        if (!_tutorialSystem.IsCompleteMissionTutorial() && _tutorialSystem.GetTutorialStepEnum() < TutorialStepEnum.MissionSettlementChangeResourceRequired_27) return;

        CustomEvents.FireCompleteTutorialStep(TutorialStepEnum.MissionSettlementChangeResourceRequired_27);

        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        var resourcesForWork = _tileObject.BuildingTileObject().GetCurrentBuilding().ResourcesForWork;

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
