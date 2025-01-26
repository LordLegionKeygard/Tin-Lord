using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EndMissionSystem : MonoBehaviour
{
    [Inject] private readonly WorldSaveGame _worldSaveGame;
    [Inject] private readonly CommandCenterSaveGame _commandCenterSaveGame;
    [Inject] private PlayerResources _playerResources;
    [SerializeField] private GameSpeedSystem _gameSpeedSystem;
    [SerializeField] private UIPanelsWorld _uIPanelsWorld;

    [Header("View")]
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _headerText;
    [SerializeField] private TextMeshProUGUI _receivedFragmentsText;
    [SerializeField] private TextMeshProUGUI _maxFragmentsText;
    [SerializeField] private Slider _slider;
    private bool _isMissionEnd = false;
    private int _receivedFragments;
    public bool IsMissionEnd() => _isMissionEnd;


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
        var percent = missionEndEnum switch
        {
            MissionEndEnum.Defeat => WorldGameInfo.DefeatFragmentsPercent / 100,
            MissionEndEnum.Escape => WorldGameInfo.EscapeFragmentsPercent / 100,
            MissionEndEnum.Victory => WorldGameInfo.VictoryFragmentsPercent / 100,
            _ => 0f
        };

        var headerTextNumber = missionEndEnum is MissionEndEnum.Defeat ? 64 : missionEndEnum is MissionEndEnum.Escape ? 65 : 63;
        var headerTextColor = missionEndEnum is MissionEndEnum.Defeat ? Color.red : missionEndEnum is MissionEndEnum.Escape ? Colors.GreyEight : Colors.WarningYellow;
        var allFragmentsAmount = (int)_playerResources.GetResourceNumberForEnum(ResourceEnum.MemoryFragment);
        _receivedFragments = Mathf.RoundToInt(allFragmentsAmount * percent);

        _headerText.text = Language.TextStatic[headerTextNumber];
        _headerText.color = headerTextColor;
        _receivedFragmentsText.text = $"{Language.TextStatic[62]} 0";
        _maxFragmentsText.text = allFragmentsAmount.ToString();
        _slider.value = 0;

        _panel.SetActive(true);

        StartCoroutine(UpdateFragmentsAndSlider(allFragmentsAmount, _receivedFragments, percent));
    }

    private void PrepareData(MissionEndEnum missionEndEnum)
    {
        _worldSaveGame.DeleteMissionGameData();
        _commandCenterSaveGame.SaveCommandCenterWorldData(_receivedFragments, CurrentMissionInfo.Instance.GetLastOpenedMissionId(missionEndEnum));
    }

    private IEnumerator UpdateFragmentsAndSlider(int allFragmentsAmount, int targetFragments, float targetPercent)
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
        CustomEvents.FireFade(FadeType.StartFade);
        StartCoroutine(nameof(PrepareLoad));
    }

    private IEnumerator PrepareLoad()
    {
        yield return new WaitForSecondsRealtime(1);
        CustomEvents.FireLoadScene(SceneEnum.CommandCenter, WorldGameInfo.LoadSceneTime, true);
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
