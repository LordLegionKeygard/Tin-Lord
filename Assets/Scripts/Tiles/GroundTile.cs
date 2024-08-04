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
    [SerializeField] private TileObject _tileObject;
    private int _groundModelRotation;
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
    public bool CheckTileView(GroundTileViewEnum tileView) => _currentGroundTile != null ? _currentGroundTile.GroundTileView == tileView : false;
    public bool NeighbourTileView(int number, GroundTileViewEnum tileView)
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

    //ModelRotation
    public int GroundModelRotation() => _groundModelRotation;
    public void SetGroundModelRotation(int rotation) => _groundModelRotation = rotation;
    //Other
    public GameObject CurrentGroundTileObject() => _currentGroundTileObject;
    public void SetGroundTile(Tile tile) => _currentGroundTile = tile;
    public bool IsForwardRoad() => _tileRoad.IsForwardRoad();
    public Tile CurrentGroundTile() => _currentGroundTile;
    public GroundTile NeighbourGroundTile(int number) => _neighbourTiles[number];
    public void TurnOffTileCollider() => _tileView.TurnOffCollider();
    public void SetId(int id) => _tileObject.SetId(id);

    public void DestroyGroundTile()
    {
        _currentGroundTile = null;
        _groundModelRotation = 0;
        CustomEvents.FireChangeEcology(0, _tileObject.GetId(), true);
        SelectTile(false, SelectTileEnum.TileSelect);
        Destroy(_currentGroundTileObject);
    }

    public void TurnOffFourTileNeighboursCollider()
    {
        _neighbourTiles[0].TurnOffTileCollider();
        _neighbourTiles[1].TurnOffTileCollider();
        _neighbourTiles[2].TurnOffTileCollider();
    }



    public int _riverNumber = 0;

    private void Awake()
    {
        _tileView = GetComponent<TileView>();
        _tileRiver = GetComponent<TileRiver>();
        _tileRoad = GetComponent<TileRoad>();
    }

    public void SetNeighbourTiles(GroundTile[] array)
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

        if (_currentGroundTileObject != null)
        {
            _groundModelRotation = 0;
            Destroy(_currentGroundTileObject);
        }

        _currentGroundTileObject = _diContainer.InstantiatePrefab(_currentGroundTile.TileObject, _groundParent.position, Quaternion.identity, null);

        _currentGroundTileObject.transform.SetParent(_groundParent);

        RefreshGroundTile();
        UpdateNeighbourGroundTiles();
        _tileView.SetTileView(_currentGroundTileObject.transform, _currentGroundTile);
        CustomEvents.FireChangeEcology(_tileObject.TileEcology().GetEcology(GetEcologyEnum.Total), _tileObject.GetId(), false);
        _tileObject.ChangeResourceExtraction();
        _tileObject.SetResourceModifier();
    }

    private void UpdateNeighbourGroundTiles()
    {
        for (int i = 0; i < _neighbourTiles.Length; i++)
        {
            if (_neighbourTiles[i] != null) _neighbourTiles[i].RefreshGroundTile();
        }
    }

    public void RefreshGroundTile()
    {

        _riverNumber = 0;
        if (_currentGroundTile == null) return;
        switch (_currentGroundTile.GroundTileView)
        {
            case GroundTileViewEnum.Plain:
                for (int i = 0; i < _neighbourTiles.Length; i++)
                {
                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].CheckTileView(GroundTileViewEnum.OilField))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.Barrenland));
                        SpawnGroundTile();
                        return;
                    }

                    if (_neighbourTiles[i].CheckTileView(GroundTileViewEnum.Mountain))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.Meadow));
                        SpawnGroundTile();
                        return;
                    }
                }
                break;

            case GroundTileViewEnum.Meadow:
                for (int i = 0; i < _neighbourTiles.Length; i++)
                {
                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].CheckTileView(GroundTileViewEnum.OilField))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.Barrenland));
                        SpawnGroundTile();
                        return;
                    }
                }
                break;

            case GroundTileViewEnum.Highland:
                for (int i = 0; i < _neighbourTiles.Length; i++)
                {
                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].CheckTileView(GroundTileViewEnum.Mountain))
                    {
                        var rnd = Random.Range(0, 2);
                        SetGroundTile(_tilesSystem.TakeGroundTile(rnd == 0 ? GroundTileViewEnum.IronDeposits : GroundTileViewEnum.CopperDeposits));
                        SpawnGroundTile();
                        return;
                    }
                }
                break;

            case GroundTileViewEnum.River:
                for (int i = 0; i < _neighbourTiles.Length; i++)
                {
                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].CheckTileView(GroundTileViewEnum.OilField))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.PollutedRiver));
                        SpawnGroundTile();
                        return;
                    }

                    if (_neighbourTiles[i].CheckTileView(GroundTileViewEnum.Oasis))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.DesertRiver));
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

            case GroundTileViewEnum.PollutedRiver:
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

            case GroundTileViewEnum.Ground:
                for (int i = 0; i < _neighbourTiles.Length; i++)
                {
                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].CheckTileView(GroundTileViewEnum.OilField))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.Barrenland));
                        SpawnGroundTile();
                        return;
                    }

                    if (_neighbourTiles[i].CheckTileView(GroundTileViewEnum.Mountain))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.CoalDeposits));
                        SpawnGroundTile();
                        return;
                    }
                }
                break;

            case GroundTileViewEnum.Forest:
                for (int i = 0; i < _neighbourTiles.Length; i++)
                {
                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].CheckTileView(GroundTileViewEnum.OilField))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.DeadForest));
                        SpawnGroundTile();
                        return;
                    }
                }
                break;

            case GroundTileViewEnum.DesertRiver:
                for (int i = 0; i < _neighbourTiles.Length; i++)
                {
                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].CheckTileView(GroundTileViewEnum.OilField))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.PollutedRiver));
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

            case GroundTileViewEnum.Desert:
                for (int i = 0; i < _neighbourTiles.Length; i++)
                {
                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].CheckTileView(GroundTileViewEnum.OilField))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.BlackDesert));
                        SpawnGroundTile();
                        return;
                    }

                    if (_neighbourTiles[i].CheckTileView(GroundTileViewEnum.River))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.Oasis));
                        SpawnGroundTile();
                        return;
                    }

                    if (_neighbourTiles[i].CheckTileView(GroundTileViewEnum.DesertRiver))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.Oasis));
                        SpawnGroundTile();
                        return;
                    }
                }
                break;

            case GroundTileViewEnum.CoalDeposits:
                for (int i = 0; i < _neighbourTiles.Length; i++)
                {
                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].CheckTileView(GroundTileViewEnum.OilField))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.ScarceCoalDeposits));
                        SpawnGroundTile();
                        return;
                    }
                }
                break;

            case GroundTileViewEnum.Barrenland:
                for (int i = 0; i < _neighbourTiles.Length; i++)
                {
                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].CheckTileView(GroundTileViewEnum.Mountain))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.ScarceCoalDeposits));
                        SpawnGroundTile();
                        return;
                    }
                }
                break;
            case GroundTileViewEnum.Oasis:
                for (int i = 0; i < _neighbourTiles.Length; i++)
                {
                    if (!IsNeedCheck(i, true)) continue;

                    if (_neighbourTiles[i].CheckTileView(GroundTileViewEnum.OilField))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.BlackDesert));
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
