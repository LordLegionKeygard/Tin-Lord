using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class CardHolderSystem : MonoBehaviour
{
    [Inject] private readonly TutorialSystem _tutorialSystem;
    [Inject] private readonly DiContainer _diContainer;
    [Inject] private readonly TilesSystem _tilesSystem;
    [Inject] private readonly MissionResources _missionResources;
    [Inject] private readonly RarityCardsSystem _rarityCardsSystem;

    [Header("Test")]
    [SerializeField] private bool _dontRemoveCards;

    [Header("Base")]
    [SerializeField] private CardObject _cardObject;
    [SerializeField] private TileDetector _tileDetector;
    [SerializeField] private CardsLayout _cardsLayout;

    [Header("Cards")]
    [SerializeField] private Tile _baseCard;
    [SerializeField] private Tile[] _startCards;
    private CardObject _currentSelectCardObject;
    private List<CardObject> _currentCards = new();
    private Card[] _availableTileCards;
    private Card[] _availableTacticCards;


    public bool IsHaveCurrentSelectedCardObject() => _currentSelectCardObject != null;
    public Tile CurrentCardHolderSelectedTile() => _currentSelectCardObject.GetTile();
    public bool CheckCurrentCardHolderSelectedTileIsFourTile() => _currentSelectCardObject == null || !IsSelectedCardTile() ? false : _currentSelectCardObject.GetTile().IsFourTile;
    public Card CurrentSelectedCard() => _currentSelectCardObject?.GetCard();
    public bool IsSelectedCardTile() => CurrentSelectedCard() is Tile;
    public int GetCurrentSelectCardObjectRarity() => _currentSelectCardObject.GetRarity();


    private void Awake()
    {
        CustomEvents.OnDayEnd += AddCardsAfterDayEnd;
        CustomEvents.OnSetBase += AddCardAfterSetBase;
    }

    public void LoadCards(bool isStartMission, CardData[] cardsData)
    {
        SetAvailableCards();

        if (isStartMission)
        {
            // в начале игры всегда даем фундамент базы обычной редкости
            AddNewCards(new CardHolderCardData[]
            {
                new() { Card = _baseCard, Rarity = (int)CardRarityEnum.Common }
            });
        }
        else
        {
            var loadCardsData = new Card[cardsData.Length];

            // получаем сами Card по их id
            for (int i = 0; i < cardsData.Length; i++)
            {
                loadCardsData[i] = _tilesSystem.GetCardForId(cardsData[i].CardId);
            }

            // упаковываем в CardHolderCardData, перенося редкость из CardData
            var data = new CardHolderCardData[cardsData.Length];
            for (int i = 0; i < cardsData.Length; i++)
                data[i] = new CardHolderCardData
                {
                    Card = loadCardsData[i],
                    Rarity = cardsData[i].CardRarity
                };

            AddNewCards(data, true);
        }
    }

    private void SetAvailableCards()
    {
        _availableTileCards = CurrentMissionInfo.Instance.GetCurrentLandscape().Cards;
        _availableTacticCards = CurrentMissionInfo.Instance.GetCurrentLandscape().TacticCards;
    }

    public CardData[] GetAllCards()
    {
        var result = new List<CardData>(_currentCards.Count);

        for (int i = 0; i < _currentCards.Count; i++)
        {
            var obj = _currentCards[i];
            var card = obj.GetCard();

            result.Add(new CardData
            {
                CardId = card.Id,
                CardRarity = obj.GetRarity()
            });
        }

        return result.ToArray();
    }

    public void CancelSelectCard()
    {
        if (_currentSelectCardObject == null) return;
        _currentSelectCardObject.SelectViewToggle(false);
        ClearCardHolderSystem();
    }

    public void AddNewCards(CardHolderCardData[] cardsData, bool isLoad = false)
    {
        int cardsToAddCount = cardsData.Length;
        int cardsToRemove = Mathf.Max(0, _currentCards.Count + cardsToAddCount - _cardsLayout.MaxCards());

        if (cardsToRemove > 0 && !isLoad)
        {
            RemoveFirstCards(cardsToRemove, () => AddCards(cardsData));
        }
        else
        {
            AddCards(cardsData);
        }
    }

    private void AddCards(CardHolderCardData[] cardsData)
    {
        foreach (var cardData in cardsData)
        {
            var card = _diContainer.InstantiatePrefab(_cardObject, transform.position, Quaternion.identity, null);
            var cardObject = card.GetComponent<CardObject>();
            card.transform.SetParent(_cardsLayout.gameObject.transform, false);
            cardObject.SetCardInfo(cardData.Card, this, cardData.Rarity);
            _currentCards.Add(cardObject);

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
                        _missionResources.ChangeResource(ResourceEnum.BeamEnergy, cardToRemove.GetRarity());
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

    private void AddNewRandomTileCards(int count)
    {
        var data = new CardHolderCardData[count];

        for (int i = 0; i < count; i++)
        {
            var rndIdx = Random.Range(0, _availableTileCards.Length);
            var card = _availableTileCards[rndIdx];

            data[i] = new CardHolderCardData
            {
                Card = card,
                Rarity = _rarityCardsSystem.GetRarity()
            };
        }

        AddNewCards(data);
    }

    private void AddNewRandomTacticCard()
    {
        var card = _availableTacticCards[Random.Range(0, _availableTacticCards.Length)];

        AddNewCards(new CardHolderCardData[]
        {
            new() { Card = card, Rarity = _rarityCardsSystem.GetRarity()}
        });
    }

    private void AddCardAfterSetBase(int level)
    {
        if (level > 1) return;

        int len = _startCards.Length;
        var startCardsData = new CardHolderCardData[len];
        for (int i = 0; i < len; i++)
        {
            startCardsData[i] = new CardHolderCardData
            {
                Card = _startCards[i],
                Rarity = _rarityCardsSystem.GetRarity()
            };
        }

        AddNewCards(startCardsData); // даем лес и гору точно
        AddCardsAfterDayEnd(0); // даем еще случайно, как будто день кончился
    }


    private void AddCardsAfterDayEnd(int _)
    {
        var rnd = Random.Range(0, 100);
        var addTacticCard = _tutorialSystem.IsCompleteMissionTutorial() && Random.Range(0, 100) <= WorldGameInfo.TacticCardChance;

        if (rnd < 50) //шанс выдачи только 1 карты
        {
            if (addTacticCard)
            {
                AddNewRandomTacticCard();
            }
            else
            {
                AddNewRandomTileCards(1);
            }
        }
        else //выдаем 2 карты
        {
            if (addTacticCard)
            {
                AddNewRandomTacticCard();
                AddNewRandomTileCards(1);
            }
            else
            {
                AddNewRandomTileCards(2);
            }
        }
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

[System.Serializable]
public class CardHolderCardData
{
    public Card Card;
    public int Rarity;
}
