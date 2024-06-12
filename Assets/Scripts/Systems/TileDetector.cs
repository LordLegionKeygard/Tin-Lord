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
    private Transform _lastRayCastTransform;

    private void Update()
    {
        if (_cardHolderSystem.IsHaveCurrentSelectedCardObject())
        {
            if (Input.GetMouseButtonDown(0) && _currentTileObject != null && !EventSystem.current.IsPointerOverGameObject())
            {
                _currentTileObject.SetTile(_cardHolderSystem.CurrentCardHolderSelectedTile());
                _currentTileObject.SpawnTile();
                Clear();
            }

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
                    DetectEmptyTileForSetNewGroundTile(raycastHit.transform.gameObject);
                }
            }

            if(EventSystem.current.IsPointerOverGameObject())
            {
                Clear();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
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
    }

    public void DetectEmptyTileForSetNewGroundTile(GameObject gameObject)
    {
        var newTileObject = gameObject.GetComponent<TileObject>();

        UnselectLastTile();

        if (!newTileObject.HaveTile())
        {
            _currentTileObject = newTileObject;
            _currentTileObject.SelectTile(true, TileTypeEnum.Ground);
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
            _currentTileObject.SelectTile(true, TileTypeEnum.Building);
        }
    }

    public void UnselectLastTile()
    {
        if (_currentTileObject != null) _currentTileObject.SelectTile(false, TileTypeEnum.Building);
    }

    public void Clear()
    {
        UnselectLastTile();
        _currentTileObject = null;
    }
}
