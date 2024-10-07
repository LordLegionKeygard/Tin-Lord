using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRoad : MonoBehaviour
{
    private GroundTile _groundTile;
    private bool _isForwardRoad;
    public bool IsForwardRoad() => _isForwardRoad;
    private int _roadAngle;
    public int RoadAngle() => _roadAngle;

    private void Awake()
    {
        _groundTile = GetComponent<GroundTile>();
        CustomEvents.OnSpawnRoadComplete += PrepareRoads;
    }

    public void SetRoadTile(Tile tile)
    {
        _groundTile.SetGroundTile(tile);
        _groundTile.SpawnGroundTile();
    }

    private void PrepareRoads()
    {
        if (!_groundTile.HaveTile() || !_groundTile.CheckTileView(GroundTileViewEnum.Road)) return;

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
            if (_groundTile.NeighbourGroundTile((int)dir1).CheckTileView(GroundTileViewEnum.Road) && _groundTile.NeighbourGroundTile((int)dir2).CheckTileView(GroundTileViewEnum.Road))
            {
                _groundTile.CurrentGroundTileObject().GetComponent<PrepareTileRoad>().SetRoad(roadType, angle);

                _isForwardRoad = roadType == 0;
                _roadAngle = angle;
                break;
            }
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnSpawnRoadComplete -= PrepareRoads;
    }
}
