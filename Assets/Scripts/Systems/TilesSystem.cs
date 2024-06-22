using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesSystem : MonoBehaviour
{
    [SerializeField] private Tile[] _allGroundTiles;
    [SerializeField] private Tile[] _allBuildingTiles;

    [Header("WorldTileInfo")]
    public bool IsHaveRiver = false;

    public Tile TakeGroundTile(GroundTileViewEnum tileView) => _allGroundTiles[(int)tileView - 1];
    public Tile TakeBuildingTile(GroundTileViewEnum tileView) => _allBuildingTiles[(int)tileView - 1];
}
