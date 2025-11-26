using UnityEngine;

public class TurretCombatState : TurretState
{
    [SerializeField] private TurretBuilding _turretBuilding;
    [SerializeField] private TurretPatrolState _patrolState;
    [SerializeField] private TurretAttackState _attackState;

    public override TurretState Tick(TurretStateChanger stateChanger, BaseAnimator animator, AIDestinationSetter aiDestinationSetter)
    {
        var targetTransform = aiDestinationSetter.CurrentTarget;

        if (targetTransform == null)
        {
            // Нет цели — возвращаемся к патрулю
            return _patrolState;
        }

        stateChanger.CanRotateForwardToggle(true);
        float attackRadius = _turretBuilding.Building().AttackRadius;
        float distanceToTarget = stateChanger.DistanceToTarget();

        // Если цель стала невалидной или вышла за радиус — сбрасываем и уходим в патруль
        if (CheckNeedChangeTarget(targetTransform.gameObject, attackRadius: attackRadius, distanceToTarget: distanceToTarget, stateChanger.IsToxicGasActive()))
        {
            aiDestinationSetter.CurrentTarget = null;
            return _patrolState;
        }

        // Готовы атаковать и в радиусе — переходим в атаку
        if (stateChanger.CurrentAttackRecoveryTime <= 0 && distanceToTarget <= attackRadius)
        {
            return _attackState;
        }

        return this;
    }

    private bool CheckNeedChangeTarget(GameObject target, float attackRadius, float distanceToTarget, bool isToxicGasActive)
    {
        if (target == null) return true;

        if (!target.TryGetComponent<BaseHealth>(out var health)) return true;

        if (health.IsDeath() || !health.IsCanTarget()) return true;

        // вне радиуса — считаем цель непригодной для текущей турели
        if (distanceToTarget < 0f || distanceToTarget > attackRadius) return true;

        if(isToxicGasActive) return true;

        return false;
    }
}
