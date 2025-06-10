using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PanelDoMoveY : MonoBehaviour
{
    [SerializeField] private int _closePositionY;
    [SerializeField] private int _openPositionY;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private RectTransform _objectTransform;
    [SerializeField] private ScrollRect _scrollRect;
    private bool _isOpen = false;
    public bool IsOpen() => _isOpen;

    public void PanelMove(bool needSound = true)
    {
        if (needSound) AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        _isOpen = !_isOpen;

        if (_isOpen)
        {
            if (_scrollRect != null) _scrollRect.verticalNormalizedPosition = 1;
            _objectTransform.DOAnchorPosY(_openPositionY, _moveSpeed).SetUpdate(true);
        }
        else
        {
            _objectTransform.DOAnchorPosY(_closePositionY, _moveSpeed).SetUpdate(true);
        }
    }

    public void PanelClose()
    {
        _isOpen = false;
        _objectTransform.DOAnchorPosX(-_openPositionY, _moveSpeed).SetUpdate(true);
    }
}
