using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsLayout : MonoBehaviour
{
    [Header("Layout Settings")]
    private float _cardWidth = 120;
    private float _spacing = 7;
    private int _maxCards = 8;
    private float _leftPadding = 68;
    private float _bottomPadding = 90;
    private float _duration = 0.5f;

    private Dictionary<RectTransform, float> targetPositions = new Dictionary<RectTransform, float>();

    public int MaxCards() => _maxCards;

    public void PositionNewCard(CardObject card, int index)
    {
        RectTransform cardRect = card.GetComponent<RectTransform>();
        if (cardRect == null) return;

        float initialY = -_bottomPadding;
        float targetY = _bottomPadding;

        // Устанавливаем начальную позицию
        cardRect.anchoredPosition = new Vector2(GetCardPosition(index), initialY);

        // Запускаем анимацию поднятия
        if(gameObject.activeInHierarchy) StartCoroutine(AnimateYPosition(cardRect, targetY));
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

            if (targetPositions.TryGetValue(cardRect, out float existingTargetX) && Mathf.Approximately(existingTargetX, targetX))
            {
                continue;
            }

            targetPositions[cardRect] = targetX;
            if(gameObject.activeInHierarchy) StartCoroutine(AnimateXPosition(cardRect, targetX));
        }
    }

    private IEnumerator AnimateYPosition(RectTransform rectTransform, float targetY)
    {
        if (rectTransform == null) yield break;

        float startY = rectTransform.anchoredPosition.y;
        float elapsedTime = 0;

        while (elapsedTime < _duration)
        {
            if (rectTransform == null) yield break;

            elapsedTime += Time.unscaledDeltaTime; // Используем только Time.unscaledDeltaTime для независимости от timeScale
            float newY = Mathf.Lerp(startY, targetY, elapsedTime / _duration);
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, newY);
            yield return null;
        }

        if (rectTransform != null)
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, targetY);
    }

    private IEnumerator AnimateXPosition(RectTransform rectTransform, float targetX)
    {
        if (rectTransform == null) yield break;

        float startX = rectTransform.anchoredPosition.x;
        float elapsedTime = 0;

        while (elapsedTime < _duration)
        {
            if (rectTransform == null) yield break;

            elapsedTime += Time.unscaledDeltaTime;
            float newX = Mathf.Lerp(startX, targetX, elapsedTime / _duration);
            rectTransform.anchoredPosition = new Vector2(newX, rectTransform.anchoredPosition.y);
            yield return null;
        }

        if (rectTransform != null)
            rectTransform.anchoredPosition = new Vector2(targetX, rectTransform.anchoredPosition.y);
    }

    private float GetCardPosition(int index)
    {
        float startX = _leftPadding;
        return startX + index * (_cardWidth + _spacing);
    }
}
