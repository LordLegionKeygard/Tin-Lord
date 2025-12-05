using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _objectiveText;
    [SerializeField] private Image _icon;
    [SerializeField] private Sprite[] _sprites;
    private ObjectiveWrapper _objectiveWrapper;

    public void SetupItem(ObjectiveWrapper objectiveWrapper, int currentAmount, bool complete)
    {
        _objectiveWrapper = objectiveWrapper;
        UpdateText(currentAmount, complete);
    }

    public void UpdateText(int currentAmount, bool isComplete)
    {
        switch (_objectiveWrapper.ObjectiveEnum)
        {
            case ObjectiveEnum.RestoreEcology:
                _objectiveText.text = $"{string.Format(Language.TextStatic[58], currentAmount, _objectiveWrapper.ObjectiveAmount)}";
                break;
            case ObjectiveEnum.KillEnemies:
                _objectiveText.text = $"{string.Format(Language.TextStatic[59], currentAmount, _objectiveWrapper.ObjectiveAmount)}";
                break;
            case ObjectiveEnum.ConstructBuilding:
                _objectiveText.text = $"{string.Format(Language.TextStatic[60], currentAmount, _objectiveWrapper.ObjectiveAmount)}";
                break;
            case ObjectiveEnum.SurviveDays:
                _objectiveText.text = $"{string.Format(Language.TextStatic[61], currentAmount, _objectiveWrapper.ObjectiveAmount)}";
                break;
            case ObjectiveEnum.KillBoss:
                _objectiveText.text = $"{string.Format(Language.TextStatic[150], currentAmount, _objectiveWrapper.ObjectiveAmount)}";
                break;
            case ObjectiveEnum.CollectDataFragments:
                _objectiveText.text = $"{Language.TextStatic[226]} {currentAmount}/{_objectiveWrapper.ObjectiveAmount} {Language.TextStatic[175]}";
                break;
            case ObjectiveEnum.CollectIronIngots:
                _objectiveText.text = $"{Language.TextStatic[226]} {currentAmount}/{_objectiveWrapper.ObjectiveAmount} {Language.TextStatic[163]}";
                break;
            case ObjectiveEnum.CollectWood:
                _objectiveText.text = $"{Language.TextStatic[226]} {currentAmount}/{_objectiveWrapper.ObjectiveAmount} {Language.TextStatic[153]}";
                break;
        }
        _objectiveText.color = isComplete ? Colors.LightGreen : Color.white;
        _icon.sprite = _sprites[isComplete ? 1 : 0];
    }
}
