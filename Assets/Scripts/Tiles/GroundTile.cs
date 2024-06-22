using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class GroundTile : MonoBehaviour
{
    [Inject] private DiContainer _diContainer;
    [Inject] private TilesSystem _tilesSystem;
    [SerializeField] private Tile _currentGroundTile;
    [SerializeField] private GroundTile[] _neighbourTiles;
    [SerializeField] private Transform _groundParent;
    [SerializeField] private GameObject _currentGroundTileObject;

    private TileView _tileView;
    private TileRiver _tileRiver;
    private TileRoad _tileRoad;


    //LastRiverTile
    public bool GetLastRiverTile() => _tileRiver.IsLastRiverTile();
    public bool NeighbourHaveLastRiverTile(int number) => _neighbourTiles[number].GetLastRiverTile();

    //HaveTile
    public bool HaveTile() => _currentGroundTile != null;
    public bool NeighbourHaveTile(int number) => _neighbourTiles[number] == null ? false : _neighbourTiles[number].HaveTile();

    //TileView
    public bool CheckTileView(TileViewEnum tileView) => _currentGroundTile.TileView == tileView;
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
    public bool IsWaterTile() => _currentGroundTile == null ? false : _currentGroundTile.IsWater;
    public bool NeighbourTileIsWater(int number) => _neighbourTiles[number] == null ? false : _neighbourTiles[number].IsWaterTile();

    //Other
    public GameObject CurrentGroundTileObject() => _currentGroundTileObject;
    public void SetGroundTile(Tile tile) => _currentGroundTile = tile;
    public bool IsForwardRoad() => _tileRoad.IsForwardRoad();


    public int _riverNumber = 0;

    private void Awake()
    {
        _tileView = GetComponent<TileView>();
        _tileRiver = GetComponent<TileRiver>();
        _tileRoad = GetComponent<TileRoad>();
    }


    public void NeighbourTiles(GroundTile[] array)
    {
        for (int i = 0; i < _neighbourTiles.Length; i++)
        {
            if (array[i] == null) continue;
            _neighbourTiles[i] = array[i];
        }
    }

    public void SpawnGroundTile()
    {
        if (_currentGroundTile == null) return;

        if (_currentGroundTileObject != null) Destroy(_currentGroundTileObject);

        _currentGroundTileObject = _diContainer.InstantiatePrefab(_currentGroundTile.TileObject, _groundParent.position, Quaternion.identity, null);

        _currentGroundTileObject.transform.parent = _groundParent;

        RefreshGrodunTile();
        UpdateNeighbourGrodunTiles();
    }

    private void UpdateNeighbourGrodunTiles()
    {
        for (int i = 0; i < _neighbourTiles.Length; i++)
        {
            if (_neighbourTiles[i] != null) _neighbourTiles[i].RefreshGrodunTile();
        }
    }

    public void RefreshGrodunTile()
    {
        _riverNumber = 0;
        if (_currentGroundTile == null) return;
        switch (_currentGroundTile.TileView)
        {
            case TileViewEnum.Plain:
                for (int i = 0; i < _neighbourTiles.Length; i++)
                {
                    if (!IsNeedCheck(i, false)) continue;

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.Junkyard))
                    {
                        SetGroundTile(_tilesSystem.TakeTile(TileViewEnum.Barrenland));
                        SpawnGroundTile();
                        return;
                    }

                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.OilField))
                    {
                        SetGroundTile(_tilesSystem.TakeTile(TileViewEnum.Barrenland));
                        SpawnGroundTile();
                        return;
                    }

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.Mountain))
                    {
                        SetGroundTile(_tilesSystem.TakeTile(TileViewEnum.Meadow));
                        SpawnGroundTile();
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
                        SetGroundTile(_tilesSystem.TakeTile(TileViewEnum.Barrenland));
                        SpawnGroundTile();
                        return;
                    }

                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.OilField))
                    {
                        SetGroundTile(_tilesSystem.TakeTile(TileViewEnum.Barrenland));
                        SpawnGroundTile();
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
                        SetGroundTile(_tilesSystem.TakeTile(rnd == 0 ? TileViewEnum.IronDeposits : TileViewEnum.CopperDeposits));
                        SpawnGroundTile();
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
                        SetGroundTile(_tilesSystem.TakeTile(TileViewEnum.PollutedRiver));
                        SpawnGroundTile();
                        return;
                    }

                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.OilField))
                    {
                        SetGroundTile(_tilesSystem.TakeTile(TileViewEnum.PollutedRiver));
                        SpawnGroundTile();
                        return;
                    }

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.Oasis))
                    {
                        SetGroundTile(_tilesSystem.TakeTile(TileViewEnum.DesertRiver));
                        SpawnGroundTile();
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
                        SetGroundTile(_tilesSystem.TakeTile(TileViewEnum.Barrenland));
                        SpawnGroundTile();
                        return;
                    }

                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.OilField))
                    {
                        SetGroundTile(_tilesSystem.TakeTile(TileViewEnum.Barrenland));
                        SpawnGroundTile();
                        return;
                    }

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.Mountain))
                    {
                        SetGroundTile(_tilesSystem.TakeTile(TileViewEnum.CoalDeposits));
                        SpawnGroundTile();
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
                        SetGroundTile(_tilesSystem.TakeTile(TileViewEnum.DeadForest));
                        SpawnGroundTile();
                        return;
                    }

                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.OilField))
                    {
                        SetGroundTile(_tilesSystem.TakeTile(TileViewEnum.DeadForest));
                        SpawnGroundTile();
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
                        SetGroundTile(_tilesSystem.TakeTile(TileViewEnum.PollutedRiver));
                        SpawnGroundTile();
                        return;
                    }

                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.OilField))
                    {
                        SetGroundTile(_tilesSystem.TakeTile(TileViewEnum.PollutedRiver));
                        SpawnGroundTile();
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
                        SetGroundTile(_tilesSystem.TakeTile(TileViewEnum.Oasis));
                        SpawnGroundTile();
                        return;
                    }

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.DesertRiver))
                    {
                        SetGroundTile(_tilesSystem.TakeTile(TileViewEnum.Oasis));
                        SpawnGroundTile();
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
                        SetGroundTile(_tilesSystem.TakeTile(TileViewEnum.ScarceCoalDeposits));
                        SpawnGroundTile();
                        return;
                    }

                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].CheckTileView(TileViewEnum.OilField))
                    {
                        SetGroundTile(_tilesSystem.TakeTile(TileViewEnum.ScarceCoalDeposits));
                        SpawnGroundTile();
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
                        SetGroundTile(_tilesSystem.TakeTile(TileViewEnum.ScarceCoalDeposits));
                        SpawnGroundTile();
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
