using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLevelInformation", menuName = "TinLord/Info/PlayerLevelInformation")]
public class PlayerLevelInformation : ScriptableObject
{
    [SerializeField] public float[] Health;
    [SerializeField] public int[] PhysAttack;
}
