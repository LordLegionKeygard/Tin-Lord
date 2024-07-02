using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingResourceExtraction : MonoBehaviour
{
    public void UpdateExtraction(Tile tile, int level, TileObject tileObject)
    {
        if (tile.Resource == null) return;
        CustomEvents.FireChangeResourceExtraction(tile.Resource.ResourceEnum, tile.UpgradeBuildingWrapper[level - 1].RecourcesAmount * StaticMethods.GetResourceModifier(tileObject), tileObject.CurrentTileId());
    }
}
