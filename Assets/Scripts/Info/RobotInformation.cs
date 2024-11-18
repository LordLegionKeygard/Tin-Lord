using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RobotInformation", menuName = "TinLord/Info/RobotInformation")]
public class RobotInformation : ScriptableObject
{
    public string[] Name; //0 eng, 1 rus
    public Sprite RobotSprite;
    [SerializeField] public float[] Health;
    [SerializeField] public int[] PhysAttack;
    public ResourcesForBuildWrapper[] ResourcesForBuild; // кол-во ресурсов для строительства робота
}
