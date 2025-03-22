using UnityEngine;

[CreateAssetMenu(fileName = "MachineInformation", menuName = "TinLord/Info/MachineInformation")]
public class MachineInformation : ScriptableObject
{
    public string[] Name; //0 eng, 1 rus
    public Sprite MachineSprite;
    public MachineType MachineType;
    public float DetectionRadius;
    public float[] Durability;
    public int[] MeleeDamage;
    public int[] RangeDamage;
    public ResourceWrapper[] ResourcesForBuild; // кол-во ресурсов для строительства робота
}
