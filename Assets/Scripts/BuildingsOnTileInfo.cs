using UnityEngine;

[CreateAssetMenu(fileName = "BuildingsOnTileInfo", menuName = "Info/BuildingsOnTileInfo")]
public class BuildingsOnTileInfo : ScriptableObject
{
    public BuildingsOnTileInfoWrapper[] BuildingsOnTileInfoWrapper;
}

[System.Serializable]
public class BuildingsOnTileInfoWrapper
{
    public GroundTileViewEnum GroundTileView;
    public Tile[] BuildingTiles;
}
