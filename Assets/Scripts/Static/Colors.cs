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
    public static readonly Color WarningRed = new Color(0.75f, 0, 0, 1);
    public static readonly Color FadedYellow = new Color(0.8f, 0.55f, 0, 1);
    public static readonly Color LightGreen = new Color(0.25f, 0.75f, 0, 1);
    public static readonly Color Green = new Color(0, 0.85f, 0, 1);
    public static readonly Color ConstructionBlue = new Color(0.2f, 0.44f, 0.8f, 1);
    public static readonly Color DecayYellow = new Color(0.8f, 0.56f, 0, 1);

    public static readonly Color CommonRarity = new Color(0.8f, 0.8f, 0.8f, 1);
    public static readonly Color UncommonRarity = new Color(0.25f, 0.75f, 0, 1);
    public static readonly Color RareRarity = new Color(0, 0.44f, 0.86f, 1);
    public static readonly Color EpicRarity = new Color(0.64f, 0.2f, 0.93f, 1);
    public static readonly Color LegendaryRarity = new Color(0.84f, 0.43f, 0.01f, 1);

    [Header("Hex")]
    public static readonly string HexCommonRarity = "#CCCCCC";
    public static readonly string HexUncommonRarity = "#40BF00";
    public static readonly string HexRareRarity = "#0070DB";
    public static readonly string HexEpicRarity = "#A333ED";
    public static readonly string HexLegendaryRarity = "#D66E03";

    public static string HexWhite = "#FFFFFF";
    public static string HexWarningYellow = "#FFA800";
    public static string HexGreen = "#00FF00";
    public static string HexLightGreen = "#40BF00";
    public static string HexRed = "#FF0000";
    public static string HexGreySeven = "#B7B7B7";

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
                return HexRed;
            case 0.5f:
                return HexWarningYellow;
            case 1:
                return HexWhite;
            case > 1:
                return HexLightGreen;
        }
        return HexWhite;
    }

    public static string GetSelectTilePanelEcologyColor(float ecology)
    {
        switch (ecology)
        {
            case > 0:                       // положительные значения
                return HexLightGreen;

            case 0:                         // ровно ноль
                return HexWhite;

            case < 0 and >= -3:             // -0.0001 … -3 включительно
                return HexWarningYellow;

            default:                        // всё, что меньше -3
                return HexRed;
        }
    }

    public static string GetSelectTilePanelProductionColor(float production)
    {
        return production == 0 ? HexRed : HexWhite;
    }

    public static Color GetRarityColor(int rarity)
    {
        return rarity switch
        {
            1 => CommonRarity,
            2 => UncommonRarity,
            3 => RareRarity,
            4 => EpicRarity,
            5 => LegendaryRarity,
            _ => CommonRarity,
        };
    }

    public static string GetRarityHexColor(int rarity)
    {
        return rarity switch
        {
            1 => HexCommonRarity,
            2 => HexUncommonRarity,
            3 => HexRareRarity,
            4 => HexEpicRarity,
            5 => HexLegendaryRarity,
            _ => HexCommonRarity,
        };
    }
}

public enum SelectTileEnum
{
    EmptyTileSelect = 0,
    TileSelect = 1,
    ErrorSelect = 2,
}
