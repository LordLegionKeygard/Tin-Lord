using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MachineItem : MonoBehaviour
{
    [Inject] private readonly MissionHangarSystem _missionHangarSystem;
    [Inject] private readonly MissionResources _missionResources;
    [SerializeField] private MachineInfo machineInfo;
    [SerializeField] private MachinePanel _machinePanel;
    [SerializeField] private MachineSpawnerSystem _machineSpawnerSystem;
    private bool _isSelect;
    public bool IsSelect() => _isSelect;

    [Header("View")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _backImage;

    [Header("Other")]
    [SerializeField] private CurrentMachineSystem _currentMachineSystem;
    [SerializeField] private ResourcesViewMission _resourcesViewMission;
    private bool _resourcesEnough;

    public int GetRequiredBuildingLevel() => machineInfo.RequiredBuildingLevel;
    public bool CanRepair() => _currentMachineSystem.IsHaveMachine() &&
                                !_currentMachineSystem.IsMachineDeath() &&
                                !_currentMachineSystem.GetMachineHealth().IsFullHealth() &&
                                machineInfo.MachineType == _currentMachineSystem.GetMachineType();


    private void Start()
    {
        UpdateView();
        CustomEvents.OnTimeTick += TimeTickUpdateMachineItem;
        CustomEvents.OnMachineDie += UpdateViewAfterMachineDie;
        CustomEvents.OnMachineTakeDamage += UpdateView;
    }

    private void TimeTickUpdateMachineItem()
    {
        if (!_machinePanel.PanelActive() || !_isSelect) return;

        SetButtonAndTextColor();
        UpdateResourceCells();
    }

    private void UpdateViewAfterMachineDie()
    {
        var time = WorldGameInfo.MachineDieDelay + WorldGameInfo.MachineDieDuration + 0.1f;
        _icon.color = Colors.GreyFive;
        _nameText.color = Colors.GreyEight;
        Invoke(nameof(SetButtonAndTextColor), time);
    }

    public void UpdateView()
    {
        _nameText.text = CanRepair() ? _missionHangarSystem.GetRepairText() : Language.TextStatic[machineInfo.NameLanguageNumber];
        _icon.sprite = machineInfo.MachineSprite;
        if (_isSelect)
        {
            SetButtonAndTextColor();
            UpdateResourceCells();
        }
    }

    public void SelectView()
    {
        if (_currentMachineSystem.IsHaveMachine() && machineInfo.MachineType != _currentMachineSystem.GetMachineType()) return;

        _machinePanel.DeselectAllMachineItems();
        SelectToggleState(true);
        UpdateResourceCells();
    }

    public void UpdateResourceCells()
    {
        if (!_currentMachineSystem.IsHaveMachine() || CanRepair())
        {
            _resourcesViewMission.SetResourcesView(GetResources());
        }
        else _resourcesViewMission.ResetCells();
    }

    public void SelectToggleState(bool state)
    {
        _isSelect = state;
        _machinePanel.UpdateMachineInfo(machineInfo);
        SetButtonAndTextColor();
    }

    public void SetButtonAndTextColor()
    {
        var isDeath = _currentMachineSystem.IsHaveMachine() ? _currentMachineSystem.IsMachineDeath() : false;
        var haveAliveMachine = _currentMachineSystem.IsHaveMachine() && !isDeath;
        var currentMachineDeath = _currentMachineSystem.IsHaveMachine() && isDeath;
        var isCurrentMachineType = machineInfo.MachineType == _currentMachineSystem.GetMachineType();

        _resourcesEnough = _missionResources.ResourcesEnough(GetResources());
        _nameText.color = haveAliveMachine ? isCurrentMachineType ? Colors.LightGreen : Colors.GreyEight : currentMachineDeath && isCurrentMachineType ? Color.red : _resourcesEnough ? _isSelect ? Color.white : Colors.GreyEight : _isSelect ? Colors.WarningYellow : Colors.FadedYellow;
        _icon.color = haveAliveMachine ? isCurrentMachineType && !isDeath ? Color.white : Colors.GreyFive : _isSelect && !currentMachineDeath ? Color.white : Colors.GreyFive;
        _backImage.color = _isSelect && !currentMachineDeath ? Color.white : Colors.GreyEight;
    }

    public ResourceWrapper[] GetResources()
    {
        if (CanRepair())
        {
            var robotHealth = _currentMachineSystem.GetMachineHealth();
            float healthPercentage = (float)(robotHealth.GetMaxHealth() - robotHealth.GetCurrentHealth()) / robotHealth.GetMaxHealth();

            return machineInfo.ResourcesForBuild
                .Select(resource => new ResourceWrapper
                {
                    ResourceEnum = resource.ResourceEnum,
                    RecourceAmount = Mathf.CeilToInt(resource.RecourceAmount * healthPercentage * _missionHangarSystem.GetArbalesterRepairBonus())
                })
                .ToArray();
        }

        return machineInfo.ResourcesForBuild;
    }

    public void CreateOrRepairMachine()
    {
        var canCreateOrRepair = _currentMachineSystem.IsHaveMachine() ? machineInfo.MachineType == _currentMachineSystem.GetMachineType() ? _currentMachineSystem.GetMachineHealth().IsFullHealth() || _currentMachineSystem.GetMachineHealth().IsDeath() ? false : _resourcesEnough : false : _resourcesEnough;

        if (!canCreateOrRepair)
        {
            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Error], transform.position);
            return;
        }

        _resourcesViewMission.ResetCells();
        _missionResources.UseResourcesForBuilding(GetResources());


        if (CanRepair())
        {
            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Repair], transform.position);
            CustomEvents.FireRepairMachine();
            UpdateView();
            _machinePanel.UpdateStatTexts();
        }
        else
        {
            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
            _machineSpawnerSystem.SpawnMachine(machineInfo.MachineType);
            _machinePanel.RefreshAllMachineItemsView();
            _machinePanel.UpdateDestroyButtonState();
            _machinePanel.UpdateStatTexts();
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnTimeTick -= TimeTickUpdateMachineItem;
        CustomEvents.OnMachineDie -= UpdateViewAfterMachineDie;
        CustomEvents.OnMachineTakeDamage -= UpdateView;
    }
}
