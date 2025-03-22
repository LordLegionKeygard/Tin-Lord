using UnityEngine;

public class RobotPatrolState : RobotState
{
    [SerializeField] private RobotMove _robotMove;
    [SerializeField] private RobotRepairState _repairState;
    [SerializeField] private RobotCombatState _combatState;
    [SerializeField] private BaseDamage _creatureDamage;
    [SerializeField] private RobotPatrolPath _patrolPath;

    private int _currentPatrolPointIndex = 0;
    private int _nextPointNumber = 1;
    private bool _isInitialized = false;

    public int GetCurrentPatrolPointIndex() => _currentPatrolPointIndex;

    public void InitializePatrol(int startIndex)
    {
        _currentPatrolPointIndex = startIndex;
        _isInitialized = true;
    }

    public override RobotState Tick(RobotStateChanger stateChanger, BaseHealth health, BaseAnimator animator, AIDestinationSetter aiDestinationSetter, RobotAttacks attacks, RobotSpeed playerSpeed)
    {
        if (!_isInitialized || health.IsDeath())
        {
            return this;
        }

        stateChanger.CanRotateForwardToggle(false);

        BaseHealth targetHealth = FindNearestTargetInRange(stateChanger);

        if (targetHealth != null)
        {
            stateChanger.AttackToggle(true);
            SetTarget(aiDestinationSetter, targetHealth);
            return _combatState;
        }

        if (_repairState != null)
        {
            BuildingHealth buildingToRepair = FindNearestBuildingToRepair(stateChanger);
            if (buildingToRepair != null)
            {
                _repairState.SetRepairTarget(buildingToRepair);
                return _repairState;
            }
        }

        playerSpeed.CanMove();
        Patrol();
        return this;
    }

    private void SetTarget(AIDestinationSetter aiDestinationSetter, BaseHealth targetHealth)
    {
        aiDestinationSetter.CurrentTarget = targetHealth.transform;
        var targetTransform = targetHealth.gameObject.GetComponent<EnemyCenterPoint>().GetTransform();
        aiDestinationSetter.SetTargetTransform(targetTransform);
        _creatureDamage.SetTarget(targetHealth, targetTransform);
    }

    private void Patrol()
    {
        int nextPointIndex = _patrolPath.GetNextPointIndex(_currentPatrolPointIndex, _nextPointNumber);
        int previousPointIndex = _patrolPath.GetPreviousPointIndex(_currentPatrolPointIndex, _nextPointNumber);

        if (_patrolPath.CheckTileForGate(nextPointIndex))
        {
            _patrolPath.GetTile(nextPointIndex).BuildingTileObject().CurrentBuildingTileProtective().ControlGate(true);
        }

        if (_patrolPath.CheckTileForGate(previousPointIndex))
        {
            _patrolPath.GetTile(previousPointIndex).BuildingTileObject().CurrentBuildingTileProtective().ControlGate(false);
        }

        if (_patrolPath.ShouldChangeDirection(nextPointIndex))
        {
            _nextPointNumber *= -1;
        }

        Vector3 nextPoint = _patrolPath.GetTile(nextPointIndex).transform.position;

        // Используем PlayerMove для передвижения
        _robotMove.MoveTo(nextPoint);

        // Если достигли точки, обновляем индекс
        if (Vector3.Distance(_robotMove.transform.position, nextPoint) <= 0.1f)
        {
            _currentPatrolPointIndex = nextPointIndex;
        }
    }

    private BaseHealth FindNearestTargetInRange(RobotStateChanger stateChanger)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, stateChanger.DetectionRadius(), stateChanger.DetectionLayer());

        BaseHealth nearestTarget = null;
        float closestAngle = Mathf.Infinity;

        for (int i = 0; i < colliders.Length; i++)
        {
            BaseHealth targetHealth = colliders[i].GetComponent<BaseHealth>();

            if (targetHealth != null && !targetHealth.IsDeath())
            {
                // Вычисляем направление до цели
                Vector3 directionToTarget = (colliders[i].transform.position - transform.position).normalized;
                float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

                if (angleToTarget < closestAngle)
                {
                    closestAngle = angleToTarget;
                    nearestTarget = targetHealth;
                }
            }
        }

        return nearestTarget;
    }

    private BuildingHealth FindNearestBuildingToRepair(RobotStateChanger stateChanger)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, WorldGameInfo.MachineEngineerRepairBuildingsDistance, stateChanger.BuildingDetectionLayer());

        BuildingHealth nearestBuilding = null;
        float nearestDistance = Mathf.Infinity;

        foreach (var collider in colliders)
        {
            BuildingHealth buildingHealth = collider.GetComponent<BuildingHealth>();

            if (buildingHealth != null && !buildingHealth.IsDeath() && !buildingHealth.IsFullHealth())
            {
                float distanceToBuilding = Vector3.Distance(transform.position, buildingHealth.transform.position);

                if (distanceToBuilding < nearestDistance)
                {
                    nearestDistance = distanceToBuilding;
                    nearestBuilding = buildingHealth;
                }
            }
        }

        return nearestBuilding;
    }
}
