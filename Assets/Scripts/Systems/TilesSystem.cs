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
    public Tile GetGroundTileForEnum(GroundTileViewEnum tileView) => _allGroundTiles[(int)tileView - 1];
    public Tile GetGroundTileForNumber(int number) => _allGroundTiles[number - 1];
    public Tile GetBuildingTileForEnum(BuildingTileViewEnum tileView) => _allBuildingTiles[(int)tileView];
    public Tile GetBuildingTileForNumber(int number) => _allBuildingTiles[number];

    private void Start()
    {
        CustomEvents.OnSetBase += () => _isHaveBase = true;
    }

    private void OnDestroy()
    {
        CustomEvents.OnSetBase -= () => _isHaveBase = true;
    }
}
