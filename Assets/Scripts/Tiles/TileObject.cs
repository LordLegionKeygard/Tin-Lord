using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour
{
    [SerializeField] private int _id;
    private GroundTile _groundTile;
    private BuildingTile _buildingTile;
    private BuildingResourcesRequired _buildingResourcesRequired;
    public GroundTile GroundTileObject() => _groundTile;
    public BuildingTile BuildingTileObject() => _buildingTile;
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
        _buildingResourcesRequired = GetComponent<BuildingResourcesRequired>();
    }

    public void SetId(int id) => _id = id;
    public int GetId() => _id;

    public void SetIsHaveRequiredResource(bool state)
    {
        _isHaveRequiredResource = state;
        FireEvent();
    }

    public int GetEcology()
    {
        var buildingTile = BuildingTileObject().CurrentBuildingTile();
        var haveTile = BuildingTileObject().HaveTile();
        var buildingLevel = haveTile ? BuildingTileObject().CurrentBuildingLevel() : 0;

        var groundEcology = GroundTileObject().CurrentGroundTile().GroundEcology;
        var buildingEcology = haveTile ? buildingTile.UpgradeBuildingWrapper[buildingLevel - 1].BuildingEcology : 0;

        return groundEcology + buildingEcology;
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
        var resourcesExtracted = _isHaveRequiredResource ? _buildingTile.CurrentUpgradeBuildingWrapper().ResourceExtractedAmount * CurrentModifier : 0;
        CustomEvents.FireChangeResourceExtraction(_buildingTile.CurrentBuildingTile().Resource.ResourceEnum, resourcesExtracted, _id);
    }
}
