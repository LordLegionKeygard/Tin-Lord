using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDamage : BaseDamage
{
    private TurretBuilding _turretBuilding;
    [SerializeField] private TurretAttackState _turretAttackState;

    public override void Awake()
    {
        base.Awake();
        _turretBuilding = GetComponent<TurretBuilding>();
    }

    public override void Attack(int attackNumber)
    {
        if (_turretAttackState.AttackOneByOne())
        {
            _turretAttackState.ChangeAttackIndex();
        }
        if (BaseAttackVFX != null) BaseAttackVFX.PlayeVFX(attackNumber);
        if (CurrentTargetBaseHealth == null) return;
        CurrentTargetBaseHealth.CalculateDamage(Damage, _turretBuilding.Building().KnockbackPoints);
    }
}
