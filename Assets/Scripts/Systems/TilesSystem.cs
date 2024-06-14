using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesSystem : MonoBehaviour
{
    [SerializeField] private Tile[] _allTiles;

    [Header("WorldTileInfo")]
    public bool IsHaveRiver = false;

    public Tile TakeTile(TileViewEnum tileView) => _allTiles[(int)tileView];
}
