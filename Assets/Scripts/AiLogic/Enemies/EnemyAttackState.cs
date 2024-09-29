using UnityEngine;


public class EnemyAttackState : EnemyState
{
    [SerializeField] private EnemyCombatStanceState _combatStanceState;
    [SerializeField] private CreatureAttack _currentAttack;

    public override EnemyState Tick(EnemyStateChanger enemyStateChanger, CreatureHealth enemyHealth, CreatureAnimator enemyAnimator, AIDestinationSetter aiDestinationSetter, BaseHealth baseHealth, CreatureAttacks creatureAttacks)
    {
        if (aiDestinationSetter.CurrentTarget == null) return _combatStanceState;

        enemyStateChanger.CanRotateForwardToggle(true);

        if (aiDestinationSetter.CurrentTarget.gameObject.TryGetComponent<BaseHealth>(out BaseHealth health))
        {
            if (health.IsDeath())
            {
                aiDestinationSetter.CurrentTarget = null;
                return _combatStanceState;
            }
        }

        var targetPos = aiDestinationSetter.CurrentTarget.transform.position;
        Vector3 targetDirection = new Vector3(targetPos.x, transform.position.y, targetPos.z) - transform.position;
        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

        if (!enemyStateChanger.CanAttack())
            return _combatStanceState;

        if (_currentAttack != null)
        {
            if (enemyStateChanger.DistanceToTarget < _currentAttack.MinimumDistanceNeededToAttack)
            {
                _currentAttack = null;
                return this;
            }
            else if (enemyStateChanger.DistanceToTarget < _currentAttack.MaximumDistanceNeededToAttack + creatureAttacks.GetBonusAttackDistance())
            {
                if (viewableAngle <= _currentAttack.MaximumAttackAngle &&
                    viewableAngle >= _currentAttack.MinimumAttackAngle)
                {

                    if (enemyStateChanger.CurrentAttackRecoveryTime <= 0 && enemyStateChanger.CanAttack())
                    {
                        enemyAnimator.AttackAnim(_currentAttack.ActionNumber);
                        enemyStateChanger.AttackToggle(false);

                        enemyStateChanger.CurrentAttackRecoveryTime = _currentAttack.RecoveryTime;
                        _currentAttack = null;
                        return _combatStanceState;
                    }

                }
            }
        }
        else
        {
            var rnd = Random.Range(0, creatureAttacks.GetCreatureAttacks().Length);
            CreatureAttack enemyAttackAction = creatureAttacks.GetCreatureAttacks()[rnd];

            if (enemyStateChanger.DistanceToTarget <= enemyAttackAction.MaximumDistanceNeededToAttack + creatureAttacks.GetBonusAttackDistance()
                && enemyStateChanger.DistanceToTarget >= enemyAttackAction.MinimumDistanceNeededToAttack)
            {
                if (viewableAngle <= enemyAttackAction.MaximumAttackAngle
                    && viewableAngle >= enemyAttackAction.MinimumAttackAngle)
                {
                    if (_currentAttack != null) return this;
                    _currentAttack = enemyAttackAction;
                }
            }
        }

        return _combatStanceState;
    }
}
