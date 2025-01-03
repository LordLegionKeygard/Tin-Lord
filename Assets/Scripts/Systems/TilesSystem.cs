using UnityEngine;

public class TilesSystem : MonoBehaviour
{
    [SerializeField] private Tile[] _allGroundTiles;
    [SerializeField] private Tile[] _allBuildingTiles;

    [Header("WorldTileInfo")]
    private bool _isHaveRiver = false;
    private bool _isHaveBase = false;
    public bool IsHaveBase() => _isHaveBase;
    public bool IsHaveRiver() => _isHaveRiver;
    public void SetIsHaveRiver(bool state) => _isHaveRiver = state;
    public void SetIsHaveBase(bool state) => _isHaveBase = state;
    public Tile TakeGroundTile(GroundTileViewEnum tileView) => _allGroundTiles[(int)tileView - 1];
    public Tile TakeBuildingTile(BuildingTileViewEnum tileView) => _allBuildingTiles[(int)tileView];

    private void Start()
    {
        CustomEvents.OnSetBase += () => _isHaveBase = true;
    }

    private void OnDestroy()
    {
        CustomEvents.OnSetBase -= () => _isHaveBase = true;
    }
}
