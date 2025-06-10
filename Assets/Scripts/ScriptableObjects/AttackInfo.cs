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
    public AttackType AttackType;
}

public enum AttackType
{
    None = 0,
    Melee = 1,
    Range = 2,
}
