using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayEventSystem : MonoBehaviour
{
    [Header("Event Settings")]
    [SerializeField] private TimeTickSystem _timeTickSystem;
    [SerializeField] private GameEventInfo[] _allGameEvents;
    [SerializeField] private GameObject _gameEventPrefab;
    [SerializeField] private RectTransform _container;
    private float _eventMoveDuration;
    private int _dayBeforeSpawnEvent = 3;

    [Header("Other")]
    [SerializeField] private EcologySystem _ecologySystem;

    private void Start()
    {
        CustomEvents.OnDayEnd += OnDayEnd;
        CustomEvents.OnGameEventStart += ActiveGameEvent;

        SetEventDuration();
    }

    private void SetEventDuration()
    {
        _eventMoveDuration = _timeTickSystem.TickSpeed() * _timeTickSystem.EndTime() * _dayBeforeSpawnEvent;
    }

    private void OnDayEnd(int currentDay)
    {
        var rnd = Random.Range(0, 100);

        if (rnd > 60)
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

        // Добавляем отступы
        float offset = 10f; // Настройте по необходимости

        // Определяем позиции с учётом отступов
        Vector2 startPosition = new Vector2(_container.rect.width / 2f + offset, 0f);    // Правый край панели с отступом
        Vector2 endPosition = new Vector2(-_container.rect.width / 2f - offset, 0f);     // Левый край панели с отступом


        // Инициализируем движение
        var gameEventView = prefab.GetComponent<GameEventView>();
        gameEventView.Initialize(info, startPosition, endPosition, _eventMoveDuration);
    }

    public void ActiveGameEvent(GameEventType gameEventType)
    {
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
                break;
            case GameEventType.MeteorStrike:
                break;
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnDayEnd -= OnDayEnd;
        CustomEvents.OnGameEventStart -= ActiveGameEvent;
    }
}
