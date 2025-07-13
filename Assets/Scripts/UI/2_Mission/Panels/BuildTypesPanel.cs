using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BuildTypesPanel : MonoBehaviour
{
    [Inject] private DiContainer _diContainer;
    [Inject] private readonly TilesSystem _tilesSystem;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private BuildingType _buildingType;
    [SerializeField] private RectTransform _content;
    private List<BuildingType> _buildingTypesList = new();
    [SerializeField] private BuildsPanel _buildsPanel;

    public void SpawnBuildingTypesInScrollView(TileObject tileObject, SelectTilePanel selectTilePanel)
    {
        ResetText();
        var buildingTypeTiles = _tilesSystem.GetGroundTileForEnum(tileObject.GroundTileObject().CurrentGroundTile().GroundTileView).BuildingTypes;

        if (tileObject.GroundTileObject().IsBridge())
        {
            CreateAndSetupBuildingType(_tilesSystem.GetBuildingTileForEnum(BuildingTileViewEnum.Bridge), tileObject, selectTilePanel);
        }
        // else if (tileObject.GroundTileObject().IsForwardRoad())
        // {
        //     CreateAndSetupBuildingType(_tilesSystem.GetBuildingTileForEnum(BuildingTileViewEnum.ProtectiveStructures), tileObject, selectTilePanel);
        // }
        else
        {
            foreach (var tile in buildingTypeTiles)
            {
                CreateAndSetupBuildingType(tile, tileObject, selectTilePanel);
            }

            if (tileObject.GetNearNeighbourCrossRoad() != null && !_tilesSystem.IsHaveMachineProduction())
            {
                CreateAndSetupBuildingType(_tilesSystem.GetBuildingTileForEnum(BuildingTileViewEnum.MachineProduction), tileObject, selectTilePanel);
            }
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(_content);
    }

    private void CreateAndSetupBuildingType(Tile tile, TileObject tileObject, SelectTilePanel selectTilePanel)
    {
        var item = _diContainer.InstantiatePrefab(_buildingType, transform.position, Quaternion.identity, null);
        item.transform.SetParent(_content, false);
        item.transform.localScale = Vector3.one;
        item.GetComponent<BuildingType>().SetBuildingType(tile, tileObject, selectTilePanel, _buildsPanel, this);
        _buildingTypesList.Add(item.GetComponent<BuildingType>());
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
