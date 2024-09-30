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

            if (aiDestinationSetter.CurrentTarget.gameObject.TryGetComponent<BaseHealth>(out BaseHealth h))
            {
                if (h.IsDeath())
                {
                    aiDestinationSetter.CurrentTarget = null;
                    return _enemyIdleState;
                }
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
}
