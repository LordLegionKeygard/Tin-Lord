using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardsLayout : MonoBehaviour
{
    [Header("Layout Settings")]
    [SerializeField] private float cardWidth = 120;
    [SerializeField] private float spacing = 7;
    [SerializeField] private int maxCards = 9;
    [SerializeField] private float leftPadding = 84;
    [SerializeField] private float bottomPadding = 92;
    private RectTransform _panelRectTransform;
    public int MaxCards() => maxCards;

    private void Awake()
    {
        _panelRectTransform = GetComponent<RectTransform>();
    }

    public void PositionNewCard(CardObject card, int index)
    {
        RectTransform cardRect = card.GetComponent<RectTransform>();
        float initialY = -bottomPadding; // начальная позиция по Y
        float targetY = bottomPadding; // целевая позиция по Y

        cardRect.anchoredPosition = new Vector2(GetCardPosition(index), initialY);

        // Анимация появления карты
        cardRect.DOAnchorPosY(targetY, 0.5f).SetEase(Ease.OutBounce);
    }

    public void RearrangeCards(List<CardObject> cards)
    {
        // Перемещаем все карты на нужные позиции с анимацией
        for (int i = 0; i < cards.Count; i++)
        {
            RectTransform cardRect = cards[i].GetComponent<RectTransform>();
            float targetX = GetCardPosition(i);
            cardRect.DOAnchorPosX(targetX, 0.5f).SetEase(Ease.InOutQuad);
        }
    }

    private float GetCardPosition(int index)
    {
        float startX = leftPadding; // начинаем с левого края
        return startX + index * (cardWidth + spacing); // позиция для каждой карты
    }
}

