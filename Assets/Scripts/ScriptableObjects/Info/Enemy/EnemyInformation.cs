using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInformation", menuName = "TinLord/Info/EnemyInformation")]
public class EnemyInformation : ScriptableObject
{
    public int[] Experience;
    public float[] Health;
    public int[] PhysAttack;
}
