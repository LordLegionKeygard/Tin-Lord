using UnityEngine;

public class EnemyPursueTargetState : EnemyState
{
    [SerializeField] private EnemyCombatState _combatState;
    [SerializeField] private EnemyIdleState _idleState;
    public override EnemyState Tick(EnemyStateChanger stateChanger, BaseHealth health, BaseAnimator animator, AIDestinationSetter aiDestinationSetter, EnemyAttacks attacks)
    {
        if (aiDestinationSetter.CurrentTarget != null)
        {
            if (aiDestinationSetter.CurrentTarget.gameObject.TryGetComponent<BaseHealth>(out BaseHealth h))
            {
                if (h.IsDeath())
                {
                    return _combatState;
                }
            }

            stateChanger.CanRotateForwardToggle(false);



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
}
