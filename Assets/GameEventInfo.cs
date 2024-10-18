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
    RadiationDecrease = 1,
    AcidRain = 2,
    MeteorStrike = 3,
}
