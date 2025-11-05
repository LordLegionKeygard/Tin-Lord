using UnityEngine;
using Crosstales.TrueRandom;

public class RarityCardsSystem : MonoBehaviour
{
    public int GetRarity()
    {
        var rndList = TRManager.Instance.GenerateIntegerPRNG(0, 100);
        var rnd = rndList[0];
        int rarity;

        if (rnd < 80) rarity = 1;
        else if (rnd >= 80 && rnd < 90) rarity = 2;
        else if (rnd >= 90 && rnd < 96) rarity = 3;
        else if (rnd >= 96 && rnd < 99) rarity = 4;
        else rarity = 5;

        return rarity;
    }

    public Color GetRarityColor(int rarity)
    {
        return rarity switch
        {
            1 => Colors.CommonRarity,
            2 => Colors.UncommonRarity,
            3 => Colors.RareRarity,
            4 => Colors.EpicRarity,
            5 => Colors.LegendaryRarity,
            _ => Colors.CommonRarity,
        };
    }
}

[System.Serializable]
public enum CardRarityEnum
{
    Udentified = 0,
    Common = 1, //Grey 80%
    Uncommon = 2, //Green 10%
    Rare = 3, //Blue 6%
    Epic = 4, //Violet  3%
    Legendary = 5, //Orange 1%
}
