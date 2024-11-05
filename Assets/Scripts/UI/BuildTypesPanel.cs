using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BuildTypesPanel : MonoBehaviour
{
    [Inject] private DiContainer _diContainer;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TilesSystem _tileSystem;
    [SerializeField] private BuildingType _buildingType;
    [SerializeField] private RectTransform _content;
    private List<BuildingType> _buildingTypesList = new();
    [SerializeField] private BuildsPanel _buildsPanel;

    public void SpawnBuildingTypesInScrollView(TileObject tileObject, SelectTilePanel selectTilePanel)
    {
        ResetText();
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

        LayoutRebuilder.ForceRebuildLayoutImmediate(_content);
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

        ResetText();
    }

    public void ResetText() => SetBuildingTypeText(Language.TextStatic[10]);

    public void SetBuildingTypeText(string text)
    {
        _nameText.text = text;
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
        ResetText();
    }
}
