using UnityEngine;

[CreateAssetMenu(menuName = "TinLord/Creatures/Attack")]
public class CreatureAttack : ScriptableObject
{
    public int ActionNumber;
    public float RecoveryTime = 2;

    public float MinimumDistanceNeededToAttack = 0;
    public float MaximumDistanceNeededToAttack = 1.8f;

    public float MaximumAttackAngle = 35;
    public float MinimumAttackAngle = -35;
}
