using System.Collections;
using UnityEngine;
using Zenject;


public class GroundTile : MonoBehaviour
{
    [Inject] private DiContainer _diContainer;
    [Inject] private TilesSystem _tilesSystem;
    [SerializeField] private Transform _groundParent;
    [SerializeField] private TileObject _tileObject;
    private Tile _currentGroundTile;
    private GameObject _currentGroundTileObject;
    private int _groundModelRotation;
    private TileView _tileView;
    private TileRiver _tileRiver;
    private TileRoad _tileRoad;

    [SerializeField] private GameObject _laserDestructionVFX;


    //LastRiverTile
    public TileRiver CurrentTileRiver() => _tileRiver;
    public bool GetLastRiverTile() => _tileRiver.IsLastRiverTile();
    public bool NeighbourHaveLastRiverTile(int number) => _tileObject.GetNeighbourGroundTile(number).GetLastRiverTile();

    //HaveTile
    public bool HaveTile() => _currentGroundTile != null;
    public bool NeighbourHaveGroundTile(int number) => _tileObject.GetNeighbourGroundTile(number) == null ? false : _tileObject.GetNeighbourGroundTile(number).HaveTile();
    public bool HaveNeighbour(int number) => _tileObject.GetNeighbourGroundTile(number) == null ? false : true;

    //TileView
    public bool CheckTileView(GroundTileViewEnum tileView) => _currentGroundTile != null ? _currentGroundTile.GroundTileView == tileView : false;
    public bool NeighbourTileView(int number, GroundTileViewEnum tileView)
    {
        if (_tileObject.GetNeighbourGroundTile(number) == null) return false;
        if (!_tileObject.GetNeighbourGroundTile(number).HaveTile()) return false;
        return _tileObject.GetNeighbourGroundTile(number).CheckTileView(tileView);
    }

    //IsBridge
    public bool IsBridge() => _tileRiver.IsBridge();
    public bool NeighbourTileIsBridge(int number) => _tileObject.GetNeighbourGroundTile(number) == null ? false : _tileObject.GetNeighbourGroundTile(number).IsBridge();

    //IsWater
    public bool IsWaterTile() => _currentGroundTile == null ? false : _currentGroundTile.IsWater;
    public bool NeighbourTileIsWater(int number) => _tileObject.GetNeighbourGroundTile(number) == null ? false : _tileObject.GetNeighbourGroundTile(number).IsWaterTile();

    //ModelRotation
    public int GroundModelRotation() => _groundModelRotation;
    public void SetGroundModelRotation(int rotation) => _groundModelRotation = rotation;

    //Road
    public bool IsForwardRoad() => _tileRoad.IsForwardRoad();
    public int GetRoadAngle() => _tileRoad.RoadAngle();


    //Other
    public bool IsHaveBuildingTypes() => _currentGroundTile.BuildingTypes.Length > 0;
    public GameObject CurrentGroundTileObject() => _currentGroundTileObject;
    public void SetGroundTile(Tile tile) => _currentGroundTile = tile;
    public Tile CurrentGroundTile() => _currentGroundTile;
    public GroundTile NeighbourGroundTile(int number) => _tileObject.GetNeighbourGroundTile(number);
    public void TurnOffTileCollider() => _tileView.TurnOffCollider();
    public void SetId(int id) => _tileObject.SetId(id);
    private int _riverNumber = 0;


    private void Awake()
    {
        _tileView = GetComponent<TileView>();
        _tileRiver = GetComponent<TileRiver>();
        _tileRoad = GetComponent<TileRoad>();
    }

    private void RefreshWaterNeighbourTiles()
    {
        for (int i = 0; i < _tileObject.GetNeighbourGroundTilesArray().Length; i++)
        {
            if (!IsCheckTiles(i, true)) continue;

            if (_tileObject.GetNeighbourGroundTile(i).IsWaterTile())
            {
                _tileObject.GetNeighbourGroundTile(i).CurrentTileRiver().PrepareRiver(0, _tileObject.GetNeighbourGroundTile(i).CurrentTileRiver().IsBridge(), true);
            }
        }
    }

    public void DestroyGroundTile()
    {
        _tileObject.ToggleIsGroundDestroyedNow(true);
        if (GetLastRiverTile())
        {
            _tileRiver.Reset();
            _currentGroundTile = null; // иначе река не туда повернет, так соседа IsWater найдет в цикле

            RefreshWaterNeighbourTiles();
        }

        _currentGroundTile = null;
        _groundModelRotation = 0;
        CustomEvents.FireChangeEcology(0, _tileObject.GetId(), true);
        SelectTile(false);

        if (IsForwardRoad())
        {
            _tileRoad.SetRoadTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.Road));
            CustomEvents.FireSpawnRoadComplete();
        }
        else
        {
            StartCoroutine(nameof(DestroyViewCoroutine));
        }
    }

    private IEnumerator DestroyViewCoroutine()
    {
        Instantiate(_laserDestructionVFX, transform.position, Quaternion.identity);
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.LaserDestruction, transform.position);

        float delay = 0.2f;
        float elapsed = 0f;

        while (elapsed < delay)
        {
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        _tileView.PlayAnimation(TileAnimationsEnum.Destroy, () =>
        {
            Destroy(_currentGroundTileObject);
            transform.localScale = Vector3.one * 1;
            _tileObject.ToggleIsGroundDestroyedNow(false);
        });
    }


    public void TurnOffFourTileNeighboursCollider()
    {
        _tileObject.GetNeighbourGroundTile(0).TurnOffTileCollider();
        _tileObject.GetNeighbourGroundTile(1).TurnOffTileCollider();
        _tileObject.GetNeighbourGroundTile(2).TurnOffTileCollider();
    }

    public void SpawnGroundTile(GroundTileViewEnum groundTileViewEnum = GroundTileViewEnum.None)
    {
        if (_currentGroundTile == null) return;

        if (_currentGroundTileObject != null)
        {
            _groundModelRotation = 0;
            Destroy(_currentGroundTileObject);
        }

        _currentGroundTileObject = _diContainer.InstantiatePrefab(_currentGroundTile.TileObject, _groundParent.position, Quaternion.identity, null);

        _currentGroundTileObject.transform.SetParent(_groundParent);

        if (groundTileViewEnum != GroundTileViewEnum.None) _currentGroundTileObject.GetComponent<RiftSetTileMaterial>().SetMaterial(groundTileViewEnum); //для рифта передаем прошлый тайл

        RefreshGroundTile();
        UpdateNeighbourGroundTiles();
        _tileView.SetTileView(_currentGroundTileObject.transform, _currentGroundTile);
        _tileView.PlayAnimation(TileAnimationsEnum.Spawn);
        CustomEvents.FireChangeEcology(_tileObject.TileEcology().GetEcology(GetEcologyEnum.Total), _tileObject.GetId(), false);
        _tileObject.ChangeResourceProduction();
        _tileObject.SetResourceModifier();
    }

    private void UpdateNeighbourGroundTiles()
    {
        for (int i = 0; i < _tileObject.GetNeighbourGroundTilesArray().Length; i++)
        {
            if (_tileObject.GetNeighbourGroundTile(i) != null)
            {
                _tileObject.GetNeighbourGroundTile(i).RefreshGroundTile();
            }
        }
    }

    public void RefreshGroundTile()
    {
        _riverNumber = 0;
        if (_currentGroundTile == null) return;
        switch (_currentGroundTile.GroundTileView)
        {
            case GroundTileViewEnum.Plain:
                for (int i = 0; i < _tileObject.GetNeighbourGroundTilesArray().Length; i++)
                {
                    if (!IsCheckTiles(i, true)) continue;

                    if (_tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.Volcano))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.BlazingField));
                        SpawnGroundTile();
                        return;
                    }

                    if (_tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.OilSwamp) ||
                        _tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.PollutedRiver))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.Barrenland));
                        SpawnGroundTile();
                        return;
                    }

                    if (_tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.Mountain) ||
                        _tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.Forest))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.Meadow));
                        SpawnGroundTile();
                        return;
                    }

                    if (_tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.Barrenland) ||
                        _tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.BlackDesert))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.Ground));
                        SpawnGroundTile();
                        return;
                    }
                }
                break;

            case GroundTileViewEnum.Meadow:
                for (int i = 0; i < _tileObject.GetNeighbourGroundTilesArray().Length; i++)
                {
                    if (!IsCheckTiles(i, true)) continue;

                    if (_tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.Volcano))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.BlazingField));
                        SpawnGroundTile();
                        return;
                    }

                    if (_tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.OilSwamp) ||
                        _tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.PollutedRiver))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.Barrenland));
                        SpawnGroundTile();
                        return;
                    }

                    if (_tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.Barrenland) ||
                        _tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.BlackDesert))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.Ground));
                        SpawnGroundTile();
                        return;
                    }
                }
                break;

            case GroundTileViewEnum.Highland:
                for (int i = 0; i < _tileObject.GetNeighbourGroundTilesArray().Length; i++)
                {
                    if (!IsCheckTiles(i, true)) continue;

                    if (_tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.Volcano))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.BlazingField));
                        SpawnGroundTile();
                        return;
                    }

                    if (_tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.Mountain))
                    {
                        var rnd = Random.Range(0, 2);
                        SetGroundTile(_tilesSystem.TakeGroundTile(rnd == 0 ? GroundTileViewEnum.IronDeposits : GroundTileViewEnum.CopperDeposits));
                        SpawnGroundTile();
                        return;
                    }
                }
                break;

            case GroundTileViewEnum.River:
                for (int i = 0; i < _tileObject.GetNeighbourGroundTilesArray().Length; i++)
                {
                    if (!IsCheckTiles(i, true)) continue;

                    if (_tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.OilSwamp) ||
                        _tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.PollutedRiver))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.PollutedRiver));
                        SpawnGroundTile();
                        return;
                    }

                    if (_tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.Oasis))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.DesertRiver));
                        SpawnGroundTile();
                        return;
                    }

                    if (_tileObject.GetNeighbourGroundTile(i).IsWaterTile())
                    {
                        _riverNumber++;
                    }
                }
                _tileRiver.PrepareRiver(_riverNumber, IsForwardRoad(), false);
                break;

            case GroundTileViewEnum.PollutedRiver:
                for (int i = 0; i < _tileObject.GetNeighbourGroundTilesArray().Length; i++)
                {
                    if (!IsCheckTiles(i, true)) continue;

                    if (_tileObject.GetNeighbourGroundTile(i).IsWaterTile())
                    {
                        _riverNumber++;
                    }
                }
                _tileRiver.PrepareRiver(_riverNumber, IsForwardRoad(), false);
                break;

            case GroundTileViewEnum.Ground:
                for (int i = 0; i < _tileObject.GetNeighbourGroundTilesArray().Length; i++)
                {
                    if (!IsCheckTiles(i, true)) continue;

                    if (_tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.Volcano))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.BlazingField));
                        SpawnGroundTile();
                        return;
                    }

                    if (_tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.OilSwamp) ||
                        _tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.PollutedRiver))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.Barrenland));
                        SpawnGroundTile();
                        return;
                    }

                    if (_tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.Mountain))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.CoalDeposits));
                        SpawnGroundTile();
                        return;
                    }
                }
                break;

            case GroundTileViewEnum.Forest:
                for (int i = 0; i < _tileObject.GetNeighbourGroundTilesArray().Length; i++)
                {
                    if (!IsCheckTiles(i, true)) continue;

                    if (_tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.Volcano))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.BlazingField));
                        SpawnGroundTile();
                        return;
                    }

                    if (_tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.OilSwamp) ||
                        _tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.BlackDesert) ||
                        _tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.PollutedRiver) ||
                       _tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.Barrenland))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.DeadForest));
                        SpawnGroundTile();
                        return;
                    }

                    if (_tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.Desert))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.Oasis));
                        SpawnGroundTile();
                        return;
                    }
                }
                break;

            case GroundTileViewEnum.DesertRiver:
                for (int i = 0; i < _tileObject.GetNeighbourGroundTilesArray().Length; i++)
                {
                    if (!IsCheckTiles(i, true)) continue;

                    if (_tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.OilSwamp) ||
                        _tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.PollutedRiver))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.PollutedRiver));
                        SpawnGroundTile();
                        return;
                    }

                    if (_tileObject.GetNeighbourGroundTile(i).IsWaterTile())
                    {
                        _riverNumber++;
                    }

                }
                _tileRiver.PrepareRiver(_riverNumber, IsForwardRoad(), false);
                break;

            case GroundTileViewEnum.Desert:
                for (int i = 0; i < _tileObject.GetNeighbourGroundTilesArray().Length; i++)
                {
                    if (!IsCheckTiles(i, true)) continue;

                    if (_tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.Volcano))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.BlazingField));
                        SpawnGroundTile();
                        return;
                    }

                    if (_tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.OilSwamp) ||
                        _tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.PollutedRiver))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.BlackDesert));
                        SpawnGroundTile();
                        return;
                    }

                    if (_tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.River) ||
                        _tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.DesertRiver))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.Oasis));
                        SpawnGroundTile();
                        return;
                    }
                }
                break;

            case GroundTileViewEnum.CoalDeposits:
                for (int i = 0; i < _tileObject.GetNeighbourGroundTilesArray().Length; i++)
                {
                    if (!IsCheckTiles(i, true)) continue;

                    if (_tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.Volcano))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.BlazingField));
                        SpawnGroundTile();
                        return;
                    }

                    if (_tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.OilSwamp) ||
                        _tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.PollutedRiver))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.ScarceCoalDeposits));
                        SpawnGroundTile();
                        return;
                    }
                }
                break;

            case GroundTileViewEnum.Barrenland:
                for (int i = 0; i < _tileObject.GetNeighbourGroundTilesArray().Length; i++)
                {
                    if (!IsCheckTiles(i, true)) continue;

                    if (_tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.Volcano))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.BlazingField));
                        SpawnGroundTile();
                        return;
                    }

                    if (_tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.Mountain))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.ScarceCoalDeposits));
                        SpawnGroundTile();
                        return;
                    }
                }
                break;
            case GroundTileViewEnum.Oasis:
                for (int i = 0; i < _tileObject.GetNeighbourGroundTilesArray().Length; i++)
                {
                    if (!IsCheckTiles(i, true)) continue;

                    if (_tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.Volcano))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.BlazingField));
                        SpawnGroundTile();
                        return;
                    }

                    if (_tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.OilSwamp) ||
                        _tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.PollutedRiver) ||
                        _tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.BlackDesert) ||
                        _tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.Barrenland))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.DriedOasis));
                        SpawnGroundTile();
                        return;
                    }
                }
                break;

            case GroundTileViewEnum.IronDeposits or GroundTileViewEnum.CopperDeposits or GroundTileViewEnum.OilSwamp
                or GroundTileViewEnum.DeadForest or GroundTileViewEnum.ScarceCoalDeposits or GroundTileViewEnum.BlackDesert
                or GroundTileViewEnum.DriedOasis:
                for (int i = 0; i < _tileObject.GetNeighbourGroundTilesArray().Length; i++)
                {
                    if (!IsCheckTiles(i, true)) continue;

                    if (_tileObject.GetNeighbourGroundTile(i).CheckTileView(GroundTileViewEnum.Volcano))
                    {
                        SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.BlazingField));
                        SpawnGroundTile();
                        return;
                    }
                }
                break;

            case GroundTileViewEnum.Mountain:
                if (IsCheckAllCross(GroundTileViewEnum.Meadow))
                {
                    SetGroundTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.OvergrownMountain));
                    SpawnGroundTile();
                }
                break;
        }
    }

    public bool IsCheckAllCross(GroundTileViewEnum _groundTileViewEnum)
    {
        return _tileObject.GetNeighbourGroundTile((int)TileDirectionEnum.North).CheckTileView(_groundTileViewEnum) &&
        _tileObject.GetNeighbourGroundTile((int)TileDirectionEnum.East).CheckTileView(_groundTileViewEnum) &&
        _tileObject.GetNeighbourGroundTile((int)TileDirectionEnum.West).CheckTileView(_groundTileViewEnum) &&
        _tileObject.GetNeighbourGroundTile((int)TileDirectionEnum.South).CheckTileView(_groundTileViewEnum);
    }

    public void SelectTile(bool state, SelectTileEnum selectTileEnum = SelectTileEnum.TileSelect)
    {
        _tileView.ViewToggle(state, selectTileEnum);
    }

    public bool IsCheckTiles(int i, bool cross)
    {
        if (cross)
        {
            if (i is (int)TileDirectionEnum.NorthEast or (int)TileDirectionEnum.NorthWest or (int)TileDirectionEnum.SouthEast or (int)TileDirectionEnum.SouthWest) return false;
        }

        if (_tileObject.GetNeighbourGroundTile(i) == null) return false;
        if (!_tileObject.GetNeighbourGroundTile(i).HaveTile()) return false;

        return true;
    }
}
