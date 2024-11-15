using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileRiver : MonoBehaviour
{
    [Inject] private TilesSystem _tilesSystem;
    private GroundTile _groundTile;
    [SerializeField] private RiverTypeEnum _riverType = RiverTypeEnum.None;
    [SerializeField] private bool _isLastRiverTile;
    private int _riverNumber;
    private bool _isLake;
    public bool IsLastRiverTile() => _isLastRiverTile;
    [SerializeField] private bool _isBridge;
    public bool IsBridge() => _isBridge;

    private void Awake()
    {
        _groundTile = GetComponent<GroundTile>();
    }

    public void Reset()
    {
        if (_riverType == RiverTypeEnum.Lake)
        {
            _tilesSystem.IsHaveRiver = false;
            _isLake = false;
        }

        _isBridge = false;
        _isLastRiverTile = false;
        _riverType = RiverTypeEnum.None;
    }

    public void PrepareRiver(int riverNumber, bool isBridge, bool afterDestroy)
    {
        _riverNumber = riverNumber;
        _isBridge = isBridge;
        _isLastRiverTile = riverNumber < 2;

        if (!_tilesSystem.IsHaveRiver || (afterDestroy && _riverType == RiverTypeEnum.LakeExit))
        {
            _riverType = RiverTypeEnum.Lake;
            _tilesSystem.IsHaveRiver = true;
            _isLake = true;
            _groundTile.CurrentGroundTileObject().GetComponent<PrepareTileRiver>().SetRiver(_riverType, 0);
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
                _groundTile.CurrentGroundTileObject().GetComponent<PrepareTileRiver>().SetRiver(riverDirections[i].Item3, riverDirections[i].Item4);
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
                tileRiver.SetRiver(type, rotation);
                _groundTile.SetGroundModelRotation(rotation);
                break;
            }
        }
    }
}
