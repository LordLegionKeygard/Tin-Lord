using UnityEngine;

public class BaseDamage : MonoBehaviour
{
    private BaseLevel _baseLevel;
    protected BaseHealth CurrentTargetBaseHealth;
    protected BaseAttackVFX BaseAttackVFX;
    protected float Damage;

    public virtual void Awake()
    {
        _baseLevel = GetComponent<BaseLevel>();
        BaseAttackVFX = GetComponent<BaseAttackVFX>();
    }

    private void Start()
    {
        SetDamage();
    }

    private void SetDamage()
    {
        Damage = _baseLevel.GetAiLevelInformation().PhysAttack[_baseLevel.GetLevel()];
    }

    public void SetTargetHealth(BaseHealth baseHealth)
    {
        CurrentTargetBaseHealth = baseHealth;
    }

    public virtual void Attack(int attackNumber)
    {
        if (BaseAttackVFX != null) BaseAttackVFX.PlayVFX(attackNumber);
        if (CurrentTargetBaseHealth == null) return;
        CurrentTargetBaseHealth.CalculateDamage(Damage, 0);
    }
}
