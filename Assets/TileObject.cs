using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileObject : MonoBehaviour
{
    [SerializeField] private Tile _currentTile;
    [SerializeField] private TileObject[] _neighbourTiles;
    [SerializeField] private Transform[] _parents;
    [SerializeField] private GameObject[] _currentTiles;
    [SerializeField] private GameObject _visibleObject;
    public bool HaveTile() => _currentTile != null;
    public bool CheckTileView(TileView tileView) => _currentTile.TileView == tileView;
    public void SetRoadTile(Tile tile, int number)
    {
        _currentTile = tile;
        SpawnTile();
        _currentTiles[0].transform.rotation = Quaternion.Euler(0, number, 0);
    }

    private void Awake()
    {
        CustomEvents.OnSpawnAllTiles += SpawnTile;
    }

    private void OnDestroy()
    {
        CustomEvents.OnSpawnAllTiles -= SpawnTile;
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

        if (_currentTiles[tileNumber] != null) Destroy(_currentTiles[tileNumber]);

        _currentTiles[tileNumber] = Instantiate(_currentTile.TileObject, _parents[tileNumber].position, Quaternion.identity);

        _currentTiles[tileNumber].transform.parent = _parents[tileNumber];

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
