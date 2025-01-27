using UnityEngine;

public class BaseWorldEvent : MonoBehaviour
{
    [SerializeField] private GameObject _spawnPrefab;
    [SerializeField] private AllTileObjects _allTileObjects;
    private GameObject _currentPrefab;
    private TileObject _tileObject;
    protected AllTileObjects GetAllTileObjects() => _allTileObjects;
    protected GameObject GetCurrentPrefab() => _currentPrefab;
    protected TileObject GetTileObject() => _tileObject;

    public virtual void StartEvent()
    {
        var rnd = Random.Range(0, _allTileObjects.TileObjects.Count);
        _tileObject = _allTileObjects.TileObjects[rnd];
        _currentPrefab = Instantiate(_spawnPrefab, _tileObject.transform.position, Quaternion.identity);
    }
}
