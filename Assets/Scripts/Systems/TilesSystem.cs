using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesSystem : MonoBehaviour
{
    [SerializeField] private Tile[] _allGroundTiles;

    [Header("WorldTileInfo")]
    public bool IsHaveRiver = false;
    public bool IsHaveBase = false;

    public Tile TakeGroundTile(GroundTileViewEnum tileView) => _allGroundTiles[(int)tileView - 1];
}
