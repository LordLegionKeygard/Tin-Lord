using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BuildTypesPanel : MonoBehaviour
{
    [Inject] private DiContainer _diContainer;
    [SerializeField] private TilesSystem _tileSystem;
    [SerializeField] private BuildingType _buildingType;
    [SerializeField] private Transform _content;
    private List<BuildingType> _buildingTypesList = new List<BuildingType>();
    [SerializeField] private BuildsPanel _buildsPanel;

    public void SpawnBuildingTypesInScrollView(TileObject tileObject, SelectTilePanel selectTilePanel)
    {
        var buildingTypeTiles = _tileSystem.TakeGroundTile(tileObject.GroundTileObject().CurrentGroundTile().GroundTileView).BuildingTypes;

        if (tileObject.GroundTileObject().IsBridge())
        {
            var item = _diContainer.InstantiatePrefab(_buildingType, transform.position, Quaternion.identity, null);
            item.transform.SetParent(_content);
            item.GetComponent<BuildingType>().SetBuildingType(_tileSystem.TakeBuildingTile(BuildingTileViewEnum.Bridge), tileObject, selectTilePanel, _buildsPanel, this);
            _buildingTypesList.Add(item.gameObject.GetComponent<BuildingType>());
        }
        else if (tileObject.GroundTileObject().IsForwardRoad())
        {
            var item = _diContainer.InstantiatePrefab(_buildingType, transform.position, Quaternion.identity, null);
            item.transform.SetParent(_content);
            item.GetComponent<BuildingType>().SetBuildingType(_tileSystem.TakeBuildingTile(BuildingTileViewEnum.PretectiveStructures), tileObject, selectTilePanel, _buildsPanel, this);
            _buildingTypesList.Add(item.gameObject.GetComponent<BuildingType>());
        }
        else
        {
            for (int i = 0; i < buildingTypeTiles.Length; i++)
            {
                var item = _diContainer.InstantiatePrefab(_buildingType, transform.position, Quaternion.identity, null);
                item.transform.SetParent(_content);
                item.GetComponent<BuildingType>().SetBuildingType(buildingTypeTiles[i], tileObject, selectTilePanel, _buildsPanel, this);
                _buildingTypesList.Add(item.gameObject.GetComponent<BuildingType>());
            }
        }
    }

    public void PlayerInputBuildTypesButton(int number)
    {
        if (number > _buildingTypesList.Count) return;

        _buildingTypesList[number - 1].GetComponent<BuildingType>().SelectTypeButton();
    }

    public void UnselectAllTypes()
    {
        for (int i = 0; i < _buildingTypesList.Count; i++)
        {
            _buildingTypesList[i].ToggleSelectView(false);
        }
    }

    public void ClearListObjects()
    {
        foreach (var item in _buildingTypesList)
        {
            Destroy(item.gameObject);
        }
        _buildingTypesList.Clear();
    }

    private void OnDisable()
    {
        ClearListObjects();
    }
}
