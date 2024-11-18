using UnityEngine;

public class RobotPatrolState : RobotState
{
    [SerializeField] private RobotMove _robotMove;
    [SerializeField] private RobotCombatState _combatState;
    [SerializeField] private BaseDamage _creatureDamage;
    [SerializeField] private RobotPatrolPath _patrolPath;

    private int _currentPatrolPointIndex = 0;
    private int _nextPointNumber = 1;
    private bool _isInitialized = false;

    public void InitializePatrol(int startIndex)
    {
        _currentPatrolPointIndex = startIndex;
        _isInitialized = true;
    }

    public override RobotState Tick(RobotStateChanger stateChanger, BaseHealth health, BaseAnimator animator, AIDestinationSetter aiDestinationSetter, RobotAttacks attacks, RobotSpeed playerSpeed)
    {
        if (!_isInitialized)
        {
            return this;
        }

        stateChanger.CanRotateForwardToggle(false);

        BaseHealth targetHealth = FindNearestTargetInRange(stateChanger);

        if (targetHealth != null)
        {
            SetCombatTarget(aiDestinationSetter, targetHealth, stateChanger);
            return _combatState;
        }

        playerSpeed.CanMove();
        Patrol();
        return this;
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
        _creatureDamage.SetTargetHealth(targetHealth);
    }
}
