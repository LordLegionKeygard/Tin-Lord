using UnityEngine;

[CreateAssetMenu(fileName = "LightTimeInfo", menuName = "TinLord/Info/LightTimeInfo")]
public class LightTimeInfo : ScriptableObject
{
    public DayLightTimeWrapper[] DayLightTimeWrapper;
}

[System.Serializable]
public class DayLightTimeWrapper
{
    public DayLightTime DayLightTime;
    public Vector2 LightRotation;
}



[System.Serializable]
public enum DayLightTime
{
    Morning = 0,
    Daytime = 1,
    Evening = 2,
    Night = 3,

}
