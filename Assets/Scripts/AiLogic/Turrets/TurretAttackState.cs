using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttackState : TurretState
{
    [SerializeField] private TurretCombatState _combatState;
    [SerializeField] private AttackInfo _currentAttack;
    public override TurretState Tick(TurretStateChanger stateChanger, BaseAnimator animator, AIDestinationSetter aiDestinationSetter, BaseAttacks attacks)
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
            else if (stateChanger.DistanceToTarget() < _currentAttack.MaximumDistanceNeededToAttack)
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
            AttackInfo attackInfo = attacks.GetCreatureAttacks()[rnd];

            if (stateChanger.DistanceToTarget() <= attackInfo.MaximumDistanceNeededToAttack
                && stateChanger.DistanceToTarget() >= attackInfo.MinimumDistanceNeededToAttack)
            {
                if (viewableAngle <= attackInfo.MaximumAttackAngle
                    && viewableAngle >= attackInfo.MinimumAttackAngle)
                {
                    if (_currentAttack != null) return this;
                    _currentAttack = attackInfo;
                }
            }
        }

        return _combatState;
    }
}
