using UnityEngine;

public class BaseDamage : MonoBehaviour
{
    
    protected BaseHealth CurrentTargetBaseHealth;
    protected BaseAttackVFX BaseAttackVFX;
    protected float Damage;

    public virtual void Awake()
    {
        BaseAttackVFX = GetComponent<BaseAttackVFX>();
    }

    private void Start()
    {
        SetDamage();
    }

    public virtual void SetDamage()
    {
        
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
