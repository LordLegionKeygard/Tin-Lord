using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MissionPanel : MonoBehaviour
{
    [Inject] private readonly CommandCenterSaveGame _commandCenterSaveGame;
    [Inject] private readonly WorldSaveGame _worldSaveGame;
    [SerializeField] private AllNodesInfo _allMissionsInfo;
    [SerializeField] private TextMeshProUGUI _missionNameHeaderText;
    [SerializeField] private TextMeshProUGUI _ecologyLevelText;
    [SerializeField] private TextMeshProUGUI _objectiveText;
    [SerializeField] private TextMeshProUGUI[] _objectiveTexts;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private GameObject[] _activeObjects;
    [SerializeField] private RectTransform _objectivesRectTransform;
    [SerializeField] private GameObject _loadMissionButton;
    [SerializeField] private GameObject _areYouSurePanel;
    private bool _isContinueMission;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private Image[] _buttonsIcon;
    private MissionNode _currentNode;
    private int _currentNodeId;
    private bool HaveSaveData() => _worldSaveGame.GetWorldGameSaveDataWriter().CheckIfSaveFileExists();

    public void RefreshInfo(MissionNode node, int nodeId)
    {
        _currentNode = node;
        _currentNodeId = nodeId;

        _loadMissionButton.SetActive(HaveSaveData());

        UnactiveAll();

        _missionNameHeaderText.text = _currentNode.Landscape.Name[Language.LanguageNumber];
        _ecologyLevelText.text = $"{Language.TextStatic[34]}: {_currentNode.Landscape.StartEcology}";
        _objectiveText.text = $"{Language.TextStatic[36]}: ";
        _descriptionText.text = _currentNode.Landscape.Description[Language.LanguageNumber];

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
        if (HaveSaveData())
        {
            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
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
        if (_isContinueMission) _worldSaveGame.LoadMissionGameData(_currentNode.Landscape.LoadingScreenSprite);
        else _worldSaveGame.NewMission(_currentNode.Landscape, _commandCenterSaveGame.CommandCenterSaveData.MainResourcesData);
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

    private void SaveSelectedMission()
    {
        var ccSave = _commandCenterSaveGame.CommandCenterSaveData;
        var info = _allMissionsInfo;

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

        /* ---------- сохраняем ---------- */
        ccSave.CurrentMission = sel;
        _commandCenterSaveGame
            .GetCommandCenterSaveGameDataWriter()
            .WriteCommandCenterDataToSaveFile(ccSave);
    }
}
