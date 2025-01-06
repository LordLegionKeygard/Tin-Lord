using UnityEngine;
using Zenject;

public class TileDetector : MonoBehaviour
{
    [Inject] private readonly TilesSystem _tilesSystem;
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private TileObject _currentTileObject;
    [SerializeField] private CardHolderSystem _cardHolderSystem;
    [SerializeField] private SelectTilePanel _selectTilePanel;
    [SerializeField] private UIPanelsWorld _uiPanels;
    [SerializeField] private BuildsPanel _buildsPanel;
    private Transform _lastRayCastTransform;
    private bool _canSetTile = false;

    public bool IsHaveCurrentSelectedTileObject() => _currentTileObject == null ? false : true;

    private void Awake()
    {
        CustomEvents.OnBuildingDestroyed += CheckCurrentTileObject;
    }

    private void CheckCurrentTileObject(int tileId)
    {
        if (_currentTileObject == null) return;
        if (_currentTileObject.GetId() == tileId)
        {
            UnselectLastTile(true);
            ClearTileDetector();
        }
    }

    private void Update()
    {
        if (_cardHolderSystem.IsHaveCurrentSelectedCardObject())
        {
            RaycastHit raycastHit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 500f, _layerMask) && !IsPointerOverUISystem.IsPointerOverUI)
            {
                if (raycastHit.transform != null)
                {
                    if (_lastRayCastTransform == raycastHit.transform)
                    {
                        return;
                    }
                    _lastRayCastTransform = raycastHit.transform;

                    if (TrySelectBridge(raycastHit.transform.gameObject))
                    {
                        return;
                    }
                    else
                    {
                        SelectEmptyTile(raycastHit.transform.gameObject);
                    }
                }
            }

            if (IsPointerOverUISystem.IsPointerOverUI)
            {
                ClearTileDetector();
            }
        }
    }

    public void InputOnTile()
    {
        if (_cardHolderSystem.IsHaveCurrentSelectedCardObject() && _currentTileObject != null)
        {
            if (_canSetTile && !IsPointerOverUISystem.IsPointerOverUI)
            {
                if (_cardHolderSystem.CurrentCardHolderSelectedTile().GroundTileView is GroundTileViewEnum.River)
                {
                    if (!CanSetRiver() || _currentTileObject.BuildingTileObject().HaveTile()) return;
                    if (_currentTileObject.GroundTileObject().HaveTile() && !_tilesSystem.IsHaveRiver()) return;
                    if (_currentTileObject.GroundTileObject().HaveTile())
                    {
                        if (!_currentTileObject.GroundTileObject().IsForwardRoad()) return;
                    }
                    if (_currentTileObject.GroundTileObject().HaveTile())
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            if (i is (int)TileDirectionEnum.NorthEast or (int)TileDirectionEnum.NorthWest or (int)TileDirectionEnum.SouthEast or (int)TileDirectionEnum.SouthWest) continue;
                            if (_currentTileObject.GroundTileObject().NeighbourTileIsBridge(i))
                            {
                                return;
                            }
                        }
                    }
                }

                _currentTileObject.GroundTileObject().SetGroundTile(_cardHolderSystem.CurrentCardHolderSelectedTile());
                _currentTileObject.GroundTileObject().SpawnGroundTile();
                if (_currentTileObject.GroundTileObject().CurrentGroundTile().IsFourTile) _currentTileObject.GroundTileObject().TurnOffFourTileNeighboursCollider();
                ClearTileDetector();
                _cardHolderSystem.RemoveCurrentCard();
            }
        }
        else if (!_cardHolderSystem.IsHaveCurrentSelectedCardObject())
        {
            RaycastHit raycastHit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 500f, _layerMask) && !IsPointerOverUISystem.IsPointerOverUI)
            {
                if (raycastHit.transform != null)
                {
                    DetectTileForBuilding(raycastHit.transform.gameObject);
                }
            }
        }
    }

    private bool TrySelectBridge(GameObject gameObject)
    {
        var newTileObject = gameObject.GetComponent<TileObject>();
        UnselectLastTile(true);

        if (newTileObject.GroundTileObject().HaveTile() && _cardHolderSystem.CurrentCardHolderSelectedTile().GroundTileView is GroundTileViewEnum.River)
        {
            _currentTileObject = newTileObject;
            if (!newTileObject.GroundTileObject().CheckTileView(GroundTileViewEnum.Road) || !_tilesSystem.IsHaveRiver())
            {
                _currentTileObject.GroundTileObject().SelectTile(true, SelectTileEnum.ErrorSelect);
                return true;
            }
            else //если это дорога и есть река
            {
                if (_currentTileObject.GroundTileObject().IsForwardRoad() && !_currentTileObject.BuildingTileObject().HaveTile())
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if (i is (int)TileDirectionEnum.NorthEast or (int)TileDirectionEnum.NorthWest or (int)TileDirectionEnum.SouthEast or (int)TileDirectionEnum.SouthWest) continue;
                        if (_currentTileObject.GroundTileObject().NeighbourTileIsBridge(i))
                        {
                            _currentTileObject.GroundTileObject().SelectTile(true, SelectTileEnum.ErrorSelect);
                            return true;
                        }

                    }

                    _currentTileObject.GroundTileObject().SelectTile(true, !CanSetRiver() ? SelectTileEnum.ErrorSelect : SelectTileEnum.EmptyTileSelect);
                    return true;
                }
                else
                {
                    _currentTileObject.GroundTileObject().SelectTile(true, SelectTileEnum.ErrorSelect);
                    return true;
                }
            }
        }
        else return false;
    }


    private void SelectEmptyTile(GameObject gameObject)
    {
        var newTileObject = gameObject.GetComponent<TileObject>();

        if (newTileObject.IsGroundDestroyedNow()) return;

        if (_cardHolderSystem.CheckCurrentCardHolderSelectedTileIsFourTile())
        {
            UnselectLastX4Tiles(true);

            _currentTileObject = newTileObject;

            var groundTile = _currentTileObject.GroundTileObject();

            groundTile.SelectTile(true, _currentTileObject.GroundTileObject().HaveTile() ? SelectTileEnum.ErrorSelect : SelectTileEnum.EmptyTileSelect);
            if (groundTile.HaveNeighbour(0)) groundTile.NeighbourGroundTile(0).SelectTile(true, groundTile.NeighbourHaveGroundTile(0) ? SelectTileEnum.ErrorSelect : SelectTileEnum.EmptyTileSelect);
            if (groundTile.HaveNeighbour(1)) groundTile.NeighbourGroundTile(1).SelectTile(true, groundTile.NeighbourHaveGroundTile(1) ? SelectTileEnum.ErrorSelect : SelectTileEnum.EmptyTileSelect);
            if (groundTile.HaveNeighbour(2)) groundTile.NeighbourGroundTile(2).SelectTile(true, groundTile.NeighbourHaveGroundTile(2) ? SelectTileEnum.ErrorSelect : SelectTileEnum.EmptyTileSelect);

            var haveNeighbours = groundTile.HaveNeighbour(0) && groundTile.HaveNeighbour(1) && groundTile.HaveNeighbour(2);
            var neighboursHaveGroundTile = groundTile.NeighbourHaveGroundTile(0) || groundTile.NeighbourHaveGroundTile(1) || groundTile.NeighbourHaveGroundTile(2);

            _canSetTile = !groundTile.HaveTile() && !neighboursHaveGroundTile && haveNeighbours;
        }
        else
        {
            UnselectLastTile(true);

            _currentTileObject = newTileObject;

            if (_cardHolderSystem.CurrentCardHolderSelectedTile().GroundTileView is GroundTileViewEnum.River or GroundTileViewEnum.PollutedRiver)
            {
                _currentTileObject.GroundTileObject().SelectTile(true, !CanSetRiver() ? SelectTileEnum.ErrorSelect : SelectTileEnum.EmptyTileSelect);
            }
            else
            {
                _currentTileObject.GroundTileObject().SelectTile(true, newTileObject.GroundTileObject().HaveTile() ? SelectTileEnum.ErrorSelect : SelectTileEnum.EmptyTileSelect);
            }

            _canSetTile = !_currentTileObject.GroundTileObject().HaveTile();
        }
    }

    public void DetectTileForBuilding(GameObject gameObject)
    {
        var newTileObject = gameObject.GetComponent<TileObject>();

        if (newTileObject.IsBuildingDestroyedNow()) return;

        UnselectLastTile(false);
        _selectTilePanel.ResetPanels();

        if (newTileObject.GroundTileObject().HaveTile())
        {
            _currentTileObject = newTileObject;
            _currentTileObject.GroundTileObject().SelectTile(true);
            _uiPanels.MainPanelsViewToggle(true, false);
            _selectTilePanel.SetTile(_currentTileObject);
            _selectTilePanel.RefreshInfo();
        }
        else ClearTileDetector();
    }

    public void UnselectLastTile(bool isResetMainPanels)
    {
        if (_currentTileObject != null)
        {
            _currentTileObject.GroundTileObject().SelectTile(false);

            if (isResetMainPanels)
            {
                _selectTilePanel.ResetPanels();
                _uiPanels.MainPanelsViewToggle(false, false);
            }
        }
    }

    public void UnselectLastX4Tiles(bool isResetMainPanels)
    {
        if (_currentTileObject != null)
        {
            var groundTile = _currentTileObject.GroundTileObject();

            groundTile.SelectTile(false);
            if (groundTile.HaveNeighbour(0)) groundTile.NeighbourGroundTile(0).SelectTile(false);
            if (groundTile.HaveNeighbour(1)) groundTile.NeighbourGroundTile(1).SelectTile(false);
            if (groundTile.HaveNeighbour(2)) groundTile.NeighbourGroundTile(2).SelectTile(false);

            if (isResetMainPanels)
            {
                _selectTilePanel.ResetPanels();
                _uiPanels.MainPanelsViewToggle(false, false);
            }
        }
    }

    private bool CanSetRiver()
    {
        if (_tilesSystem.IsHaveRiver())
        {
            var riverNumber = 0;
            var lastRiver = false;
            for (int i = 0; i < 8; i++)
            {
                if (i is (int)TileDirectionEnum.NorthEast or (int)TileDirectionEnum.NorthWest or (int)TileDirectionEnum.SouthEast or (int)TileDirectionEnum.SouthWest) continue;
                if (_currentTileObject.GroundTileObject().NeighbourTileIsWater(i))
                {
                    if (_currentTileObject.GroundTileObject().NeighbourHaveLastRiverTile(i)) lastRiver = true;
                    riverNumber++;
                }
            }

            if (riverNumber > 1 || !lastRiver)
            {
                return false;
            }
            else return true;
        }
        else return true;
    }

    public void ClearTileDetector()
    {
        if (_cardHolderSystem.CheckCurrentCardHolderSelectedTileIsFourTile())
        {
            UnselectLastX4Tiles(true);
        }
        else
        {
            UnselectLastTile(true);
        }

        _currentTileObject = null;
    }

    private void OnDestroy()
    {
        CustomEvents.OnBuildingDestroyed -= CheckCurrentTileObject;
    }
}
