using UnityEngine;

[CreateAssetMenu(menuName = "TinLord/Info/GameEvent")]
public class GameEventInfo : ScriptableObject
{
    public GameEventType GameEventType;
    public Sprite EventIcon;
}

public enum GameEventType
{
    RadiationIncrease = 0,
    RadiationIncreaseMedium = 1,
    RadiationIncreaseStrong = 2,
    RadiationDecrease = 3,
    RadiationDecreaseMedium = 4,
    RadiationDecreaseStrong = 5,
    AcidRain = 6,
    MeteorStrike = 7,
}
