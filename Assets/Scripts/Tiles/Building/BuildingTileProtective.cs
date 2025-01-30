using UnityEngine;

public class BuildingTileProtective : MonoBehaviour
{
    [SerializeField] private ProtectiveTypeEnum _protectiveType = ProtectiveTypeEnum.None;
    private BuildingTile _buildingTile;
    private GroundTile _groundTile;
    private BuildingGateView _buildingGateView;

    private void Awake()
    {
        _buildingTile = GetComponent<BuildingTile>();
        _groundTile = GetComponent<GroundTile>();
    }
    public void Reset()
    {
        _protectiveType = ProtectiveTypeEnum.None;
    }

    public void PrepareProtective()
    {
        if (!_buildingTile.HaveTile() || !_buildingTile.IsProtectiveTile() || !_buildingTile.HaveBuildingGameObject())
        {
            return;
        }

        _buildingGateView = _buildingTile.CurrentBuildingGameObject().GetComponent<BuildingGateView>();

        var prepareBuildingProtective = _buildingTile.CurrentBuildingGameObject().GetComponent<PrepareBuildingProtective>();

        if (_groundTile.CurrentGroundTile().GroundTileView == GroundTileViewEnum.Road)
        {
            prepareBuildingProtective.SetBuildingProtective(ProtectiveTypeEnum.Gate, _groundTile.GetRoadAngle() + 90, _buildingTile.CurrentBuildingLevel());
        }
        else
        {
            if (CheckSolo())
            {
                prepareBuildingProtective.SetBuildingProtective(_protectiveType, 0, _buildingTile.CurrentBuildingLevel());
                return;
            }

            if (CheckCross()) return;

            if (CheckTWall()) return;

            if (CheckTurn()) return;

            else SetForward(ProtectiveTypeEnum.WallForward);
        }
    }

    private bool CheckSolo()
    {
        _protectiveType = ProtectiveTypeEnum.WallForward;

        (TileDirectionEnum, ProtectiveTypeEnum, int)[] directions = new (TileDirectionEnum, ProtectiveTypeEnum, int)[]
        {
            (TileDirectionEnum.North, _protectiveType, -90),
            (TileDirectionEnum.East, _protectiveType, 0),
            (TileDirectionEnum.South, _protectiveType, 90),
            (TileDirectionEnum.West, _protectiveType, 180),
        };

        for (int i = 0; i < directions.Length; i++)
        {
            if (_buildingTile.NeightbourTileIsProtective((int)directions[i].Item1))
            {
                return false;
            }
        }
        return true;
    }

    private bool CheckCross()
    {
        _protectiveType = ProtectiveTypeEnum.WallCross;

        // Проверяем наличие защитных тайлов со всех четырех сторон
        if (_buildingTile.NeightbourTileIsProtective((int)TileDirectionEnum.North) &&
            _buildingTile.NeightbourTileIsProtective((int)TileDirectionEnum.East) &&
            _buildingTile.NeightbourTileIsProtective((int)TileDirectionEnum.South) &&
            _buildingTile.NeightbourTileIsProtective((int)TileDirectionEnum.West))
        {
            _buildingTile.CurrentBuildingGameObject().GetComponent<PrepareBuildingProtective>().SetBuildingProtective(_protectiveType, 0, _buildingTile.CurrentBuildingLevel());
            return true;
        }
        return false;
    }

    private bool CheckTWall()
    {
        _protectiveType = ProtectiveTypeEnum.WallT;

        // Массив, где проверяем перед, зад и один бок
        (TileDirectionEnum, TileDirectionEnum, TileDirectionEnum, ProtectiveTypeEnum, int)[] directions = new (TileDirectionEnum, TileDirectionEnum, TileDirectionEnum, ProtectiveTypeEnum, int)[]
        {
        (TileDirectionEnum.North, TileDirectionEnum.East, TileDirectionEnum.South, _protectiveType, 0),
        (TileDirectionEnum.East, TileDirectionEnum.North, TileDirectionEnum.West, _protectiveType, -90),
        (TileDirectionEnum.South, TileDirectionEnum.East, TileDirectionEnum.West, _protectiveType, 90),
        (TileDirectionEnum.West, TileDirectionEnum.North, TileDirectionEnum.South, _protectiveType, 180),
        };

        for (int i = 0; i < directions.Length; i++)
        {
            // Проверяем наличие защитных тайлов спереди, сзади и с одного из боков
            if (_buildingTile.NeightbourTileIsProtective((int)directions[i].Item1) &&
                _buildingTile.NeightbourTileIsProtective((int)directions[i].Item2) &&
                _buildingTile.NeightbourTileIsProtective((int)directions[i].Item3))
            {
                _buildingTile.CurrentBuildingGameObject().GetComponent<PrepareBuildingProtective>().SetBuildingProtective(directions[i].Item4, directions[i].Item5, _buildingTile.CurrentBuildingLevel());
                return true;
            }
        }
        return false;
    }

    private bool CheckTurn()
    {
        _protectiveType = ProtectiveTypeEnum.WallTurn;

        (TileDirectionEnum, TileDirectionEnum, ProtectiveTypeEnum, int)[] directions = new (TileDirectionEnum, TileDirectionEnum, ProtectiveTypeEnum, int)[]
        {
            (TileDirectionEnum.North, TileDirectionEnum.East, _protectiveType, -90),
            (TileDirectionEnum.East, TileDirectionEnum.South, _protectiveType, 0),
            (TileDirectionEnum.South, TileDirectionEnum.West, _protectiveType, 90),
            (TileDirectionEnum.West, TileDirectionEnum.North, _protectiveType, 180),
        };

        for (int i = 0; i < directions.Length; i++)
        {
            if (_buildingTile.NeightbourTileIsProtective((int)directions[i].Item1) && _buildingTile.NeightbourTileIsProtective((int)directions[i].Item2))
            {
                _buildingTile.CurrentBuildingGameObject().GetComponent<PrepareBuildingProtective>().SetBuildingProtective(directions[i].Item3, directions[i].Item4, _buildingTile.CurrentBuildingLevel());
                return true;
            }
        }
        return false;
    }

    private void SetForward(ProtectiveTypeEnum protectiveType)
    {
        (TileDirectionEnum, ProtectiveTypeEnum, int)[] directions = new (TileDirectionEnum, ProtectiveTypeEnum, int)[]
        {
            (TileDirectionEnum.North, protectiveType, 180),
            (TileDirectionEnum.East, protectiveType, -90),
            (TileDirectionEnum.South, protectiveType, 0),
            (TileDirectionEnum.West, protectiveType, 90),
        };

        var tileRiver = _buildingTile.CurrentBuildingGameObject().GetComponent<PrepareBuildingProtective>();

        foreach (var (direction, type, rotation) in directions)
        {
            if (_buildingTile.NeightbourTileIsProtective((int)direction))
            {
                tileRiver.SetBuildingProtective(type, rotation, _buildingTile.CurrentBuildingLevel());
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
