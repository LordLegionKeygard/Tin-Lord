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
    [SerializeField] private RobotBuildingAnimator[] _characterBuildingAnimators;
    [SerializeField] private ArmIK[] _armIk;
    [SerializeField] private GameObject[] _turnOffObjects;
    [SerializeField] private GameObject[] _turnOnObjects;
    [SerializeField] private Animator[] _animators;
    [SerializeField] private RotateAround[] _rotateArounds;

    [Header("TreeViewModifier")]
    [SerializeField] private GameObject[] _trees;

    [Header("Resource")]
    [SerializeField] private bool _needSetResourceView;
    [SerializeField] private ResourceViewMeshRenders[] _resourceViewMeshRenders;
    [SerializeField] private ResourceViewActiveGameObjects[] _resourceViewActiveGameObjects;

    [Header("MemoryRecovery")]
    [SerializeField] private MeshRenderer[] _meshRenderers;


    public void SetCurrentTileObject(TileObject tileObject)
    {
        _tileObject = tileObject;
        _tileObject.SetBuildingProductionView(this);
    }

    public void RefreshModifierView()
    {
        SetTreeView();
        CheckMainBuildingView();
    }

    private void SetTreeView()
    {
        if (_trees.Length == 0) return;

        foreach (var item in _trees)
        {
            item.SetActive(false);
        }

        var biomEnum = CurrentMissionInfo.Instance.GetCurrentLandscape().MissionView.BiomEnum;
        switch (_tileObject.GroundTileObject().CurrentGroundTile().GroundTileView)
        {
            case GroundTileViewEnum.Forest:
                _trees[biomEnum == BiomEnum.Winter ? 3 : 0].SetActive(true);
                break;
            case GroundTileViewEnum.Oasis:
                _trees[1].SetActive(true);
                break;
            case GroundTileViewEnum.DeadForest:
                _trees[2].SetActive(true);
                break;
            case GroundTileViewEnum.Grove:
                _trees[biomEnum == BiomEnum.Winter ? 4 : 5].SetActive(true);
                break;
        }
    }

    public void CheckMainBuildingView()
    {
        if ((_tileObject.CurrentModifier() > 0 || _tileObject.BuildingTileObject().IsEcologyBuilding()) && _tileObject.IsHaveRequiredResource() && _tileObject.IsBuildingWork())
        {
            SetMainView(true);
        }
        else
        {
            SetMainView(false);
        }
        _selectTilePanel.RefreshInfoAfterTakeDamage(_tileObject.GetId());
        if (_needSetResourceView) SetResourceView();
    }

    private void SetMainView(bool state)
    {
        if (_lastState == state) return;

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

        foreach (var item in _meshRenderers)
        {
            var material = item.material;
            material.SetColor("_EmissionColor", state ? Colors.MemoryOn * 1.5f : Color.white * 0);
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
        if (!_tileObject.IsBuildingWork())
        {
            // ресурсы должны отключаться когда здание выключено, чтобы визуально было лучше понятно, поэтому нет смысла вызывать этот метод, если здание не работает
            return;
        }

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

