using UnityEngine;

public class RobotCombatState : RobotState
{
    [SerializeField] private RobotAttackState _attackState;
    [SerializeField] private RobotPatrolState _patrolState;

    public override RobotState Tick(RobotStateChanger stateChanger, BaseHealth health, BaseAnimator animator, AIDestinationSetter aiDestinationSetter, RobotAttacks attacks, RobotSpeed playerSpeed)
    {
        if (aiDestinationSetter.CurrentTarget != null)
        {
            playerSpeed.CantMove();

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
                return _patrolState;
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
