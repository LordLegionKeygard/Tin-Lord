using UnityEngine;

[CreateAssetMenu(fileName = "RobotInformation", menuName = "TinLord/Info/RobotInformation")]
public class RobotInformation : ScriptableObject
{
    public string[] Name; //0 eng, 1 rus
    public Sprite RobotSprite;
    public RobotType RobotType;
    public float DetectionRadius;
    public float[] Durability;
    public int[] MeleeDamage;
    public int[] RangeDamage;
    public ResourceWrapper[] ResourcesForBuild; // кол-во ресурсов для строительства робота
}
