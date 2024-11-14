using UnityEngine;

[CreateAssetMenu(menuName = "TinLord/Info/AttackInfo")]
public class AttackInfo : ScriptableObject
{
    public int ActionNumber;
    public float RecoveryTime = 2;
    public float MinimumDistanceNeededToAttack = 0;
    public float MaximumDistanceNeededToAttack = 9;
    public float MaximumAttackAngle = 35;
    public float MinimumAttackAngle = -35;
}
