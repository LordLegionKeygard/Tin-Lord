using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProloguePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _storyText;
    [SerializeField] private TextMeshProUGUI _consoleText;

    private float _typingSpeed = 0.04f;            // Скорость печати "истории"
    private float _consoleTypingSpeed = 0.02f;     // Скорость печати в "консоли"
    private float _storyDelayBetweenTexts = 3f;    // Пауза между блоками истории
    private float _consoleDelayBetweenTexts = 2f;  // Время мигания курсора перед печатью сообщения

    // Индексы строк для истории
    private int[] _storyTextIndices = new int[7] { 69, 70, 71, 89, 72, 73, 74 };
    // Индексы строк для консоли
    private int[] _consoleTextIndices = new int[14] { 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88 };

    // Хранит несколько последних строк консоли
    private List<string> _consoleMessages = new();

    private void Start()
    {
        StartCoroutine(ShowStory());
        StartCoroutine(UpdateConsoleMessages());
    }

    // ------------------------- 1) "История" -------------------------
    private IEnumerator ShowStory()
    {
        // Небольшая задержка перед историей (примерно 3 секунды)
        yield return new WaitForSeconds(3f);

        foreach (int textIndex in _storyTextIndices)
        {
            string fullText = Language.TextStatic[textIndex];

            // Если уже что-то было, добавляем несколько переводов строки
            if (!string.IsNullOrEmpty(_storyText.text))
            {
                _storyText.text += "\n\n\n";
            }

            string displayedText = _storyText.text;

            // Посимвольно печатаем фрагмент
            foreach (char letter in fullText)
            {
                displayedText += letter;
                _storyText.text = displayedText;
                yield return new WaitForSeconds(_typingSpeed);
            }

            // Задержка перед следующим блоком
            yield return new WaitForSeconds(_storyDelayBetweenTexts);
        }
    }

    // ------------------------- 2) "Консоль" -------------------------
    private IEnumerator UpdateConsoleMessages()
    {
        int textIndex = 0;

        while (textIndex < _consoleTextIndices.Length)
        {
            int currentIndex = _consoleTextIndices[textIndex];
            textIndex++;

            // Берём очередное сообщение из Language.TextStatic
            string message = Language.TextStatic[currentIndex];

            // 1) Удаляем подчёркивание в предыдущей строке, если оно есть
            RemoveUnderscoreFromLastLine();

            // 2) Добавляем новую строку вида "[HH:mm:ss] >_" (курсор для мигания)
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            _consoleMessages.Add($"[{timestamp}] >_");

            LimitConsoleLines();
            UpdateConsoleText();

            // 3) Запускаем мигание подчёркивания заданное время
            yield return StartCoroutine(BlinkCursor(_consoleDelayBetweenTexts));

            // 4) Убираем подчёркивание из этой строки (но не удаляем саму строку, 
            //    оставляем "[HH:mm:ss] >")
            RemoveUnderscoreFromLastLine();

            // 5) Теперь посимвольно «допечатываем» текст сообщения на той же строке
            //    (получаем "[HH:mm:ss] >ВашТекст")
            yield return StartCoroutine(TypeMessageOnLastLine(message));

            // 6) Сохраняем получившуюся строку. (Она уже в _consoleMessages, просто дополнена).
            //    Переходим к следующему сообщению.
        }
    }

    /// <summary>
    /// Удаляет подчёркивание на конце последней строки, если оно есть.
    /// Например, "[20:06:34] >_" превращается в "[20:06:34] >".
    /// </summary>
    private void RemoveUnderscoreFromLastLine()
    {
        if (_consoleMessages.Count == 0)
            return;

        int lastIndex = _consoleMessages.Count - 1;
        string line = _consoleMessages[lastIndex];

        if (line.EndsWith("_"))
        {
            // Убираем только последний символ
            _consoleMessages[lastIndex] = line.Substring(0, line.Length - 1);
            UpdateConsoleText();
        }
    }

    /// <summary>
    /// Корутина мигания подчёркивания в последней строке.
    /// Каждую секунду добавляем/убираем "_", пока не пройдёт duration.
    /// </summary>
    private IEnumerator BlinkCursor(float duration)
    {
        float elapsed = 0f;
        float blinkInterval = 0.5f;

        while (elapsed < duration)
        {
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;

            ToggleCursorInLastLine();
            UpdateConsoleText();
        }
    }

    /// <summary>
    /// Переключает "_": если есть, убираем, если нет, добавляем в последнюю строку.
    /// </summary>
    private void ToggleCursorInLastLine()
    {
        if (_consoleMessages.Count == 0)
            return;

        int lastIndex = _consoleMessages.Count - 1;
        string line = _consoleMessages[lastIndex];

        if (line.EndsWith("_"))
        {
            // убрать подчёркивание
            _consoleMessages[lastIndex] = line.Substring(0, line.Length - 1);
        }
        else
        {
            // добавить подчёркивание
            _consoleMessages[lastIndex] = line + "_";
        }
    }

    /// <summary>
    /// Посимвольно добавляет "message" к последней строке в _consoleMessages.
    /// Предполагается, что она уже имеет вид "[HH:mm:ss] >" без "_".
    /// </summary>
    private IEnumerator TypeMessageOnLastLine(string message)
    {
        if (_consoleMessages.Count == 0)
            yield break;

        int lastIndex = _consoleMessages.Count - 1;
        string currentLine = _consoleMessages[lastIndex];

        // Посимвольно добавляем message
        foreach (char letter in message)
        {
            currentLine += letter;
            _consoleMessages[lastIndex] = currentLine; // записываем обратно
            UpdateConsoleText();

            yield return new WaitForSeconds(_consoleTypingSpeed);
        }
    }

    /// <summary>
    /// Ограничиваем кол-во строк (если нужно хранить не более 5, например).
    /// </summary>
    private void LimitConsoleLines()
    {
        int maxLines = 6;
        while (_consoleMessages.Count > maxLines)
        {
            _consoleMessages.RemoveAt(0);
        }
    }

    /// <summary>
    /// Обновляет текстовое поле _consoleText, собирая строки из _consoleMessages.
    /// </summary>
    private void UpdateConsoleText()
    {
        _consoleText.text = string.Join("\n", _consoleMessages);
    }
}
