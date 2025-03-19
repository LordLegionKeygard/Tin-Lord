using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInformation", menuName = "TinLord/Info/EnemyInformation")]
public class EnemyInformation : ScriptableObject
{
    public int GetExperience(int level) => 2 + 2 * level - 1;
    public float GetHealth(int level) => 6 + 3 * level - 1;
    public int GetPhysAttack(int level) => 1 + 1 * level - 1;
}
