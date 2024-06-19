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
    private TileRoad _tileRoad;


    //LastRiverTile
    public bool GetLastRiverTile() => _tileRiver.IsLastRiverTile();
    public bool NeighbourHaveLastRiverTile(int number) => _neighbourTiles[number].GetLastRiverTile();

    //HaveTile
    public bool HaveTile() => _currentTile != null;
    public bool NeighbourHaveTile(int number) => _neighbourTiles[number] == null ? false : _neighbourTiles[number].HaveTile();

    //TileView
    public bool CheckTileView(TileViewEnum tileView) => _currentTile.TileView == tileView;
    public bool NeighbourTileView(int number, TileViewEnum tileView)
    {
        if (_neighbourTiles[number] == null) return false;
        if (!_neighbourTiles[number].HaveTile()) return false;
        return _neighbourTiles[number].CheckTileView(tileView);
    }

    //IsBridge
    public bool IsBridge() => _tileRiver.IsBridge();
    public bool NeighbourTileIsBridge(int number) => _neighbourTiles[number] == null ? false : _neighbourTiles[number].IsBridge();

    //IsWater
    public bool IsWaterTile() => _currentTile == null ? false : _currentTile.IsWater;
    public bool NeighbourTileIsWater(int number) => _neighbourTiles[number] == null ? false : _neighbourTiles[number].IsWaterTile();

    //Other
    public GameObject CurrentTileObjects(int number) => _currentTileObjects[number];
    public void SetTile(Tile tile) => _currentTile = tile;
    public bool IsForwardRoad() => _tileRoad.IsForwardRoad();


    public int _riverNumber = 0;

    private void Awake()
    {
        _tileView = GetComponent<TileView>();
        _tileRiver = GetComponent<TileRiver>();
        _tileRoad = GetComponent<TileRoad>();
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

        RefreshTile();
        UpdateNeighbourTiles();
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
        _riverNumber = 0;
        if (_currentTile == null) return;
        switch (_currentTile.TileView)
        {
            case TileViewEnum.Plain:
                for (int i = 0; i < _neighbourTiles.Length; i++)
                {
                    if (!IsNeedCheck(i, false)) continue;

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.Junkyard))
                    {
                        SetTile(_tilesSystem.TakeTile(TileViewEnum.Barrenland));
                        SpawnTile();
                        return;
                    }

                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.OilField))
                    {
                        SetTile(_tilesSystem.TakeTile(TileViewEnum.Barrenland));
                        SpawnTile();
                        return;
                    }

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

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.Junkyard))
                    {
                        SetTile(_tilesSystem.TakeTile(TileViewEnum.Barrenland));
                        SpawnTile();
                        return;
                    }

                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.OilField))
                    {
                        SetTile(_tilesSystem.TakeTile(TileViewEnum.Barrenland));
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
                        SetTile(_tilesSystem.TakeTile(rnd == 0 ? TileViewEnum.IronDeposits : TileViewEnum.CopperDeposits));
                        SpawnTile();
                        return;
                    }
                }
                break;

            case TileViewEnum.River:
                for (int i = 0; i < _neighbourTiles.Length; i++)
                {
                    if (!IsNeedCheck(i, false)) continue;

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.Junkyard))
                    {
                        SetTile(_tilesSystem.TakeTile(TileViewEnum.PollutedRiver));
                        SpawnTile();
                        return;
                    }

                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.OilField))
                    {
                        SetTile(_tilesSystem.TakeTile(TileViewEnum.PollutedRiver));
                        SpawnTile();
                        return;
                    }

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.Oasis))
                    {
                        SetTile(_tilesSystem.TakeTile(TileViewEnum.DesertRiver));
                        SpawnTile();
                        return;
                    }

                    if (_neighbourTiles[i].IsWaterTile())
                    {
                        _riverNumber++;
                    }
                }
                _tileRiver.PrepareRiver(_riverNumber < 2, IsForwardRoad());
                break;

            case TileViewEnum.PollutedRiver:
                for (int i = 0; i < _neighbourTiles.Length; i++)
                {
                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].IsWaterTile())
                    {
                        _riverNumber++;
                    }
                }
                _tileRiver.PrepareRiver(_riverNumber < 2, IsForwardRoad());
                break;

            case TileViewEnum.Ground:
                for (int i = 0; i < _neighbourTiles.Length; i++)
                {
                    if (!IsNeedCheck(i, false)) continue;

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.Junkyard))
                    {
                        SetTile(_tilesSystem.TakeTile(TileViewEnum.Barrenland));
                        SpawnTile();
                        return;
                    }

                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.OilField))
                    {
                        SetTile(_tilesSystem.TakeTile(TileViewEnum.Barrenland));
                        SpawnTile();
                        return;
                    }

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.Mountain))
                    {
                        SetTile(_tilesSystem.TakeTile(TileViewEnum.CoalDeposits));
                        SpawnTile();
                        return;
                    }
                }
                break;

            case TileViewEnum.Forest:
                for (int i = 0; i < _neighbourTiles.Length; i++)
                {
                    if (!IsNeedCheck(i, false)) continue;

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.Junkyard))
                    {
                        SetTile(_tilesSystem.TakeTile(TileViewEnum.DeadForest));
                        SpawnTile();
                        return;
                    }

                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.OilField))
                    {
                        SetTile(_tilesSystem.TakeTile(TileViewEnum.DeadForest));
                        SpawnTile();
                        return;
                    }
                }
                break;

            case TileViewEnum.DesertRiver:
                for (int i = 0; i < _neighbourTiles.Length; i++)
                {
                    if (!IsNeedCheck(i, false)) continue;

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.Junkyard))
                    {
                        SetTile(_tilesSystem.TakeTile(TileViewEnum.PollutedRiver));
                        SpawnTile();
                        return;
                    }

                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.OilField))
                    {
                        SetTile(_tilesSystem.TakeTile(TileViewEnum.PollutedRiver));
                        SpawnTile();
                        return;
                    }
                    
                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].IsWaterTile())
                    {
                        _riverNumber++;
                    }

                }
                _tileRiver.PrepareRiver(_riverNumber < 2, IsForwardRoad());
                break;

            case TileViewEnum.Desert:
                for (int i = 0; i < _neighbourTiles.Length; i++)
                {
                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.River))
                    {
                        SetTile(_tilesSystem.TakeTile(TileViewEnum.Oasis));
                        SpawnTile();
                        return;
                    }
                }
                break;

            case TileViewEnum.CoalDeposits:
                for (int i = 0; i < _neighbourTiles.Length; i++)
                {
                    if (!IsNeedCheck(i, false)) continue;

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.Junkyard))
                    {
                        SetTile(_tilesSystem.TakeTile(TileViewEnum.PoorCoalDeposits));
                        SpawnTile();
                        return;
                    }

                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.OilField))
                    {
                        SetTile(_tilesSystem.TakeTile(TileViewEnum.PoorCoalDeposits));
                        SpawnTile();
                        return;
                    }
                }
                break;

            case TileViewEnum.Barrenland:
                for (int i = 0; i < _neighbourTiles.Length; i++)
                {
                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.Mountain))
                    {
                        SetTile(_tilesSystem.TakeTile(TileViewEnum.PoorCoalDeposits));
                        SpawnTile();
                        return;
                    }
                }
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
