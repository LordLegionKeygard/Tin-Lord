using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingLevels : MonoBehaviour
{
    [SerializeField] private GameObject[] _buildingLevels;
    [SerializeField] private int _currentBuildingLevel;
    private TileObject _tileObject;
    public int CurrentBuildingLevel() => _currentBuildingLevel;
    public TileObject CurrentTileObject() => _tileObject;

    public void SetBuildingView(int level, TileObject tileObject)
    {
        _tileObject = tileObject;
        _currentBuildingLevel = level;

        foreach (var item in _buildingLevels) item.SetActive(false);
        _buildingLevels[_currentBuildingLevel - 1].SetActive(true);
    }
}
