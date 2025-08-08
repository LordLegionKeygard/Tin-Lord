using TMPro;
using UnityEngine;
using Zenject;

public class TimeTickSystem : MonoBehaviour
{
    [Inject] private readonly TilesSystem _tilesSystem;
    [Inject] private readonly EndMissionSystem _endMissionSystem;
    [SerializeField] private AllSkills _allSkills;
    [SerializeField] private int _currentTick;
    [SerializeField] private CellsView _timeView;
    [SerializeField] private TextMeshProUGUI _dayText;
    [SerializeField] private EveryTickResourcesProduction _everyTickResourcesExtraction;
    [SerializeField] private EveryTickResourcesRequired _everyTickResourcesRequired;
    [SerializeField] private GameSpeedSystem _gameSpeedSystem;
    [SerializeField] private float _currentTime = 0f;
    private int _currentDay = 0;
    public float GetEndTime() => WorldGameInfo.OneDayTicksCount + 1;
    public int GetCurrentTick() => _currentTick;
    public int GetCurrentDay() => _currentDay;

    public void LoadTime(int day, int tick)
    {
        _currentDay = day;
        _currentTick = tick;
        UpdateDayText();
        _timeView.UpdateCellSlotsView(_currentTick);
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
            CustomEvents.FireTimeTick();

            if (_currentTick >= GetEndTime())
            {
                _currentDay++;
                _currentTick = 0;
                UpdateDayText();
                CustomEvents.FireDayEnd(_currentDay);
                CustomEvents.FireObjectiveAmountChange(ObjectiveEnum.SurviveDays, _currentDay);
            }

            _timeView.UpdateCellSlotsView(_currentTick);
        }
    }

    private void UpdateResourcesAfterTick()
    {
        _everyTickResourcesRequired.UseEveryTickRequiredResources();
        _everyTickResourcesExtraction.AddEveryTickResources();
    }

    private void UpdateDayText()
    {
        _dayText.text = $"{_currentDay:D3}";
    }
}
