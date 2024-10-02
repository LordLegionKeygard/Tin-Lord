using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
public class CardHolderSystem : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] private bool _addAllCards;
    [SerializeField] private bool _dontRemoveCards;
    [SerializeField] private CardObject _cardObject;
    [SerializeField] private TileDetector _tileDetector;
    [SerializeField] private Transform _parentTransform;
    [SerializeField] private Tile[] _startCards;
    [SerializeField] private Tile[] _availableCards;

    [Header("Current")]
    [SerializeField] private List<CardObject> _currentCards;
    [SerializeField] private CardObject _currentSelectCardObject;

    [Header("Layout Settings")]
    [SerializeField] private float cardWidth = 120;
    [SerializeField] private float spacing = 7;
    [SerializeField] private int maxCards = 9;
    [SerializeField] private RectTransform _panelRectTransform;
    [SerializeField] private float leftPadding = 84;
    [SerializeField] private float bottomPadding = 92;

    public bool IsHaveCurrentSelectedCardObject() => _currentSelectCardObject != null;
    public Tile CurrentCardHolderSelectedTile() => _currentSelectCardObject.GetTile();
    public bool CheckCurrentCardHolderSelectedTileIsFourTile() => _currentSelectCardObject == null ? false : _currentSelectCardObject.GetTile().IsFourTile;

    private void Awake()
    {
        CustomEvents.OnDayEnd += AddCardsAfterDayEnd;
        CustomEvents.OnSetBase += AddCardAfterSetBase;
    }

    private void Start()
    {
        if (_addAllCards)
        {
            AddNewCards(_availableCards);
            AddNewCards(new Tile[] { _startCards[0] });
        }
        else
        {
            AddNewCards(_startCards);
        }
    }

    public void AddNewCards(Tile[] tiles)
    {
        int cardsToAdd = tiles.Length;
        int cardsToRemove = Mathf.Max(0, _currentCards.Count + cardsToAdd - maxCards);

        // Если есть карты для удаления, ждем их удаления
        if (cardsToRemove > 0)
        {
            RemoveLeftmostCards(cardsToRemove, () =>
            {
                // После завершения удаления добавляем новые карты
                AddCards(tiles);
            });
        }
        else
        {
            // Если удалять карты не нужно, сразу добавляем новые карты
            AddCards(tiles);
        }
    }

    private void AddCards(Tile[] tiles)
    {
        // Добавляем новые карты
        foreach (var tile in tiles)
        {
            var card = Instantiate(_cardObject, transform.position, Quaternion.identity);
            _currentCards.Add(card);
            card.transform.SetParent(_parentTransform, false);
            card.SetCardInfo(tile, this);

            // Рассчитываем корректную позицию для карты по X и Y с учетом отступов
            RectTransform cardRect = card.GetComponent<RectTransform>();
            float initialY = -bottomPadding; // Уровень карты относительно панели с учетом отступа снизу
            float targetY = bottomPadding; // Конечная позиция по Y с учетом отступа снизу

            cardRect.anchoredPosition = new Vector2(GetCardPosition(_currentCards.Count - 1), initialY);

            // Анимация подъема карты на нужную позицию
            cardRect.DOAnchorPosY(targetY, 0.5f).SetEase(Ease.OutBounce);
        }

        // Перераспределяем оставшиеся карты после добавления
        RearrangeCards();
    }

    private void RearrangeCards()
    {
        // Перемещаем все карты на нужные позиции с анимацией
        for (int i = 0; i < _currentCards.Count; i++)
        {
            RectTransform cardRect = _currentCards[i].GetComponent<RectTransform>();
            float targetX = GetCardPosition(i);
            cardRect.DOAnchorPosX(targetX, 0.5f).SetEase(Ease.InOutQuad);
        }
    }

    private float GetCardPosition(int index)
    {
        // Рассчитываем позицию по оси X для карты, начиная с левого края панели и с учетом отступа слева
        float startX = leftPadding; // Левый край панели с учетом отступа
        return startX + index * (cardWidth + spacing); // Позиция для карты с учетом отступа
    }

    private void RemoveLeftmostCards(int count, TweenCallback onComplete)
    {
        List<CardObject> cardsToRemove = new List<CardObject>();

        for (int i = 0; i < count; i++)
        {
            if (_currentCards.Count == 0) return;

            cardsToRemove.Add(_currentCards[0]);
            _currentCards.RemoveAt(0);
        }

        Sequence removeSequence = DOTween.Sequence(); // Создаем последовательность анимации

        foreach (var cardToRemove in cardsToRemove)
        {
            RectTransform cardRect = cardToRemove.GetComponent<RectTransform>();
            removeSequence.Join(cardRect.DOAnchorPosY(300, 0.5f).SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    cardToRemove.transform.DOScaleX(0, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
                    {
                        Destroy(cardToRemove.gameObject);
                    });
                }));
        }

        removeSequence.OnComplete(onComplete); // Вызываем завершение после всех анимаций удаления
    }

    public void RemoveCurrentCard()
    {
        if (_currentSelectCardObject == null || _dontRemoveCards) return;

        _currentCards.Remove(_currentSelectCardObject);
        Destroy(_currentSelectCardObject.gameObject);
        Clear();
        RearrangeCards(); // Обновляем позиции оставшихся карт
    }

    public void SelectCardInCardHolder(CardObject newCardObject)
    {
        _tileDetector.Clear();
        if (_currentSelectCardObject != null) _currentSelectCardObject.CardObjectViewToggle(false);
        _currentSelectCardObject = newCardObject;
    }

    private void AddNewRandomCards(int cards)
    {
        Tile[] randomTiles = new Tile[cards];

        for (int i = 0; i < cards; i++)
        {
            var rndCard = Random.Range(0, _availableCards.Length);
            randomTiles[i] = _availableCards[rndCard];
        }

        AddNewCards(randomTiles);
    }

    private void AddCardAfterSetBase()
    {
        AddNewRandomCards(3);
    }

    private void AddCardsAfterDayEnd(int dayNumber)
    {
        AddNewRandomCards(2);
    }

    private void Clear()
    {
        _currentSelectCardObject = null;
    }

    private void OnDestroy()
    {
        CustomEvents.OnDayEnd -= AddCardsAfterDayEnd;
        CustomEvents.OnSetBase -= AddCardAfterSetBase;
    }
    public void CancelSelectCard(bool isPause)
    {
        if (isPause) return;

        _tileDetector.Clear();
        if (_currentSelectCardObject == null) return;

        _currentSelectCardObject.CardObjectViewToggle(false);
        Clear();
    }
}






