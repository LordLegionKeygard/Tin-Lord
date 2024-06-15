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
    private Transform _lastRayCastTransform;

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
        if (_cardHolderSystem.IsHaveCurrentSelectedCardObject())
        {
            if (_currentTileObject != null && !EventSystem.current.IsPointerOverGameObject())
            {
                if (_cardHolderSystem.CurrentCardHolderSelectedTile().TileView == TileViewEnum.River)
                {
                    if (!CanSetRiver()) return;
                    if(_currentTileObject.HaveTile() && !_tileSystem.IsHaveRiver) return;
                }

                _currentTileObject.SetTile(_cardHolderSystem.CurrentCardHolderSelectedTile());
                _currentTileObject.SpawnTile();
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
        UnselectLastTile();

        if (newTileObject.HaveTile() && _cardHolderSystem.CurrentCardHolderSelectedTile().TileView == TileViewEnum.River)
        {
            _currentTileObject = newTileObject;
            if (!newTileObject.CheckTileView(TileViewEnum.Road) || !_tileSystem.IsHaveRiver)
            {
                _currentTileObject.SelectTile(true, SelectTileEnum.ErrorSelect);
                return true;
            }
            else
            {
                if (_currentTileObject.IsForwardRoad())
                {
                    _currentTileObject.SelectTile(true, !CanSetRiver() ? SelectTileEnum.ErrorSelect : SelectTileEnum.EmptyTileSelect);
                    return true;
                }
                else
                {
                    _currentTileObject.SelectTile(true, SelectTileEnum.ErrorSelect);
                    return true;
                }
            }
        }
        else return false;
    }


    private void SelectEmptyTile(GameObject gameObject)
    {
        var newTileObject = gameObject.GetComponent<TileObject>();

        UnselectLastTile();

        if (!newTileObject.HaveTile())
        {
            _currentTileObject = newTileObject;

            if (_cardHolderSystem.CurrentCardHolderSelectedTile().TileView == TileViewEnum.River)
            {
                _currentTileObject.SelectTile(true, !CanSetRiver() ? SelectTileEnum.ErrorSelect : SelectTileEnum.EmptyTileSelect);
            }
            else
            {
                _currentTileObject.SelectTile(true, SelectTileEnum.EmptyTileSelect);
            }
        }
        else _currentTileObject = null;
    }

    public void DetectTileForBuilding(GameObject gameObject)
    {
        var newTileObject = gameObject.GetComponent<TileObject>();

        UnselectLastTile();

        if (newTileObject.HaveTile())
        {
            _currentTileObject = newTileObject;
            _currentTileObject.SelectTile(true, SelectTileEnum.TileSelect);
        }
    }

    public void UnselectLastTile()
    {
        if (_currentTileObject != null) _currentTileObject.SelectTile(false, SelectTileEnum.EmptyTileSelect);
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
                if (_currentTileObject.NeighbourTileView(i, TileViewEnum.River))
                {
                    if (_currentTileObject.NeighbourHaveLastRiverTile(i)) lastRiver = true;
                    riverNumber++;
                }
            }

            if (riverNumber > 1 || !lastRiver) return false;
            else return true;
        }
        else return true;
    }

    public void Clear()
    {
        UnselectLastTile();
        _currentTileObject = null;
    }
}
