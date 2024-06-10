using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesSystem : MonoBehaviour
{
    public static TilesSystem Instance;
    [SerializeField] private Tile[] _allTiles;

    private void Awake()
    {
        if (Instance != null) Debug.Log("Two, or more TilesSystem Instances");
        else Instance = this;
    }

    public Tile TakeTile(TileView tileView) => _allTiles[(int)tileView];
}
