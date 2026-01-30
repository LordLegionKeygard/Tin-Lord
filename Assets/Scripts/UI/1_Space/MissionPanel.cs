using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MissionPanel : MonoBehaviour
{
    [Inject] private SpaceSaveGame _save;
    [Inject] private readonly SpaceSaveGame _spaceSaveGame;
    [Inject] private readonly MissionSaveGame _missionSaveGame;
    [SerializeField] private ActInfo[] _actsInfo;
    [SerializeField] private AiCoreSystem _aiCoreSystem;
    [SerializeField] private RectTransform _descriptionPanel;
    [SerializeField] private TextMeshProUGUI _missionNameHeaderText;
    [SerializeField] private TextMeshProUGUI _ecologyLevelText;
    [SerializeField] private TextMeshProUGUI _objectiveText;
    [SerializeField] private TextMeshProUGUI[] _objectiveTexts;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private GameObject[] _activeObjects;
    [SerializeField] private RectTransform _objectivesRectTransform;
    [SerializeField] private GameObject _loadMissionButton;
    [SerializeField] private GameObject _areYouSurePanel;
    [SerializeField] private TextMeshProUGUI _areYouSureText;
    [SerializeField] private Button _areYouSureYesButton;
    private bool _isContinueMission;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private Image[] _buttonsIcon;
    private MissionNode _currentNode;
    private int _currentNodeId;
    private bool HaveSaveData() => _missionSaveGame.GetWorldGameSaveDataWriter().CheckIfSaveFileExists();

    public void RefreshInfo(MissionNode node, int nodeId)
    {
        CustomEvents.FireRunStepAfterWait(TutorialStepEnum.SpaceStartMission_8);
        _currentNode = node;
        _currentNodeId = nodeId;

        _loadMissionButton.SetActive(HaveSaveData());

        UnactiveAll();

        var descriptionWrapper = _currentNode.Landscape.DescriptionWrappers[Language.LanguageNumber];
        _missionNameHeaderText.text = Language.TextStatic[_currentNode.Landscape.NameLanguageNumber];
        _ecologyLevelText.text = $"{Language.TextStatic[34]}: {_currentNode.Landscape.StartEcology}";
        _objectiveText.text = $"{Language.TextStatic[36]}: ";
        _descriptionText.text = Language.TextStatic[descriptionWrapper.DescriptionLanguageNumber];
        _descriptionPanel.sizeDelta = new Vector2(_descriptionPanel.sizeDelta.x, descriptionWrapper.PanelHeight);

        var objectives = _currentNode.Objective.Objectives;
        for (int i = 0; i < objectives.Length; i++)
        {
            _objectiveTexts[i].gameObject.SetActive(true);
            switch (objectives[i].ObjectiveEnum)
            {
                case ObjectiveEnum.RestoreEcology:
                    _objectiveTexts[i].text = $"{i + 1}. {Language.TextStatic[39]} {objectives[i].ObjectiveAmount}";
                    break;
                case ObjectiveEnum.KillEnemies:
                    _objectiveTexts[i].text = $"{i + 1}. {string.Format(Language.TextStatic[40], objectives[i].ObjectiveAmount)}";
                    break;
                case ObjectiveEnum.ConstructBuilding:
                    _objectiveTexts[i].text = $"{i + 1}. {string.Format(Language.TextStatic[41], objectives[i].ObjectiveAmount)}";
                    break;
                case ObjectiveEnum.SurviveDays:
                    _objectiveTexts[i].text = $"{i + 1}. {string.Format(Language.TextStatic[42], objectives[i].ObjectiveAmount)}";
                    break;
                case ObjectiveEnum.KillBoss:
                    _objectiveTexts[i].text = $"{i + 1}. {Language.TextStatic[151]}";
                    break;
                case ObjectiveEnum.CollectDataFragments:
                    _objectiveTexts[i].text = $"{i + 1}. {Language.TextStatic[226]} {objectives[i].ObjectiveAmount} {Language.TextStatic[175]}";
                    break;
                case ObjectiveEnum.CollectIronIngots:
                    _objectiveTexts[i].text = $"{i + 1}. {Language.TextStatic[226]} {objectives[i].ObjectiveAmount} {Language.TextStatic[163]}";
                    break;
                case ObjectiveEnum.CollectWood:
                    _objectiveTexts[i].text = $"{i + 1}. {Language.TextStatic[226]} {objectives[i].ObjectiveAmount} {Language.TextStatic[153]}";
                    break;
            }
        }

        foreach (var item in _activeObjects)
        {
            item.SetActive(true);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(_objectivesRectTransform);
    }

    private void UnactiveAll()
    {
        foreach (var item in _objectiveTexts)
        {
            item.gameObject.SetActive(false);
        }
    }

    public void StartNewMission()
    {
        CustomEvents.FireCompleteTutorialStep(TutorialStepEnum.SpaceStartMission_8);
        if (HaveSaveData())
        {
            var haveMoreThenOneAicore = _aiCoreSystem.GetAiCores() > 1;

            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);

            _areYouSureYesButton.interactable = haveMoreThenOneAicore;
            _areYouSureText.text = haveMoreThenOneAicore ? Language.TextStatic[48] : Language.TextStatic[86];
            _areYouSurePanel.SetActive(true);
            ButtonsToggle(false);
        }
        else
        {
            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.StartMission, transform.position);
            SaveSelectedMission();
            CustomEvents.FireFade(FadeType.StartFade);
            _isContinueMission = false;
            StartCoroutine(nameof(PrepareLoad));
        }
    }

    public void LoadMission()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.StartMission, transform.position);
        CustomEvents.FireFade(FadeType.StartFade);
        SaveSelectedMission();
        _isContinueMission = true;
        StartCoroutine(nameof(PrepareLoad));
    }

    private IEnumerator PrepareLoad()
    {
        yield return new WaitForSecondsRealtime(1);
        if (_isContinueMission)
        {
            _missionSaveGame.LoadMissionFromJson();
            CustomEvents.FireLoadScene(SceneEnum.Mission, WorldGameInfo.LoadSceneTime, _currentNode.Landscape.LoadingScreenSprite);
        }
        else
        {
            _missionSaveGame.NewMissionData(_currentNode.Landscape, _spaceSaveGame.SpaceSaveData.HangarCommandCenterData.MainResourcesData);
        }
    }

    public void AreYouSureYes()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.StartMission, transform.position);

        _aiCoreSystem.ChangeAiCores(-1);
        _spaceSaveGame.SaveDataToJson();

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

    private void SaveSelectedMission()
    {
        var ccSave = _spaceSaveGame.SpaceSaveData;
        var info = _actsInfo[_save.SpaceSaveData.Act];

        /* ---------- индекс ландшафта ---------- */
        int landId = System.Array.IndexOf(info.Landscapes, _currentNode.Landscape);

        /* ---------- данные из SavedMap для этого узла ---------- */
        var mapNode = ccSave.Map.Nodes[_currentNodeId];
        int deckIdx = mapNode.MissionDeckIndex;

        // если миссия ещё ни разу не открывалась (крайне редкий случай),
        // подстрахуемся, чтобы не получить -1
        if (deckIdx < 0)
        {
            deckIdx = 0;
            Debug.LogWarning("MissionDeckIndex not set yet – fallback to 0");
        }

        /* ---------- конвертация ObjectiveWrapper[] → ObjectiveSave[] ---------- */
        var wrappers = _currentNode.Objective.Objectives;
        var savedObj = new ObjectiveSave[wrappers.Length];

        for (int i = 0; i < wrappers.Length; i++)
        {
            savedObj[i] = new ObjectiveSave
            {
                Objective = wrappers[i].ObjectiveEnum,
                Amount = wrappers[i].ObjectiveAmount
            };
        }

        /* ---------- формируем структуру SelectedMissionData ---------- */
        var sel = new SelectedMissionData
        {
            NodeId = _currentNodeId,
            MissionDeckIndex = deckIdx,
            LandscapeId = landId,
            SavedObjectives = savedObj
        };

        ccSave.CurrentMission = sel;
        _spaceSaveGame.GetCommandCenterSaveGameDataWriter().WriteSpaceDataToSaveFile(ccSave);
    }
}
