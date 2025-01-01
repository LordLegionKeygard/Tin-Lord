using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PanelDoMove : MonoBehaviour
{
    [SerializeField] private int _positionX;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private RectTransform _objectTransform;
    [SerializeField] private ScrollRect _scrollRect;
    private bool _isOpen = false;
    public void PanelMove()
    {
        _isOpen = !_isOpen;

        if (_isOpen)
        {
            _scrollRect.verticalNormalizedPosition = 1;
            _objectTransform.DOAnchorPosX(_positionX, _moveSpeed).SetUpdate(true);
        }
        else
        {
            _objectTransform.DOAnchorPosX(-_positionX, _moveSpeed).SetUpdate(true);
        }
    }
}
