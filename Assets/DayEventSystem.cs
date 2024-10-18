using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayEventSystem : MonoBehaviour
{
    [Header("Event Settings")]
    [SerializeField] private GameEventInfo[] _allGameEvents;          // Массив доступных событий
    [SerializeField] private GameObject _gameEventPrefab;         // Префаб EventIcon
    [SerializeField] private RectTransform _container;           // Панель, где будут размещаться иконки
    [SerializeField] private float eventMoveDuration = 25f;       // Продолжительность движения иконки (например, один день)

    private void Start()
    {
        CustomEvents.OnDayEnd += OnDayEnd;
        CustomEvents.OnGameEventStart += ActiveGameEvent;
    }

    private void OnDayEnd(int currentDay)
    {
        SpawnRandomEvent();
    }

    private void SpawnRandomEvent()
    {
        // Выбираем случайное событие из массива
        int rnd = Random.Range(0, _allGameEvents.Length);
        var info = _allGameEvents[rnd];

        // Создаём экземпляр префаба EventIcon
        var prefab = Instantiate(_gameEventPrefab, _container);

        // Определяем позиции
        Vector2 startPosition = new Vector2(_container.rect.width / 2f, 0f);    // Правый край панели
        Vector2 endPosition = new Vector2(-_container.rect.width / 2f, 0f);     // Левый край панели

        // Инициализируем движение
        var gameEventView = prefab.GetComponent<GameEventView>();
        gameEventView.Initialize(info, startPosition, endPosition, eventMoveDuration);
    }

    public void ActiveGameEvent(GameEventType gameEventType)
    {
        Debug.Log($"Произошло событие: {gameEventType}!");
    }

    private void OnDestroy()
    {
        CustomEvents.OnDayEnd -= OnDayEnd;
        CustomEvents.OnGameEventStart -= ActiveGameEvent;
    }
}
