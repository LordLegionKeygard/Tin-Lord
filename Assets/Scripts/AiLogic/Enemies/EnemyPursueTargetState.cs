using UnityEngine;

public class EnemyPursueTargetState : EnemyState
{
    [SerializeField] private EnemyCombatState _combatState;
    [SerializeField] private EnemyIdleState _idleState;

    public override EnemyState Tick(EnemyStateChanger stateChanger, BaseHealth health, BaseAnimator animator, AIDestinationSetter aiDestinationSetter, EnemyAttacks attacks)
    {
        if (aiDestinationSetter.CurrentTarget != null)
        {
            if (IsTargetDead(aiDestinationSetter.CurrentTarget.gameObject))
            {
                return _combatState;
            }

            stateChanger.CanRotateForwardToggle(false);

            // Debug.Log($"DistanceToTarget {stateChanger.DistanceToTarget()} <= MaxAtkRange {attacks.MaxAtkRange()}");

            if (stateChanger.DistanceToTarget() <= attacks.MaxAtkRange())
            {
                return _combatState;
            }
            return this;
        }
        else
        {
            return _idleState;
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
