using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RobotPanel : MonoBehaviour
{
    [SerializeField] RobotItem[] _robotItems;
    [SerializeField] private RectTransform _objectTransform;
    [SerializeField] private RobotsData _robotData;
    [SerializeField] private CurrentRobotSystem _currentRobotSystem;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private TextMeshProUGUI _experience;
    [SerializeField] private TextMeshProUGUI _durability;
    [SerializeField] private TextMeshProUGUI _meleeDamage;
    [SerializeField] private TextMeshProUGUI _rangeDamage;

    [Header("Button")]
    [SerializeField] private Button _destroyButton;
    [SerializeField] private Image _destroyButtonIcon;


    private void Start()
    {
        CustomEvents.OnRobotDie += UpdateDestroyButton;
    }

    public void PanelViewToggle(bool state)
    {
        if (state)
        {
            ShowInfoPanel();
        }
        else
        {
            HideInfoPanel();
        }
    }

    private void ShowInfoPanel()
    {
        _objectTransform.DOAnchorPosX(-250, 0.3f).SetUpdate(true);
    }

    private void HideInfoPanel()
    {
        _objectTransform.DOAnchorPosX(250, 0.3f).SetUpdate(true);
    }

    public void UnselectAllRobots()
    {
        for (int i = 0; i < _robotItems.Length; i++)
        {
            _robotItems[i].SelectToggleState(false);
        }
    }

    public void UpdateTexts(RobotInformation robotInformation)
    {
        var type = robotInformation.RobotType;
        var level = _robotData.GetRobotDataLevel(type);
        _level.text = Language.TextStatic[17] + level.ToString();
        _durability.text = Language.TextStatic[18] + robotInformation.Durability[level].ToString();
        _meleeDamage.text = Language.TextStatic[19] + robotInformation.MeleeDamage[level].ToString();
        _rangeDamage.text = Language.TextStatic[20] + robotInformation.RangeDamage[level].ToString();
    }

    public void DestroyRobbotButton()
    {
        var robotHealth = _currentRobotSystem.RobotHealth();
        robotHealth.CalculateDamage(robotHealth.MaxHealth);
        UpdateDestroyButton();
    }

    public void UpdateDestroyButton()
    {
        var state = _currentRobotSystem.HaveRobot() && !_currentRobotSystem.RobotDeath();
        _destroyButton.interactable = state;
        _destroyButtonIcon.color = new Color(_destroyButtonIcon.color.r, _destroyButtonIcon.color.g, _destroyButtonIcon.color.b, state ? 1 : 0.2f);
    }

    private void OnDestroy()
    {
        CustomEvents.OnRobotDie -= UpdateDestroyButton;
    }
}
