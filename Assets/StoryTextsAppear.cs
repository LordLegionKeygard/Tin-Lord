using System.Collections;
using TMPro;
using UnityEngine;

public class StoryTextsAppear : MonoBehaviour
{
    [SerializeField] private TextWrapper[] _storyTexts;
    private float _fadeDuration = 4;
    private float _delayBetweenTexts = 1;
    [SerializeField] private GameObject _continueButton;

    private void Start()
    {
        StartCoroutine(ShowStory());
    }

    private IEnumerator ShowStory()
    {
        yield return new WaitForSeconds(4f);

        foreach (var textWrapper in _storyTexts)
        {
            textWrapper.TextMeshPro.text = Language.TextStatic[textWrapper.TextNumber];

            Color color = textWrapper.TextMeshPro.color;

            float timer = 0f;
            while (timer < _fadeDuration)
            {
                timer += Time.deltaTime;
                float alpha = Mathf.Clamp01(timer / _fadeDuration);
                color.a = alpha;
                textWrapper.TextMeshPro.color = color;
                yield return null;
            }

            yield return new WaitForSeconds(_delayBetweenTexts);
        }

        _continueButton.SetActive(true);
    }
}

[System.Serializable]
public class TextWrapper
{
    public TextMeshProUGUI TextMeshPro;
    public int TextNumber; 
}
