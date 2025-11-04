using UnityEngine;

public class TilesSystem : MonoBehaviour
{
    [SerializeField] private Card[] _allCards;
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
    public Tile GetBuildingTileForEnum(BuildingTileViewEnum tileView) => _allBuildingTiles[(int)tileView];
    public Tile GetBuildingTileForId(int id) => _allBuildingTiles[id];

    private void Start()
    {
        CustomEvents.OnSetBase += SetBaseLevel;
    }

    public Tile GetGroundTileForId(int id)
    {
        for (int i = 0; i < _allGroundTiles.Length; i++)
        {
            if (_allGroundTiles[i].Id == id) return _allGroundTiles[i];
        }
        return null;
    }

    public Card GetCardForId(int id)
    {
        for (int i = 0; i < _allCards.Length; i++)
        {
            if (_allCards[i].Id == id) return _allCards[i];
        }
        return null;
    }

    private void OnDestroy()
    {
        CustomEvents.OnSetBase -= SetBaseLevel;
    }
}
