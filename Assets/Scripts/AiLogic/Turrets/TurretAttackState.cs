using UnityEngine;

public class TurretAttackState : TurretState
{
    [SerializeField] private int _attacksNumber;
    [SerializeField] private bool _attackOneByOne;
    [SerializeField] private TurretBuilding _turretBuilding;
    [SerializeField] private TurretPatrolState _patrolState;
    [SerializeField] private TurretCombatState _combatState;
    private int _currentAttackIndex = 1;
    private int _currentAttack;
    public bool AttackOneByOne() => _attackOneByOne;

    public override TurretState Tick(TurretStateChanger stateChanger, BaseAnimator animator, AIDestinationSetter aiDestinationSetter)
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

        if (_currentAttack != 0)
        {
            if (IsTargetWithinAttackRange(stateChanger))
            {
                if (IsTargetInAttackAngle(viewableAngle))
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
                _currentAttack = 0;
            }
        }
        else
        {
            _currentAttackIndex = SelectNextAttack();
            if (IsTargetWithinAttackRange(stateChanger) && IsTargetInAttackAngle(viewableAngle))
            {
                _currentAttack = _currentAttackIndex;
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

    private bool IsTargetWithinAttackRange(TurretStateChanger stateChanger)
    {
        float distanceToTarget = stateChanger.DistanceToTarget();
        return distanceToTarget >= 0
                && distanceToTarget <= _turretBuilding.Building().AttackRadius;
    }

    private bool IsTargetInAttackAngle(float viewableAngle)
    {
        return viewableAngle >= WorldGameInfo.TurretMinimumAttackAngle && viewableAngle <= WorldGameInfo.TurretMaximumAttackAngle;
    }

    private bool CanPerformAttack(TurretStateChanger stateChanger)
    {
        return stateChanger.CurrentAttackRecoveryTime <= 0 && stateChanger.CanAttack();
    }

    private void PerformAttack(TurretStateChanger stateChanger, BaseAnimator animator)
    {
        animator.AttackAnim(_currentAttack);
        stateChanger.AttackToggle(false);
        stateChanger.CurrentAttackRecoveryTime = _turretBuilding.Building().AttackRecoveryTime;
        _currentAttack = 0;
    }




    private int SelectNextAttack()
    {
        if (_attackOneByOne)
        {
            return _currentAttackIndex;
        }
        else
        {
            int rnd = Random.Range(1, _attacksNumber + 1);
            return rnd;
        }
    }

    public void ChangeAttackIndex()
    {
        if (_currentAttackIndex >= _attacksNumber)
        {
            _currentAttackIndex = 1;
        }
        else
        {
            _currentAttackIndex++;
        }
    }
}
