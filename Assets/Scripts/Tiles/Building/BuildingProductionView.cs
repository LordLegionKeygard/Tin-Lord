using System.Collections;
using System.Collections.Generic;
using RootMotion.FinalIK;
using UnityEngine;

/// <summary>
/// Обновляет view, если модификатор добычи 0 или закончился требуемый ресурс для работы или здание выключено игроком
/// </summary>
public class BuildingProductionView : MonoBehaviour
{
    private TileObject _currentTileObject;

    [Header("MainWorkObjects")]
    [SerializeField] private CharacterBuildingAnimator[] _characterBuildingAnimators;
    [SerializeField] private ArmIK[] _armIk;
    [SerializeField] private GameObject[] _turnOffObjects;
    [SerializeField] private GameObject[] _turnOnObjects;
    [SerializeField] private Animator[] _animators;
    [SerializeField] private RotateAround[] _rotateArounds;

    [Header("AdditionalObjects")]
    [SerializeField] private float _changeViewIfModifier;
    [SerializeField] private GameObject[] _additionalturnOffObjects;
    [SerializeField] private GameObject[] _additionalturnOnObjects;

    private void Awake()
    {
        CustomEvents.OnRefreshBuildingModifier += RefreshBuildingModifier;
        CustomEvents.OnHaveRequiredResource += ToggleResourceRequired;
    }

    public void SetCurrentTileObject(TileObject tileObject)
    {
        _currentTileObject = tileObject;
        RefreshModifier();
    }

    private void ToggleResourceRequired(int tileId, bool state)
    {
        if (_currentTileObject == null) return;
        if (_currentTileObject.GetId() != tileId) return;

        _currentTileObject.SetIsHaveRequiredResource(state);

        CheckMainBuildingView();
    }

    private void RefreshBuildingModifier(int tileId)
    {
        if (_currentTileObject == null) return;
        if (_currentTileObject.GetId() != tileId) return;
        RefreshModifier();
    }

    private void RefreshModifier()
    {
        _currentTileObject.SetResourceModifier();
        if (_changeViewIfModifier != 0) SetAdditionalBuildingView(_changeViewIfModifier != _currentTileObject.CurrentModifier());
        CheckMainBuildingView();
    }

    private void SetAdditionalBuildingView(bool state)
    {
        foreach (var item in _additionalturnOffObjects)
        {
            item.SetActive(state);
        }

        foreach (var item in _additionalturnOnObjects)
        {
            item.SetActive(!state);
        }
    }

    private void CheckMainBuildingView()
    {
        if (_currentTileObject.CurrentModifier() > 0 && (_currentTileObject.BuildingTileObject().CurrentUpgradeBuildingWrapper().ResourceRequiredEnum == ResourceRequiredEnum.None || _currentTileObject.IsHaveRequiredResource()))
        {
            SetMainBuildingView(true);
        }
        else
        {
            SetMainBuildingView(false);
        }
        CustomEvents.FireRefreshShowInfo(_currentTileObject.GetId());
    }

    private void SetMainBuildingView(bool state)
    {
        foreach (var animator in _characterBuildingAnimators)
        {
            if (state) animator.TriggerWorkAnimator();
            else animator.TriggerNotWorkAnimator();
        }

        foreach (var ik in _armIk)
        {
            ik.enabled = state;
        }

        foreach (var item in _turnOffObjects)
        {
            item.SetActive(state);
        }

        foreach (var item in _animators)
        {
            item.enabled = state;
        }

        foreach (var item in _rotateArounds)
        {
            item.RotationToggle(state);
        }

        foreach (var item in _turnOnObjects)
        {
            item.SetActive(!state);
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnRefreshBuildingModifier -= RefreshBuildingModifier;
        CustomEvents.OnHaveRequiredResource -= ToggleResourceRequired;
    }
}
