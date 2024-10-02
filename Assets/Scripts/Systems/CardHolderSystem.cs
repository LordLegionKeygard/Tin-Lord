using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

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

    public void CancelSelectCard(bool isPause)
    {
        if (isPause) return;
        _tileDetector.Clear();
        if (_currentSelectCardObject == null) return;

        _currentSelectCardObject.CardObjectViewToggle(false);
        Clear();
    }

    public void AddNewCards(Tile[] tiles)
    {
        // Рассчитываем, сколько карт нужно удалить, если их больше 9
        int cardsToAdd = tiles.Length;
        int cardsToRemove = Mathf.Max(0, _currentCards.Count + cardsToAdd - 9);

        // Удаляем лишние карты с анимацией
        if (cardsToRemove > 0)
        {
            RemoveLeftmostCards(cardsToRemove);
        }

        // Добавляем новые карты
        foreach (var tile in tiles)
        {
            var card = Instantiate(_cardObject, transform.position, Quaternion.identity);
            _currentCards.Add(card);
            card.transform.SetParent(_parentTransform, false);
            card.SetCardInfo(tile, this);
        }
    }

    private void RemoveLeftmostCards(int count)
    {
        // Отключаем HorizontalLayoutGroup перед удалением карт для предотвращения мгновенного сдвига
        GridLayoutGroup layoutGroup = _parentTransform.GetComponent<GridLayoutGroup>();
        if (layoutGroup != null)
        {
            layoutGroup.enabled = false;
        }

        // Создаем копию текущего списка карт, которые нужно удалить
        List<CardObject> cardsToRemove = new List<CardObject>();

        // Сохраняем карты для удаления
        for (int i = 0; i < count; i++)
        {
            if (_currentCards.Count == 0) return;

            cardsToRemove.Add(_currentCards[0]);
            _currentCards.RemoveAt(0);
        }

        // Удаляем карты с анимацией подъема и изменения масштаба
        foreach (var cardToRemove in cardsToRemove)
        {
            cardToRemove.GetComponent<RectTransform>().DOAnchorPosY(100, 0.5f).SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    cardToRemove.transform.DOScaleX(0, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
                    {
                        Destroy(cardToRemove.gameObject);
                    });
                });
        }

        // Плавно смещаем оставшиеся карты и включаем HorizontalLayoutGroup после завершения анимации
        ShiftRemainingCards(() =>
        {
            if (layoutGroup != null)
            {
                layoutGroup.enabled = true;
                // LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup.GetComponent<RectTransform>());
            }
        });
    }

    private void ShiftRemainingCards(TweenCallback onComplete)
    {
        // Сначала сохраняем текущие позиции всех оставшихся карт
        List<Vector3> originalPositions = new List<Vector3>();
        foreach (var card in _currentCards)
        {
            originalPositions.Add(card.transform.position);
        }

        // Плавно перемещаем оставшиеся карты на новые позиции
        Sequence moveSequence = DOTween.Sequence();
        for (int i = 0; i < _currentCards.Count; i++)
        {
            var card = _currentCards[i];
            Vector3 newPosition = originalPositions[i];
            moveSequence.Join(card.transform.DOMove(newPosition, 0.5f).SetEase(Ease.InOutQuad));
        }

        // Когда анимация завершена, вызываем onComplete
        moveSequence.OnComplete(onComplete);
    }



    public void RemoveCurrentCard()
    {

        if (_currentSelectCardObject == null || _dontRemoveCards) return;

        _currentCards.Remove(_currentSelectCardObject);
        Destroy(_currentSelectCardObject.gameObject);
        Clear();
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

        // Передаем массив с несколькими случайными картами
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
}
