using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInformation", menuName = "TinLord/Info/EnemyInformation")]
public class EnemyInformation : ScriptableObject
{
    [Header("StartParams")]
    [SerializeField] private int StartExperience;
    [SerializeField] private int StartHealth;
    [SerializeField] private int StartPhysAttack;

    [Header("Factor")]
    [SerializeField] private int FactorExperience;
    [SerializeField] private int FactorHealth;
    [SerializeField] private int FactorPhysAttack;
    public int GetExperience(int level) => StartExperience + FactorExperience * (level - 1);
    public float GetHealth(int level) => StartHealth + FactorHealth * (level - 1);
    public int GetPhysAttack(int level) => StartPhysAttack + FactorPhysAttack * (level - 1);
}
