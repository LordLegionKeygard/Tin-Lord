using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticMethods
{
    public static float GetResourceModifier(TileObject tileObject)
    {
        var buildingsOnTile = tileObject.GroundTileObject().CurrentGroundTile().BuildingsOnTile;

        for (int i = 0; i < buildingsOnTile.Length; i++)
        {
            if (buildingsOnTile[i].BuildingTile == tileObject.BuildingTileObject().CurrentBuildingTile())
            {
                return buildingsOnTile[i].ResourceModifier;
            }
        }
        return 0; //данного ресурса больше нет на тайле
    }
}
