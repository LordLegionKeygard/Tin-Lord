using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRoad : MonoBehaviour
{
    private TileObject _tileObject;
    public bool _isForwardRoad;
    public bool IsForwardRoad() => _isForwardRoad;
    private void Awake()
    {
        _tileObject = GetComponent<TileObject>();
        CustomEvents.OnPrepareRoads += PrepareRoads;
    }

    public void SetRoadTile(Tile tile)
    {
        _tileObject.SetTile(tile);
        _tileObject.SpawnTile();
    }

    private void PrepareRoads()
    {
        if (!_tileObject.HaveTile() || !_tileObject.CheckTileView(TileViewEnum.Road)) return;

        (TileDirectionEnum, TileDirectionEnum, int, int)[] roadDirections = new (TileDirectionEnum, TileDirectionEnum, int, int)[]
    {
        (TileDirectionEnum.North, TileDirectionEnum.South, 0, 0),
        (TileDirectionEnum.East, TileDirectionEnum.West, 0, 90),
        (TileDirectionEnum.North, TileDirectionEnum.East, 1, -90),
        (TileDirectionEnum.East, TileDirectionEnum.South, 1, 0),
        (TileDirectionEnum.South, TileDirectionEnum.West, 1, 90),
        (TileDirectionEnum.West, TileDirectionEnum.North, 1, 180)
    };

        foreach (var (dir1, dir2, roadType, angle) in roadDirections)
        {
            if (_tileObject.NeighbourHaveTile((int)dir1) && _tileObject.NeighbourHaveTile((int)dir2))
            {
                _tileObject.CurrentTileObjects((int)TileTypeEnum.Ground).GetComponent<PrepareTileRoad>().SetRoad(roadType, angle);
                _isForwardRoad = roadType == 0;

                break;
            }
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnPrepareRoads -= PrepareRoads;
    }
}
