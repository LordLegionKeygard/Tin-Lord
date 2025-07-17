using UnityEngine;

public class Colors : MonoBehaviour
{
    public static Colors Instance;
    public static readonly Color GreyFive = new Color(0.5f, 0.5f, 0.5f, 1);
    public static readonly Color GreySix = new Color(0.6f, 0.6f, 0.6f, 1);
    public static readonly Color GreySeven = new Color(0.7f, 0.7f, 0.7f, 1);
    public static readonly Color GreyEight = new Color(0.8f, 0.8f, 0.8f, 1);
    public static readonly Color AlphaGreyFive = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    public static readonly Color AlphaGreySeven = new Color(0.7f, 0.7f, 0.7f, 0.5f);
    public static readonly Color WarningYellow = new Color(1, 0.7f, 0, 1);
    public static readonly Color FadedYellow = new Color(0.8f, 0.55f, 0, 1);
    public static readonly Color LightGreen = new Color(0.25f, 0.75f, 0, 1);
    public static readonly Color ConstructionBlue = new Color(0.2f, 0.44f, 0.8f, 1);
    public static readonly Color DecayYellow = new Color(0.8f, 0.56f, 0, 1);

    public static string HexColorWhite = "#FFFFFF";
    public static string HexColorWarningYellow = "#FFA800";
    public static string HexColorGreen = "#00FF00";
    public static string HexColorLightGreen = "#40BF00";
    public static string HexColorRed = "#FF0000";

    //MemoryRecovery
    public static readonly Color MemoryOn = new Color(0, 3, 12, 1);

    //Enemies Debuff
    public static readonly Color SlowEmission = new Color(0.02f, 0.04f, 0.15f, 1);

    public Color[] SelectTileView;

    private void Awake()
    {
        if (Instance != null) Debug.Log("Two, or more Clors Instances");
        else Instance = this;
    }

    public static string GetSelectTilePanelProductionModifierColor(float modifier)
    {
        switch (modifier)
        {
            case 0:
                return HexColorRed;
            case 0.5f:
                return HexColorWarningYellow;
            case 1:
                return HexColorWhite;
            case > 1:
                return HexColorLightGreen;
        }
        return HexColorWhite;
    }

    public static string GetSelectTilePanelEcologyColor(float ecology)
    {
        switch (ecology)
        {
            case > 0:
                return HexColorLightGreen;
            case 0:
                return HexColorWhite;
            case -1:
                return HexColorWarningYellow;
            case -2:
                return HexColorWarningYellow;
            case -3:
                return HexColorWarningYellow;
        }
        return HexColorRed;
    }

    public static string GetSelectTilePanelProductionColor(float production)
    {
        return production == 0 ? HexColorRed : HexColorWhite;
    }
}

public enum SelectTileEnum
{
    EmptyTileSelect = 0,
    TileSelect = 1,
    ErrorSelect = 2,
}
