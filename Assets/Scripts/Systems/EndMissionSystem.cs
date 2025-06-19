using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EndMissionSystem : MonoBehaviour
{
    [Inject] private readonly WorldSaveGame _worldSaveGame;
    [Inject] private readonly CommandCenterSaveGame _commandCenterSaveGame;
    [Inject] private MissionResources _missionResources;

    [SerializeField] private EcologySystem _ecologySystem;
    [SerializeField] private GameSpeedSystem _gameSpeedSystem;
    [SerializeField] private UIPanelsWorld _uIPanelsWorld;
    [SerializeField] private TerminalSystem _terminalSystem;

    [Header("View")]
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _headerText;

    [SerializeField] private TextMeshProUGUI _memoryRestoredText;
    [SerializeField] private TextMeshProUGUI _ecologyBonusText;
    [SerializeField] private TextMeshProUGUI _receivedFragmentsText;

    [SerializeField] private TextMeshProUGUI _maxFragmentsText;
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
        _uIPanelsWorld.CloseAllPanels();
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
        var memoryRestoredAmount = (int)_missionResources.GetResourceAmountForEnum(ResourceEnum.MemoryFragment);
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
    }

    private float GetEcologyBonus()
    {
        int ecology = _ecologySystem.GetTotalEcology();

        if (ecology <= -75) return 0.25f;
        if (ecology <= -50) return 0.5f;
        if (ecology <= -25) return 0.75f;
        if (ecology <= 0) return 1f;
        if (ecology <= 25) return 1.25f;
        if (ecology <= 50) return 1.5f;
        if (ecology <= 75) return 1.75f;

        return 2f;
    }


    private void PrepareData(MissionEndEnum missionEndEnum)
    {
        var aiCores = missionEndEnum == MissionEndEnum.Victory ? 0 : -2;
        if (missionEndEnum == MissionEndEnum.Victory)
        {
            var ccSave = _commandCenterSaveGame.CommandCenterSaveData;
            var map = ccSave.Map;

            int curIdx = map.CurrentNodeIndex;
            if (curIdx >= 0 && curIdx < map.Nodes.Count)
            {
                map.Nodes[curIdx].IsCompleted = true;
            }

            // сразу сохраняем изменения карты
            _commandCenterSaveGame.GetCommandCenterSaveGameDataWriter().WriteCommandCenterDataToSaveFile(ccSave);
        }

        _worldSaveGame.DeleteMissionGameData();
        _commandCenterSaveGame.SaveCommandCenterFragmentsAiCoresData(_receivedFragments, aiCores);
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
        CustomEvents.FireLoadScene(SceneEnum.CommandCenter, WorldGameInfo.LoadSceneTime, true, null);
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
