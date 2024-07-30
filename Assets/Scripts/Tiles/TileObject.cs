using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour
{
    [SerializeField] private int _id;
    private GroundTile _groundTile;
    private BuildingTile _buildingTile;
    public GroundTile GroundTileObject() => _groundTile;
    public BuildingTile BuildingTileObject() => _buildingTile;
    public int CurrentTileId() => _id;

    private void Awake()
    {
        _groundTile = GetComponent<GroundTile>();
        _buildingTile = GetComponent<BuildingTile>();
    }

    public void SetId(int id) => _id = id;
    public int GetId() => _id;

    public int GetEcology()
    {
        var buildingTile = BuildingTileObject().CurrentBuildingTile();
        var haveTile = BuildingTileObject().HaveTile();
        var buildingLevel = haveTile ? BuildingTileObject().CurrentBuildingLevel() : 0;

        var groundEcology = GroundTileObject().CurrentGroundTile().GroundEcology;
        var buildingEcology = haveTile ? buildingTile.UpgradeBuildingWrapper[buildingLevel - 1].BuildingEcology : 0;

        return groundEcology + buildingEcology;
    }
}
