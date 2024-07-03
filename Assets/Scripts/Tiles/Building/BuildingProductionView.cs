using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Обновляет view, если модификатор добычи может стать 0
/// </summary>
public class BuildingProductionView : MonoBehaviour
{
    [SerializeField] private float _currentModifier;
    [SerializeField] private float _changeViewIfModifier;

    private bool _isChangeView;
    private TileObject _currentTileObject;

    [Header("PrepareObjects")]
    [SerializeField] private CharacterBuildingAnimator[] _characterBuildingAnimators;
    [SerializeField] private GameObject[] _turnOffObjects;
    [SerializeField] private GameObject[] _turnOnObjects;
    [SerializeField] private Animator[] _animators;
    [SerializeField] private RotateAround[] _rotateArounds;

    private void Start()
    {
        CustomEvents.OnRefreshAnyTileInfo += RefreshViewFromEvent;
    }

    public void SetCurrentTileObject(TileObject tileObject)
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
        _currentModifier = StaticMethods.GetResourceModifier(_currentTileObject);
        _isChangeView = _currentModifier != _changeViewIfModifier;
        SetBuildingView();
    }

    private void SetBuildingView()
    {
        if (_isChangeView) return;

        foreach (var animator in _characterBuildingAnimators)
        {
            animator.TriggerNotWorkAnimator();
        }

        foreach (var item in _turnOffObjects)
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

        foreach (var item in _turnOnObjects)
        {
            item.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnRefreshAnyTileInfo -= RefreshViewFromEvent;
    }
}
