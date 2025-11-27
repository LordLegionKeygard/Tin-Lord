using UnityEngine;

[CreateAssetMenu(fileName = "CityRobotInfo", menuName = "TinLord/Info/CityRobotInfo")]
public class CityRobotInfo : ScriptableObject
{
    public float Damage;
    public float AttackSpeed;
    public float AttackRadius;
    public float RotationSpeed;
    public float KnockbackPoints;
}
