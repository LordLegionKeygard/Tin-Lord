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
    Patch = 0,
    Titan = 1,
    AimBot = 2,
}
