using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Меняет view в зависимости от уровня здания
/// </summary>
public class BuildingLevels : MonoBehaviour
{
    [SerializeField] private GameObject[] _buildingLevels;
    [SerializeField] private BuildingTargetPoints[] _buildingTargetPoints;
    private int _currentBuildingLevel;
    private BuildingProductionView _buildingProductionView;
    private TileObject _tileObject;
    public TileObject CurrentTileObject() => _tileObject;

    private void Awake()
    {
        _buildingProductionView = GetComponent<BuildingProductionView>();
    }

    public void SetBuildingLevelView(int level, TileObject tileObject)
    {
        _tileObject = tileObject;
        _currentBuildingLevel = level;

        foreach (var item in _buildingLevels) item.SetActive(false);
        _buildingLevels[_currentBuildingLevel - 1].SetActive(true);
    }

    public void DisableAllBuilding()
    {
        foreach (var item in _buildingLevels) item.SetActive(false);
    }

    public void SetBuildingProductionView()
    {
        if (_buildingProductionView != null) _buildingProductionView.SetCurrentTileObject(_tileObject);
    }

    public Transform GetCurrentBuildingCenterTransform()
    {
        var rnd = Random.Range(0, _buildingTargetPoints[_currentBuildingLevel - 1].TargetPoints.Length);
        return _buildingTargetPoints[_currentBuildingLevel - 1].TargetPoints[rnd];
    }
}

[System.Serializable]
public class BuildingTargetPoints
{
    public Transform[] TargetPoints;
}
