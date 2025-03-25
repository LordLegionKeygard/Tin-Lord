using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        // Находим все объекты в радиусе
        Collider[] colliders = Physics.OverlapSphere(transform.position,stateChanger.DetectionRadius(),stateChanger.DetectionLayer());

        // Сохраним кандидатов (живые цели)
        var possibleTargets = new List<BaseHealth>();

        foreach (var collider in colliders)
        {
            BaseHealth targetHealth = collider.GetComponent<BaseHealth>();
            if (targetHealth != null && !targetHealth.IsDeath())
            {
                possibleTargets.Add(targetHealth);
            }
        }

        // Если целей вообще нет
        if (possibleTargets.Count == 0) return null;

        // Сортируем цели по расстоянию: ближайшие будут первыми
        var sortedTargets = possibleTargets.OrderBy(t => Vector3.Distance(transform.position, t.transform.position)).ToList();

        // Определяем, сколько ближайших зданий возьмём для выбора (например, половину)
        int halfCount = Mathf.Max(1, sortedTargets.Count / 2);

        // Формируем подсписок из ближайших `halfCount` зданий
        var topClosest = sortedTargets.GetRange(0, halfCount);

        // Выбираем случайную цель из этой ближайшей половины
        var randomIndex = Random.Range(0, topClosest.Count);
        var randomTarget = topClosest[randomIndex];

        return randomTarget;
    }


    private void SetTargetAndStartPursuit(BaseHealth targetHealth, EnemyAttacks attacks)
    {
        var buildingTile = targetHealth.BuildingTile();
        var destinationTarget = buildingTile != null
            ? (buildingTile.IsFourTile ? targetHealth.GetFoutTileTransform() : targetHealth.gameObject.transform)
            : targetHealth.gameObject.transform;

        attacks.UpdateCreatureAttackDistance(buildingTile);
        _aiPath.endReachedDistance = attacks.MaxMeleeAtkRange();
        _aiDestinationSetter.CurrentTarget = destinationTarget;

        var buildingLevels = targetHealth.gameObject.GetComponent<BuildingTile>()?.GetBuildingLevels();
        var bulletTarget = buildingLevels?.GetCurrentBuildingCenterTransform() ?? targetHealth.transform;

        _creatureDamage.SetTarget(targetHealth, bulletTarget);
    }

    private void SetBaseTarget()
    {
        if (BasePoint.Instance == null)
        {
            _aiDestinationSetter.CurrentTarget = null;
            return;
        }

        _aiDestinationSetter.CurrentTarget = BasePoint.Instance.GetRandomBasePoint();
    }
}