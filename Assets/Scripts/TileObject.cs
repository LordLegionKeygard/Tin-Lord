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



    public bool HaveTile() => _currentTile != null;
    public bool CheckTileView(TileViewEnum tileView) => _currentTile.TileView == tileView;
    public bool NeighbourHaveTile(int number) => _neighbourTiles[number].HaveTile();
    public GameObject CurrentTileObjects(int number) => _currentTileObjects[number];
    public void SetTile(Tile tile) => _currentTile = tile;

    private void Awake()
    {
        _tileView = GetComponent<TileView>();
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
        RefreshTile();
    }

    private void UpdateNeighbourTiles()
    {
        for (int i = 0; i < _neighbourTiles.Length; i++)
        {
            if (_neighbourTiles[i] != null) _neighbourTiles[i].RefreshTile();
        }
    }

    public void RefreshTile()
    {
        if (_currentTile == null) return;
        switch (_currentTile.TileView)
        {
            case TileViewEnum.Plain:
                for (int i = 0; i < _neighbourTiles.Length; i++)
                {
                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.Mountain))
                    {
                        SetTile(_tilesSystem.TakeTile(TileViewEnum.Meadow));
                        SpawnTile();
                        return;
                    }
                }
                break;
        }
    }

    public void SelectTile(bool state, TileTypeEnum tileTypeEnum)
    {
        _tileView.ViewToggle(state, tileTypeEnum);
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
