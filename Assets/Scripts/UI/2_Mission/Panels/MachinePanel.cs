using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MachinePanel : MonoBehaviour
{
    [SerializeField] MachineItem[] _machineItems;
    [SerializeField] private RectTransform _objectTransform;
    [SerializeField] private CurrentMachineSystem _currentMachineSystem;
    private MachineInformation _currentSelectMachineInfo;
    public MachineType GetCurrentMachineType() => _currentSelectMachineInfo != null ? _currentSelectMachineInfo.MachineType : MachineType.None;
    private bool _active;
    public bool PanelActive() => _active;

    [Header("View")]
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _experienceText;
    [SerializeField] private TextMeshProUGUI _durabilityText;
    [SerializeField] private TextMeshProUGUI _meleeDamageText;
    [SerializeField] private TextMeshProUGUI _rangeDamageText;
    [SerializeField] private Slider _expSlider;

    [Header("Button")]
    [SerializeField] private Button _destroyButton;
    [SerializeField] private Image _destroyButtonIcon;


    private void Start()
    {
        CustomEvents.OnMachineDie += UpdateDestroyButtonState;
        CustomEvents.OnDestroyMachineProductionBuilding += ClosePanelAfterDestroyMachineProductionBuilding;
        CustomEvents.OnMachineTakeDamage += UpdateStatTexts;
    }

    private void ClosePanelAfterDestroyMachineProductionBuilding()
    {
        if (_active) PanelViewToggle(false);
    }

    public void PlayerInputMachineItemButton(int number)
    {
        if (number > _machineItems.Length - 1) return;

        var machineItem = _machineItems[number - 1];

        if (!machineItem.gameObject.activeInHierarchy) return;

        if (machineItem.IsSelect())
        {
            machineItem.CreateOrRepairMachine();
        }
        else
        {
            machineItem.SelectView();
        }
    }

    public void PanelViewToggle(bool state)
    {
        CustomEvents.FireTooltipToggle(false, 0);
        _active = state;

        if (state)
        {
            _objectTransform.DOAnchorPosX(-250, 0.3f).SetUpdate(true);
            RefreshAllMachineItemsView();
            UpdateDestroyButtonState();

            foreach (var item in _machineItems)
            {
                item.UpdateView();
            }

            var currentMachineType = _currentMachineSystem.GetMachineType();
            if (currentMachineType != MachineType.None) _machineItems[(int)currentMachineType].SelectView();
        }
        else
        {
            _objectTransform.DOAnchorPosX(250, 0.3f).SetUpdate(true);
        }
    }

    public void DeselectAllMachineItems()
    {
        for (int i = 0; i < _machineItems.Length; i++)
        {
            _machineItems[i].SelectToggleState(false);
        }
    }

    public void RefreshAllMachineItemsView()
    {
        for (int i = 0; i < _machineItems.Length; i++)
        {
            _machineItems[i].SetButtonAndTextColor();
        }
    }

    public void ActiveMacnineItems(int buildingLevel)
    {
        for (int i = 0; i < _machineItems.Length; i++)
        {
            _machineItems[i].gameObject.SetActive(_machineItems[i].GetRequiredBuildingLevel() <= buildingLevel);
        }
    }

    public void UpdateMachineInfo(MachineInformation machineInfo)
    {
        if (machineInfo == null) return;

        _currentSelectMachineInfo = machineInfo;

        UpdateLevelAndExperience();
        UpdateStatTexts();
    }

    public void UpdateStatTexts()
    {
        if (_currentSelectMachineInfo == null) return;

        var level = MachinesDataMission.Instance.GetCurrentLevel();
        var haveAliveMachine = _currentMachineSystem.IsHaveMachine() && !_currentMachineSystem.IsMachineDeath();
        var currentMachineHealth = _currentMachineSystem.GetMachineHealth() != null ? _currentMachineSystem.GetMachineHealth().GetCurrentHealth() : 0;

        var durability = haveAliveMachine ? $"{currentMachineHealth} / {_currentSelectMachineInfo.GetDurability(level)}" : "-";

        var _meleeDamage = haveAliveMachine ? $"{_currentSelectMachineInfo.GetMeleeDamage(level)}" : "-";
        var rangeDamage = haveAliveMachine ? $"{_currentSelectMachineInfo.GetRangeDamage(level)}" : "-";

        _durabilityText.text = $"{Language.TextStatic[18]}: {durability}";
        _meleeDamageText.text = $"{Language.TextStatic[19]}: {_meleeDamage}";
        _rangeDamageText.text = $"{Language.TextStatic[20]}: {rangeDamage}";
    }

    public void UpdateLevelAndExperience()
    {
        if (_currentSelectMachineInfo == null) return;

        var level = MachinesDataMission.Instance.GetCurrentLevel();
        var maxExp = MachinesDataMission.Instance.GetMachineMaxExpForLevel();
        var currentExp = MachinesDataMission.Instance.GetMachineExperience();

        _levelText.text = $"{level}";
        _expSlider.maxValue = maxExp;
        _expSlider.value = currentExp;
        _experienceText.text = $"{currentExp} / {maxExp}";
    }

    public void DestroyMachineButton()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        var robotHealth = _currentMachineSystem.GetMachineHealth();
        robotHealth.CalculateDamage(robotHealth.GetMaxHealth());
        DeselectAllMachineItems();
        UpdateStatTexts();
    }

    public void UpdateDestroyButtonState()
    {
        var state = _currentMachineSystem.IsHaveMachine() && !_currentMachineSystem.IsMachineDeath();
        _destroyButton.interactable = state;
        _destroyButtonIcon.color = new Color(_destroyButtonIcon.color.r, _destroyButtonIcon.color.g, _destroyButtonIcon.color.b, state ? 1 : 0.2f);
    }

    private void OnDestroy()
    {
        CustomEvents.OnMachineDie -= UpdateDestroyButtonState;
        CustomEvents.OnDestroyMachineProductionBuilding -= ClosePanelAfterDestroyMachineProductionBuilding;
        CustomEvents.OnMachineTakeDamage -= UpdateStatTexts;
    }
}
