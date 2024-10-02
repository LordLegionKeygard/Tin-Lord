using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardStartAnimate : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform.DOAnchorPosY(0, 0.5f).SetEase(Ease.InOutQuad);
    }
}
