using UnityEngine;

public class PlayerCombatState : PlayerState
{
    [SerializeField] private PlayerAttackState _attackState;
    [SerializeField] private PlayerPatrolState _patrolState;

    public override PlayerState Tick(PlayerStateChanger stateChanger, BaseHealth health, BaseAnimator animator, AIDestinationSetter aiDestinationSetter, PlayerAttacks attacks)
    {
        if (aiDestinationSetter.CurrentTarget != null)
        {
            stateChanger.CanRotateForwardToggle(true);

            if (IsTargetDead(aiDestinationSetter.CurrentTarget.gameObject))
            {
                aiDestinationSetter.CurrentTarget = null;
                return _patrolState;
            }

            if (stateChanger.CurrentAttackRecoveryTime <= 0 && stateChanger.DistanceToTarget() <= attacks.MaxAtkRange())
            {
                return _attackState;
            }
            else if (stateChanger.DistanceToTarget() > attacks.MaxAtkRange())
            {
                return _patrolState; // так как не можем преследовать
            }
            else
            {
                return this;
            }
        }
        else
        {
            return _patrolState;
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
