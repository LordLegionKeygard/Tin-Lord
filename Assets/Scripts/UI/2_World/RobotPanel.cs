using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RobotPanel : MonoBehaviour
{
    [SerializeField] RobotItem[] _robotItems;
    [SerializeField] private RectTransform _objectTransform;
    [SerializeField] private CurrentRobotSystem _currentRobotSystem;
    private RobotInformation _currentSelectRobotInfo;
    public RobotType GetCurrentRobotType() => _currentSelectRobotInfo != null ? _currentSelectRobotInfo.RobotType : RobotType.None;
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
        CustomEvents.OnRobotDie += UpdateDestroyButtonState;
    }

    public void PlayerInputRobotItemButton(int number)
    {
        var robotitem = _robotItems[number - 1];

        if(robotitem.IsSelect())
        {
            robotitem.CreateOrRepairRobot();
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
            RefreshAllRobotItemsView();
            UpdateDestroyButtonState();
        }
        else
        {
            _objectTransform.DOAnchorPosX(250, 0.3f).SetUpdate(true);
        }
    }

    public void DeselectAllRobotItems()
    {
        for (int i = 0; i < _robotItems.Length; i++)
        {
            _robotItems[i].SelectToggleState(false);
        }
    }

    public void RefreshAllRobotItemsView()
    {
        for (int i = 0; i < _robotItems.Length; i++)
        {
            _robotItems[i].SetButtonAndTextColor();
        }
    }

    public void UpdateRobotInfo(RobotInformation selectRobotInfo)
    {
        _currentSelectRobotInfo = selectRobotInfo;

        UpdateLevelAndExperience();
        UpdateStatTexts();
    }

    public void UpdateStatTexts()
    {
        if (_currentSelectRobotInfo == null) return;

        var level = RobotsDataWorld.Instance.GetSelectRobotDataLevel(_currentSelectRobotInfo.RobotType);

        _durabilityText.text = $"{Language.TextStatic[18]} {_currentSelectRobotInfo.Durability[level]}";
        _meleeDamageText.text = $"{Language.TextStatic[19]} {_currentSelectRobotInfo.MeleeDamage[level]}";
        _rangeDamageText.text = $"{Language.TextStatic[20]} {_currentSelectRobotInfo.RangeDamage[level]}";
    }

    public void UpdateLevelAndExperience()
    {
        if (_currentSelectRobotInfo == null) return;

        var type = _currentSelectRobotInfo.RobotType;
        var level = RobotsDataWorld.Instance.GetSelectRobotDataLevel(type);
        var maxExp = RobotsDataWorld.Instance.GetSelectRobotMaxExpForLevel(type);
        var currentExp = RobotsDataWorld.Instance.GetSelectRobotExperience(type);

        _levelText.text = $"{level}";
        _expSlider.maxValue = maxExp;
        _expSlider.value = currentExp;
        _experienceText.text = $"{currentExp} / {maxExp}";
    }

    public void DestroyRobbotButton()
    {
        var robotHealth = _currentRobotSystem.RobotHealth();
        robotHealth.CalculateDamage(robotHealth.MaxHealth);
        UpdateDestroyButtonState();
    }

    public void UpdateDestroyButtonState()
    {
        var state = _currentRobotSystem.HaveRobot() && !_currentRobotSystem.RobotDeath();
        _destroyButton.interactable = state;
        _destroyButtonIcon.color = new Color(_destroyButtonIcon.color.r, _destroyButtonIcon.color.g, _destroyButtonIcon.color.b, state ? 1 : 0.2f);
    }

    private void OnDestroy()
    {
        CustomEvents.OnRobotDie -= UpdateDestroyButtonState;
    }
}
