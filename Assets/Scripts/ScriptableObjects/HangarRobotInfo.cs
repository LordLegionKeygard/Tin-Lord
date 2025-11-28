using UnityEngine;

[CreateAssetMenu(fileName = "HangarRobotInfo", menuName = "TinLord/Info/HangarRobotInfo")]
public class HangarRobotInfo : ScriptableObject
{
    public int Name;
    public Sprite RobotSprite;
    public int Price; // нейро осколки
    public HangarRobotType HangarRobotType;

}

[System.Serializable]
public enum HangarRobotType
{
    None = -1,
    Arbalester = 0,
    Sniper = 1,
    Titan = 2,
}
