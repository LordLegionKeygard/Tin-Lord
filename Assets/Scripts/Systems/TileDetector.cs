using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileDetector : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private TileObject _currentTileObject;
    [SerializeField] private CardHolderSystem _cardHolderSystem;
    [SerializeField] private TilesSystem _tileSystem;
    [SerializeField] private SelectTilePanel _selectTilePanel;
    [SerializeField] private ResourcesPanel _resourcesPanel;
    private Transform _lastRayCastTransform;
    private bool _canSetTile = false;

    private void Update()
    {
        if (_cardHolderSystem.IsHaveCurrentSelectedCardObject())
        {
            RaycastHit raycastHit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 500f, _layerMask) && !EventSystem.current.IsPointerOverGameObject())
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

            if (EventSystem.current.IsPointerOverGameObject())
            {
                Clear();
            }
        }
    }

    public void InputOnTile()
    {
        if (_cardHolderSystem.IsHaveCurrentSelectedCardObject() && _currentTileObject != null)
        {
            if (_canSetTile && !EventSystem.current.IsPointerOverGameObject())
            {
                if (_cardHolderSystem.CurrentCardHolderSelectedTile().GroundTileView is GroundTileViewEnum.River or GroundTileViewEnum.PollutedRiver)
                {
                    if (!CanSetRiver()) return;
                    if (_currentTileObject.GroundTileObject().HaveTile() && !_tileSystem.IsHaveRiver) return;
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
                Clear();
            }
        }
        else
        {
            RaycastHit raycastHit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 500f, _layerMask) && !EventSystem.current.IsPointerOverGameObject())
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

        if (newTileObject.GroundTileObject().HaveTile() && _cardHolderSystem.CurrentCardHolderSelectedTile().GroundTileView is GroundTileViewEnum.River or GroundTileViewEnum.PollutedRiver)
        {
            _currentTileObject = newTileObject;
            if (!newTileObject.GroundTileObject().CheckTileView(GroundTileViewEnum.Road) || !_tileSystem.IsHaveRiver)
            {
                _currentTileObject.GroundTileObject().SelectTile(true, SelectTileEnum.ErrorSelect);
                return true;
            }
            else //если это дорога и есть река
            {
                if (_currentTileObject.GroundTileObject().IsForwardRoad())
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

        if (_cardHolderSystem.CheckCurrentCardHolderSelectedTileIsFourTile())
        {
            UnselectLastX4Tiles(true);

            _currentTileObject = newTileObject;

            _currentTileObject.GroundTileObject().SelectTile(true, _currentTileObject.GroundTileObject().HaveTile() ? SelectTileEnum.ErrorSelect : SelectTileEnum.EmptyTileSelect);
            _currentTileObject.GroundTileObject().NeighbourGroundTile(0).SelectTile(true, _currentTileObject.GroundTileObject().NeighbourHaveTile(0) ? SelectTileEnum.ErrorSelect : SelectTileEnum.EmptyTileSelect);
            _currentTileObject.GroundTileObject().NeighbourGroundTile(1).SelectTile(true, _currentTileObject.GroundTileObject().NeighbourHaveTile(1) ? SelectTileEnum.ErrorSelect : SelectTileEnum.EmptyTileSelect);
            _currentTileObject.GroundTileObject().NeighbourGroundTile(2).SelectTile(true, _currentTileObject.GroundTileObject().NeighbourGroundTile(2).HaveTile() ? SelectTileEnum.ErrorSelect : SelectTileEnum.EmptyTileSelect);

            _canSetTile = !_currentTileObject.GroundTileObject().HaveTile() && !_currentTileObject.GroundTileObject().NeighbourHaveTile(0)
            && !_currentTileObject.GroundTileObject().NeighbourHaveTile(1) && !_currentTileObject.GroundTileObject().NeighbourHaveTile(2);
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

        UnselectLastTile(false);

        if (newTileObject.GroundTileObject().HaveTile())
        {
            _currentTileObject = newTileObject;
            _currentTileObject.GroundTileObject().SelectTile(true, SelectTileEnum.TileSelect);
            _selectTilePanel.ShowInfo(_currentTileObject);
            _resourcesPanel.PanelViewToggle(_currentTileObject.GroundTileObject().CurrentGroundTile().GroundTileView == GroundTileViewEnum.BaseFoundation);
        }
    }

    public void UnselectLastTile(bool isPanelView)
    {
        if (_currentTileObject != null)
        {
            _currentTileObject.GroundTileObject().SelectTile(false, SelectTileEnum.EmptyTileSelect);
            if (isPanelView) _selectTilePanel.PanelViewToggle(false);
        }
    }

    public void UnselectLastX4Tiles(bool isPanelView)
    {
        if (_currentTileObject != null)
        {
            _currentTileObject.GroundTileObject().SelectTile(false, SelectTileEnum.EmptyTileSelect);
            _currentTileObject.GroundTileObject().NeighbourGroundTile(0).SelectTile(false, SelectTileEnum.EmptyTileSelect);
            _currentTileObject.GroundTileObject().NeighbourGroundTile(1).SelectTile(false, SelectTileEnum.EmptyTileSelect);
            _currentTileObject.GroundTileObject().NeighbourGroundTile(2).SelectTile(false, SelectTileEnum.EmptyTileSelect);
            if (isPanelView) _selectTilePanel.PanelViewToggle(false);
        }
    }

    private bool CanSetRiver()
    {
        if (_tileSystem.IsHaveRiver)
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

    public void Clear()
    {
        _resourcesPanel.PanelViewToggle(false);
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
}
