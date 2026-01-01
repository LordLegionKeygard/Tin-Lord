using UnityEngine;


[CreateAssetMenu(fileName = "New Tile", menuName = "TinLord/Tile")]
public class Tile : Card
{
    [Header("TextInfo")]

    [Header("Base")]
    public GameObject TileObject;
    public TileTypeEnum TileTypeEnum;

    [Header("Ground")]
    public GroundTileViewEnum GroundTileView;
    public float GroundEcology;
    public bool IsWater;
    public bool IsFourTile;
    public bool CanBuildMachineProduction;
    public Tile[] BuildingTypes;
    public ProductionOnGroundResources[] ProductionOnGroundResources;

    [Header("Building")]
    public BuildingTileViewEnum BuildingTileView;
    public Building[] Buildings;
    public bool IsHaveProductionResources() => Buildings[0].ResourcesProduction.Length > 0; //берем 0 здание так как у нас обычно все здания определенного типа имеют ресурс или нет

    [Header("Other")]
    public Material MaterialForRift;


    public float GetEnergyBeam()
    {
        if (GroundEcology < 0)
        {
            return Mathf.Abs(GroundEcology * 2);
        }

        return GroundEcology + 1;
    }
}

[System.Serializable]
public class ProductionOnGroundResources
{
    public Resource ProductionOnGroundResource;
    public float ProductionOnGroundResourceModifier;
}

[System.Serializable]
public class BuildingsOnTile
{
    public Tile BuildingTile;
    public float ResourceModifier;
}

public enum TileTypeEnum
{
    Ground = 0,
    Building = 1,
}

public enum GroundTileViewEnum
{
    None = 0,
    Plain = 1,
    Meadow = 2,
    Road = 3,
    Forest = 4,
    Mountain = 5,
    IronDeposits = 6,
    CopperDeposits = 7,
    OilSwamp = 8,
    Desert = 9,
    Barrenland = 10,
    Ground = 11,
    CoalDeposits = 12,
    Highland = 13,
    River = 14,
    PollutedRiver = 15,
    DeadForest = 16,
    Oasis = 17,
    DesertRiver = 18,
    ScarceCoalDeposits = 19,
    BaseFoundation = 20,
    BlackDesert = 21,
    DriedOasis = 22,
    Volcano = 23,
    BlazingField = 24,
    OvergrownMountain = 25,
    Rift = 26,
    Crater = 27,
    Grove = 28,
}

public enum BuildingTileViewEnum
{
    None = -1,
    Base = 0,
    ElectricPowerIndustry = 1,
    CoalMining = 2,
    OreMining = 3,
    WoodExtraction = 4,
    SandMining = 5,
    OilProduction = 6,
    StoneMining = 7,
    WaterExtraction = 8,
    Bridge = 9,
    StoneBlockProduction = 10,
    SmeltingProduction = 11,
    ConcreteProduction = 12,
    SteamProduction = 13,
    ComponentsProduction = 14,
    AttackingStructures = 15,
    Walls = 16,
    EcologyPurifier = 17,
    RadioCommunication = 18,
    MachineProduction = 19,
    Traps = 20,
    Gates = 21
}

public enum TileDirectionEnum
{
    North = 0,
    NorthEast = 1,
    East = 2,
    SouthEast = 3,
    South = 4,
    SouthWest = 5,
    West = 6,
    NorthWest = 7
}
