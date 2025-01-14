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
        _isMissionEnd = true;

        _gameSpeedSystem.ChangeGameSpeed((int)GameSpeedEnum.Default);
        StopAllCoroutines();
        _uIPanelsWorld.CloseAllPanels();
        _panel.SetActive(true);
        var headerTextNumber = missionEndEnum is MissionEndEnum.Defeat ? 64 : missionEndEnum is MissionEndEnum.Escape ? 65 : 63;
        var headerTextColor = missionEndEnum is MissionEndEnum.Defeat ? Color.red : missionEndEnum is MissionEndEnum.Escape ? Colors.GreyEight : Colors.WarningYellow;
        var allFragmentsAmount = (int)_playerResources.GetResourceNumberForEnum(ResourceEnum.MemoryFragment);
        var percent = (int)missionEndEnum / 100;
        _receivedFragments = allFragmentsAmount * percent;

        _headerText.text = Language.TextStatic[headerTextNumber];
        _headerText.color = headerTextColor;
        _receivedFragmentsText.text = $"{Language.TextStatic[62]} {_receivedFragments}";
        _maxFragmentsText.text = allFragmentsAmount.ToString();
        _slider.value = percent;


        _worldSaveGame.DeleteMissionGameData();
        _commandCenterSaveGame.SaveFragmentsData(_receivedFragments);
    }

    public void CenterButton()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.DefaultClick, transform.position);
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
    Nothing = 0,
    Defeat = 10,
    Escape = 50,
    Victory = 100,
}
