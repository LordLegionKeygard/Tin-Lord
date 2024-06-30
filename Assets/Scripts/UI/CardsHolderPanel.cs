using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardsHolderPanel : MonoBehaviour
{
    [SerializeField] private RectTransform _objectTransform;

    private void Awake()
    {
        // CustomEvents.OnPauseChanged += PanelViewToggle;
    }

    public void PanelViewToggle(bool state)
    {
        if (state)
        {
            _objectTransform.DOAnchorPosY(87, 0.3f).SetUpdate(true);
        }
        else
        {
            _objectTransform.DOAnchorPosY(-130, 0.3f).SetUpdate(true);
        }
    }

    private void OnDestroy()
    {
        // CustomEvents.OnPauseChanged -= PanelViewToggle;
    }
}
