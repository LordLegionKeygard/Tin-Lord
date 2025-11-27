using UnityEngine;

public class CityRobotAttackState : CityRobotState
{
    [SerializeField] private int _attacksNumber;
    [SerializeField] private CityRobotCombatState _combatState;
    private int _currentAttackIndex = 1;
    private int _currentAttack;

    public override CityRobotState Tick(CityRobotStateChanger stateChanger, BaseAnimator animator, AIDestinationSetter aiDestinationSetter)
    {
        var targetTransform = aiDestinationSetter.CurrentTarget;
        if (targetTransform == null)
        {
            // Нет цели — возвращаемся к боевой логике (она решит: искать/патрулить)
            return _combatState;
        }

        stateChanger.CanRotateForwardToggle(true);
        float attackRadius = stateChanger.GetAttackRadius();
        float distanceToTarget = stateChanger.DistanceToTarget();

        // Единая точка валидации: цель мертва/не таргетится/вышла из радиуса/рассинхрон
        if (CheckNeedChangeTarget(targetTransform.gameObject, attackRadius: attackRadius, distanceToTarget: distanceToTarget))
        {
            aiDestinationSetter.CurrentTarget = null;
            return _combatState;
        }

        var targetPos = aiDestinationSetter.CurrentTarget.transform.position;
        Vector3 targetDirection = GetDirectionToTarget(targetPos);
        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

        if (!stateChanger.CanAttack())
        {
            return _combatState;
        }

        if (_currentAttack != 0)
        {
            if (IsTargetWithinAttackRange(attackRadius: attackRadius, distanceToTarget: distanceToTarget))
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
            if (IsTargetWithinAttackRange(attackRadius: attackRadius, distanceToTarget: distanceToTarget) && IsTargetInAttackAngle(viewableAngle))
            {
                _currentAttack = _currentAttackIndex;
            }
        }

        return _combatState;
    }

    private bool CheckNeedChangeTarget(GameObject target, float attackRadius, float distanceToTarget)
    {
        if (target == null) return true;

        if (!target.TryGetComponent<BaseHealth>(out var health)) return true;

        if (health.IsDeath() || !health.IsCanTarget()) return true;

        if (distanceToTarget < 0f || distanceToTarget > attackRadius) return true;

        return false;
    }

    private Vector3 GetDirectionToTarget(Vector3 targetPos)
    {
        return new Vector3(targetPos.x, transform.position.y, targetPos.z) - transform.position;
    }

    private bool IsTargetWithinAttackRange(float attackRadius, float distanceToTarget)
    {
        return distanceToTarget >= 0 && distanceToTarget <= attackRadius;
    }

    private bool IsTargetInAttackAngle(float viewableAngle)
    {
        return viewableAngle >= WorldGameInfo.CityRobotMinimumAttackAngle && viewableAngle <= WorldGameInfo.CityRobotMaximumAttackAngle;
    }

    private bool CanPerformAttack(CityRobotStateChanger stateChanger)
    {
        return stateChanger.CurrentAttackRecoveryTime <= 0 && stateChanger.CanAttack();
    }

    private void PerformAttack(CityRobotStateChanger stateChanger, BaseAnimator animator)
    {
        animator.AttackAnimation(_currentAttack);
        stateChanger.AttackToggle(false);
        stateChanger.CurrentAttackRecoveryTime = stateChanger.GetAttackSpeed();
        _currentAttack = 0;
    }

    private int SelectNextAttack()
    {
        int rnd = Random.Range(1, _attacksNumber + 1);
        return rnd;
    }
}
