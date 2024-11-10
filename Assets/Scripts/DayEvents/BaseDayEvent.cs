using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDayEvent : MonoBehaviour
{
    [SerializeField] private GameObject _spawnPrefab;
    [SerializeField] private SetTileNeighbours _setTileNeighbours;
    public SetTileNeighbours SetTileNeighbours() => _setTileNeighbours;
    private GameObject _currentPrefab;
    public GameObject CurrentPrefab() => _currentPrefab;
    private TileObject _tileObject;
    public TileObject TileObject() => _tileObject;

    public virtual void StartEvent()
    {
        var rnd = Random.Range(0, _setTileNeighbours.TileObjects.Count);

        _tileObject = _setTileNeighbours.TileObjects[rnd];

        _currentPrefab = Instantiate(_spawnPrefab, _tileObject.transform.position, Quaternion.identity);
    }
}
