using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EndMissionSystem : MonoBehaviour
{
    [Inject] private readonly TutorialSystem _tutorialSystem;
    [Inject] private readonly MissionSaveGame _missionSaveGame;
    [Inject] private readonly SpaceSaveGame _spaceSaveGame;
    [Inject] private MissionResources _missionResources;

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
    [SerializeField] private TextMeshProUGUI _quantsText;
    [SerializeField] private Slider _slider;
    private bool _isMissionEnd = false;
    private int _receivedFragments;
    public bool IsMissionEnd() => _isMissionEnd;
    private MissionEndEnum _missionEndEnum;


    private void Start()
    {
        CustomEvents.OnMissionEnd += MissionEnd;
    }

    private void MissionEnd(MissionEndEnum missionEndEnum)
    {
        if (_isMissionEnd) return;
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.EndMission[(int)missionEndEnum], transform.position);
        PrepareEndMission();
        SetMissionEndViewInfo(missionEndEnum);
        PrepareData(missionEndEnum);
    }

    private void PrepareEndMission()
    {
        _isMissionEnd = true;
        _gameSpeedSystem.ChangeGameSpeed((int)GameSpeedEnum.Default);
        StopAllCoroutines();
        _uiPanelsMission.CloseAllPanels();
    }

    private void SetMissionEndViewInfo(MissionEndEnum missionEndEnum)
    {
        _missionEndEnum = missionEndEnum;

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
        _memoryRestoredText.text = $"{Language.TextStatic[147]} {memoryRestoredAmount}";
        _ecologyBonusText.text = $"{Language.TextStatic[148]} {ecologyBonus}x";

        _maxFragmentsText.text = totalFragmentsAmount.ToString();

        _quantsText.text = _missionEndEnum is MissionEndEnum.Victory ? $"{Language.TextStatic[187]} {_missionQuantSystem.GetQuants()}" : $"{Language.TextStatic[187]} 0";
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

    private void PrepareData(MissionEndEnum missionEndEnum)
    {
        var aiCores = missionEndEnum == MissionEndEnum.Victory ? 0 : -1;
        var quants = missionEndEnum == MissionEndEnum.Victory ? _missionQuantSystem.GetQuants() : 0;
        if (missionEndEnum is MissionEndEnum.Victory or MissionEndEnum.Escape)
        {
            var saveData = _spaceSaveGame.SpaceSaveData;
            var map = saveData.Map;

            int curIdx = map.CurrentNodeIndex;
            if (curIdx >= 0 && curIdx < map.Nodes.Count)
            {
                map.Nodes[curIdx].IsCompleted = true;
            }

            _spaceSaveGame.GetCommandCenterSaveGameDataWriter().WriteCommandCenterDataToSaveFile(saveData);
        }

        _tutorialSystem.SaveTutorial(TutorialStepEnum.SpaceOpenLearningPanel_64);
        _missionSaveGame.DeleteMissionJson();
        _spaceSaveGame.SaveEndMissionDataToJson(_receivedFragments, aiCores, quants);
    }

    private IEnumerator UpdateFragmentsAndSlider(int targetFragments, float targetPercent)
    {
        float duration = 2f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float progress = Mathf.Clamp01(elapsedTime / duration);

            int currentFragments = Mathf.RoundToInt(Mathf.Lerp(0, targetFragments, progress));
            float currentSliderValue = Mathf.Lerp(0, targetPercent, progress);

            _receivedFragmentsText.text = $"{Language.TextStatic[62]} {currentFragments}";
            _slider.value = currentSliderValue;

            yield return null;
        }

        _receivedFragmentsText.text = $"{Language.TextStatic[62]} {targetFragments}";
        _slider.value = targetPercent;
    }

    public void ContinueButton()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);

        // if (_missionEndEnum == MissionEndEnum.Victory)
        // {
        //     _terminalSystem.ActiveTerminal();
        // }
        // else
        // {
        LoadCommandCenter();
        // }
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
