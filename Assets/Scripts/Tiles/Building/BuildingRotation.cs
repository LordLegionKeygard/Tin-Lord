using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Вращает обьект на ту же позицию, что и тайл земли
/// </summary>
public class BuildingRotation : MonoBehaviour
{
    [SerializeField] private BuildingLevels _buildingLevels;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, _buildingLevels.CurrentTileObject().GroundTileObject().GroundModelRotation(), 0);
    }
}
