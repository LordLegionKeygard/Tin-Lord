using System.Collections;
using TMPro;
using UnityEngine;

public class StoryTextTyping : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _storyText;
    private int[] _storyTextIndices = new int[7] { 69, 70, 71, 72, 73, 74, 75 };
    private float _typingSpeed = 0.04f;
    private float _storyDelayBetweenTexts = 3f;

    private void Start()
    {
        StartCoroutine(ShowStory());
    }

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
}
