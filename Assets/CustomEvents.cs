using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomEvents
{
    public static event Action OnSpawnAllTiles;
    public static void FireSpawnAllTiles()
    {
        OnSpawnAllTiles?.Invoke();
    }
}
