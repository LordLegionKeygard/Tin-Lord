using UnityEngine;

public class CityRobotCombatState : CityRobotState
{
    [SerializeField] private CityRobotPatrolState _patrolState;
    [SerializeField] private CityRobotAttackState _attackState;

    public override CityRobotState Tick(CityRobotStateChanger stateChanger, BaseAnimator animator, AIDestinationSetter aiDestinationSetter)
    {
        if (aiDestinationSetter.CurrentTarget != null)
        {
            animator.IsCombat(true);
            stateChanger.CanRotateForwardToggle(true);
            float attackRadius = stateChanger.GetAttackRadius();
            float distanceToTarget = stateChanger.DistanceToTarget();

            // Если цель стала невалидной или вышла за радиус — сбрасываем и уходим в патруль
            if (CheckNeedChangeTarget(aiDestinationSetter.CurrentTarget.gameObject, attackRadius: attackRadius, distanceToTarget: distanceToTarget))
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
        else
        {
            return _patrolState;
        }
    }

    private bool CheckNeedChangeTarget(GameObject target, float attackRadius, float distanceToTarget)
    {
        if (target == null) return true;

        if (!target.TryGetComponent<BaseHealth>(out var health)) return true;

        if (health.IsDeath() || !health.IsCanTarget()) return true;

        // вне радиуса — считаем цель непригодной для текущей турели
        if (distanceToTarget < 0f || distanceToTarget > attackRadius) return true;

        return false;
    }
}
