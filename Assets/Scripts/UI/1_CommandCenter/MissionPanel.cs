using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MissionPanel : MonoBehaviour
{
    [Inject] private readonly WorldSaveGame _worldSaveGame;
    [SerializeField] private MissionItem[] _missionItems;
    [SerializeField] private TextMeshProUGUI _ecologyLevelText;
    [SerializeField] private TextMeshProUGUI _startResourcesText;
    [SerializeField] private TextMeshProUGUI _objectiveText;
    [SerializeField] private TextMeshProUGUI[] _objectiveTexts;
    [SerializeField] private GameObject[] _activeObjects;
    [SerializeField] private ResourcesViewCommandCenter _resourcesView;
    [SerializeField] private RectTransform _objectivesRectTransform;
    [SerializeField] private GameObject _loadMissionButton;
    [SerializeField] private GameObject _areYouSurePanel;
    private Mission _currentMission;
    private bool _isContinueMission;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private Image[] _buttonsIcon;
    [SerializeField] private RectTransform _planetTargetTransform;
    private bool HaveSaveData() => _worldSaveGame.GetWorldGameSaveDataWriter().CheckIfSaveFileExists(_currentMission.MissionId.ToString());

    public void LoadLastOpenedMissionId(int lastOpenedMissionId)
    {
        foreach (var item in _missionItems)
        {
            item.SetMissionOpened(lastOpenedMissionId);
        }
    }

    public void RefreshInfo(Mission mission)
    {
        _currentMission = mission;
        _worldSaveGame.ChangeSelectedMissionId(_currentMission.MissionId.ToString());
        _loadMissionButton.SetActive(HaveSaveData());

        UnselectAllMission();
        UnactiveAll();
        UpdatePlanetTargetTransform();

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

    private void UpdatePlanetTargetTransform()
    {
        _planetTargetTransform.anchoredPosition = new Vector2(_currentMission.PlanetTarget.x, _currentMission.PlanetTarget.y);
        _planetTargetTransform.gameObject.SetActive(true);
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

    public void StartNewMission()
    {
        if (HaveSaveData())
        {
            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
            _areYouSurePanel.SetActive(true);
            ButtonsToggle(false);
        }
        else
        {
            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.StartMission, transform.position);
            CustomEvents.FireFade(FadeType.StartFade);
            _isContinueMission = false;
            StartCoroutine(nameof(PrepareLoad));
        }
    }

    public void LoadMission()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.StartMission, transform.position);
        CustomEvents.FireFade(FadeType.StartFade);
        _isContinueMission = true;
        StartCoroutine(nameof(PrepareLoad));
    }

    private IEnumerator PrepareLoad()
    {
        yield return new WaitForSecondsRealtime(1);
        if (_isContinueMission) _worldSaveGame.LoadMissionGameData();
        else _worldSaveGame.NewMission(_currentMission);
    }

    public void AreYouSureYes()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.StartMission, transform.position);
        CustomEvents.FireFade(FadeType.StartFade);
        _isContinueMission = false;
        _loadMissionButton.SetActive(false);
        StartCoroutine(nameof(PrepareLoad));
        _areYouSurePanel.SetActive(false);
        CustomEvents.FireCloseTooltips();
    }

    public void AreYouSureNo()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        _areYouSurePanel.SetActive(false);
        ButtonsToggle(true);
        CustomEvents.FireCloseTooltips();
    }

    private void ButtonsToggle(bool state)
    {
        foreach (var item in _buttons)
        {
            item.interactable = state;
        }

        foreach (var item in _buttonsIcon)
        {
            item.color = state == false ? Colors.GreySix : Color.white;
        }
    }
}
