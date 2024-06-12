using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesSystem : MonoBehaviour
{
    [SerializeField] private Tile[] _allTiles;

    public Tile TakeTile(TileViewEnum tileView) => _allTiles[(int)tileView];
}
