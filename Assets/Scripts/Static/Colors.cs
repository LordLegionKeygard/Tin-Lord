using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Colors : MonoBehaviour
{
    public static Colors Instance;
    public static readonly Color AlphaGrey = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    public static readonly Color StandartGrey = new Color(0.5f, 0.5f, 0.5f, 1);
    public static readonly Color OnOffButtonWork = new Color(0.3f, 0.7f, 0, 1);

    public Color[] SelectTileView;

    private void Awake()
    {
        if (Instance != null) Debug.Log("Two, or more Clors Instances");
        else Instance = this;
    }
}

public enum SelectTileEnum
{
    EmptyTileSelect = 0,
    TileSelect = 1,
    ErrorSelect = 2,
}
