using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResourceView : BaseView
{
    [SerializeField] private Image _icon;

    public void Initialize(Vector3 worldPos, Sprite sprite, int amount)
    {
        _worldPos = worldPos;
        _icon.sprite = sprite;
        Text.text = $"+{amount}";

        gameObject.SetActive(true);

        UpdatePosition();

        Content.anchoredPosition = Vector2.zero;
        Content.localScale = Vector3.one;
        CanvasGroup.alpha = 1f;

        var endLocal = new Vector2(0f, _riseDistance);

        _seq = DOTween.Sequence()
            .Append(Content.DOAnchorPos(endLocal, _duration).SetEase(Ease.OutCubic))
            .Join(Content.DOScale(0f, _duration).SetEase(Ease.InBack))
            .Join(CanvasGroup.DOFade(0f, _duration * 0.9f).SetEase(Ease.InSine))
            .OnComplete(Despawn)
            .SetUpdate(true); 
    }
}
