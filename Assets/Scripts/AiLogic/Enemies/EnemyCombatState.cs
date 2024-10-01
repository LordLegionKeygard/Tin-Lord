using UnityEngine;

public class EnemyCombatState : EnemyState
{
    [SerializeField] private EnemyAttackState _attackState;
    [SerializeField] private EnemyPursueTargetState _pursueTargetState;
    [SerializeField] private EnemyIdleState _enemyIdleState;

    public override EnemyState Tick(EnemyStateChanger stateChanger, BaseHealth health, BaseAnimator animator, AIDestinationSetter aiDestinationSetter, EnemyAttacks attacks)
    {
        if (aiDestinationSetter.CurrentTarget != null)
        {
            stateChanger.CanRotateForwardToggle(true);

            if (IsTargetDead(aiDestinationSetter.CurrentTarget.gameObject))
            {
                aiDestinationSetter.CurrentTarget = null;
                return _enemyIdleState;
            }

            if (stateChanger.CurrentAttackRecoveryTime <= 0 && stateChanger.DistanceToTarget() <= attacks.MaxAtkRange())
            {
                return _attackState;
            }
            else if (stateChanger.DistanceToTarget() > attacks.MaxAtkRange())
            {
                return _pursueTargetState;
            }
            else
            {
                return this;
            }
        }
        else
        {
            return _enemyIdleState;
        }
    }

    private bool IsTargetDead(GameObject target)
    {
        if (target.TryGetComponent<BaseHealth>(out BaseHealth health))
        {
            return health.IsDeath();
        }
        return false;
    }
}
