using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTile : MonoBehaviour
{
   [SerializeField] private Tile _currentBuildingTile; 
   [SerializeField] private Transform _buildingParent;
   [SerializeField] private GameObject _currentBuildingTileObject;
}
