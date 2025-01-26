using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    public static FMODEvents Instance;

    [Header("UiSFX")]
    public EventReference[] UiClick;
    public EventReference EscapePanel;


    [Header("Environment")]
    public EventReference[] GroundTiles;
    public EventReference LaserDestruction;

    [Header("Building")]
    public EventReference CompleteConstructBuilding;
    public EventReference CompleteUpgradeBuilding;
    public EventReference DestructionBuilding;

    [Header("Robot")]
    public EventReference RobotSpawn;
    public EventReference RobotDeath;
    public EventReference RobotExplosion;

    [Header("Mission")]
    public EventReference SelectMission;
    public EventReference StartMission;


    private void Awake()
    {
        if (Instance != null) Debug.LogError("Two FMODEvents");
        Instance = this;
    }
}

[System.Serializable]
public enum UiClickEnum
{
    Default = 0,
    Terminal = 1,
    Repair = 2,
    GameSpeed = 3,
    Work = 4,
    Rotate = 5,
    Card = 6,
    SelectTile = 7,
    LearnBuilding = 8,
    StartConstruct = 9,
}
