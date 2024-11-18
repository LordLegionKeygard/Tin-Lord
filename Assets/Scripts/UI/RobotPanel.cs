using DG.Tweening;
using TMPro;
using UnityEngine;

public class RobotPanel : MonoBehaviour
{
    [SerializeField] RobotItem[] _robotItems;
    [SerializeField] private RectTransform _objectTransform;
    [SerializeField] private RobotsData _robotData;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private TextMeshProUGUI _experience;
    [SerializeField] private TextMeshProUGUI _durability;
    [SerializeField] private TextMeshProUGUI _meleeDamage;
    [SerializeField] private TextMeshProUGUI _rangeDamage;

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
}
