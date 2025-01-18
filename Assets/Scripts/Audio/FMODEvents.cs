using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    public static FMODEvents Instance;

    [Header("UiSFX")]
    public EventReference DefaultClick;
    public EventReference EscapePanel;


    [Header("Environment")]
    public EventReference[] GroundTiles;
    public EventReference LaserDestruction;


    private void Awake()
    {
        if (Instance != null) Debug.LogError("Two FMODEvents");
        Instance = this;
    }
}
