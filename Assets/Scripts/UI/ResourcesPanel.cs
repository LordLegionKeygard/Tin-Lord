using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ResourcesPanel : MonoBehaviour
{
    [SerializeField] private RectTransform _objectTransform;
    [SerializeField] private ScrollRect _scrollRect;
    private bool _isOpen = false;
    public void PanelViewToggle()
    {
        _isOpen = !_isOpen;

        if (_isOpen)
        {
            _scrollRect.verticalNormalizedPosition = 1;
            _objectTransform.DOAnchorPosX(185, 0.3f).SetUpdate(true);
        }
        else
        {
            _objectTransform.DOAnchorPosX(-185, 0.3f).SetUpdate(true);
        }
    }
}
