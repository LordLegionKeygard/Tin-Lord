using UnityEngine;
using Zenject;

public class TileRiver : MonoBehaviour
{
    [Inject] private readonly TilesSystem _tilesSystem;
    private GroundTile _groundTile;
    [SerializeField] private bool _isLake;
    private int _riverNumber;
    private bool _isBridge;
    [SerializeField] private bool _isLastRiverTile;
    private RiverTypeEnum _riverType = RiverTypeEnum.None;
    private int _riverRotation;
    public bool IsLake() => _isLake;
    public int GetRiverNumber() => _riverNumber;
    public bool IsBridge() => _isBridge;
    public bool IsLastRiverTile() => _isLastRiverTile;
    public RiverTypeEnum GetRiverTypeEnum() => _riverType;
    public int GetRiverRotation() => _riverRotation;

    private void Awake()
    {
        _groundTile = GetComponent<GroundTile>();
    }

    public void Reset()
    {
        if (_riverType == RiverTypeEnum.Lake)
        {
            _tilesSystem.SetIsHaveRiver(false);
            _isLake = false;
        }

        _riverNumber = 0;
        _isBridge = false;
        _isLastRiverTile = false;
        _riverType = RiverTypeEnum.None;
        _riverRotation = 0;
    }

    public void LoadRiver(TileDataWrapper data)
    {
        _isLake = data.WaterData.IsLake;
        _riverNumber = data.WaterData.RiverNumber;
        _isBridge = data.WaterData.IsBridge;
        _isLastRiverTile = data.WaterData.IsLastRiverTile;
        _riverType = (RiverTypeEnum)data.WaterData.RiverType;
        _riverRotation = data.WaterData.RiverRotation;

        _groundTile.CurrentGroundTileObject().GetComponent<PrepareTileRiver>().SetRiver(_riverType, _riverRotation);
    }

    public void PrepareRiver(int riverNumber, bool isBridge, bool afterDestroy)
    {
        _riverNumber = riverNumber;
        _isBridge = isBridge;
        _isLastRiverTile = riverNumber < 2;

        if (!_tilesSystem.IsHaveRiver() || (afterDestroy && _riverType == RiverTypeEnum.LakeExit))
        {
            _riverType = RiverTypeEnum.Lake;
            _tilesSystem.SetIsHaveRiver(true);
            _isLake = true;
            _riverRotation = 0;
            _groundTile.CurrentGroundTileObject().GetComponent<PrepareTileRiver>().SetRiver(_riverType, _riverRotation);
        }
        else
        {
            if (!_groundTile.HaveTile() || !_groundTile.IsWaterTile())
            {
                return;
            }

            if (CheckTurn() && !afterDestroy)
            {
                return;
            }

            else CheckForward();
        }
    }

    private bool CheckTurn()
    {
        _riverType = _isLake ? RiverTypeEnum.LakeExit : RiverTypeEnum.RiverTurn;
        (TileDirectionEnum, TileDirectionEnum, RiverTypeEnum, int)[] riverDirections = new (TileDirectionEnum, TileDirectionEnum, RiverTypeEnum, int)[]
        {
            (TileDirectionEnum.North, TileDirectionEnum.East, _riverType, -90),
            (TileDirectionEnum.East, TileDirectionEnum.South, _riverType, 0),
            (TileDirectionEnum.South, TileDirectionEnum.West, _riverType, 90),
            (TileDirectionEnum.West, TileDirectionEnum.North, _riverType, 180),
        };

        for (int i = 0; i < riverDirections.Length; i++)
        {
            if (_groundTile.NeighbourTileIsWater((int)riverDirections[i].Item1) && _groundTile.NeighbourTileIsWater((int)riverDirections[i].Item2))
            {
                _riverRotation = riverDirections[i].Item4;
                _groundTile.CurrentGroundTileObject().GetComponent<PrepareTileRiver>().SetRiver(riverDirections[i].Item3, _riverRotation);
                return true;
            }
        }
        return false;
    }


    private void CheckForward()
    {
        _riverType = CheckRiverType();

        if (_riverType == RiverTypeEnum.LakeExit) _isLastRiverTile = false;
        if (_isBridge) _riverType = _isLastRiverTile ? RiverTypeEnum.RiverBridgeEnd : RiverTypeEnum.RiverBridge;

        SetRiverToTile();
    }

    private RiverTypeEnum CheckRiverType()
    {
        if (_isLake && _riverNumber == 0)
        {
            return RiverTypeEnum.Lake;
        }

        return _isLake ? RiverTypeEnum.LakeExit : _isLastRiverTile ? RiverTypeEnum.RiverEnd : RiverTypeEnum.RiverForward;
    }

    private void SetRiverToTile()
    {
        (TileDirectionEnum, RiverTypeEnum, int)[] riverDirections = new (TileDirectionEnum, RiverTypeEnum, int)[]
        {
            (TileDirectionEnum.North, _riverType, 180),
            (TileDirectionEnum.East, _riverType, -90),
            (TileDirectionEnum.South, _riverType, 0),
            (TileDirectionEnum.West, _riverType, 90),
        };

        foreach (var (direction, type, rotation) in riverDirections)
        {
            if (_groundTile.NeighbourTileIsWater((int)direction) || _riverType == RiverTypeEnum.Lake)
            {
                var tileRiver = _groundTile.CurrentGroundTileObject().GetComponent<PrepareTileRiver>();
                _riverRotation = rotation;
                tileRiver.SetRiver(type, _riverRotation);
                _groundTile.SetGroundModelRotation(_riverRotation);
                break;
            }
        }
    }
}
