using System.Collections;
using TMPro;
using UnityEngine;

public class StoryTextTyping : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _storyText;
    private int[] _storyTextIndexes;
    private float _typingSpeed = 0.04f;
    private float _storyDelayBetweenTexts = 3f;
    [SerializeField] private GameObject _continueButton;

    public void StartTyping(int[] indexes)
    {
        _storyTextIndexes = indexes;
        StartCoroutine(ShowStory());
    }

    private IEnumerator ShowStory()
    {
        // Небольшая задержка перед историей (примерно 3 секунды)
        yield return new WaitForSeconds(3f);

        foreach (int textIndex in _storyTextIndexes)
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
        
        _continueButton.SetActive(true);
    }
}
