using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ResourcesPanel : MonoBehaviour
{
    [SerializeField] private RectTransform _objectTransform;
    [SerializeField] private ScrollRect _scrollRect;
    public void PanelViewToggle(bool state)
    {
        if (state)
        {
            _scrollRect.verticalNormalizedPosition = 1;
            _objectTransform.DOAnchorPosX(184, 0.3f).SetUpdate(true);
        }
        else
        {
            _objectTransform.DOAnchorPosX(-200, 0.3f).SetUpdate(true);
        }
    }
}
