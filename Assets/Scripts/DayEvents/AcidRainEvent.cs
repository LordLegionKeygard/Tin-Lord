using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidRainEvent : MonoBehaviour
{
    [SerializeField] private SetTileNeighbours _setTileNeighbours;
    [SerializeField] private GameObject _rainPrefab;

    public void StartAcidRain()
    {
        var rnd = Random.Range(0, _setTileNeighbours.TileObjects.Count);

        var tileObject = _setTileNeighbours.TileObjects[rnd];

        var prefab = Instantiate(_rainPrefab, tileObject.transform.position, Quaternion.identity);

        prefab.GetComponent<AcidRainDealDamage>().SetTile(tileObject);
    }
}
