using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ResourcesPanel : MonoBehaviour
{
    [SerializeField] private RectTransform _objectTransform;
    public void PanelViewToggle(bool state)
    {
        if (state)
        {
            _objectTransform.DOAnchorPosX(184, 0.3f).SetUpdate(true);
        }
        else
        {
            _objectTransform.DOAnchorPosX(-200, 0.3f).SetUpdate(true);
        }
    }
}
