using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileRiver : MonoBehaviour
{
    [Inject] private TilesSystem _tilesSystem;
    private TileObject _tileObject;
    public bool _isLastRiverTile;
    private bool _isLake;
    public bool IsLastRiverTile() => _isLastRiverTile;
    public bool _isBridge;
    public bool IsBridge() => _isBridge;

    private void Awake()
    {
        _tileObject = GetComponent<TileObject>();
    }

    public void PrepareRiver(bool isLastRiverTile, bool isBridge)
    {
        _isBridge = isBridge;
        _isLastRiverTile = isLastRiverTile;

        if (!_tilesSystem.IsHaveRiver)
        {

            _tilesSystem.IsHaveRiver = true;
            _isLake = true;
            _tileObject.CurrentTileObjects((int)TileTypeEnum.Ground).GetComponent<PrepareTileRiver>().SetRiver(RiverTypeEnum.Lake, 0);
        }
        else
        {
            if (!_tileObject.HaveTile() || !_tileObject.IsWaterTile()) return;

            if (CheckTurn()) return;
            else CheckForward(isBridge);

        }
    }

    private bool CheckTurn()
    {
        var riverType = _isLake ? RiverTypeEnum.LakeExit : RiverTypeEnum.RiverTurn;
        (TileDirectionEnum, TileDirectionEnum, RiverTypeEnum, int)[] riverDirections = new (TileDirectionEnum, TileDirectionEnum, RiverTypeEnum, int)[]
        {
            (TileDirectionEnum.North, TileDirectionEnum.East, riverType, -90),
            (TileDirectionEnum.East, TileDirectionEnum.South, riverType, 0),
            (TileDirectionEnum.South, TileDirectionEnum.West, riverType, 90),
            (TileDirectionEnum.West, TileDirectionEnum.North, riverType, 180),
        };

        for (int i = 0; i < riverDirections.Length; i++)
        {
            if (_tileObject.NeighbourTileIsWater((int)riverDirections[i].Item1) && _tileObject.NeighbourTileIsWater((int)riverDirections[i].Item2))
            {
                _tileObject.CurrentTileObjects((int)TileTypeEnum.Ground).GetComponent<PrepareTileRiver>().SetRiver(riverDirections[i].Item3, riverDirections[i].Item4);
                return true;
            }
        }
        return false;
    }

    private void CheckForward(bool isBridge)
    {
        var riverType = _isLake ? RiverTypeEnum.LakeExit : _isLastRiverTile ? RiverTypeEnum.RiverEnd : RiverTypeEnum.RiverForward;
        if(riverType == RiverTypeEnum.LakeExit) _isLastRiverTile = false;
        if (isBridge) riverType = RiverTypeEnum.RiverBridge;
        (TileDirectionEnum, RiverTypeEnum, int)[] riverDirections = new (TileDirectionEnum, RiverTypeEnum, int)[]
        {
            (TileDirectionEnum.North, riverType, 180),
            (TileDirectionEnum.East, riverType, -90),
            (TileDirectionEnum.South, riverType, 0),
            (TileDirectionEnum.West, riverType, 90),
        };

        for (int i = 0; i < riverDirections.Length; i++)
        {
            if (_tileObject.NeighbourTileIsWater((int)riverDirections[i].Item1))
            {
                _tileObject.CurrentTileObjects((int)TileTypeEnum.Ground).GetComponent<PrepareTileRiver>().SetRiver(riverDirections[i].Item2, riverDirections[i].Item3);
                break;
            }
        }
    }
}
