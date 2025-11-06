
using UnityEngine;

[CreateAssetMenu(fileName = "New UpgradeCard", menuName = "TinLord/UpgradeCard")]
public class TacticCard : Card
{
    public TacticCardType CardType;
}

public enum TacticCardType
{
    IncreaseDamage = 0,
    IncreaseHealth = 1,
    Repair = 2,
    OverProduction = 3,
}
