using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsLayout : MonoBehaviour
{
    [Header("Layout Settings")]
    [SerializeField] private float cardWidth = 120;
    [SerializeField] private float spacing = 7;
    [SerializeField] private int maxCards = 9;
    [SerializeField] private float leftPadding = 84;
    [SerializeField] private float bottomPadding = 92;

    // Коэффициент сглаживания для адаптации скорости анимаций
    [SerializeField] private float smoothingFactor = 0.5f;

    private Dictionary<RectTransform, float> targetPositions = new Dictionary<RectTransform, float>();

    public int MaxCards() => maxCards;

    public void PositionNewCard(CardObject card, int index)
    {
        RectTransform cardRect = card.GetComponent<RectTransform>();
        if (cardRect == null) return;

        float initialY = -bottomPadding;
        float targetY = bottomPadding;

        // Устанавливаем начальную позицию
        cardRect.anchoredPosition = new Vector2(GetCardPosition(index), initialY);

        // Запускаем анимацию поднятия
        StartCoroutine(AnimateYPosition(cardRect, targetY, 0.5f));
    }

    public void RearrangeCards(List<CardObject> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            var card = cards[i];
            if (card == null) continue;

            RectTransform cardRect = card.GetComponent<RectTransform>();
            if (cardRect == null) continue;

            float targetX = GetCardPosition(i);

            // Проверяем, если карта уже имеет целевую позицию и она не изменилась, пропускаем
            if (targetPositions.TryGetValue(cardRect, out float existingTargetX) && Mathf.Approximately(existingTargetX, targetX))
            {
                continue;
            }

            // Обновляем целевую позицию и запускаем анимацию
            targetPositions[cardRect] = targetX;
            StartCoroutine(AnimateXPosition(cardRect, targetX, 0.5f));
        }
    }

    private IEnumerator AnimateYPosition(RectTransform rectTransform, float targetY, float duration)
    {
        if (rectTransform == null) yield break;

        float startY = rectTransform.anchoredPosition.y;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            if (rectTransform == null) yield break;

            // Используем нелинейную функцию для сглаживания timeScale на высоких скоростях
            float adjustedTimeScale = Mathf.Pow(Time.timeScale, 0.5f);
            float adjustedDeltaTime = Mathf.Lerp(Time.unscaledDeltaTime, Time.deltaTime, smoothingFactor * adjustedTimeScale);

            elapsedTime += adjustedDeltaTime;
            float newY = Mathf.Lerp(startY, targetY, elapsedTime / duration);
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, newY);
            yield return null;
        }

        if (rectTransform != null)
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, targetY);
    }

    private IEnumerator AnimateXPosition(RectTransform rectTransform, float targetX, float duration)
    {
        if (rectTransform == null) yield break;

        float startX = rectTransform.anchoredPosition.x;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            if (rectTransform == null) yield break;

            float adjustedTimeScale = Mathf.Pow(Time.timeScale, 0.5f);
            float adjustedDeltaTime = Mathf.Lerp(Time.unscaledDeltaTime, Time.deltaTime, smoothingFactor * adjustedTimeScale);

            elapsedTime += adjustedDeltaTime;
            float newX = Mathf.Lerp(startX, targetX, elapsedTime / duration);
            rectTransform.anchoredPosition = new Vector2(newX, rectTransform.anchoredPosition.y);
            yield return null;
        }

        if (rectTransform != null)
            rectTransform.anchoredPosition = new Vector2(targetX, rectTransform.anchoredPosition.y);
    }

    private float GetCardPosition(int index)
    {
        float startX = leftPadding;
        return startX + index * (cardWidth + spacing);
    }
}
