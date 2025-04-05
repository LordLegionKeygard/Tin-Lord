using System.Collections;
using UnityEngine;
using Zenject;


public class GroundTile : MonoBehaviour
{
    [Inject] private DiContainer _diContainer;
    [Inject] private readonly TilesSystem _tilesSystem;
    [SerializeField] private Transform _groundParent;
    [SerializeField] private TileObject _tileObject;
    private Tile _currentGroundTile;
    private GameObject _currentGroundTileObject;
    private float _groundModelRotation;
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
    public float GroundModelRotation() => _groundModelRotation;
    public void SetGroundModelRotation(float rotation) => _groundModelRotation = rotation;

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
        var neighbours = _tileObject.GetNeighbourGroundTilesArray();

        for (int i = 0; i < neighbours.Length; i++)
        {
            if (!IsCheckTiles(i, true)) continue;

            var neighbour = neighbours[i];
            if (neighbour == null) continue;

            if (neighbour.IsWaterTile())
            {
                neighbour.CurrentTileRiver().PrepareRiver(0, neighbour.CurrentTileRiver().IsBridge(), true);
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
            _tileRoad.SetRoadTile(_tilesSystem.GetGroundTileForEnum(GroundTileViewEnum.Road));
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

    public void SpawnGroundTile(GroundTileViewEnum previousGroundTileViewEnum = GroundTileViewEnum.None)
    {
        if (_currentGroundTile == null) return;
        if (_currentGroundTile.GroundTileView != GroundTileViewEnum.Road) AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.GroundTiles[(int)CurrentGroundTile().GroundTileView - 1], transform.position);

        if (_currentGroundTileObject != null)
        {
            _groundModelRotation = 0;
            Destroy(_currentGroundTileObject);
        }

        _currentGroundTileObject = _diContainer.InstantiatePrefab(_currentGroundTile.TileObject, _groundParent.position, Quaternion.identity, null);

        _currentGroundTileObject.transform.SetParent(_groundParent);

        if (previousGroundTileViewEnum != GroundTileViewEnum.None) // его передает только ивент EarthQuake при землетрясении
        {
            _tileObject.SetRiftViewNumber((int)previousGroundTileViewEnum);
            var riftSetTileMaterial = _currentGroundTileObject.GetComponent<RiftSetTileMaterial>();
            riftSetTileMaterial.SetMaterial(previousGroundTileViewEnum); //для рифта передаем прошлый тайл
        }

        RefreshGroundTile();
        UpdateNeighbourGroundTiles();
        _tileView.SetTileView(_currentGroundTileObject.transform, _currentGroundTile);
        _tileView.PlayAnimation(TileAnimationsEnum.Spawn);
        CustomEvents.FireChangeEcology(_tileObject.TileEcology().GetEcology(GetEcologyEnum.Total), _tileObject.GetId(), false);
        _tileObject.ChangeResourceProduction();
        _tileObject.SetResourceModifier();
    }

    public void LoadGroundTile(TileDataWrapper tileDataWrapper)
    {
        _currentGroundTile = _tilesSystem.GetGroundTileForNumber(tileDataWrapper.GroundData.GroundTileId);

        _currentGroundTileObject = _diContainer.InstantiatePrefab(_currentGroundTile.TileObject, _groundParent.position, Quaternion.identity, null);
        _currentGroundTileObject.transform.SetParent(_groundParent);

        if (IsWaterTile()) _tileRiver.LoadRiver(tileDataWrapper);

        _tileView.SetTileView(_currentGroundTileObject.transform, _currentGroundTile);
        CustomEvents.FireChangeEcology(_tileObject.TileEcology().GetEcology(GetEcologyEnum.Total), _tileObject.GetId(), false);
        _tileObject.ChangeResourceProduction();
        _tileObject.SetResourceModifier();
        _tileRoad.LoadForwardRoad(tileDataWrapper.GroundData.IsForwardRoad);

        if (_tileObject.GroundTileObject().CurrentGroundTileObject().TryGetComponent<RotationView>(out var rotationView))
        {
            rotationView.LoadRotate(tileDataWrapper.GroundData.GroundTileRotation);
        }

        _tileObject.GroundTileObject().SetGroundModelRotation(tileDataWrapper.GroundData.GroundModelRotation);

        if (_currentGroundTile.GroundTileView == GroundTileViewEnum.Rift)
        {
            var number = tileDataWrapper.GroundData.RiftViewNumber;
            if(number == -1)
            {
                Debug.Log("Bug");
                return;
            }
            var riftSetTileMaterial = _currentGroundTileObject.GetComponent<RiftSetTileMaterial>();
            riftSetTileMaterial.SetMaterial(_tilesSystem.GetGroundTileForNumber(number).GroundTileView);
        }
    }

    private void UpdateNeighbourGroundTiles()
    {
        var neighbours = _tileObject.GetNeighbourGroundTilesArray();

        for (int i = 0; i < neighbours.Length; i++)
        {
            var neighbour = neighbours[i];
            if (neighbour != null)
            {
                neighbour.RefreshGroundTile();
            }
        }
    }

    /// <summary>
    /// Превращаем тайлв  новый
    /// </summary>
    private void TransformTo(GroundTileViewEnum newView)
    {
        SetGroundTile(_tilesSystem.GetGroundTileForEnum(newView));
        SpawnGroundTile();
    }

    public void RefreshGroundTile()
    {
        _riverNumber = 0;
        if (_currentGroundTile == null) return;

        // Один раз получаем массив из 8 потенциальных соседей
        var neighbours = _tileObject.GetNeighbourGroundTilesArray();

        switch (_currentGroundTile.GroundTileView)
        {
            case GroundTileViewEnum.Plain:
                for (int i = 0; i < neighbours.Length; i++)
                {
                    if (!IsCheckTiles(i, true)) continue;

                    var neighbour = neighbours[i];
                    if (neighbour == null) continue;

                    if (neighbour.CheckTileView(GroundTileViewEnum.Volcano))
                    {
                        TransformTo(GroundTileViewEnum.BlazingField);
                        return;
                    }

                    if (neighbour.CheckTileView(GroundTileViewEnum.OilSwamp) ||
                        neighbour.CheckTileView(GroundTileViewEnum.PollutedRiver))
                    {
                        TransformTo(GroundTileViewEnum.Barrenland);
                        return;
                    }

                    if (neighbour.CheckTileView(GroundTileViewEnum.Mountain) ||
                        neighbour.CheckTileView(GroundTileViewEnum.Forest))
                    {
                        TransformTo(GroundTileViewEnum.Meadow);
                        return;
                    }

                    if (neighbour.CheckTileView(GroundTileViewEnum.Barrenland) ||
                        neighbour.CheckTileView(GroundTileViewEnum.BlackDesert))
                    {
                        TransformTo(GroundTileViewEnum.Ground);
                        return;
                    }
                }
                break;

            case GroundTileViewEnum.Meadow:
                for (int i = 0; i < neighbours.Length; i++)
                {
                    if (!IsCheckTiles(i, true)) continue;

                    var neighbour = neighbours[i];
                    if (neighbour == null) continue;

                    if (neighbour.CheckTileView(GroundTileViewEnum.Volcano))
                    {
                        TransformTo(GroundTileViewEnum.BlazingField);
                        return;
                    }

                    if (neighbour.CheckTileView(GroundTileViewEnum.OilSwamp) ||
                        neighbour.CheckTileView(GroundTileViewEnum.PollutedRiver))
                    {
                        TransformTo(GroundTileViewEnum.Barrenland);
                        return;
                    }

                    if (neighbour.CheckTileView(GroundTileViewEnum.Barrenland) ||
                        neighbour.CheckTileView(GroundTileViewEnum.BlackDesert))
                    {
                        TransformTo(GroundTileViewEnum.Ground);
                        return;
                    }
                }
                break;

            case GroundTileViewEnum.Highland:
                for (int i = 0; i < neighbours.Length; i++)
                {
                    if (!IsCheckTiles(i, true)) continue;

                    var neighbour = neighbours[i];
                    if (neighbour == null) continue;

                    if (neighbour.CheckTileView(GroundTileViewEnum.Volcano))
                    {
                        TransformTo(GroundTileViewEnum.BlazingField);
                        return;
                    }

                    if (neighbour.CheckTileView(GroundTileViewEnum.Mountain))
                    {
                        var rnd = Random.Range(0, 2);
                        TransformTo(rnd == 0 ? GroundTileViewEnum.IronDeposits : GroundTileViewEnum.CopperDeposits);
                        return;
                    }
                }
                break;

            case GroundTileViewEnum.River:
                for (int i = 0; i < neighbours.Length; i++)
                {
                    if (!IsCheckTiles(i, true)) continue;

                    var neighbour = neighbours[i];
                    if (neighbour == null) continue;

                    if (neighbour.CheckTileView(GroundTileViewEnum.OilSwamp) ||
                        neighbour.CheckTileView(GroundTileViewEnum.PollutedRiver))
                    {
                        TransformTo(GroundTileViewEnum.PollutedRiver);
                        return;
                    }

                    if (neighbour.CheckTileView(GroundTileViewEnum.Oasis))
                    {
                        TransformTo(GroundTileViewEnum.DesertRiver);
                        return;
                    }

                    if (neighbour.IsWaterTile()) _riverNumber++;               
                }
                _tileRiver.PrepareRiver(_riverNumber, IsForwardRoad(), false);
                break;

            case GroundTileViewEnum.PollutedRiver:
                for (int i = 0; i < neighbours.Length; i++)
                {
                    if (!IsCheckTiles(i, true)) continue;

                    var neighbour = neighbours[i];
                    if (neighbour == null) continue;

                    if (neighbour.IsWaterTile()) _riverNumber++;                
                }
                _tileRiver.PrepareRiver(_riverNumber, IsForwardRoad(), false);
                break;

            case GroundTileViewEnum.Ground:
                for (int i = 0; i < neighbours.Length; i++)
                {
                    if (!IsCheckTiles(i, true)) continue;

                    var neighbour = neighbours[i];
                    if (neighbour == null) continue;

                    if (neighbour.CheckTileView(GroundTileViewEnum.Volcano))
                    {
                        TransformTo(GroundTileViewEnum.BlazingField);
                        return;
                    }

                    if (neighbour.CheckTileView(GroundTileViewEnum.OilSwamp) ||
                        neighbour.CheckTileView(GroundTileViewEnum.PollutedRiver))
                    {
                        TransformTo(GroundTileViewEnum.Barrenland);
                        return;
                    }

                    if (neighbour.CheckTileView(GroundTileViewEnum.Mountain))
                    {
                        TransformTo(GroundTileViewEnum.CoalDeposits);
                        return;
                    }
                }
                break;

            case GroundTileViewEnum.Forest:
                for (int i = 0; i < neighbours.Length; i++)
                {
                    if (!IsCheckTiles(i, true)) continue;

                    var neighbour = neighbours[i];
                    if (neighbour == null) continue;

                    if (neighbour.CheckTileView(GroundTileViewEnum.Volcano))
                    {
                        TransformTo(GroundTileViewEnum.BlazingField);
                        return;
                    }

                    if (neighbour.CheckTileView(GroundTileViewEnum.OilSwamp) ||
                        neighbour.CheckTileView(GroundTileViewEnum.BlackDesert) ||
                        neighbour.CheckTileView(GroundTileViewEnum.PollutedRiver) ||
                        neighbour.CheckTileView(GroundTileViewEnum.Barrenland))
                    {
                        TransformTo(GroundTileViewEnum.DeadForest);
                        return;
                    }

                    if (neighbour.CheckTileView(GroundTileViewEnum.Desert))
                    {
                        TransformTo(GroundTileViewEnum.Oasis);
                        return;
                    }
                }
                break;

            case GroundTileViewEnum.DesertRiver:
                for (int i = 0; i < neighbours.Length; i++)
                {
                    if (!IsCheckTiles(i, true)) continue;

                    var neighbour = neighbours[i];
                    if (neighbour == null) continue;

                    if (neighbour.CheckTileView(GroundTileViewEnum.OilSwamp) ||
                        neighbour.CheckTileView(GroundTileViewEnum.PollutedRiver))
                    {
                        TransformTo(GroundTileViewEnum.PollutedRiver);
                        return;
                    }

                    if (neighbour.IsWaterTile()) _riverNumber++;
                }
                _tileRiver.PrepareRiver(_riverNumber, IsForwardRoad(), false);
                break;

            case GroundTileViewEnum.Desert:
                for (int i = 0; i < neighbours.Length; i++)
                {
                    if (!IsCheckTiles(i, true)) continue;

                    var neighbour = neighbours[i];
                    if (neighbour == null) continue;

                    if (neighbour.CheckTileView(GroundTileViewEnum.Volcano))
                    {
                        TransformTo(GroundTileViewEnum.BlazingField);
                        return;
                    }

                    if (neighbour.CheckTileView(GroundTileViewEnum.OilSwamp) ||
                        neighbour.CheckTileView(GroundTileViewEnum.PollutedRiver))
                    {
                        TransformTo(GroundTileViewEnum.BlackDesert);
                        return;
                    }

                    if (neighbour.CheckTileView(GroundTileViewEnum.River) ||
                        neighbour.CheckTileView(GroundTileViewEnum.DesertRiver))
                    {
                        TransformTo(GroundTileViewEnum.Oasis);
                        return;
                    }
                }
                break;

            case GroundTileViewEnum.CoalDeposits:
                for (int i = 0; i < neighbours.Length; i++)
                {
                    if (!IsCheckTiles(i, true)) continue;

                    var neighbour = neighbours[i];
                    if (neighbour == null) continue;

                    if (neighbour.CheckTileView(GroundTileViewEnum.Volcano))
                    {
                        TransformTo(GroundTileViewEnum.BlazingField);
                        return;
                    }

                    if (neighbour.CheckTileView(GroundTileViewEnum.OilSwamp) ||
                        neighbour.CheckTileView(GroundTileViewEnum.PollutedRiver))
                    {
                        TransformTo(GroundTileViewEnum.ScarceCoalDeposits);
                        return;
                    }
                }
                break;

            case GroundTileViewEnum.Barrenland:
                for (int i = 0; i < neighbours.Length; i++)
                {
                    if (!IsCheckTiles(i, true)) continue;

                    var neighbour = neighbours[i];
                    if (neighbour == null) continue;

                    if (neighbour.CheckTileView(GroundTileViewEnum.Volcano))
                    {
                        TransformTo(GroundTileViewEnum.BlazingField);
                        return;
                    }

                    if (neighbour.CheckTileView(GroundTileViewEnum.Mountain))
                    {
                        TransformTo(GroundTileViewEnum.ScarceCoalDeposits);
                        return;
                    }
                }
                break;
            case GroundTileViewEnum.Oasis:
                for (int i = 0; i < neighbours.Length; i++)
                {
                    if (!IsCheckTiles(i, true)) continue;

                    var neighbour = neighbours[i];
                    if (neighbour == null) continue;

                    if (neighbour.CheckTileView(GroundTileViewEnum.Volcano))
                    {
                        TransformTo(GroundTileViewEnum.BlazingField);
                        return;
                    }

                    if (neighbour.CheckTileView(GroundTileViewEnum.OilSwamp) ||
                        neighbour.CheckTileView(GroundTileViewEnum.PollutedRiver) ||
                        neighbour.CheckTileView(GroundTileViewEnum.BlackDesert) ||
                        neighbour.CheckTileView(GroundTileViewEnum.Barrenland))
                    {
                        TransformTo(GroundTileViewEnum.DriedOasis);
                        return;
                    }
                }
                break;

            case GroundTileViewEnum.IronDeposits or GroundTileViewEnum.CopperDeposits or GroundTileViewEnum.OilSwamp
                or GroundTileViewEnum.DeadForest or GroundTileViewEnum.ScarceCoalDeposits or GroundTileViewEnum.BlackDesert
                or GroundTileViewEnum.DriedOasis:
                for (int i = 0; i < neighbours.Length; i++)
                {
                    if (!IsCheckTiles(i, true)) continue;

                    var neighbour = neighbours[i];
                    if (neighbour == null) continue;

                    if (neighbour.CheckTileView(GroundTileViewEnum.Volcano))
                    {
                        TransformTo(GroundTileViewEnum.BlazingField);
                        return;
                    }
                }
                break;

            case GroundTileViewEnum.Mountain:
                if (IsCheckAllCross(GroundTileViewEnum.Meadow))
                {
                    TransformTo(GroundTileViewEnum.OvergrownMountain);
                }
                break;
        }
    }

    public bool IsCheckAllCross(GroundTileViewEnum tileView)
    {
        var directions = new[]
        {
        TileDirectionEnum.North,
        TileDirectionEnum.East,
        TileDirectionEnum.West,
        TileDirectionEnum.South
        };

        foreach (var dir in directions)
        {
            var neighbor = _tileObject.GetNeighbourGroundTile((int)dir);
            if (neighbor == null || !neighbor.CheckTileView(tileView)) return false;
        }

        return true;
    }

    public void SelectTile(bool state, SelectTileEnum selectTileEnum = SelectTileEnum.TileSelect, bool checkEdge = true)
    {
        _tileView.SelectViewToggle(state, selectTileEnum);
        if (checkEdge) _tileView.EdgeViewToggle(transform.position.x, transform.position.z, state);
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
