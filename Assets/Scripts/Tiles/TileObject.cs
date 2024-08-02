using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour
{
    [SerializeField] private int _id;
    private GroundTile _groundTile;
    private BuildingTile _buildingTile;
    private TileEcology _tileEcology;
    private BuildingResourcesRequired _buildingResourcesRequired;
    public GroundTile GroundTileObject() => _groundTile;
    public BuildingTile BuildingTileObject() => _buildingTile;
    public TileEcology TileEcology() => _tileEcology;
    public BuildingResourcesRequired BuildingResourcesRequired() => _buildingResourcesRequired;
    public int CurrentTileId() => _id;
    [SerializeField] private bool _isHaveRequiredResource;
    public bool IsHaveRequiredResource() => _isHaveRequiredResource;


    public float CurrentModifier;

    public Resource CurrentResourceRequired;
    public float CurrentResourceRequiredAmount;

    private void Awake()
    {
        _groundTile = GetComponent<GroundTile>();
        _buildingTile = GetComponent<BuildingTile>();
        _tileEcology = GetComponent<TileEcology>();
        _buildingResourcesRequired = GetComponent<BuildingResourcesRequired>();
    }

    public void SetId(int id) => _id = id;
    public int GetId() => _id;

    public void SetIsHaveRequiredResource(bool state)
    {
        _isHaveRequiredResource = state;
        FireEvent();
    }

    public void SetResourceModifier()
    {
        var buildingsOnTile = GroundTileObject().CurrentGroundTile().BuildingsOnTile;

        for (int i = 0; i < buildingsOnTile.Length; i++)
        {
            if (buildingsOnTile[i].BuildingTile == BuildingTileObject().CurrentBuildingTile())
            {
                CurrentModifier = buildingsOnTile[i].ResourceModifier;
                FireEvent();
                return;
            }
        }
        CurrentModifier = 0; //данного ресурса больше нет на тайле
        FireEvent();
    }

    private void FireEvent()
    {
        Debug.Log("FireEvent - NeedOptimize");
        var resourcesExtracted = 0f;

        if(_buildingTile.CurrentUpgradeBuildingWrapper().ResourceRequiredEnum == ResourceRequiredEnum.None)
        {
            resourcesExtracted = _buildingTile.CurrentUpgradeBuildingWrapper().ResourceExtractedAmount * CurrentModifier;
        }
        else
        {
            resourcesExtracted = _isHaveRequiredResource ? _buildingTile.CurrentUpgradeBuildingWrapper().ResourceExtractedAmount * CurrentModifier : 0;
        }

        CustomEvents.FireChangeResourceExtraction(_buildingTile.CurrentBuildingTile().Resource.ResourceEnum, resourcesExtracted, _id);
    }
}
