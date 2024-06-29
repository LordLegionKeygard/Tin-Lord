using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CardObject : MonoBehaviour
{
    [SerializeField] private Tile _tile;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private RectTransform _objectTransform;
    private CardHolderSystem _cardHolderSystem;
    public Tile GetTile() => _tile;

    public void SetCardInfo(Tile tile, CardHolderSystem cardHolderSystem)
    {
        _tile = tile;
        _cardHolderSystem = cardHolderSystem;
        gameObject.name = _tile.Name[0];
        _image.sprite = _tile.Icon;
        _text.text = _tile.Name[Language.LanguageNumber];
    }

    public void SelectCardObject()
    {
        _cardHolderSystem.SelectCardInCardHolder(this);
        CardObjectViewToggle(true);
    }

    public void CardObjectViewToggle(bool state)
    {
        if (state) _objectTransform.DOAnchorPosY(42, 0.3f).SetUpdate(true);
        else _objectTransform.DOAnchorPosY(0, 0.3f).SetUpdate(true);
    }

}
