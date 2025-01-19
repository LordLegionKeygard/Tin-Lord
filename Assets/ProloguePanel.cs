using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProloguePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _storyText;
    [SerializeField] private TextMeshProUGUI _consoleText;
    private float _typingSpeed = 0.04f;
    private float _consoleTypingSpeed = 0.02f;
    private float _storyDelayBetweenTexts = 3f;
    private float _consoleDelayBetweenTexts = 2f;

    private int[] _storyTextIndices = new int[6] { 69, 70, 71, 72, 73, 74 };
    private int[] _consoleTextIndices = new int[14] { 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88 };

    private Queue<string> _consoleMessages = new();

    private void Start()
    {
        StartCoroutine(ShowStory());
        StartCoroutine(UpdateConsoleMessages());
    }

    private IEnumerator ShowStory()
    {
        yield return new WaitForSeconds(3f);

        // Проходим по всем индексам истории
        foreach (int textIndex in _storyTextIndices)
        {
            string fullText = Language.TextStatic[textIndex];

            // Если _storyText уже содержит какой-то текст — добавим перевод строки
            if (!string.IsNullOrEmpty(_storyText.text))
            {
                _storyText.text += "\n\n\n";
            }

            // Запоминаем уже имеющийся текст
            string displayedText = _storyText.text;

            // «Печатаем» новый фрагмент символ за символом
            foreach (char letter in fullText)
            {
                displayedText += letter;
                _storyText.text = displayedText;
                yield return new WaitForSeconds(_typingSpeed);
            }

            // Задержка перед следующим фрагментом
            yield return new WaitForSeconds(_storyDelayBetweenTexts);
        }
    }

    private IEnumerator UpdateConsoleMessages()
    {
        int textIndex = 0;

        while (textIndex < _consoleTextIndices.Length)
        {
            int currentIndex = _consoleTextIndices[textIndex];
            textIndex++;

            string message = Language.TextStatic[currentIndex];

            // 1. Посимвольно набираем сообщение
            yield return StartCoroutine(TypeConsoleMessage(message));

            // 2. После "печати" добавляем сообщение в очередь
            if (_consoleMessages.Count >= 5)
            {
                _consoleMessages.Dequeue();
            }
            _consoleMessages.Enqueue(message);

            // Обновляем консоль
            UpdateConsoleText();

            // Пауза перед следующим сообщением
            yield return new WaitForSeconds(_consoleDelayBetweenTexts);
        }
    }

    private IEnumerator TypeConsoleMessage(string message)
    {
        // Отобразим уже «зафиксированные» сообщения
        _consoleText.text = string.Join("\n", _consoleMessages);

        if (_consoleMessages.Count > 0)
            _consoleText.text += "\n";

        string displayedText = _consoleText.text;

        // Печать посимвольно
        foreach (char letter in message)
        {
            displayedText += letter;
            _consoleText.text = displayedText;
            yield return new WaitForSeconds(_consoleTypingSpeed);
        }
    }

    private void UpdateConsoleText()
    {
        _consoleText.text = string.Join("\n", _consoleMessages);
    }
}
