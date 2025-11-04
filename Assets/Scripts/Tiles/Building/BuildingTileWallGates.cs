using UnityEngine;

public class BuildingTileWallGates : MonoBehaviour
{
    [SerializeField] private WallTypeEnum _wallType = WallTypeEnum.None;
    private GroundTile _groundTile;
    private BuildingTile _buildingTile;
    private BuildingGateView _buildingGateView;

    private void Awake()
    {
        _groundTile = GetComponent<GroundTile>();
        _buildingTile = GetComponent<BuildingTile>();
    }
    public void Reset()
    {
        _wallType = WallTypeEnum.None;
    }

    public void PrepareWallsAndGates()
    {
        if (!_buildingTile.IsHaveTile() || (!_buildingTile.IsWallTile() && !_buildingTile.IsGateTile()) || !_buildingTile.IsHaveBuildingGameObject())
        {
            return;
        }

        var currentBuildingGameObject = _buildingTile.CurrentBuildingGameObject();

        _buildingGateView = currentBuildingGameObject.GetComponent<BuildingGateView>();
        var buildingWallView = currentBuildingGameObject.GetComponent<BuildingWallView>();

        if (_buildingTile.IsGateTile())
        {
            _buildingGateView.SetBuildingGate(_groundTile.GetRoadAngle() + 90);
        }
        else
        {
            if (CheckSolo())
            {
                buildingWallView.SetBuildingWall(_wallType, 0, _buildingTile.GetCurrentBuildingLevel());
                return;
            }

            if (CheckCross()) return;

            if (CheckTWall()) return;

            if (CheckTurn()) return;

            else SetForward(WallTypeEnum.WallForward);
        }
    }

    private bool CheckSolo()
    {
        _wallType = WallTypeEnum.WallForward;

        (TileDirectionEnum, WallTypeEnum, int)[] directions = new (TileDirectionEnum, WallTypeEnum, int)[]
        {
            (TileDirectionEnum.North, _wallType, -90),
            (TileDirectionEnum.East, _wallType, 0),
            (TileDirectionEnum.South, _wallType, 90),
            (TileDirectionEnum.West, _wallType, 180),
        };

        for (int i = 0; i < directions.Length; i++)
        {
            if (_buildingTile.NeightbourTileIsWallOrGate((int)directions[i].Item1))
            {
                return false;
            }
        }
        return true;
    }

    private bool CheckCross()
    {
        _wallType = WallTypeEnum.WallCross;

        // Проверяем наличие защитных тайлов со всех четырех сторон
        if (_buildingTile.NeightbourTileIsWallOrGate((int)TileDirectionEnum.North) &&
            _buildingTile.NeightbourTileIsWallOrGate((int)TileDirectionEnum.East) &&
            _buildingTile.NeightbourTileIsWallOrGate((int)TileDirectionEnum.South) &&
            _buildingTile.NeightbourTileIsWallOrGate((int)TileDirectionEnum.West))
        {
            _buildingTile.CurrentBuildingGameObject().GetComponent<BuildingWallView>().SetBuildingWall(_wallType, 0, _buildingTile.GetCurrentBuildingLevel());
            return true;
        }
        return false;
    }

    private bool CheckTWall()
    {
        _wallType = WallTypeEnum.WallT;

        // Массив, где проверяем перед, зад и один бок
        (TileDirectionEnum, TileDirectionEnum, TileDirectionEnum, WallTypeEnum, int)[] directions = new (TileDirectionEnum, TileDirectionEnum, TileDirectionEnum, WallTypeEnum, int)[]
        {
        (TileDirectionEnum.North, TileDirectionEnum.East, TileDirectionEnum.South, _wallType, 0),
        (TileDirectionEnum.East, TileDirectionEnum.North, TileDirectionEnum.West, _wallType, -90),
        (TileDirectionEnum.South, TileDirectionEnum.East, TileDirectionEnum.West, _wallType, 90),
        (TileDirectionEnum.West, TileDirectionEnum.North, TileDirectionEnum.South, _wallType, 180),
        };

        for (int i = 0; i < directions.Length; i++)
        {
            // Проверяем наличие защитных тайлов спереди, сзади и с одного из боков
            if (_buildingTile.NeightbourTileIsWallOrGate((int)directions[i].Item1) &&
                _buildingTile.NeightbourTileIsWallOrGate((int)directions[i].Item2) &&
                _buildingTile.NeightbourTileIsWallOrGate((int)directions[i].Item3))
            {
                _buildingTile.CurrentBuildingGameObject().GetComponent<BuildingWallView>().SetBuildingWall(directions[i].Item4, directions[i].Item5, _buildingTile.GetCurrentBuildingLevel());
                return true;
            }
        }
        return false;
    }

    private bool CheckTurn()
    {
        _wallType = WallTypeEnum.WallTurn;

        (TileDirectionEnum, TileDirectionEnum, WallTypeEnum, int)[] directions = new (TileDirectionEnum, TileDirectionEnum, WallTypeEnum, int)[]
        {
            (TileDirectionEnum.North, TileDirectionEnum.East, _wallType, -90),
            (TileDirectionEnum.East, TileDirectionEnum.South, _wallType, 0),
            (TileDirectionEnum.South, TileDirectionEnum.West, _wallType, 90),
            (TileDirectionEnum.West, TileDirectionEnum.North, _wallType, 180),
        };

        for (int i = 0; i < directions.Length; i++)
        {
            if (_buildingTile.NeightbourTileIsWallOrGate((int)directions[i].Item1) && _buildingTile.NeightbourTileIsWallOrGate((int)directions[i].Item2))
            {
                _buildingTile.CurrentBuildingGameObject().GetComponent<BuildingWallView>().SetBuildingWall(directions[i].Item3, directions[i].Item4, _buildingTile.GetCurrentBuildingLevel());
                return true;
            }
        }
        return false;
    }

    private void SetForward(WallTypeEnum protectiveType)
    {
        (TileDirectionEnum, WallTypeEnum, int)[] directions = new (TileDirectionEnum, WallTypeEnum, int)[]
        {
            (TileDirectionEnum.North, protectiveType, 180),
            (TileDirectionEnum.East, protectiveType, -90),
            (TileDirectionEnum.South, protectiveType, 0),
            (TileDirectionEnum.West, protectiveType, 90),
        };

        var tileRiver = _buildingTile.CurrentBuildingGameObject().GetComponent<BuildingWallView>();

        foreach (var (direction, type, rotation) in directions)
        {
            if (_buildingTile.NeightbourTileIsWallOrGate((int)direction))
            {
                tileRiver.SetBuildingWall(type, rotation, _buildingTile.GetCurrentBuildingLevel());
                break;
            }
        }
    }

    public void ControlGate(bool open)
    {
        if (_buildingGateView == null) return;

        _buildingGateView.ControlGateView(open);
    }
}
