using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Случайным образом вращает тайл земли по оси y. Так же сетит значение в groundTile, чтобы здание могло считать текущую позицию.
/// </summary>
public class RandomTileRotation : MonoBehaviour
{
    [SerializeField] private int _parentCount;
    [SerializeField] private bool _isSetGroundModelRotation;
    private GroundTile _groundTile;
    private void Start()
    {
        if (_isSetGroundModelRotation) SetGroundTile();
        RandomRotation();
    }
    
    private void SetGroundTile()
    {
        switch (_parentCount)
        {
            case 3:
                _groundTile = gameObject.transform.parent.parent.parent.GetComponent<GroundTile>();

                break;
            case 4:
                _groundTile = gameObject.transform.parent.parent.parent.parent.GetComponent<GroundTile>();

                break;
        }
    }

    private void RandomRotation()
    {
        var rnd = Random.Range(0, 3);
        var newRotation = rnd * 90;

        transform.rotation = Quaternion.Euler(0, newRotation, 0);

        if (_isSetGroundModelRotation) _groundTile.SetGroundModelRotation(newRotation);
    }

}
