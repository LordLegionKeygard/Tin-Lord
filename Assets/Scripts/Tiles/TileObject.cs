using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileObject : MonoBehaviour
{
    [Inject] PlayerResources _playerResources;
    [Inject] SelectTilePanel _selectTilePanel;
    private GroundTile _groundTile;
    private BuildingTile _buildingTile;
    private TileEcology _tileEcology;
    private BuildingProductionView _buildingProductionView;
    private int _id;
    private float _currentModifier;
    private Resource _currentResourcesProduction;
    private Resource _currentResourceRequired;
    private float _currentResourceRequiredAmount;
    public bool IsBuildingWork;
    private bool _isHaveResourceRequired;

    public GroundTile GroundTileObject() => _groundTile;
    public BuildingTile BuildingTileObject() => _buildingTile;
    public TileEcology TileEcology() => _tileEcology;
    public int CurrentTileId() => _id;
    public int GetId() => _id;
    public float CurrentModifier() => _currentModifier;
    public Resource CurrentResourcesProduction() => _currentResourcesProduction;
    public Resource CurrentResourceRequired() => _currentResourceRequired;
    public float CurrentResourceRequiredAmount() => _currentResourceRequiredAmount;
    public void SetBuildingProductionView(BuildingProductionView buildingProductionView) => _buildingProductionView = buildingProductionView;

    private void Awake()
    {
        _groundTile = GetComponent<GroundTile>();
        _buildingTile = GetComponent<BuildingTile>();
        _tileEcology = GetComponent<TileEcology>();
    }
    public bool IsHaveRequiredResource()
    {
        return _buildingTile.CurrentBuilding().ResourcesForWork.Length != 0 ? _playerResources.ResourceEnough(_currentResourceRequired.ResourceEnum, _currentResourceRequiredAmount) : true;
    }

    public void ClearBuildingInfo()
    {
        _currentResourcesProduction = null;
        _currentResourceRequired = null;
        _currentResourceRequiredAmount = 0;
        _buildingProductionView = null;
        _currentModifier = 0;
        CustomEvents.FireChangeResourceProduction(ResourceEnum.None, 0, _id, true);
        CustomEvents.FireChangeResourceRequired(this, null, 0);
    }

    public void SetId(int id) => _id = id;

    public void SetResourceModifier()
    {
        if (_buildingTile.CurrentBuildingTile() == null || !_buildingTile.CurrentBuildingTile().IsHaveProdictionResources()) return;

        _currentModifier = CalculateCurrentModifier();
        _buildingProductionView.RefreshModifierView();
        ChangeResourceProduction();
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
            ChangeResourceProduction();
        }
    }

    public void CheckBuildingView()
    {
        if (_buildingProductionView != null) _buildingProductionView.CheckMainBuildingView();
    }

    public void ChangeResourceProduction()
    {
        if (_buildingTile.CurrentBuildingTile() == null) return;
        // Debug.Log("ChangeResourceProduction - CheckCount");

        var resourceWrapper = _buildingTile.CurrentBuilding();
        var resourcesProduction = IsBuildingWork ? resourceWrapper.ResourcesForWork.Length == 0
            ? resourceWrapper.ResourceExtractedAmount * _currentModifier
            : (IsHaveRequiredResource() ? resourceWrapper.ResourceExtractedAmount * _currentModifier : 0) : 0;

        CustomEvents.FireChangeResourceProduction(_currentResourcesProduction.ResourceEnum, resourcesProduction, _id, false);
    }

    public void SetResourceRequied(Resource resource, float amount)
    {
        _currentResourceRequired = resource;
        _currentResourceRequiredAmount = amount;
        CustomEvents.FireChangeResourceRequired(this, _currentResourceRequired, _currentResourceRequiredAmount);
    }

    public void SetResourceProduction(Resource resource)
    {
        _currentResourcesProduction = resource;
        ChangeResourceProduction();
        _selectTilePanel.SetInfo(this);
    }
}
