using UnityEngine;

public class RobotRepairState : RobotState
{
    [SerializeField] private RobotMove _robotMove;
    [SerializeField] private RobotPatrolState _patrolState;
    [SerializeField] private RobotCombatState _combatState;

    private BuildingHealth _targetBuilding;

    public void SetRepairTarget(BuildingHealth building)
    {
        _targetBuilding = building;
    }

    public override RobotState Tick(RobotStateChanger stateChanger, BaseHealth health, BaseAnimator animator, AIDestinationSetter aiDestinationSetter, RobotAttacks attacks, RobotSpeed playerSpeed)
    {
        if (_targetBuilding == null || _targetBuilding.IsDeath() || _targetBuilding.IsFullHealth())
        {
            return _patrolState;
        }

        playerSpeed.CantMove();

        BaseHealth targetHealth = FindNearestTargetInRange(stateChanger);
        if (targetHealth != null)
        {
            SetCombatTarget(aiDestinationSetter, targetHealth, stateChanger);
            return _combatState;
        }

        PerformRepair();
        return this;
    }

    private void PerformRepair()
    {
        // Вращаемся к зданию
        _robotMove.RotateTo(_targetBuilding.transform.position);

        // Начинаем ремонт
        _targetBuilding.SlowTimeRepair(1);

        // Если здание полностью починено, возвращаемся к патрулированию
        if (_targetBuilding.IsFullHealth())
        {
            _targetBuilding = null;
        }
    }


    private BaseHealth FindNearestTargetInRange(RobotStateChanger stateChanger)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, stateChanger.DetectionRadius(), stateChanger.DetectionLayer());

        BaseHealth nearestTarget = null;
        float nearestDistance = Mathf.Infinity;

        foreach (var collider in colliders)
        {
            BaseHealth targetHealth = collider.GetComponent<BaseHealth>();

            if (targetHealth != null && !targetHealth.IsDeath())
            {
                float distanceToTarget = Vector3.Distance(transform.position, targetHealth.transform.position);

                if (distanceToTarget < nearestDistance)
                {
                    nearestDistance = distanceToTarget;
                    nearestTarget = targetHealth;
                }
            }
        }

        return nearestTarget;
    }

    private void SetCombatTarget(AIDestinationSetter aiDestinationSetter, BaseHealth targetHealth, RobotStateChanger stateChanger)
    {
        stateChanger.AttackToggle(true);
        aiDestinationSetter.CurrentTarget = targetHealth.transform;
    }
}
