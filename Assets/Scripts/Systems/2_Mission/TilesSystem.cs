using UnityEngine;

public class TilesSystem : MonoBehaviour
{
    [SerializeField] private Tile[] _allGroundTiles;
    [SerializeField] private Tile[] _allBuildingTiles;

    [Header("WorldTileInfo")]
    private bool _isHaveRiver = false;
    private int _baseLevel = 0;
    private bool _isHaveMachineProduction;
    public bool IsHaveBase() => _baseLevel > 0;
    public int GetBaseLevel() => _baseLevel;
    public void SetBaseLevel(int level) => _baseLevel = level;
    public bool IsHaveRiver() => _isHaveRiver;
    public void SetIsHaveRiver(bool state) => _isHaveRiver = state;
    public bool IsHaveMachineProduction() => _isHaveMachineProduction;
    public void SetIsHaveMachineProduction(bool state) => _isHaveMachineProduction = state;
    public Tile GetGroundTileForEnum(GroundTileViewEnum tileView) => _allGroundTiles[(int)tileView - 1];
    public Tile GetGroundTileForNumber(int number) => _allGroundTiles[number - 1];
    public Tile GetBuildingTileForEnum(BuildingTileViewEnum tileView) => _allBuildingTiles[(int)tileView];
    public Tile GetBuildingTileForNumber(int number) => _allBuildingTiles[number];

    private void Start()
    {
        CustomEvents.OnSetBase += SetBaseLevel;
    }

    private void OnDestroy()
    {
        CustomEvents.OnSetBase -= SetBaseLevel;
    }
}
