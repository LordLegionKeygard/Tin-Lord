using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttackState : TurretState
{
    [SerializeField] private TurretPatrolState _patrolState;
    [SerializeField] private TurretCombatState _combatState;
    [SerializeField] private AttackInfo _currentAttack;
    private AttackInfo _attackInfo;

    public override TurretState Tick(TurretStateChanger stateChanger, BaseAnimator animator, AIDestinationSetter aiDestinationSetter, BaseAttacks attacks)
    {
        if (aiDestinationSetter.CurrentTarget == null) return _combatState;

        stateChanger.CanRotateForwardToggle(true);

        if (IsTargetDead(aiDestinationSetter.CurrentTarget.gameObject))
        {
            aiDestinationSetter.CurrentTarget = null;
            return _patrolState;
        }

        var targetPos = aiDestinationSetter.CurrentTarget.transform.position;
        Vector3 targetDirection = GetDirectionToTarget(targetPos);
        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

        if (!stateChanger.CanAttack()) return _combatState;

        if (_currentAttack != null)
        {
            if (IsTargetWithinAttackRange(stateChanger, _currentAttack))
            {
                if (IsTargetInAttackAngle(viewableAngle, _currentAttack))
                {
                    if (CanPerformAttack(stateChanger))
                    {
                        PerformAttack(stateChanger, animator);
                        return _combatState;
                    }
                }
            }
            else
            {
                _currentAttack = null;
            }
        }
        else
        {
            _attackInfo = SelectNextAttack(attacks);
            if (IsTargetWithinAttackRange(stateChanger, _attackInfo) && IsTargetInAttackAngle(viewableAngle, _attackInfo))
            {
                _currentAttack = _attackInfo;
            }
        }

        return _combatState;
    }

    private bool IsTargetDead(GameObject target)
    {
        if (target.TryGetComponent<BaseHealth>(out BaseHealth health))
        {
            return health.IsDeath();
        }
        return false;
    }

    private Vector3 GetDirectionToTarget(Vector3 targetPos)
    {
        return new Vector3(targetPos.x, transform.position.y, targetPos.z) - transform.position;
    }

    private bool IsTargetWithinAttackRange(TurretStateChanger stateChanger, AttackInfo attack)
    {
        float distanceToTarget = stateChanger.DistanceToTarget();
        return distanceToTarget >= attack.MinimumDistanceNeededToAttack 
                && distanceToTarget <= attack.MaximumDistanceNeededToAttack;
    }

    private bool IsTargetInAttackAngle(float viewableAngle, AttackInfo attack)
    {
        return viewableAngle >= attack.MinimumAttackAngle && viewableAngle <= attack.MaximumAttackAngle;
    }

    private bool CanPerformAttack(TurretStateChanger stateChanger)
    {
        return stateChanger.CurrentAttackRecoveryTime <= 0 && stateChanger.CanAttack();
    }

    private void PerformAttack(TurretStateChanger stateChanger, BaseAnimator animator)
    {
        animator.AttackAnim(_currentAttack.ActionNumber);
        stateChanger.AttackToggle(false);
        stateChanger.CurrentAttackRecoveryTime = _currentAttack.RecoveryTime;
        _currentAttack = null;
    }

    private AttackInfo SelectNextAttack(BaseAttacks attacks)
    {
        if (attacks.AttackOneByOne())
        {
            return attacks.GetCreatureAttacks()[attacks.CurrentAttackIndex()];
        }
        else
        {
            int rnd = Random.Range(0, attacks.GetCreatureAttacks().Length);
            return attacks.GetCreatureAttacks()[rnd];
        }
    }
}
