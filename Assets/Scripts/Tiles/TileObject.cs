using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class TileObject : MonoBehaviour
{
    [Inject] private DiContainer _diContainer;
    [Inject] private TilesSystem _tilesSystem;
    [SerializeField] private Tile _currentTile;
    [SerializeField] private TileObject[] _neighbourTiles;
    [SerializeField] private Transform[] _parents;
    [SerializeField] private GameObject[] _currentTileObjects;

    private TileView _tileView;
    private TileRiver _tileRiver;


    public bool NeighbourHaveLastRiverTile(int number)
    {
        return _neighbourTiles[number].GetLastRiverTile();
    }

    public bool GetLastRiverTile() => _tileRiver.IsLastRiverTile();

    public bool HaveTile() => _currentTile != null;
    public bool CheckTileView(TileViewEnum tileView) => _currentTile.TileView == tileView;
    public bool NeighbourTileView(int number, TileViewEnum tileView)
    {
        if (_neighbourTiles[number] == null) return false;
        if (!_neighbourTiles[number].HaveTile()) return false;
        return _neighbourTiles[number].CheckTileView(tileView);
    }
    public bool NeighbourHaveTile(int number) => _neighbourTiles[number] == null ? false : _neighbourTiles[number].HaveTile();
    public GameObject CurrentTileObjects(int number) => _currentTileObjects[number];
    public void SetTile(Tile tile) => _currentTile = tile;

    private void Awake()
    {
        _tileView = GetComponent<TileView>();
        _tileRiver = GetComponent<TileRiver>();
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

        _currentTileObjects[tileNumber] = _diContainer.InstantiatePrefab(_currentTile.TileObject, _parents[tileNumber].position, Quaternion.identity, null);

        _currentTileObjects[tileNumber].transform.parent = _parents[tileNumber];

        UpdateNeighbourTiles();
        RefreshTile(false, _currentTile.TileView == TileViewEnum.River);
    }

    private void UpdateNeighbourTiles()
    {
        for (int i = 0; i < _neighbourTiles.Length; i++)
        {
            if (_neighbourTiles[i] != null) _neighbourTiles[i].RefreshTile(true, _currentTile.TileView == TileViewEnum.River);
        }
    }

    public void RefreshTile(bool isNeighbours, bool isRiver)
    {
        if (_currentTile == null) return;
        switch (_currentTile.TileView)
        {
            case TileViewEnum.Plain:
                for (int i = 0; i < _neighbourTiles.Length; i++)
                {
                    if (!IsNeedCheck(i, false)) continue;

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.OilField))
                    {
                        SetTile(_tilesSystem.TakeTile(TileViewEnum.Badlands));
                        SpawnTile();
                        return;
                    }

                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.Mountain))
                    {
                        SetTile(_tilesSystem.TakeTile(TileViewEnum.Meadow));
                        SpawnTile();
                        return;
                    }
                }
                break;
            case TileViewEnum.Meadow:
                for (int i = 0; i < _neighbourTiles.Length; i++)
                {
                    if (!IsNeedCheck(i, false)) continue;

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.OilField))
                    {
                        SetTile(_tilesSystem.TakeTile(TileViewEnum.Badlands));
                        SpawnTile();
                        return;
                    }
                }
                break;
            case TileViewEnum.Highland:
                for (int i = 0; i < _neighbourTiles.Length; i++)
                {
                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.Mountain))
                    {
                        var rnd = Random.Range(0, 2);
                        SetTile(_tilesSystem.TakeTile(rnd == 0 ? TileViewEnum.IronVein : TileViewEnum.CopperVein));
                        SpawnTile();
                        return;
                    }
                }
                break;
            case TileViewEnum.River:
                _tileRiver.PrepareRiver(isNeighbours, isRiver);
                break;

        }
    }

    public void SelectTile(bool state, SelectTileEnum selectTileEnum)
    {
        _tileView.ViewToggle(state, selectTileEnum);
    }

    public bool IsNeedCheck(int i, bool cross)
    {
        if (cross)
        {
            if (i is (int)TileDirectionEnum.NorthEast or (int)TileDirectionEnum.NorthWest or (int)TileDirectionEnum.SouthEast or (int)TileDirectionEnum.SouthWest) return false;
        }

        if (_neighbourTiles[i] == null) return false;
        if (!_neighbourTiles[i].HaveTile()) return false;

        return true;
    }
}
