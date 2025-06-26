using UnityEngine;

public class BaseDamage : MonoBehaviour
{
    protected BaseHealth CurrentTargetBaseHealth;
    protected Transform CurrentTargetTransform;
    protected BaseAttackVFX BaseAttackVFX;
    protected float Damage;

    public virtual void Awake()
    {
        BaseAttackVFX = GetComponent<BaseAttackVFX>();
    }

    public virtual void SetDamage()
    {

    }

    public void SetTarget(BaseHealth baseHealth, Transform newTransform)
    {
        CurrentTargetBaseHealth = baseHealth;
        CurrentTargetTransform = newTransform;
    }

    public virtual void Attack(int firePointNumber)
    {
        if (BaseAttackVFX != null) BaseAttackVFX.PlayVFX(firePointNumber);
        if (CurrentTargetBaseHealth == null) return;
        CurrentTargetBaseHealth.CalculateDamage(Damage, 0);
    }

    public virtual void Shoot(int attackNumber)
    {

    }
}
