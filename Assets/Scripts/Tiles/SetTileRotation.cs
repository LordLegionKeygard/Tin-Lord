using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Сетит значение в groundTile, чтобы здание могло считать текущую позицию, используется например для моста
/// </summary>
public class SetTileRotation : MonoBehaviour
{
    [SerializeField] private int _parentCount;
    private GroundTile _groundTile;
    private void Start()
    {
        SetGroundTile();
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

        _groundTile.SetGroundModelRotation((int)transform.eulerAngles.y);
    }
}
