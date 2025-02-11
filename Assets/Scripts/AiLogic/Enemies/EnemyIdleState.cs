using System.Collections;
using Pathfinding;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    [SerializeField] private EnemyPursueTargetState _pursueTargetState;
    [SerializeField] private AIDestinationSetter _aiDestinationSetter;
    [SerializeField] private BaseDamage _creatureDamage;
    [SerializeField] private AIPath _aiPath;

    private void Start()
    {
        SetBaseTarget();
    }

    public override EnemyState Tick(EnemyStateChanger stateChanger, BaseHealth health, BaseAnimator animator, AIDestinationSetter aiDestinationSetter, EnemyAttacks attacks)
    {
        stateChanger.CanRotateForwardToggle(false);

        BaseHealth targetHealth = FindNearestTargetInRange(stateChanger);
        
        if (targetHealth != null)
        {
            SetTargetAndStartPursuit(targetHealth, attacks);
            return _pursueTargetState;
        }

        if (_aiDestinationSetter.CurrentTarget == null)
        {
            SetBaseTarget();
        }

        return this;
    }

    private BaseHealth FindNearestTargetInRange(EnemyStateChanger stateChanger)
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

    private void SetTargetAndStartPursuit(BaseHealth targetHealth, EnemyAttacks attacks)
    {
        var buildingTile = targetHealth.BuildingTile();
        var targetTransform = buildingTile != null
            ? (buildingTile.IsFourTile ? targetHealth.GetFoutTileTransform() : targetHealth.gameObject.transform)
            : targetHealth.gameObject.transform;

        attacks.UpdateCreatureAttackDistance(buildingTile);
        _aiPath.endReachedDistance = attacks.MaxMeleeAtkRange();

        _aiDestinationSetter.CurrentTarget = targetTransform;
        _creatureDamage.SetTarget(targetHealth, targetHealth.transform); // пока что враги ближнего боя и не стреляют поэтому передает трансформ здоровья цели
    }

    private void SetBaseTarget()
    {
        if (BasePoint.Instance == null)
        {
            _aiDestinationSetter.CurrentTarget = null;
            return;
        }

        _aiDestinationSetter.CurrentTarget = BasePoint.Instance.gameObject.transform;
    }
}