using UnityEngine;


public class EnemyAttackState : EnemyState
{
    [SerializeField] private EnemyCombatState _combatState;
    [SerializeField] private AttackInfo _currentAttack;

    public override EnemyState Tick(EnemyStateChanger stateChanger, BaseHealth health, BaseAnimator animator, AIDestinationSetter aiDestinationSetter, EnemyAttacks attacks)
    {
        if (aiDestinationSetter.CurrentTarget == null) return _combatState;

        stateChanger.CanRotateForwardToggle(true);

        if (aiDestinationSetter.CurrentTarget.gameObject.TryGetComponent<BaseHealth>(out BaseHealth h))
        {
            if (h.IsDeath())
            {
                aiDestinationSetter.CurrentTarget = null;
                return _combatState;
            }
        }

        var targetPos = aiDestinationSetter.CurrentTarget.transform.position;
        Vector3 targetDirection = new Vector3(targetPos.x, transform.position.y, targetPos.z) - transform.position;
        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

        if (!stateChanger.CanAttack())
            return _combatState;

        if (_currentAttack != null)
        {
            if (stateChanger.DistanceToTarget() < _currentAttack.MinimumDistanceNeededToAttack)
            {
                _currentAttack = null;
                return this;
            }
            else if (stateChanger.DistanceToTarget() < _currentAttack.MaximumDistanceNeededToAttack + attacks.GetBonusAttackDistance())
            {
                if (viewableAngle <= _currentAttack.MaximumAttackAngle &&
                    viewableAngle >= _currentAttack.MinimumAttackAngle)
                {

                    if (stateChanger.CurrentAttackRecoveryTime <= 0 && stateChanger.CanAttack())
                    {
                        animator.AttackAnim(_currentAttack.ActionNumber);
                        stateChanger.AttackToggle(false);

                        stateChanger.CurrentAttackRecoveryTime = _currentAttack.RecoveryTime;
                        _currentAttack = null;
                        return _combatState;
                    }

                }
            }
        }
        else
        {
            var rnd = Random.Range(0, attacks.GetCreatureAttacks().Length);
            AttackInfo enemyAttackAction = attacks.GetCreatureAttacks()[rnd];

            if (stateChanger.DistanceToTarget() <= enemyAttackAction.MaximumDistanceNeededToAttack + attacks.GetBonusAttackDistance()
                && stateChanger.DistanceToTarget() >= enemyAttackAction.MinimumDistanceNeededToAttack)
            {
                if (viewableAngle <= enemyAttackAction.MaximumAttackAngle
                    && viewableAngle >= enemyAttackAction.MinimumAttackAngle)
                {
                    if (_currentAttack != null) return this;
                    _currentAttack = enemyAttackAction;
                }
            }
        }

        return _combatState;
    }
}
