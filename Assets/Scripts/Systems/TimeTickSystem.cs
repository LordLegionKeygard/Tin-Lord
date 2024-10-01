using TMPro;
using UnityEngine;

public class TimeTickSystem : MonoBehaviour
{
    [SerializeField] private float _tickSpeed;
    [SerializeField] private int _currentTick;
    [SerializeField] private TimeView _timeView;
    [SerializeField] private TilesSystem _tilesSystem;
    [SerializeField] private TextMeshProUGUI _dayText;
    [SerializeField] private EveryTickResourcesProduction _everyTickResourcesExtraction;
    [SerializeField] private EveryTickResourcesRequired _everyTickResourcesRequired;
    [SerializeField] private EnemiesSpawnerSystem _enemiesSpawnerSystem;
    private float _endTime = 25;
    private int _currentDay = 0;
    [SerializeField] private float _currentTime = 0f;
    private bool _isPaused = false;

    private void Awake()
    {
        CustomEvents.OnPauseChanged += TogglePause;
    }

    private void Start()
    {
        UpdateDayText();
    }

    private void Update()
    {
        if (_isPaused || !_tilesSystem.IsHaveBase()) return;

        _currentTime += Time.deltaTime;

        if (_currentTime >= _tickSpeed)
        {
            _currentTime = 0;
            _currentTick++;
            UpdateResourcesAfterTick();

            if (_currentTick >= _endTime)
            {
                _currentDay++;
                _currentTick = 0;
                UpdateDayText();
                CustomEvents.FireDayEnd(_currentDay);
            }

            _timeView.UpdateTimeSlotsView(_currentTick);
        }
    }

    private void UpdateResourcesAfterTick()
    {
        _everyTickResourcesExtraction.AddEveryTickResources();
        _everyTickResourcesRequired.UseEveryTickRequiredResources();
        CustomEvents.FireTickAfterResourcesChanged();
    }

    private void UpdateDayText()
    {
        _dayText.text = $"{Language.TextStatic[12]} {_currentDay}";
    }

    public void TogglePause(bool isPause)
    {
        _isPaused = isPause;
    }

    private void OnDestroy()
    {
        CustomEvents.OnPauseChanged -= TogglePause;
    }
}
