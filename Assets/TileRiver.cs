using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileRiver : MonoBehaviour
{
    [Inject] private TilesSystem _tilesSystem;
    private TileObject _tileObject;
    private bool _lastRiverTile;
    public bool GetLastRiverTile() => _lastRiverTile;

    private void Awake()
    {
        _tileObject = GetComponent<TileObject>();
        CustomEvents.OnResetLastRiverTile += ResetLastRiverTile;
    }

    private void ResetLastRiverTile()
    {
        _lastRiverTile = false;
    }

    public void PrepareRiver()
    {
        CustomEvents.FireResetLastRiverTile();
        _lastRiverTile = true;

        if (!_tilesSystem.IsHaveRiver)
        {
            _tilesSystem.IsHaveRiver = true;
            _tileObject.CurrentTileObjects((int)TileTypeEnum.Ground).GetComponent<PrepareTileRiver>().SetRiver(0, 0);
        }
        else
        {
            if (!_tileObject.HaveTile() || !_tileObject.CheckTileView(TileViewEnum.River)) return;

            if (CheckTurn()) return;
            else CheckForward();

        }
    }

    private bool CheckTurn()
    {
        (TileDirectionEnum, TileDirectionEnum, int, int)[] riverDirections = new (TileDirectionEnum, TileDirectionEnum, int, int)[]
        {
            (TileDirectionEnum.North, TileDirectionEnum.East, 1, -90),
            (TileDirectionEnum.East, TileDirectionEnum.South, 1, 0),
            (TileDirectionEnum.South, TileDirectionEnum.West, 1, 90),
            (TileDirectionEnum.West, TileDirectionEnum.North, 1, 180),
        };

        for (int i = 0; i < riverDirections.Length; i++)
        {
            if (_tileObject.NeighbourTileView((int)riverDirections[i].Item1, TileViewEnum.River) && _tileObject.NeighbourTileView((int)riverDirections[i].Item2, TileViewEnum.River))
            {
                _tileObject.CurrentTileObjects((int)TileTypeEnum.Ground).GetComponent<PrepareTileRiver>().SetRiver(riverDirections[i].Item3, riverDirections[i].Item4);
                return true;
            }
        }
        return false;
    }

    private void CheckForward()
    {
        (TileDirectionEnum, TileDirectionEnum, int, int)[] riverDirections = new (TileDirectionEnum, TileDirectionEnum, int, int)[]
    {
            (TileDirectionEnum.North, TileDirectionEnum.South, 0, 0),
            (TileDirectionEnum.East, TileDirectionEnum.West, 0, 90),
    };

        for (int i = 0; i < riverDirections.Length; i++)
        {
            if (_tileObject.NeighbourTileView((int)riverDirections[i].Item1, TileViewEnum.River) || _tileObject.NeighbourTileView((int)riverDirections[i].Item2, TileViewEnum.River))
            {
                _tileObject.CurrentTileObjects((int)TileTypeEnum.Ground).GetComponent<PrepareTileRiver>().SetRiver(riverDirections[i].Item3, riverDirections[i].Item4);
                break;
            }
        }
    }


    private void OnDestroy()
    {
        CustomEvents.OnResetLastRiverTile -= ResetLastRiverTile;
    }

}
