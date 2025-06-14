using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MissionPanel : MonoBehaviour
{
    [Inject] private readonly CommandCenterSaveGame _commandCenterSaveGame;
    [Inject] private readonly WorldSaveGame _worldSaveGame;
    [SerializeField] private AllMissionsInfo _allMissionsInfo;
    [SerializeField] private TextMeshProUGUI _missionNameHeaderText;
    [SerializeField] private TextMeshProUGUI _ecologyLevelText;
    [SerializeField] private TextMeshProUGUI _startResourcesText;
    [SerializeField] private TextMeshProUGUI _objectiveText;
    [SerializeField] private TextMeshProUGUI[] _objectiveTexts;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private GameObject[] _activeObjects;
    [SerializeField] private ResourcesViewCommandCenter _resourcesView;
    [SerializeField] private RectTransform _objectivesRectTransform;
    [SerializeField] private GameObject _loadMissionButton;
    [SerializeField] private GameObject _areYouSurePanel;
    private bool _isContinueMission;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private Image[] _buttonsIcon;
    private MissionNode _currentNode;
    private int _currentNodeId;
    private bool HaveSaveData() => _worldSaveGame.GetWorldGameSaveDataWriter().CheckIfSaveFileExists(_currentNodeId.ToString());

    public void RefreshInfo(MissionNode node, int nodeId)
    {
        _currentNode = node;
        _currentNodeId = nodeId;

        _worldSaveGame.ChangeNodeId(_currentNodeId.ToString());
        _loadMissionButton.SetActive(HaveSaveData());

        UnactiveAll();

        _missionNameHeaderText.text = _currentNode.Landscape.Name[Language.LanguageNumber];
        _ecologyLevelText.text = $"{Language.TextStatic[34]}: {_currentNode.Landscape.StartEcology}";
        _startResourcesText.text = $"{Language.TextStatic[35]}: ";
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

        _resourcesView.SetResourcesView(_currentNode.Landscape.StartResources);

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
        else _worldSaveGame.NewMission(_currentNode.Landscape);
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
        var info = _allMissionsInfo;

        int landId = System.Array.IndexOf(info.Landscapes, _currentNode.Landscape);
        int objId = System.Array.IndexOf(info.Objectives, _currentNode.Objective);
        int spId = System.Array.IndexOf(info.EnemiesSpawnerInformation, _currentNode.EnemiesSpawner);

        var sel = new SelectedMissionData();
        sel.NodeId = _currentNodeId;
        sel.LandscapeId = landId;
        sel.ObjectiveId = objId;
        sel.SpawnerId = spId;

        var ccSave = _commandCenterSaveGame.CommandCenterSaveData;
        ccSave.CurrentMission = sel;

        _commandCenterSaveGame.GetCommandCenterSaveGameDataWriter().WriteCommandCenterDataToSaveFile(ccSave);
    }
}
