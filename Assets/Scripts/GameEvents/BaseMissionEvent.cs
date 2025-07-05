using UnityEngine;

public class BaseMissionEvent : MonoBehaviour
{
    [SerializeField] private AllTileObjects _allTileObjects;
    private TileObject _tileObject;
    protected AllTileObjects GetAllTileObjects() => _allTileObjects;
    protected TileObject GetTileObject() => _tileObject;

    public virtual void StartEvent()
    {
        var rnd = Random.Range(0, _allTileObjects.TileObjects.Count);
        _tileObject = _allTileObjects.TileObjects[rnd];
    }
}
