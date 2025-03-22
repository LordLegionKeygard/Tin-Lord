using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MachineItem : MonoBehaviour
{
    [Inject] private PlayerResources _playerResources;
    [SerializeField] private MachineInformation _macniheInformation;
    [SerializeField] private MachinePanel _machinePanel;
    [SerializeField] private MachineSpawnerSystem _machineSpawnerSystem;
    private bool _isSelect;
    public bool IsSelect() => _isSelect;

    [Header("View")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;
    [SerializeField] private Image _backImage;

    [Header("Other")]
    [SerializeField] private CurrentMachineSystem _currentMachineSystem;
    [SerializeField] private WorldResourcesView _worldResourcesView;
    private bool _resourcesEnough;
    public bool CanRepair() => _currentMachineSystem.HaveMachine() &&
                                !_currentMachineSystem.MachineDeath() &&
                                !_currentMachineSystem.MachineHealth().FullHealth() &&
                                _macniheInformation.MachineType == _currentMachineSystem.GetMachineType();


    private void Start()
    {
        UpdateView();
        CustomEvents.OnMachineDie += UpdateViewAfterMachineDie;
        CustomEvents.OnMachineTakeDamage += UpdateView;
    }

    private void UpdateViewAfterMachineDie()
    {
        var time = WorldGameInfo.MachineDieDelay + WorldGameInfo.MachineDieDuration + 0.1f;
        _icon.color = Color.black;
        Invoke(nameof(SetButtonAndTextColor), time);
    }

    public void UpdateView()
    {
        _nameText.text = CanRepair() ? $"{Language.TextStatic[4]} {_macniheInformation.Name[Language.LanguageNumber]}" : _macniheInformation.Name[Language.LanguageNumber];
        _icon.sprite = _macniheInformation.MachineSprite;
        if (_isSelect)
        {
            SetButtonAndTextColor();
            UpdateResourceCells();
        }
    }

    public void SelectView()
    {
        _machinePanel.DeselectAllMachineItems();
        SelectToggleState(true);
        UpdateResourceCells();
    }

    public void UpdateResourceCells()
    {
        if (!_currentMachineSystem.HaveMachine() || CanRepair())
        {
            _worldResourcesView.SetResourcesView(GetResources());
        }
        else _worldResourcesView.ResetCells();
    }

    public void SelectToggleState(bool state)
    {
        _isSelect = state;
        _machinePanel.UpdateMachineInfo(_macniheInformation);
        SetButtonAndTextColor();
    }

    public void SetButtonAndTextColor()
    {
        _resourcesEnough = _playerResources.ResourcesEnough(GetResources());
        _button.enabled = _currentMachineSystem.HaveMachine() ? _macniheInformation.MachineType == _currentMachineSystem.GetMachineType() ? _currentMachineSystem.MachineHealth().FullHealth() || _currentMachineSystem.MachineHealth().IsDeath() ? false : _resourcesEnough : false : _resourcesEnough;
        _nameText.color = _currentMachineSystem.HaveMachine() ? _isSelect ? Color.white : Colors.GreyEight : _resourcesEnough ? _isSelect ? Color.white : Colors.GreyEight : _isSelect ? Colors.WarningYellow : Colors.FadedYellow;
        _icon.color = _currentMachineSystem.HaveMachine() ? _macniheInformation.MachineType == _currentMachineSystem.GetMachineType() && !_currentMachineSystem.MachineHealth().IsDeath() ? Color.white : Color.black : _isSelect ? Color.white : Colors.GreyEight;
        _backImage.color = _isSelect ? Color.white : Colors.GreyEight;
    }

    public ResourceWrapper[] GetResources()
    {
        if (CanRepair())
        {
            var robotHealth = _currentMachineSystem.MachineHealth();
            float healthPercentage = (float)(robotHealth.GetMaxHealth() - robotHealth.GetCurrentHealth()) / robotHealth.GetMaxHealth();

            return _macniheInformation.ResourcesForBuild
                .Select(resource => new ResourceWrapper
                {
                    ResourceEnum = resource.ResourceEnum,
                    RecourceAmount = Mathf.CeilToInt(resource.RecourceAmount * healthPercentage)
                })
                .ToArray();
        }

        return _macniheInformation.ResourcesForBuild;
    }

    public void CreateOrRepairMachine()
    {
        if (!_button.enabled) return;

        _worldResourcesView.ResetCells();
        _playerResources.UseResourcesForBuilding(GetResources());


        if (CanRepair())
        {
            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Repair], transform.position);
            CustomEvents.FireRepairMachine();
            UpdateView();
        }
        else
        {
            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
            _machineSpawnerSystem.SpawnRobot(_macniheInformation.MachineType);
            _machinePanel.RefreshAllMachineItemsView();
            _machinePanel.UpdateDestroyButtonState();
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnMachineDie -= UpdateViewAfterMachineDie;
        CustomEvents.OnMachineTakeDamage -= UpdateView;
    }
}
