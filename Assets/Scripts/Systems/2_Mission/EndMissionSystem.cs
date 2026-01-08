using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EndMissionSystem : MonoBehaviour
{
    [Inject] private readonly MissionModeSystem _missionModeSystem;
    [Inject] private readonly TutorialSystem _tutorialSystem;
    [Inject] private readonly MissionSaveGame _missionSaveGame;
    [Inject] private readonly SpaceSaveGame _spaceSaveGame;
    [Inject] private MissionResources _missionResources;

    [SerializeField] private EndStorySystem _endGameSystem;
    [SerializeField] private ShardsCalculateSystem _shardsCalculateSystem;
    [SerializeField] private EcologySystem _ecologySystem;
    [SerializeField] private GameSpeedSystem _gameSpeedSystem;
    [SerializeField] private UIPanelsMission _uiPanelsMission;
    [SerializeField] private TerminalSystem _terminalSystem;
    [SerializeField] private MissionQuantSystem _missionQuantSystem;

    [Header("View")]
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _headerText;
    [SerializeField] private TextMeshProUGUI _memoryRestoredText;
    [SerializeField] private TextMeshProUGUI _ecologyBonusText;
    [SerializeField] private TextMeshProUGUI _receivedFragmentsText;
    [SerializeField] private TextMeshProUGUI _maxFragmentsText;
    [SerializeField] private TextMeshProUGUI _receivedQuantsText;
    [SerializeField] private Slider _slider;
    private bool _isMissionEnd = false;
    private int _receivedFragments;
    public bool IsMissionEnd() => _isMissionEnd;
    private MissionEndEnum _missionEndEnum;
    public bool _isVictoryBoss = false;


    private void Start()
    {
        CustomEvents.OnMissionEnd += MissionEnd;
    }

    private void MissionEnd(MissionEndEnum missionEndEnum)
    {
        CustomEvents.FireCloseTooltips();
        if (_isMissionEnd) return;

        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.EndMission[(int)missionEndEnum], transform.position);

        _isMissionEnd = true;
        _missionEndEnum = missionEndEnum;

        PrepareEndMission();
        SetMissionEndViewInfo();
        PrepareData();
    }

    private void PrepareEndMission()
    {
        _gameSpeedSystem.ChangeGameSpeed((int)GameSpeedEnum.Default, false);
        StopAllCoroutines();
        _uiPanelsMission.UnactiveAllPanelsAfterEndMission();
        _missionModeSystem.ChangeModeAfterMissionEnd();
    }

    private void SetMissionEndViewInfo()
    {
        var missionEndPercent = _missionEndEnum switch
        {
            MissionEndEnum.Defeat => WorldGameInfo.DefeatFragmentsPercent / 100f,
            MissionEndEnum.Escape => WorldGameInfo.EscapeFragmentsPercent / 100f,
            MissionEndEnum.Victory => WorldGameInfo.VictoryFragmentsPercent / 100f,
            _ => 0f
        };

        var ecologyBonus = GetEcologyBonus();
        var memoryRestoredAmount = (int)_missionResources.GetResourceAmountForEnum(ResourceEnum.DataFragment);
        var totalFragmentsAmount = Mathf.RoundToInt(memoryRestoredAmount * ecologyBonus);
        _receivedFragments = Mathf.RoundToInt(totalFragmentsAmount * missionEndPercent);

        SetTexts(memoryRestoredAmount, ecologyBonus, totalFragmentsAmount);
        _slider.value = 0;
        _panel.SetActive(true);
        StartCoroutine(UpdateFragmentsAndSlider(_receivedFragments, missionEndPercent));
    }

    private void SetTexts(int memoryRestoredAmount, float ecologyBonus, float totalFragmentsAmount)
    {
        var headerTextNumber = _missionEndEnum is MissionEndEnum.Defeat ? 64 : _missionEndEnum is MissionEndEnum.Escape ? 65 : 63;
        var headerTextColor = _missionEndEnum is MissionEndEnum.Defeat ? Color.black : _missionEndEnum is MissionEndEnum.Escape ? Colors.GreyEight : Colors.WarningYellow;

        _headerText.color = headerTextColor;
        _headerText.text = Language.TextStatic[headerTextNumber];

        var memoryRestoredText = $"<color={Colors.HexGreySeven}>{Language.TextStatic[147]}:</color>";
        var ecologyBonusText = $"<color={Colors.HexGreySeven}>{Language.TextStatic[148]}:</color>";
        var receivedQuantsText = $"<color={Colors.HexGreySeven}>{Language.TextStatic[187]}:</color>";

        _memoryRestoredText.text = $"{memoryRestoredText} {memoryRestoredAmount}";
        _ecologyBonusText.text = $"{ecologyBonusText} {ecologyBonus}x";
        _receivedQuantsText.text = _missionEndEnum is MissionEndEnum.Victory ? $"{receivedQuantsText} {_missionQuantSystem.GetQuants()}" : $"{receivedQuantsText} 0";
        _maxFragmentsText.text = totalFragmentsAmount.ToString();
    }

    private float GetEcologyBonus()
    {
        var everyDayEcology = _ecologySystem.GetEveryDayEcology();
        int totalMiddleEcology = 0;

        if (everyDayEcology.Length > 0)
        {
            int sum = 0;
            for (int i = 0; i < everyDayEcology.Length; i++)
            {
                sum += everyDayEcology[i];
            }
            totalMiddleEcology = sum / everyDayEcology.Length;
        }

        if (totalMiddleEcology <= -75) return 0.25f;
        if (totalMiddleEcology <= -50) return 0.5f;
        if (totalMiddleEcology <= -25) return 0.75f;
        if (totalMiddleEcology <= 0) return 1f;
        if (totalMiddleEcology <= 25) return 1.25f;
        if (totalMiddleEcology <= 50) return 1.5f;
        if (totalMiddleEcology <= 75) return 1.75f;

        return 2f;
    }

    private void PrepareData()
    {
        var aiCores = _missionEndEnum == MissionEndEnum.Victory ? 0 : -1;
        var quants = _missionEndEnum == MissionEndEnum.Victory ? _missionQuantSystem.GetQuants() : 0;
        if (_missionEndEnum is MissionEndEnum.Victory or MissionEndEnum.Escape)
        {
            var saveData = _spaceSaveGame.SpaceSaveData;
            var map = saveData.Map;

            int index = map.CurrentNodeIndex;
            if (index >= 0 && index < map.Nodes.Count)
            {
                map.Nodes[index].IsCompleted = true;
            }

            // победа, игра окончена
            if (CurrentMissionInfo.Instance.GetCurrentLandscape().LandscapeEnum == LandscapeEnum.Canyon)
            {
                saveData.EndGame = true;
            }
            else
            {
                CheckChangeAct();
            }

            _spaceSaveGame.GetCommandCenterSaveGameDataWriter().WriteSpaceDataToSaveFile(saveData);
        }

        if (!_tutorialSystem.IsCompleteAllTutorial() || _tutorialSystem.GetTutorialStepEnum() <= TutorialStepEnum.MissionGoodLuckDescription_66)
        {
            if (_missionEndEnum is MissionEndEnum.Victory or MissionEndEnum.Escape)
            {
                _tutorialSystem.SaveTutorial(TutorialStepEnum.SpaceOpenLearningPanel_67);
            }
            else
            {
                _tutorialSystem.SaveTutorial(TutorialStepEnum.SpaceStartMission_8);
            }
        }

        _missionSaveGame.DeleteMissionJson();
        _spaceSaveGame.SaveEndMissionDataToJson(_receivedFragments, aiCores, quants);
    }

    public void EndGameStory()
    {
        var saveData = _spaceSaveGame.SpaceSaveData;

        var shardsForThisAct = _shardsCalculateSystem.CalculateShardsForThisAct();
        saveData.PreviousActsShards += shardsForThisAct;

        _spaceSaveGame.GetCommandCenterSaveGameDataWriter().WriteSpaceDataToSaveFile(saveData);

        LoadCommandCenter();
    }


    private void CheckChangeAct()
    {
        var saveData = _spaceSaveGame.SpaceSaveData;
        var map = saveData.Map;

        int index = map.CurrentNodeIndex;

        _isVictoryBoss = _missionEndEnum == MissionEndEnum.Victory && index >= 0 && index < map.Nodes.Count && map.Nodes[index].NodeType == NodeType.Boss;

        if (_isVictoryBoss)
        {
            // Считаем осколки за ТЕКУЩИЙ акт до того, как обнулим карту
            var shardsForThisAct = _shardsCalculateSystem.CalculateShardsForThisAct();

            // Накапливаем прогресс по прошлым актам
            saveData.PreviousActsShards += shardsForThisAct;

            // Переходим к следующему акту
            saveData.Act += 1;

            // Сбрасываем карту, чтобы при следующем входе в космос сгенерировалась новая под новый акт
            saveData.Map = null;
            saveData.CurrentMission = null;
        }
    }

    private IEnumerator UpdateFragmentsAndSlider(int targetFragments, float targetPercent)
    {
        float duration = 2f;
        float elapsedTime = 0f;
        var receivedFragmentsText = $"<color={Colors.HexGreySeven}>{Language.TextStatic[149]}:</color>";

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float progress = Mathf.Clamp01(elapsedTime / duration);

            int currentFragments = Mathf.RoundToInt(Mathf.Lerp(0, targetFragments, progress));
            float currentSliderValue = Mathf.Lerp(0, targetPercent, progress);

            _receivedFragmentsText.text = $"{receivedFragmentsText} {currentFragments}";
            _slider.value = currentSliderValue;

            yield return null;
        }

        _receivedFragmentsText.text = $"{receivedFragmentsText} {targetFragments}";
        _slider.value = targetPercent;
    }

    public void ContinueButton()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);

        if(_spaceSaveGame.SpaceSaveData.EndGame)
        {
            _endGameSystem.ShowEndGameStory();
            return;
        }

        if (_isVictoryBoss)
        {
            _terminalSystem.ActiveTerminal(_spaceSaveGame.SpaceSaveData.Act - 1);
        }
        else
        {
            LoadCommandCenter();
        }
    }

    public void LoadCommandCenter()
    {
        CustomEvents.FireFade(FadeType.StartFade);
        StartCoroutine(nameof(PrepareLoad));
    }

    private IEnumerator PrepareLoad()
    {
        yield return new WaitForSecondsRealtime(1);
        CustomEvents.FireLoadScene(SceneEnum.Space, WorldGameInfo.LoadSceneTime, null);
    }

    private void OnDestroy()
    {
        CustomEvents.OnMissionEnd -= MissionEnd;
    }
}

public enum MissionEndEnum
{
    Nothing = -1,
    Victory = 0,
    Defeat = 1,
    Escape = 2,
}
