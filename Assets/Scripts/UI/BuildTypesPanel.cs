using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BuildTypesPanel : MonoBehaviour
{
    [Inject] private DiContainer _diContainer;
    [SerializeField] private TilesSystem _tileSystem;
    [SerializeField] private BuildingType _buildingType;
    [SerializeField] private Transform _content;
    private List<GameObject> _buildingTypesList = new List<GameObject>();
    [SerializeField] private BuildsPanel _buildsPanel;


    public void SpawnBuildingTypesInScrollView(TileObject tileObject, SelectTilePanel selectTilePanel) //тип зданий
    {
        var buildingTiles = _tileSystem.TakeGroundTile(tileObject.GroundTileObject().CurrentGroundTile().GroundTileView).BuildingsOnTile;

        if (tileObject.GroundTileObject().IsBridge())
        {
            var item = _diContainer.InstantiatePrefab(_buildingType, transform.position, Quaternion.identity, null);
            item.transform.SetParent(_content);
            item.GetComponent<BuildingType>().SetBuildingType(_tileSystem.TakeBuildingTile(BuildingTileViewEnum.Bridge), tileObject, selectTilePanel, _buildsPanel);
            _buildingTypesList.Add(item.gameObject);
        }
        else
        {
            for (int i = 0; i < buildingTiles.Length; i++)
            {
                var item = _diContainer.InstantiatePrefab(_buildingType, transform.position, Quaternion.identity, null);
                item.transform.SetParent(_content);
                item.GetComponent<BuildingType>().SetBuildingType(buildingTiles[i].BuildingTile, tileObject, selectTilePanel, _buildsPanel);
                _buildingTypesList.Add(item.gameObject);
            }
        }
    }
    public void ClearListObjects()
    {
        foreach (var item in _buildingTypesList)
        {
            Destroy(item);
        }
        _buildingTypesList.Clear();
    }

    private void OnDisable()
    {
        ClearListObjects();
    }
}
