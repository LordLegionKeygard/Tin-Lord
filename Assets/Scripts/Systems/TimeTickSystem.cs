using TMPro;
using UnityEngine;
using Zenject;

public class TimeTickSystem : MonoBehaviour
{
    [Inject] private readonly TilesSystem _tilesSystem;
    [Inject] private readonly EndMissionSystem _endMissionSystem;
    [SerializeField] private AllSkills _allSkills;
    [SerializeField] private int _currentTick;
    [SerializeField] private TimeView _timeView;
    [SerializeField] private TextMeshProUGUI _dayText;
    [SerializeField] private EveryTickResourcesProduction _everyTickResourcesExtraction;
    [SerializeField] private EveryTickResourcesRequired _everyTickResourcesRequired;
    [SerializeField] private GameSpeedSystem _gameSpeedSystem;
    [SerializeField] private float _currentTime = 0f;
    private readonly float _endTime = 25;
    private int _currentDay = 0;
    public float GetEndTime() => _endTime;
    public int GetCurrentTick() => _currentTick;
    public int GetCurrentDay() => _currentDay;

    public void LoadTime(int day, int tick)
    {
        _currentDay = day;
        _currentTick = tick;
        UpdateDayText();
        _timeView.UpdateTimeSlotsView(_currentTick);
    }

    private void Start()
    {
        UpdateDayText();
    }

    private void Update()
    {
        if (_gameSpeedSystem.IsPause() || !_tilesSystem.IsHaveBase() || _endMissionSystem.IsMissionEnd()) return;

        _currentTime += Time.deltaTime;

        if (_currentTime >= WorldGameInfo.TickSpeed)
        {
            _currentTime = 0;
            _currentTick++;
            UpdateResourcesAfterTick();
            _allSkills.TimeTickAllSkill();

            if (_currentTick >= _endTime)
            {
                _currentDay++;
                _currentTick = 0;
                UpdateDayText();
                CustomEvents.FireObjectiveAmountChange(ObjectiveEnum.SurviveDays, _currentDay);
                CustomEvents.FireDayEnd(_currentDay);
            }

            _timeView.UpdateTimeSlotsView(_currentTick);
        }
    }

    private void UpdateResourcesAfterTick()
    {
        _everyTickResourcesRequired.UseEveryTickRequiredResources();
        _everyTickResourcesExtraction.AddEveryTickResources();
        CustomEvents.FireTimeTick();
    }

    private void UpdateDayText()
    {
        _dayText.text = $"{_currentDay:D3}";
    }
}
