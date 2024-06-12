using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colors : MonoBehaviour
{
    public static Colors Instance;
    public Color[] SelectTileView;

    private void Awake()
    {
        if (Instance != null) Debug.Log("Two, or more Clors Instances");
        else Instance = this;
    }
}
