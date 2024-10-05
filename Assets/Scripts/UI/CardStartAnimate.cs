using DG.Tweening;
using UnityEngine;

public class CardStartAnimate : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;

    private void Start()
    {
        if(_rectTransform == null) return;
        
        _rectTransform.DOAnchorPosY(0, 0.5f).SetEase(Ease.InOutQuad).SetUpdate(true);
    }
}
