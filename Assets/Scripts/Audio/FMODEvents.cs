using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    public static FMODEvents Instance;

    [Header("UiSFX")]
    public EventReference[] UiClick;
    public EventReference EscapePanel;
    public EventReference Warp;

    [Header("DialogueReward")]

    public EventReference ReceivedAiCore;
    public EventReference ReceivedMemory;
    public EventReference ReceivedQuants;
    public EventReference LostAiCore;
    public EventReference LostMemory;
    public EventReference LostQuants;

    
    [Header("Environment")]
    public EventReference[] GroundTiles;
    public EventReference LaserDestruction;

    [Header("Building")]
    public EventReference CompleteConstructBuilding;
    public EventReference CompleteUpgradeBuilding;
    public EventReference DestructionBuilding;

    [Header("Machines")]
    public EventReference[] MachinesSpawn;
    public EventReference[] MachinesDeath;
    public EventReference RobotSniperExplosion;

    [Header("Mission")]
    public EventReference SelectMission;
    public EventReference StartMission;
    public EventReference[] EndMission;
    
    [Header("WorldEvents")] 
    public EventReference MeteorStrike;
    public EventReference EarthQuake;

    [Header("Enemies")]
    public EventReference[] Death;


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
    Error = 9,
}
