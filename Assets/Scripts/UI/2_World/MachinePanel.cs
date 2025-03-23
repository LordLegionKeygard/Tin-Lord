using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MachinePanel : MonoBehaviour
{
    [SerializeField] MachineItem[] _robotItems;
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
    }

    public void PlayerInputMachineItemButton(int number)
    {
        var robotitem = _robotItems[number - 1];

        if(robotitem.IsSelect())
        {
            robotitem.CreateOrRepairMachine();
        }
        else
        {
            robotitem.SelectView();
        }
    }

    public void PanelViewToggle(bool state)
    {
        _active = state;

        if (state)
        {
            _objectTransform.DOAnchorPosX(-250, 0.3f).SetUpdate(true);
            RefreshAllMachineItemsView();
            UpdateDestroyButtonState();
        }
        else
        {
            _objectTransform.DOAnchorPosX(250, 0.3f).SetUpdate(true);
        }
    }

    public void DeselectAllMachineItems()
    {
        for (int i = 0; i < _robotItems.Length; i++)
        {
            _robotItems[i].SelectToggleState(false);
        }
    }

    public void RefreshAllMachineItemsView()
    {
        for (int i = 0; i < _robotItems.Length; i++)
        {
            _robotItems[i].SetButtonAndTextColor();
        }
    }

    public void UpdateMachineInfo(MachineInformation selectMachineInfo)
    {
        _currentSelectMachineInfo = selectMachineInfo;

        UpdateLevelAndExperience();
        UpdateStatTexts();
    }

    public void UpdateStatTexts()
    {
        if (_currentSelectMachineInfo == null) return;

        var level = MachinesDataWorld.Instance.GetSelectMachineDataLevel(_currentSelectMachineInfo.MachineType);

        _durabilityText.text = $"{Language.TextStatic[18]}: {_currentSelectMachineInfo.GetDurability(level)}";
        _meleeDamageText.text = $"{Language.TextStatic[19]}: {_currentSelectMachineInfo.GetMeleeDamage(level)}";
        _rangeDamageText.text = $"{Language.TextStatic[20]}: {_currentSelectMachineInfo.GetRangeDamage(level)}";
    }

    public void UpdateLevelAndExperience()
    {
        if (_currentSelectMachineInfo == null) return;

        var type = _currentSelectMachineInfo.MachineType;
        var level = MachinesDataWorld.Instance.GetSelectMachineDataLevel(type);
        var maxExp = MachinesDataWorld.Instance.GetSelectMachineMaxExpForLevel(type);
        var currentExp = MachinesDataWorld.Instance.GetSelectMachineExperience(type);

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
        UpdateDestroyButtonState();
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
    }
}
