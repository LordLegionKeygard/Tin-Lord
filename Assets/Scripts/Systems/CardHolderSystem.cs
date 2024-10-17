using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardHolderSystem : MonoBehaviour
{
    [Header("Test")]
    [SerializeField] private bool _addAllCards;
    [SerializeField] private bool _dontRemoveCards;

    [Header("Base")]
    [SerializeField] private CardObject _cardObject;
    [SerializeField] private TileDetector _tileDetector;
    [SerializeField] private CardsLayout _cardsLayout;

    [Header("Cards")]
    [SerializeField] private Tile[] _startCards;
    [SerializeField] private Tile[] _availableCards;
    [SerializeField] private List<CardObject> _currentCards;
    [SerializeField] private CardObject _currentSelectCardObject;


    public bool IsHaveCurrentSelectedCardObject() => _currentSelectCardObject != null;
    public Tile CurrentCardHolderSelectedTile() => _currentSelectCardObject.GetTile();
    public bool CheckCurrentCardHolderSelectedTileIsFourTile() => _currentSelectCardObject == null ? false : _currentSelectCardObject.GetTile().IsFourTile;

    private void Awake()
    {
        CustomEvents.OnDayEnd += AddCardsAfterDayEnd;
        CustomEvents.OnSetBase += AddCardAfterSetBase;
        CustomEvents.OnSpawnRoadComplete += AddStartGameCards;
    }

    private void AddStartGameCards()
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

    public void CancelSelectCard()
    {
        if (_currentSelectCardObject == null) return;

        _currentSelectCardObject.CardObjectViewToggle(false);
        ClearCardHolderSystem();
    }

    public void AddNewCards(Tile[] tiles)
    {
        int cardsToAdd = tiles.Length;
        int cardsToRemove = Mathf.Max(0, _currentCards.Count + cardsToAdd - _cardsLayout.MaxCards());

        if (cardsToRemove > 0)
        {
            RemoveFirstCards(cardsToRemove, () =>
            {
                AddCards(tiles);
            });
        }
        else
        {
            AddCards(tiles);
        }
    }

    private void AddCards(Tile[] tiles)
    {
        foreach (var tile in tiles)
        {
            var card = Instantiate(_cardObject, transform.position, Quaternion.identity);
            _currentCards.Add(card);
            card.transform.SetParent(_cardsLayout.gameObject.transform, false);
            card.SetCardInfo(tile, this);

            _cardsLayout.PositionNewCard(card, _currentCards.Count - 1);
        }

        _cardsLayout.RearrangeCards(_currentCards); // пересчитываем позиции
    }

    public void RemoveFirstCards(int count, TweenCallback onComplete)
    {
        List<CardObject> cardsToRemove = new List<CardObject>();

        for (int i = 0; i < count; i++)
        {
            if (_currentCards.Count == 0) return;

            var cardToRemove = _currentCards[0];

            // Проверяем, является ли карта текущей выбранной
            if (cardToRemove == _currentSelectCardObject)
            {
                CancelSelectCard();
                _tileDetector.ClearTileDetector();
            }

            cardsToRemove.Add(cardToRemove);
            cardsToRemove[i].DisabledButton();
            _currentCards.RemoveAt(0);
        }

        Sequence removeSequence = DOTween.Sequence();

        foreach (var cardToRemove in cardsToRemove)
        {
            RectTransform cardRect = cardToRemove.GetComponent<RectTransform>();
            removeSequence.Join(cardRect.DOAnchorPosY(300, 0.5f).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    cardToRemove.transform.DOScaleX(0, 0.5f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
                    {
                        Destroy(cardToRemove.gameObject);
                    });
                }));
        }

        removeSequence.OnComplete(onComplete);
    }


    public void RemoveCurrentCard()
    {
        if (_currentSelectCardObject == null || _dontRemoveCards) return;

        _currentCards.Remove(_currentSelectCardObject);
        Destroy(_currentSelectCardObject.gameObject);
        ClearCardHolderSystem();
        _cardsLayout.RearrangeCards(_currentCards);
    }

    public void SelectCardInCardHolder(CardObject newCardObject)
    {
        _tileDetector.ClearTileDetector();
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

    private void ClearCardHolderSystem()
    {
        _currentSelectCardObject = null;
    }

    private void OnDestroy()
    {
        CustomEvents.OnDayEnd -= AddCardsAfterDayEnd;
        CustomEvents.OnSetBase -= AddCardAfterSetBase;
        CustomEvents.OnSpawnRoadComplete -= AddStartGameCards;
    }
}
