using UnityEngine;
using DG.Tweening;

public class PanelDoMoveX : MonoBehaviour
{
    [SerializeField] private int _positionX;
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
            _objectTransform.DOAnchorPosX(_positionX, _moveSpeed).SetUpdate(true);
        }
        else
        {
            _objectTransform.DOAnchorPosX(-_positionX, _moveSpeed).SetUpdate(true);
        }
    }

    public void PanelClose()
    {
        _isOpen = false;
        _objectTransform.DOAnchorPosX(-_positionX, _moveSpeed).SetUpdate(true);
    }
}
