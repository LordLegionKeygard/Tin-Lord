using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingProductionView : MonoBehaviour
{
    [SerializeField] private float _modifier;
    private bool _isProduction;
    private TileObject _currentTileObject;

    [Header("PrepareObjects")]
    [SerializeField] private CharacterBuildingAnimator[] _characterBuildingAnimators;
    [SerializeField] private GameObject[] _turnoffObjects;
    [SerializeField] private Animator[] _animators;
    [SerializeField] private RotateAround[] _rotateArounds;

    private void Start()
    {
        CustomEvents.OnRefreshAnyTileInfo += RefreshViewFromEvent;
    }

    public void CheckProductionModifier(TileObject tileObject)
    {
        _currentTileObject = tileObject;
        RefreshModifier();
    }

    private void RefreshViewFromEvent(int tileId)
    {
        if (_currentTileObject == null) return;
        if (_currentTileObject.GetId() != tileId) return;
        RefreshModifier();
    }

    private void RefreshModifier()
    {
        _modifier = StaticMethods.GetResourceModifier(_currentTileObject);
        _isProduction = _modifier != 0;
        SetBuildingView();
    }

    private void SetBuildingView()
    {
        if (_isProduction)
        {
            // foreach (var animator in _characterBuildingAnimators)
            // {
            //     animator.TriggerWorkAnimator();
            // }
        }
        else
        {
            foreach (var animator in _characterBuildingAnimators)
            {
                animator.TriggerNotWorkAnimator();
            }

            foreach (var item in _turnoffObjects)
            {
                item.SetActive(false);
            }

            foreach (var item in _animators)
            {
                item.enabled = false;
            }

            foreach (var item in _rotateArounds)
            {
                item.StopRotation();
            }
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnRefreshAnyTileInfo -= RefreshViewFromEvent;
    }
}
