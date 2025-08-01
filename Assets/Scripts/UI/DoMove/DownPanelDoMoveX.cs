using UnityEngine;
using DG.Tweening;

public class DownPanelDoMoveX : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransformArrow;
    [SerializeField] private int _openPositionX;
    [SerializeField] private int _closePositionX;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private RectTransform _objectTransform;
    private bool _isOpen = false;
    public bool IsOpen() => _isOpen;
    
    public void PanelMove()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        _isOpen = !_isOpen;

        if (_isOpen)
        {
            _rectTransformArrow.rotation = Quaternion.Euler(0, 0, 90);
            _objectTransform.DOAnchorPosX(_openPositionX, _moveSpeed).SetUpdate(true);
        }
        else
        {
            _rectTransformArrow.rotation = Quaternion.Euler(0, 0, -90);
            _objectTransform.DOAnchorPosX(_closePositionX, _moveSpeed).SetUpdate(true);
        }
    }

    public void PanelClose()
    {
        _isOpen = false;
        _objectTransform.DOAnchorPosX(_closePositionX, _moveSpeed).SetUpdate(true);
    }
}
