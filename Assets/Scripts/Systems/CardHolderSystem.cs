using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolderSystem : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] private CardObject _cardObject;
    [SerializeField] private TileDetector _tileDetector;
    [SerializeField] private Transform _parentTransform;
    [SerializeField] private Tile[] _testStartCards;

    [Header("Current")]
    [SerializeField] private List<CardObject> _currentCards;
    [SerializeField] private CardObject _currentSelectCardObject;
    public bool IsHaveCurrentSelectedCardObject() => _currentSelectCardObject != null;
    public Tile CurrentCardHolderSelectedTile() => _currentSelectCardObject.GetTile();
    public bool CheckCurrentCardHolderSelectedTileIsFourTile() => _currentSelectCardObject == null ? false : _currentSelectCardObject.GetTile().IsFourTile;

    private void Start()
    {
        for (int i = 0; i < _testStartCards.Length; i++)
        {
            AddNewCard(_testStartCards[i]);
        }
    }

    public void CancelSelectCard(bool isPause)
    {
        if (isPause) return;
        _tileDetector.Clear();
        if (_currentSelectCardObject == null) return;

        _currentSelectCardObject.CardObjectViewToggle(false);
        _currentSelectCardObject = null;
    }

    public void AddNewCard(Tile tile)
    {
        var card = Instantiate(_cardObject, transform.position, Quaternion.identity);
        _currentCards.Add(card);
        card.transform.SetParent(_parentTransform);
        card.SetCardInfo(tile, this);
    }

    public void SelectCardInCardHolder(CardObject newCardObject)
    {
        _tileDetector.Clear();
        if (_currentSelectCardObject != null) _currentSelectCardObject.CardObjectViewToggle(false);
        _currentSelectCardObject = newCardObject;
    }

    public void Clear()
    {
        _currentSelectCardObject = null;
    }
}
