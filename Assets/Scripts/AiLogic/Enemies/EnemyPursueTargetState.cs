using UnityEngine;

public class EnemyPursueTargetState : EnemyState
{
    [SerializeField] private EnemyCombatStanceState _combatStanceState;
    [SerializeField] private EnemyIdleState _enemyIdleState;
    public override EnemyState Tick(EnemyStateChanger enemyStateChanger, CreatureHealth enemyHealth, CreatureAnimator enemyAnimator, AIDestinationSetter aiDestinationSetter, BaseHealth baseHealth, CreatureAttacks creatureAttacks)
    {
        if (aiDestinationSetter.CurrentTarget != null)
        {
            if (aiDestinationSetter.CurrentTarget.gameObject.TryGetComponent<BaseHealth>(out BaseHealth health))
            {
                if (health.IsDeath())
                {
                    return _combatStanceState;
                }
            }

            enemyStateChanger.CanRotateForwardToggle(false);



            if (enemyStateChanger.DistanceToTarget <= creatureAttacks.MaxAtkRange)
            {
                return _combatStanceState;
            }
            return this;
        }
        else
        {
            return _enemyIdleState;
        }
    }
}
