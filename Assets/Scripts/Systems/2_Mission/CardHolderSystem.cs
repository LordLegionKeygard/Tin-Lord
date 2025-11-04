using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class CardHolderSystem : MonoBehaviour
{
    [Inject] private readonly DiContainer _diContainer;
    [Inject] private readonly TilesSystem _tilesSystem;
    [Inject] private readonly MissionResources _missionResources;

    [Header("Test")]
    [SerializeField] private bool _dontRemoveCards;

    [Header("Base")]
    [SerializeField] private CardObject _cardObject;
    [SerializeField] private TileDetector _tileDetector;
    [SerializeField] private CardsLayout _cardsLayout;

    [Header("Cards")]
    [SerializeField] private Tile _baseCard;
    [SerializeField] private Tile[] _startCards;
    [SerializeField] private List<CardObject> _currentCards;
    [SerializeField] private CardObject _currentSelectCardObject;
    private Card[] _availableTileCards;
    private Card[] _availableTacticCards;


    public bool IsHaveCurrentSelectedCardObject() => _currentSelectCardObject != null;
    public Tile CurrentCardHolderSelectedTile() => _currentSelectCardObject.GetTile();
    public bool CheckCurrentCardHolderSelectedTileIsFourTile() => _currentSelectCardObject == null || !IsSelectedCardTile() ? false : _currentSelectCardObject.GetTile().IsFourTile;
    public Card CurrentSelectedCard() => _currentSelectCardObject?.GetCard();
    public bool IsSelectedCardTile() => CurrentSelectedCard() is Tile;


    private void Awake()
    {
        CustomEvents.OnDayEnd += AddCardsAfterDayEnd;
        CustomEvents.OnSetBase += AddCardAfterSetBase;
    }

    public void LoadCards(bool isStartMission, int[] cards)
    {
        SetAvailableCards();

        if (isStartMission)
        {
            AddNewCards(new Card[] { _baseCard });
        }
        else
        {
            var loadCards = new Card[cards.Length];

            for (int i = 0; i < cards.Length; i++)
            {
                loadCards[i] = _tilesSystem.GetCardForId(cards[i]);
            }

            AddNewCards(loadCards);
        }
    }

    private void SetAvailableCards()
    {
        _availableTileCards = CurrentMissionInfo.Instance.GetCurrentLandscape().Cards;
        _availableTacticCards = CurrentMissionInfo.Instance.GetCurrentLandscape().TacticCards;
    }

    public int[] GetAllCards()
    {
        var cards = new int[_currentCards.Count];

        for (int i = 0; i < _currentCards.Count; i++)
        {
            cards[i] = _currentCards[i].GetCard().Id;
        }

        return cards;
    }

    public void CancelSelectCard()
    {
        if (_currentSelectCardObject == null) return;

        _currentSelectCardObject.SelectViewToggle(false);
        ClearCardHolderSystem();
    }

    public void AddNewCards(Card[] cards)
    {
        int cardsToAddCount = cards.Length;
        int cardsToRemove = Mathf.Max(0, _currentCards.Count + cardsToAddCount - _cardsLayout.MaxCards());

        if (cardsToRemove > 0)
        {
            RemoveFirstCards(cardsToRemove, () => AddCards(cards));
        }
        else
        {
            AddCards(cards);
        }
    }

    private void AddCards(Card[] cards)
    {
        foreach (var cardAsset in cards)
        {
            var card = _diContainer.InstantiatePrefab(_cardObject, transform.position, Quaternion.identity, null);
            var cardObject = card.GetComponent<CardObject>();
            _currentCards.Add(cardObject);
            card.transform.SetParent(_cardsLayout.gameObject.transform, false);
            cardObject.SetCardInfo(cardAsset, this);

            _cardsLayout.PositionNewCard(cardObject, _currentCards.Count - 1);
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
                        _missionResources.ChangeResource(ResourceEnum.BeamEnergy, 1);
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
        if (_currentSelectCardObject != null) _currentSelectCardObject.SelectViewToggle(false);
        _currentSelectCardObject = newCardObject;
    }

    private void AddNewRandomTileCards(int cards)
    {
        Card[] randomCards = new Card[cards];

        for (int i = 0; i < cards; i++)
        {
            var rndCard = Random.Range(0, _availableTileCards.Length);
            randomCards[i] = _availableTileCards[rndCard];
        }

        AddNewCards(randomCards);
    }

    private void AddNewRandomTacticCard()
    {
        var rnd = Random.Range(0, 100);

        if (rnd > WorldGameInfo.TacticCardChance) return;

        Card[] randomCards = new Card[1];

        for (int i = 0; i < 1; i++)
        {
            var rndCard = Random.Range(0, _availableTacticCards.Length);
            randomCards[i] = _availableTacticCards[rndCard];
        }

        AddNewCards(randomCards);
    }

    private void AddCardAfterSetBase(int level)
    {
        if (level > 1) return;

        AddNewCards(_startCards);
        AddNewRandomTileCards(2);
    }

    private void AddCardsAfterDayEnd(int dayNumber)
    {
        AddNewRandomTileCards(2);
        AddNewRandomTacticCard();
    }

    private void ClearCardHolderSystem()
    {
        _currentSelectCardObject = null;
    }

    public Card GetRandomAvailableCardExcept(Card except)
    {
        List<Card> pool = new();
        for (int i = 0; i < _availableTileCards.Length; i++)
        {
            var t = _availableTileCards[i];
            if (t != null && t.Name != except.Name)
            {
                pool.Add(t);
            }
        }

        return pool[Random.Range(0, pool.Count)];
    }


    private void OnDestroy()
    {
        CustomEvents.OnDayEnd -= AddCardsAfterDayEnd;
        CustomEvents.OnSetBase -= AddCardAfterSetBase;
    }
}
