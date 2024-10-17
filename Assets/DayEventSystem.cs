using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayEventSystem : MonoBehaviour
{
    [Header("System References")]
    [SerializeField] private TilesSystem _tilesSystem;         // Ссылка на систему плиток
    [SerializeField] private TimeTickSystem _timeTickSystem;   // Ссылка на систему времени

    [Header("Timeline Settings")]
    public GameObject tickPrefab;        // Префаб рисочки с опциональным текстом номера дня
    public RectTransform tickContainer;  // Контейнер RectTransform для рисочек
    public int numberOfTicks = 16;       // Общее количество рисочек на шкале
    public int ticksPerDay = 3;          // Количество рисочек между каждым днем

    private List<TickData> ticks = new List<TickData>();      // Список данных для каждой рисочки
    private float panelWidth;                                 // Ширина контейнера для рисочек
    private float totalTimeForOneDay;                         // Общее время, составляющее один день
    private float timeBetweenTicks;                           // Время между двумя соседними рисочками
    private List<float> tickTimes = new List<float>();        // Список времен для каждой рисочки

    private void Start()
    {
        // Проверяем, назначен ли tickContainer
        if (tickContainer == null)
        {
            return;
        }

        // Получаем ширину панели из RectTransform
        panelWidth = tickContainer.rect.width;

        // Расчитываем общее время одного дня на основе TimeTickSystem
        totalTimeForOneDay = _timeTickSystem.EndTime() * _timeTickSystem.TickSpeed();

        // Расстояние по времени между рисочками: ticksPerDay рисочек между днями, плюс 1 интервал для дня
        timeBetweenTicks = totalTimeForOneDay / (ticksPerDay + 1);

        // Общее время, отображаемое на шкале
        float totalTimeToShow = (numberOfTicks - 1) * timeBetweenTicks;

        // Инициализируем рисочки
        for (int i = 0; i < numberOfTicks; i++)
        {
            // Время для каждой рисочки
            float tickTime = i * timeBetweenTicks;
            tickTimes.Add(tickTime);

            // Создаем экземпляр префаба рисочки как дочерний объект tickContainer
            GameObject tickObj = Instantiate(tickPrefab, tickContainer);
            RectTransform tickRect = tickObj.GetComponent<RectTransform>();

            if (tickRect == null)
            {
                continue;
            }

            // Получаем компонент TextMeshProUGUI для отображения номера дня
            TextMeshProUGUI dayText = tickObj.GetComponentInChildren<TextMeshProUGUI>();
            bool isDayTick = (i % (ticksPerDay + 1) == 0); // Каждая (ticksPerDay +1)-я рисочка отображает номер дня
            if (dayText != null)
            {
                dayText.gameObject.SetActive(isDayTick);
                if (isDayTick)
                {
                    dayText.text = "0"; // Инициализируем с днем 0
                }
            }

            // Добавляем данные рисочки в список
            ticks.Add(new TickData(tickRect, dayText));
        }
    }

    private void Update()
    {
        // Проверяем доступность систем и не находится ли игра на паузе
        if (_timeTickSystem == null || _timeTickSystem.IsPause() || !_tilesSystem.IsHaveBase())
            return;

        // Получаем текущее игровое время
        float currentGameTime = _timeTickSystem.GetTotalGameTime();

        // Общее время, отображаемое на шкале
        float totalTimeToShow = (numberOfTicks - 1) * timeBetweenTicks;

        // Обновляем позиции рисочек
        for (int i = 0; i < ticks.Count; i++)
        {
            TickData tickData = ticks[i];
            float tickTime = tickTimes[i];

            // Вычисляем разницу во времени между рисочкой и текущим временем
            float timeDifference = tickTime - currentGameTime;

            // Если рисочка ушла за левый край шкалы, перемещаем её вправо
            if (timeDifference < -timeBetweenTicks)
            {
                tickTime += totalTimeToShow + timeBetweenTicks;
                tickTimes[i] = tickTime;
                timeDifference = tickTime - currentGameTime;
            }

            // Нормализуем разницу во времени в диапазон от 0 до 1
            float normalizedTime = timeDifference / totalTimeToShow;

            // Ограничиваем нормализованное время между 0 и 1
            normalizedTime = Mathf.Clamp01(normalizedTime);

            // Вычисляем позицию по X: от -panelWidth/2 (левый край) до +panelWidth/2 (правый край)
            float positionX = normalizedTime * panelWidth - panelWidth / 2f;

            // Обновляем позицию рисочки
            tickData.TickTransform.anchoredPosition = new Vector2(positionX, 0);

            // Обновляем номер дня, если это рисочка с номером дня
            if (tickData.DayText != null && tickData.DayText.gameObject.activeSelf)
            {
                int dayNumber = Mathf.FloorToInt(tickTime / totalTimeForOneDay);
                dayNumber = Mathf.Max(dayNumber, 0); // Убираем отрицательные числа
                tickData.DayText.text = dayNumber.ToString();
            }
        }
    }
}

// Класс для хранения данных о рисочке
public class TickData
{
    public RectTransform TickTransform;
    public TextMeshProUGUI DayText;

    public TickData(RectTransform tickTransform, TextMeshProUGUI dayText)
    {
        TickTransform = tickTransform;
        DayText = dayText;
    }
}
