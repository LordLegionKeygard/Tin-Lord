using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileObject : MonoBehaviour
{
    [Inject] PlayerResources _playerResources;
    private GroundTile _groundTile;
    private BuildingTile _buildingTile;
    private TileEcology _tileEcology;
    private BuildingResourcesRequired _buildingResourcesRequired;
    private BuildingProductionView _buildingProductionView;
    private int _id;
    private float _currentModifier;
    private Resource _currentResourceRequired;
    private float _currentResourceRequiredAmount;
    public bool IsBuildingWork;
    private bool _isHaveResourceRequired;

    public GroundTile GroundTileObject() => _groundTile;
    public BuildingTile BuildingTileObject() => _buildingTile;
    public TileEcology TileEcology() => _tileEcology;
    public BuildingResourcesRequired BuildingResourcesRequired() => _buildingResourcesRequired;
    public int CurrentTileId() => _id;
    public int GetId() => _id;
    public float CurrentModifier() => _currentModifier;
    public Resource CurrentResourceRequired() => _currentResourceRequired;
    public float CurrentResourceRequiredAmount() => _currentResourceRequiredAmount;
    public void SetBuildingProductionView(BuildingProductionView buildingProductionView) => _buildingProductionView = buildingProductionView;

    private void Awake()
    {
        _groundTile = GetComponent<GroundTile>();
        _buildingTile = GetComponent<BuildingTile>();
        _tileEcology = GetComponent<TileEcology>();
        _buildingResourcesRequired = GetComponent<BuildingResourcesRequired>();
    }
    public bool IsHaveRequiredResource()
    {
        return _buildingTile.CurrentUpgradeBuildingWrapper().ResourcesForWork.Length != 0 ? _playerResources.ResourceEnough(_currentResourceRequired.ResourceEnum, _currentResourceRequiredAmount) : true;
    }

    public void ClearBuildingInfo()
    {
        _currentResourceRequired = null;
        _currentResourceRequiredAmount = 0;
        _buildingProductionView = null;
        _currentModifier = 0;
        CustomEvents.FireChangeResourceExtraction(ResourceEnum.None, 0, _id, true);
        CustomEvents.FireChangeResourceRequired(this, null, 0);
    }

    public void SetResourceRequied(Resource resource, float amount)
    {
        _currentResourceRequired = resource;
        _currentResourceRequiredAmount = amount;
        CustomEvents.FireChangeResourceRequired(this, _currentResourceRequired, _currentResourceRequiredAmount);
    }

    public void SetId(int id) => _id = id;

    public void SetResourceModifier()
    {
        if (_buildingTile.CurrentBuildingTile() == null || _buildingTile.CurrentBuildingTile().Resource == null) return;

        _currentModifier = CalculateCurrentModifier();
        _buildingProductionView.RefreshModifierView();
        ChangeResourceExtraction();
    }

    private float CalculateCurrentModifier()
    {
        foreach (var building in GroundTileObject().CurrentGroundTile().BuildingsOnTile)
        {
            if (building.BuildingTile == BuildingTileObject().CurrentBuildingTile())
            {
                return building.ResourceModifier;
            }
        }
        return 0;
    }

    public void CheckResourceRequired(bool needCheck)
    {
        var state = IsHaveRequiredResource();

        if (state != _isHaveResourceRequired || needCheck)
        {
            _isHaveResourceRequired = state;
            CheckBuildingView();
            ChangeResourceExtraction();
        }
    }

    public void CheckBuildingView()
    {
        if (_buildingProductionView != null) _buildingProductionView.CheckMainBuildingView();
    }

    public void ChangeResourceExtraction()
    {
        if (_buildingTile.CurrentBuildingTile() == null) return;
        // Debug.Log("ChangeResourceExtraction - CheckCount");

        var resourceWrapper = _buildingTile.CurrentUpgradeBuildingWrapper();
        var resourcesExtracted = IsBuildingWork ? resourceWrapper.ResourcesForWork.Length == 0
            ? resourceWrapper.ResourceExtractedAmount * _currentModifier
            : (IsHaveRequiredResource() ? resourceWrapper.ResourceExtractedAmount * _currentModifier : 0) : 0;

        CustomEvents.FireChangeResourceExtraction(_buildingTile.CurrentBuildingTile().Resource.ResourceEnum, resourcesExtracted, _id, false);
    }
}
