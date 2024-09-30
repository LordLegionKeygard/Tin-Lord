using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AiLevelInformation", menuName = "TinLord/Info/AiLevelInformation")]
public class AiLevelInformation : ScriptableObject
{
    [SerializeField] public float[] Health;
    [SerializeField] public int[] PhysAttack;
}
