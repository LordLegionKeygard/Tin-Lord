using System.Collections;
using System.Collections.Generic;
using RootMotion.FinalIK;
using UnityEngine;
using Zenject;

/// <summary>
/// Обновляет view, если модификатор добычи 0 или закончился требуемый ресурс для работы или здание выключено игроком
/// </summary>
public class BuildingProductionView : MonoBehaviour
{
    [Inject] private SelectTilePanel _selectTilePanel;
    private TileObject _tileObject;

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


    public void SetCurrentTileObject(TileObject tileObject)
    {
        _tileObject = tileObject;
        _tileObject.SetBuildingProductionView(this);
    }

    public void RefreshModifierView()
    {
        if (_changeViewIfModifier != 0) SetAdditionalBuildingView(_changeViewIfModifier != _tileObject.CurrentModifier());
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

    public void CheckMainBuildingView()
    {
        if (_tileObject.CurrentModifier() > 0 && (_tileObject.BuildingTileObject().CurrentBuilding().ResourcesForWork.Length == 0 || _tileObject.IsHaveRequiredResource()) && _tileObject.IsBuildingWork)
        {
            SetMainBuildingView(true);
        }
        else
        {
            SetMainBuildingView(false);
        }
        _selectTilePanel.RefreshShowInfo(_tileObject.GetId());
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
}
