using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesSystem : MonoBehaviour
{
    [SerializeField] private Tile[] _allGroundTiles;
    [SerializeField] private Tile[] _allBuildingTiles;

    [Header("WorldTileInfo")] //нужно будет обязательно загружать эти данные
    public bool IsHaveRiver = false;
    private bool _isHaveBase = false;
    public bool IsHaveBase() => _isHaveBase;
    public Tile TakeGroundTile(GroundTileViewEnum tileView) => _allGroundTiles[(int)tileView - 1];
    public Tile TakeBuildingTile(BuildingTileViewEnum tileView) => _allBuildingTiles[(int)tileView];

    private void Awake()
    {
        CustomEvents.OnSetBase += () => _isHaveBase = true;
    }

    private void OnDestroy()
    {
        CustomEvents.OnSetBase -= () => _isHaveBase = true;
    }
}
