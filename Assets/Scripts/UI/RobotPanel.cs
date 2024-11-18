using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RobotPanel : MonoBehaviour
{
    [SerializeField] RobotItem[] _robotItems;
    [SerializeField] private RectTransform _objectTransform;

    public void PanelViewToggle(bool state)
    {
        if (state)
        {
            ShowInfoPanel();
        }
        else
        {
            HideInfoPanel();
        }
    }

    private void ShowInfoPanel()
    {
        _objectTransform.DOAnchorPosX(-250, 0.3f).SetUpdate(true);
    }

    private void HideInfoPanel()
    {
        _objectTransform.DOAnchorPosX(250, 0.3f).SetUpdate(true);
    }

    public void UnselectAllRobots()
    {
        for (int i = 0; i < _robotItems.Length; i++)
        {
            _robotItems[i].SelectToggleState(false);
        }
    }
}
