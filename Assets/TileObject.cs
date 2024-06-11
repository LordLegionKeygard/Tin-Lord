using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileObject : MonoBehaviour
{
    [SerializeField] private Tile _currentTile;
    [SerializeField] private TileObject[] _neighbourTiles;
    [SerializeField] private Transform[] _parents;
    [SerializeField] private GameObject[] _currentTileObjects;
    [SerializeField] private GameObject _visibleObject;
    public bool HaveTile() => _currentTile != null;
    public bool CheckTileView(TileView tileView) => _currentTile.TileView == tileView;

    private void Awake()
    {
        CustomEvents.OnPrepareRoads += PrepareRoads;
    }

    public void SetRoadTile(Tile tile)
    {
        _currentTile = tile;
        SpawnTile();
    }

    private void PrepareRoads()
    {
        _visibleObject.SetActive(false);
        if (_currentTile == null) return;
        if (_currentTile.TileView != TileView.Road) return;

        if (_neighbourTiles[(int)TileEnum.North].HaveTile() && _neighbourTiles[(int)TileEnum.South].HaveTile())
        {
            _currentTileObjects[(int)TileTypeEnum.Ground].GetComponent<PrepareTileRoad>().SetRoad(0, 0);
        }
        else if (_neighbourTiles[(int)TileEnum.East].HaveTile() && _neighbourTiles[(int)TileEnum.West].HaveTile())
        {
            _currentTileObjects[(int)TileTypeEnum.Ground].GetComponent<PrepareTileRoad>().SetRoad(0, 90);
        }

        else if (_neighbourTiles[(int)TileEnum.North].HaveTile() && _neighbourTiles[(int)TileEnum.East].HaveTile())
        {
            _currentTileObjects[(int)TileTypeEnum.Ground].GetComponent<PrepareTileRoad>().SetRoad(1, -90);
        }
        else if (_neighbourTiles[(int)TileEnum.East].HaveTile() && _neighbourTiles[(int)TileEnum.South].HaveTile())
        {
            _currentTileObjects[(int)TileTypeEnum.Ground].GetComponent<PrepareTileRoad>().SetRoad(1, 0);
        }
        else if (_neighbourTiles[(int)TileEnum.South].HaveTile() && _neighbourTiles[(int)TileEnum.West].HaveTile())
        {
            _currentTileObjects[(int)TileTypeEnum.Ground].GetComponent<PrepareTileRoad>().SetRoad(1, 90);
        }
        else if (_neighbourTiles[(int)TileEnum.West].HaveTile() && _neighbourTiles[(int)TileEnum.North].HaveTile())
        {
            _currentTileObjects[(int)TileTypeEnum.Ground].GetComponent<PrepareTileRoad>().SetRoad(1, 180);
        }
    }

    public void NeighbourTiles(TileObject[] array)
    {
        for (int i = 0; i < _neighbourTiles.Length; i++)
        {
            if (array[i] == null) continue;
            _neighbourTiles[i] = array[i];
        }
    }

    public void SpawnTile()
    {
        if (_currentTile == null) return;

        var tileNumber = (int)_currentTile.TileTypeEnum;

        if (_currentTileObjects[tileNumber] != null) Destroy(_currentTileObjects[tileNumber]);

        _currentTileObjects[tileNumber] = Instantiate(_currentTile.TileObject, _parents[tileNumber].position, Quaternion.identity);

        _currentTileObjects[tileNumber].transform.parent = _parents[tileNumber];

        _visibleObject.SetActive(false);

        UpdateNeighbourTiles();
    }

    private void UpdateNeighbourTiles()
    {
        for (int i = 0; i < _neighbourTiles.Length; i++)
        {
            _neighbourTiles[i].RefreshTile();
        }
    }

    public void RefreshTile()
    {
        if (_currentTile == null) return;
        switch (_currentTile.TileView)
        {
            case TileView.Plain:
                for (int i = 0; i < _neighbourTiles.Length; i++)
                {
                    if (_neighbourTiles[i] == null) continue;
                    if (!_neighbourTiles[i].HaveTile()) continue;

                    if (_neighbourTiles[i].CheckTileView(TileView.Mountain))
                    {
                        _currentTile = TilesSystem.Instance.TakeTile(TileView.Meadow);
                        SpawnTile();
                        return;
                    }
                }
                break;
        }
    }
    private void OnDestroy()
    {
        CustomEvents.OnPrepareRoads -= PrepareRoads;
    }
}

public enum TileEnum
{
    North = 0,
    NorthEast = 1,
    East = 2,
    SouthEast = 3,
    South = 4,
    SouthWest = 5,
    West = 6,
    NorthWest = 7
}
