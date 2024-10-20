using UnityEngine;

public class Colors : MonoBehaviour
{
    public static Colors Instance;
    public static readonly Color AlphaGrey = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    public static readonly Color DarkGrey = new Color(0.5f, 0.5f, 0.5f, 1);
    public static readonly Color WarningYellow = new Color(1, 0.7f, 0, 1);
    public static readonly Color FadedYellow = new Color(0.8f, 0.55f, 0, 1);
    public static readonly Color Grey = new Color(0.7f, 0.7f, 0.7f, 1);
    public static readonly Color LightGrey = new Color(0.8f, 0.8f, 0.8f, 1);
    public static readonly Color LightGreen = new Color(0.25f, 0.75f, 0, 1);

    public static string HexColorWhite = "#FFFFFF";
    public static string HexColorWarningYellow = "#FFA800";

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
