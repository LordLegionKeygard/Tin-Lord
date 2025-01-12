using System.Collections.Generic;
using UnityEngine;

public class DayEventSystem : MonoBehaviour
{
    [Header("Event Settings")]
    [SerializeField] private TimeTickSystem _timeTickSystem;
    [SerializeField] private GameEventInfo[] _allGameEvents;
    [SerializeField] private GameObject _gameEventPrefab;
    [SerializeField] private RectTransform _container;
    private float _fullDuration;
    private int _dayBeforeSpawnEvent = 3;
    private readonly float _offset = 10;
    private int _eventNumber;
    private List<DayEventForListData> _currentEventsData = new();

    [Header("Other")]
    [SerializeField] private EcologySystem _ecologySystem;
    [SerializeField] private EarthquakeGameEvent _earthquakeEvent;
    [SerializeField] private AcidRainGameEvent _acidRainEvent;
    [SerializeField] private MeteorStrikeGameEvent _meteorStrikeEvent;

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

    private void SetEventDuration()
    {
        _fullDuration = _timeTickSystem.GetTickSpeed() * _timeTickSystem.GetEndTime() * _dayBeforeSpawnEvent;
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
        int rnd = Random.Range(0, _allGameEvents.Length);
        var info = _allGameEvents[rnd];

        // Создаём экземпляр префаба EventIcon
        var prefab = Instantiate(_gameEventPrefab, _container);

        // Определяем позиции с учётом отступов
        Vector2 startPosition = new(_container.rect.width / 2f + _offset, 0f); // Правый край панели с отступом
        Vector2 endPosition = new(-_container.rect.width / 2f - _offset, 0f); // Левый край панели с отступом

        // Инициализируем движение
        var gameEventView = prefab.GetComponent<GameEventView>();
        gameEventView.Initialize(info, startPosition, endPosition, _fullDuration, 0, _eventNumber);

        AddEventToList(info, prefab);
    }

    public void LoadEvents(DayEventData[] dayEventsData)
    {
        for (int i = 0; i < dayEventsData.Length; i++)
        {
            var info = _allGameEvents[dayEventsData[i].GameEventTypeNumber];
            var prefab = Instantiate(_gameEventPrefab, _container);
            Vector2 startPosition = new(_container.rect.width / 2f + _offset, 0f);
            Vector2 endPosition = new(-_container.rect.width / 2f - _offset, 0f);
            var gameEventView = prefab.GetComponent<GameEventView>();
            gameEventView.Initialize(info, startPosition, endPosition, _fullDuration, dayEventsData[i].AlreadyElapsedTime, _eventNumber);

            AddEventToList(info, prefab);
        }
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
