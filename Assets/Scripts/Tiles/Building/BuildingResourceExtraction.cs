using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingResourceExtraction : MonoBehaviour
{
    private Tile _tile;
    private int _level;
    private TileObject _tileObject;
    public void SetExtraction(Tile tile, int level, TileObject tileObject)
    {
        if (tile.Resource == null) return;
        _tile = tile;
        _level = level;
        _tileObject = tileObject;
        UpdateExtraction();
    }

    public void UpdateExtraction()
    {
        if(_tile == null || _level == 0 || _tileObject == null) return;
        if(_tile.Resource == null) return;
        
        CustomEvents.FireChangeResourceExtraction(_tile.Resource.ResourceEnum, _tile.UpgradeBuildingWrapper[_level - 1].ResourceExtractedAmount * StaticMethods.GetResourceModifier(_tileObject), _tileObject.CurrentTileId());
    }
}
