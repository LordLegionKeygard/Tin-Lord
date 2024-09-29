using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllEnemyInformation", menuName = "TinLord/Info/Enemy")]
public class EnemiesInformation : ScriptableObject
{
    [SerializeField] public float[] Health;
    [SerializeField] public int[] PhysAttack;
}
