using UnityEngine;

public class WorldSaveData : MonoBehaviour
{
    [Header("Main")]
    public int MissionId;

    [Header("UpPanel")]
    public int Day;
    public int StartEcology;
    public int Radiation;
    public int GameSpeed; //    Pause = 0, Default = 1, Double = 2, Triple = 3,

    [Header("Experience")]
    public RobotDataWrapper[] RobotsData; //Tank = 0, Sniper = 1, Engineer = 2,

    [Header("Resources")]
    public float Stone;
    public float IronOre;
    public float CopperOre;
    public float Coal;
    public float Oil;
    public float Water;
    public float Sand;
    public float Electricity;
    public float StoneBlock;
    public float IronIngot;
    public float SteelIngot;
    public float CopperPlate;
    public float Concrete;
    public float Steam;
    public float Glass;
    public float CopperWire;
    public float GearWheel;
    public float ElectronicCircuit;
    public float Processor;
    public float Engine;
    public float ElectricEngine;
    public float MemoryFragment;
    public float BeamEnergy;

    [Header("Planet")]
    //isHaveRiver ???
    public TileDataWrapper[] TileDataWrapper;
}

[System.Serializable]
public class RobotDataWrapper
{
    public int Level;
    public int Experience;
}

[System.Serializable]
public class TileDataWrapper
{
    public int X;
    public int Y;
    public int GroundTile;
    public int GroundTileRotation;
    public int BuildingTile;
    public int BuildingTileRotation;
    public bool IsBuildingWork;

    //соседи тайла?
}
