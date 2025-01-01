using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionPanel : MonoBehaviour
{
    public int LastOpenedMissionId;
    [SerializeField] private MissionItem[] _missionItems;
    [SerializeField] private TextMeshProUGUI _durationText;
    [SerializeField] private TextMeshProUGUI _ecologyLevelText;
    [SerializeField] private TextMeshProUGUI _startResourcesText;
    [SerializeField] private TextMeshProUGUI _objectiveText;
    [SerializeField] private TextMeshProUGUI[] _objectiveTexts;
    [SerializeField] private GameObject[] _activeObjects;
    [SerializeField] private CommandCenterResourcesView _resourcesView;
    [SerializeField] private RectTransform _objectivesRectTransform;
    [SerializeField] private GameObject _loadMissionButton;
    private Mission _currentMission;


    public void RefreshInfo(Mission mission)
    {
        _currentMission = mission;

        UnselectAllMission();
        UnactiveAll();

        //проверяем есть ли файл сохранения с названием id данной миссии, если да включаем кнопку _loadMissionButton

        _durationText.text = _currentMission.Duration == 0 ? $"{Language.TextStatic[33]} {Language.TextStatic[38]}" : $"{Language.TextStatic[33]} {_currentMission.Duration} {Language.TextStatic[37]}";
        _ecologyLevelText.text = $"{Language.TextStatic[34]} {_currentMission.StartEcology}";
        _startResourcesText.text = Language.TextStatic[35];
        _objectiveText.text = Language.TextStatic[36];

        for (int i = 0; i < _currentMission.Objectives.Length; i++)
        {
            _objectiveTexts[i].gameObject.SetActive(true);
            switch (_currentMission.Objectives[i].ObjectiveEnum)
            {
                case ObjectiveEnum.RestoreEcology:
                    _objectiveTexts[i].text = $"{i + 1}. {Language.TextStatic[39]} {_currentMission.Objectives[i].ObjectiveAmount}";
                    break;
                case ObjectiveEnum.KillEnemies:
                    _objectiveTexts[i].text = $"{i + 1}. {string.Format(Language.TextStatic[40], _currentMission.Objectives[i].ObjectiveAmount)}";
                    break;
                case ObjectiveEnum.ConstructBuilding:
                    _objectiveTexts[i].text = $"{i + 1}. {string.Format(Language.TextStatic[41], _currentMission.Objectives[i].ObjectiveAmount)}";
                    break;
                case ObjectiveEnum.SurviveDays:
                    _objectiveTexts[i].text = $"{i + 1}. {string.Format(Language.TextStatic[42], _currentMission.Objectives[i].ObjectiveAmount)}";
                    break;
            }
        }

        foreach (var item in _activeObjects)
        {
            item.SetActive(true);
        }

        _resourcesView.SetResourcesView(mission.StartResources);

        LayoutRebuilder.ForceRebuildLayoutImmediate(_objectivesRectTransform);
    }

    private void UnactiveAll()
    {
        foreach (var item in _objectiveTexts)
        {
            item.gameObject.SetActive(false);
        }
    }

    private void UnselectAllMission()
    {
        foreach (var item in _missionItems)
        {
            item.SelectToggleView(false);
        }
    }
}
