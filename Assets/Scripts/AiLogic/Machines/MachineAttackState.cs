using UnityEngine;

public class MachineAttackState : MachineState
{
    [SerializeField] private MachineCombatState _combatState;
    private AttackInfo _currentAttack;
    private AttackInfo _attackInfo;

    public override MachineState Tick(MachineStateChanger stateChanger, BaseHealth health, BaseAnimator animator, AIDestinationSetter aiDestinationSetter, MachineAttacks attacks, MachineSpeed playerSpeed)
    {
       if (aiDestinationSetter.CurrentTarget == null) return _combatState;

        stateChanger.CanRotateForwardToggle(true);

        if (IsTargetDead(aiDestinationSetter.CurrentTarget.gameObject))
        {
            aiDestinationSetter.CurrentTarget = null;
            return _combatState;
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
            if (IsTargetWithinAttackRange(stateChanger, _attackInfo)
                && IsTargetInAttackAngle(viewableAngle, _attackInfo))
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

    private bool IsTargetWithinAttackRange(MachineStateChanger stateChanger, AttackInfo attack)
    {
        float distanceToTarget = stateChanger.DistanceToTarget();
        return distanceToTarget >= attack.MinimumDistanceNeededToAttack
               && distanceToTarget <= attack.MaximumDistanceNeededToAttack;
    }

    private bool IsTargetInAttackAngle(float viewableAngle, AttackInfo attack)
    {
        return viewableAngle >= attack.MinimumAttackAngle && viewableAngle <= attack.MaximumAttackAngle;
    }

    private bool CanPerformAttack(MachineStateChanger stateChanger)
    {
        return stateChanger.CurrentAttackRecoveryTime <= 0 && stateChanger.CanAttack();
    }

    private void PerformAttack(MachineStateChanger stateChanger, BaseAnimator animator)
    {
        animator.AttackAnimation(_currentAttack.ActionNumber);
        stateChanger.AttackToggle(false);
        stateChanger.CurrentAttackRecoveryTime = _currentAttack.RecoveryTime;
        _currentAttack = null;
    }

    private AttackInfo SelectNextAttack(MachineAttacks attacks)
    {
        int rnd = Random.Range(0, attacks.GetCreatureAttacks().Length);
        return attacks.GetCreatureAttacks()[rnd];
    }
}
