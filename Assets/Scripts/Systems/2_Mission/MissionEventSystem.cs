using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MissionEventSystem : MonoBehaviour
{
    [Inject] private readonly MissionHangarSystem _missionHangarSystem;

    [Header("UI")]
    [SerializeField] private InfoMissionSystem _infoMissionSystem;

    [Header("Event Settings")]
    [SerializeField] private EcologySystem _ecologySystem;
    [SerializeField] private TimeTickSystem _timeTickSystem;
    [SerializeField] private GameObject _gameEventPrefab;
    [SerializeField] private RectTransform _container;
    [SerializeField] private GameEventInfo[] _allEvents;
    private int _dayBeforeSpawnEvent = 3;
    private GameEventInfo[] _availableEvents;
    private float _fullDuration;
    private readonly float _offset = 10;
    private int _eventNumber;
    private List<DayEventForListData> _currentEventsData = new();

    [Header("Other")]
    [SerializeField] private EarthquakeMissionEvent _earthquakeEvent;
    [SerializeField] private AcidRainMissionEvent _acidRainEvent;
    [SerializeField] private MeteorStrikeMissionEvent _meteorStrikeEvent;
    [SerializeField] private ToxicGasMissionEvent _toxicGasEvent;
    [SerializeField] private OilReleaseMissionEvent _oilReleaseMissionEvent;

    public DayEventData[] GetAllCurrentEvents()
    {
        var data = new DayEventData[_currentEventsData.Count];

        for (int i = 0; i < _currentEventsData.Count; i++)
        {
            data[i] = new DayEventData
            {
                GameEventTypeNumber = (int)_currentEventsData[i].GameEventType,
                AlreadyElapsedTime = _currentEventsData[i].ViewObject.GetComponent<GameEventView>().GetAlreadyElapsedTime(),
            };
        }

        return data;
    }

    private void Start()
    {
        CustomEvents.OnDayEnd += OnDayEnd;
        CustomEvents.OnGameEventStart += ActiveGameEvent;

        SetEventDuration();
    }

    private void SetAvailableMissionEvents()
    {
        _availableEvents = CurrentMissionInfo.Instance.GetCurrentLandscape().MissionEvents;
    }

    private void SetEventDuration()
    {
        _fullDuration = WorldGameInfo.TickSpeed * _timeTickSystem.GetEndTime() * _dayBeforeSpawnEvent;
    }

    private void OnDayEnd(int currentDay)
    {
        var rnd = Random.Range(0, 100);

        if (rnd < WorldGameInfo.DayEventChance)
        {
            SpawnRandomEvent();
        }
    }

    private void SpawnRandomEvent()
    {
        // Выбираем случайное событие из массива
        int rnd = Random.Range(0, _availableEvents.Length);
        var gameEventInfo = _availableEvents[rnd];

        // Создаём экземпляр префаба EventIcon
        var prefab = Instantiate(_gameEventPrefab, _container);

        // Определяем позиции с учётом отступов
        Vector2 startPosition = new(_container.rect.width / 2f + _offset, 0f); // Правый край панели с отступом
        Vector2 endPosition = new(-_container.rect.width / 2f - _offset, 0f); // Левый край панели с отступом

        // Инициализируем движение
        var gameEventView = prefab.GetComponent<GameEventView>();
        gameEventView.Initialize(gameEventInfo, startPosition, endPosition, _fullDuration, 0, _eventNumber);

        AddEventToList(gameEventInfo, prefab);
        StartCoroutine(WarningBeforeStartCoroutine(gameEventInfo, 0));
    }

    public void LoadEvents(DayEventData[] dayEventsData, bool IsStartMission)
    {
        SetAvailableMissionEvents();
        if (IsStartMission) return;

        float oneDaySeconds = WorldGameInfo.TickSpeed * _timeTickSystem.GetEndTime();

        for (int i = 0; i < dayEventsData.Length; i++)
        {

            var data = dayEventsData[i];
            var gameEventInfo = _allEvents[dayEventsData[i].GameEventTypeNumber];
            var prefab = Instantiate(_gameEventPrefab, _container);
            var gameEventView = prefab.GetComponent<GameEventView>();

            Vector2 startPosition = new(_container.rect.width / 2f + _offset, 0f);
            Vector2 endPosition = new(-_container.rect.width / 2f - _offset, 0f);
            gameEventView.Initialize(gameEventInfo, startPosition, endPosition, _fullDuration, dayEventsData[i].AlreadyElapsedTime, _eventNumber);

            AddEventToList(gameEventInfo, prefab);

            float timeLeft = _fullDuration - data.AlreadyElapsedTime;
            if (timeLeft >= oneDaySeconds)
            {
                StartCoroutine(WarningBeforeStartCoroutine(gameEventInfo, data.AlreadyElapsedTime));
            }
        }
    }

    private IEnumerator WarningBeforeStartCoroutine(GameEventInfo gameEventInfo, float alreadyElapsed)
    {
        float oneDayDuration = WorldGameInfo.TickSpeed * _timeTickSystem.GetEndTime();
        float delay = oneDayDuration * (_dayBeforeSpawnEvent - 1) - alreadyElapsed;

        if (delay > 0) yield return new WaitForSeconds(delay);

        string infoText = Language.TextStatic[gameEventInfo.InfoNumber];
        if (!string.IsNullOrEmpty(infoText)) _infoMissionSystem.ShowInfo(infoText, _missionHangarSystem.GetCurrentRobot(), gameEventInfo.IsWarning);
    }

    public void ActiveGameEvent(GameEventType gameEventType, int eventNumber)
    {
        RemoveEventFromList(eventNumber);
        switch (gameEventType)
        {
            case GameEventType.RadiationIncrease:
                _ecologySystem.ChangeRadiation(3);
                break;
            case GameEventType.RadiationIncreaseMedium:
                _ecologySystem.ChangeRadiation(6);
                break;
            case GameEventType.RadiationIncreaseStrong:
                _ecologySystem.ChangeRadiation(9);
                break;
            case GameEventType.RadiationDecrease:
                _ecologySystem.ChangeRadiation(-3);
                break;
            case GameEventType.RadiationDecreaseMedium:
                _ecologySystem.ChangeRadiation(-6);
                break;
            case GameEventType.RadiationDecreaseStrong:
                _ecologySystem.ChangeRadiation(-9);
                break;
            case GameEventType.AcidRain:
                _acidRainEvent.StartEvent();
                break;
            case GameEventType.MeteorStrike:
                _meteorStrikeEvent.StartEvent();
                break;
            case GameEventType.Earthquake:
                _earthquakeEvent.StartEvent();
                break;
            case GameEventType.ToxicGas:
                _toxicGasEvent.StartEvent();
                break;
            case GameEventType.OilSwamp:
                _oilReleaseMissionEvent.StartEvent();
                break;
        }
    }

    private void AddEventToList(GameEventInfo info, GameObject prefab)
    {
        _currentEventsData.Add(new DayEventForListData
        {
            GameEventType = info.GameEventType,
            EventNumber = _eventNumber,
            ViewObject = prefab,
        });

        _eventNumber++;
    }

    private void RemoveEventFromList(int eventNumber)
    {
        var eventToRemove = _currentEventsData.Find(el => el.EventNumber == eventNumber);
        if (eventToRemove != null) _currentEventsData.Remove(eventToRemove);
    }

    private void OnDestroy()
    {
        CustomEvents.OnDayEnd -= OnDayEnd;
        CustomEvents.OnGameEventStart -= ActiveGameEvent;
    }
}

[System.Serializable]
public class DayEventForListData
{
    public GameEventType GameEventType;
    public int EventNumber;
    public GameObject ViewObject;
}
