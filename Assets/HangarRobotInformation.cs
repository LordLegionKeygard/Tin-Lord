using UnityEngine;

[CreateAssetMenu(fileName = "HangarRobotInformation", menuName = "TinLord/Info/HangarRobotInformation")]
public class HangarRobotInformation : ScriptableObject
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
