using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingRotation : MonoBehaviour
{
    [SerializeField] private BuildingLevels _buildingLevels;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, _buildingLevels.CurrentTileObject().GroundTileObject().GroundModelRotation(), 0);
    }
}
