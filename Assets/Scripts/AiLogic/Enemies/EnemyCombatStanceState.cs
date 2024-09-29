using UnityEngine;

public class EnemyCombatStanceState : EnemyState
{
    [SerializeField] private EnemyAttackState _attackState;
    [SerializeField] private EnemyPursueTargetState _pursueTargetState;
    [SerializeField] private EnemyIdleState _enemyIdleState;

    public override EnemyState Tick(EnemyStateChanger enemyStateChanger, CreatureHealth creatureHealth, CreatureAnimator enemyAnimator, AIDestinationSetter aiDestinationSetter, BaseHealth baseHealth, CreatureAttacks creatureAttacks)
    {
        if (aiDestinationSetter.CurrentTarget != null)
        {
            enemyStateChanger.CanRotateForwardToggle(true);

            if (aiDestinationSetter.CurrentTarget.gameObject.TryGetComponent<BaseHealth>(out BaseHealth health))
            {
                if (health.IsDeath())
                {
                    aiDestinationSetter.CurrentTarget = null;
                    return _enemyIdleState;
                }
            }

            if (enemyStateChanger.CurrentAttackRecoveryTime <= 0 && enemyStateChanger.DistanceToTarget <= creatureAttacks.MaxAtkRange)
            {

                return _attackState;
            }
            else if (enemyStateChanger.DistanceToTarget > creatureAttacks.MaxAtkRange)
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
