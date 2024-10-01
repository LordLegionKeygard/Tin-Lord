using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCombatState : TurretState
{
    [SerializeField] private TurretPatrolState _patrolState;
    [SerializeField] private TurretAttackState _attackState;
    public override TurretState Tick(TurretStateChanger stateChanger, BaseAnimator animator, AIDestinationSetter aiDestinationSetter, BaseAttacks attacks)
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
