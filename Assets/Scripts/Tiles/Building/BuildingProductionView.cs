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

    /// <summary>
    /// Изначально тру так как подразумевается, что здание при создании будет включено
    /// Если будет ложь, то при спавне здания и нехватке ресурса, анимация будет продолжать работать
    /// </summary>
    private bool _lastState = true; 

    [Header("Main")]
    [SerializeField] private CharacterBuildingAnimator[] _characterBuildingAnimators;
    [SerializeField] private ArmIK[] _armIk;
    [SerializeField] private GameObject[] _turnOffObjects;
    [SerializeField] private GameObject[] _turnOnObjects;
    [SerializeField] private Animator[] _animators;
    [SerializeField] private RotateAround[] _rotateArounds;

    [Header("Modifier")]
    [SerializeField] private float _changeViewIfModifier;
    [SerializeField] private GameObject[] _additionalturnOffObjects;
    [SerializeField] private GameObject[] _additionalturnOnObjects;

    [Header("Resource")]
    [SerializeField] private bool _needSetResourceView;
    [SerializeField] private ResourceViewMeshRenders[] _resourceViewMeshRenders;
    [SerializeField] private ResourceViewActiveGameObjects[] _resourceViewActiveGameObjects;


    public void SetCurrentTileObject(TileObject tileObject)
    {
        _tileObject = tileObject;
        _tileObject.SetBuildingProductionView(this);
    }

    public void RefreshModifierView()
    {
        if (_changeViewIfModifier != 0) SetModifierView(_changeViewIfModifier != _tileObject.CurrentModifier());
        CheckMainBuildingView();
    }

    private void SetModifierView(bool state)
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
        if (_tileObject.CurrentModifier() > 0 && _tileObject.IsHaveRequiredResource() && _tileObject.IsBuildingWork)
        {
            SetMainView(true);
        }
        else
        {
            SetMainView(false);
        }
        _selectTilePanel.RefreshShowInfo(_tileObject.GetId());
        if (_needSetResourceView) SetResourceView();
    }

    private void SetMainView(bool state)
    {
        if(_lastState == state) return;

        _lastState = state;
        foreach (var animator in _characterBuildingAnimators)
        {
            animator.ToggleWorkView(state);
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


    private void SetResourceView()
    {
        ChangeMeshRendersMaterial();
        ChangeActiveObjects();
    }



    private void ChangeMeshRendersMaterial()
    {
        for (int i = 0; i < _resourceViewMeshRenders.Length; i++)
        {
            for (int k = 0; k < _resourceViewMeshRenders[i].ResourceMaterialWrapper.Length; k++)
            {
                if (_tileObject.CurrentResourceProduction() == _resourceViewMeshRenders[i].ResourceMaterialWrapper[k].Resource)
                {
                    foreach (var item in _resourceViewMeshRenders[i].MeshRenderers)
                    {
                        item.material = _resourceViewMeshRenders[i].ResourceMaterialWrapper[k].ResourceMaterial;
                    }

                    foreach (var item in _resourceViewMeshRenders[i].SkinnedMeshRenderers)
                    {
                        item.material = _resourceViewMeshRenders[i].ResourceMaterialWrapper[k].ResourceMaterial;
                    }
                }
            }
        }
    }

    private void ChangeActiveObjects()
    {
        // if(!_tileObject.IsBuildingWork) return;
        
        for (int i = 0; i < _resourceViewActiveGameObjects.Length; i++)
        {
            foreach (var item in _resourceViewActiveGameObjects[i].ActiveGameObjects)
            {
                item.SetActive(_tileObject.CurrentResourceProduction() == _resourceViewActiveGameObjects[i].Resource);
            }
        }
    }
}

[System.Serializable]
public class ResourceViewActiveGameObjects
{
    public Resource Resource;
    public GameObject[] ActiveGameObjects;
}

[System.Serializable]
public class ResourceViewMeshRenders
{
    public MeshRenderer[] MeshRenderers;
    public SkinnedMeshRenderer[] SkinnedMeshRenderers;
    public ResourceMaterialWrapper[] ResourceMaterialWrapper;
}

[System.Serializable]
public class ResourceMaterialWrapper
{
    public Resource Resource;
    public Material ResourceMaterial;
}

