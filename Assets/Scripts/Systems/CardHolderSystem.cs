using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolderSystem : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] private bool _test;
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
        CustomEvents.OnDayEnd += AddNewRandomCard;
        CustomEvents.OnSetBase += AddCardAfterSetBase;
    }

    private void Start()
    {
        if (_test)
        {
            for (int i = 0; i < _availableCards.Length; i++)
            {
                AddNewCard(_availableCards[i]);
            }
            AddNewCard(_startCards[0]);
        }
        else
        {
            for (int i = 0; i < _startCards.Length; i++)
            {
                AddNewCard(_startCards[i]);
            }
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

    public void AddNewCard(Tile tile)
    {
        var card = Instantiate(_cardObject, transform.position, Quaternion.identity);
        _currentCards.Add(card);
        card.transform.SetParent(_parentTransform);
        card.SetCardInfo(tile, this);
    }

    public void RemoveCurrentCard()
    {

        if (_currentSelectCardObject == null || _test) return;

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

    private void AddNewRandomCard(int dayNumber)
    {
        var rndCard = Random.Range(0, _availableCards.Length);

        AddNewCard(_availableCards[rndCard]);
    }

    private void AddCardAfterSetBase()
    {
        AddNewRandomCard(0);
        AddNewRandomCard(0);
        AddNewRandomCard(0);
    }

    private void Clear()
    {
        _currentSelectCardObject = null;
    }

    private void OnDestroy()
    {
        CustomEvents.OnDayEnd -= AddNewRandomCard;
        CustomEvents.OnSetBase -= AddCardAfterSetBase;
    }
}
