using UnityEngine;
using UnityEngine.Serialization;

public class MachineRepairState : MachineState
{
    [SerializeField] private MachineMove machineMove;
    [SerializeField] private MachinePatrolState _patrolState;
    [SerializeField] private MachineCombatState _combatState;

    private BuildingHealth _targetBuilding;

    public void SetRepairTarget(BuildingHealth building)
    {
        _targetBuilding = building;
    }

    public override MachineState Tick(MachineStateChanger stateChanger, BaseHealth health, BaseAnimator animator, AIDestinationSetter aiDestinationSetter, MachineAttacks attacks, MachineSpeed playerSpeed)
    {
        if (_targetBuilding == null || _targetBuilding.IsDeath() || _targetBuilding.IsFullHealth() || _targetBuilding.IsContructionNow())
        {
            animator.RepairAnimation(false);
            return _patrolState;
        }

        playerSpeed.CantMove();

        BaseHealth targetHealth = FindNearestTargetInRange(stateChanger);
        if (targetHealth != null)
        {
            animator.RepairAnimation(false);
            SetCombatTarget(aiDestinationSetter, targetHealth, stateChanger);
            return _combatState;
        }

        PerformRepair();
        animator.RepairAnimation(true);
        return this;
    }

    private void PerformRepair()
    {
        machineMove.RotateTo(_targetBuilding.transform.position);

        var repairRate = 1 + MachinesDataMission.Instance.CurrentLevel() * 0.1f;

        _targetBuilding.SlowTimeRepair(repairRate);

        if (_targetBuilding.IsFullHealth())
        {
            CustomEvents.FireRobotFullRepairBuilding(_targetBuilding.GetTileObject().GetId());
            _targetBuilding = null;
        }
    }


    private BaseHealth FindNearestTargetInRange(MachineStateChanger stateChanger)
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

    private void SetCombatTarget(AIDestinationSetter aiDestinationSetter, BaseHealth targetHealth, MachineStateChanger stateChanger)
    {
        stateChanger.AttackToggle(true);
        aiDestinationSetter.CurrentTarget = targetHealth.transform;
    }
}
