using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomEvents
{
    public static event Action OnPrepareRoads;
    public static void FirePrepareRoads()
    {
        OnPrepareRoads?.Invoke();
    }

    public static event Action OnResetLastRiverTile;
    public static void FireResetLastRiverTile()
    {
        OnResetLastRiverTile?.Invoke();
    }
}
