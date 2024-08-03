using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour
{
    private GroundTile _groundTile;
    private BuildingTile _buildingTile;
    private TileEcology _tileEcology;
    private BuildingResourcesRequired _buildingResourcesRequired;
    private int _id;
    private bool _isHaveRequiredResource;
    private float _currentModifier;
    private Resource _currentResourceRequired;
    private float _currentResourceRequiredAmount;

    public GroundTile GroundTileObject() => _groundTile;
    public BuildingTile BuildingTileObject() => _buildingTile;
    public TileEcology TileEcology() => _tileEcology;
    public BuildingResourcesRequired BuildingResourcesRequired() => _buildingResourcesRequired;
    public int CurrentTileId() => _id;
    public int GetId() => _id;
    public bool IsHaveRequiredResource() => _isHaveRequiredResource;
    public float CurrentModifier() => _currentModifier;
    public Resource CurrentResourceRequired() => _currentResourceRequired;
    public float CurrentResourceRequiredAmount() => _currentResourceRequiredAmount;


    public void SetResourceRequied(Resource resource, float amount)
    {
        _currentResourceRequired = resource;
        _currentResourceRequiredAmount = amount;
        CustomEvents.FireChangeResourceRequired(this, _currentResourceRequired, _currentResourceRequiredAmount);
    }

    private void Awake()
    {
        _groundTile = GetComponent<GroundTile>();
        _buildingTile = GetComponent<BuildingTile>();
        _tileEcology = GetComponent<TileEcology>();
        _buildingResourcesRequired = GetComponent<BuildingResourcesRequired>();
    }

    public void SetId(int id) => _id = id;

    public void SetIsHaveRequiredResource(bool state)
    {
        _isHaveRequiredResource = state;
        FireChangeResourceExtractionEvent();
    }

    public void SetResourceModifier()
    {
        _currentModifier = CalculateCurrentModifier();
        FireChangeResourceExtractionEvent();
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
        return 0; // Resource is no longer available on the tile
    }

private void FireChangeResourceExtractionEvent()
    {
        Debug.Log("FireEvent - NeedOptimize");

        var resourceWrapper = _buildingTile.CurrentUpgradeBuildingWrapper();
        var resourcesExtracted = resourceWrapper.ResourceRequiredEnum == ResourceRequiredEnum.None
            ? resourceWrapper.ResourceExtractedAmount * _currentModifier
            : (_isHaveRequiredResource ? resourceWrapper.ResourceExtractedAmount * _currentModifier : 0);

        CustomEvents.FireChangeResourceExtraction(_buildingTile.CurrentBuildingTile().Resource.ResourceEnum, resourcesExtracted, _id);
    }
}
