using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Pathfinding;

public class EnemyIdleState : EnemyState
{
    [SerializeField] private EnemyPursueTargetState _pursueTargetState;
    [SerializeField] private AIDestinationSetter _aiDestinationSetter;
    [SerializeField] private BaseDamage _creatureDamage;

    [Header("TargetSearch")]
    private int _maxTargets = 10;
    private int _randomNearTargets = 5;

    private void Start()
    {
        SetBaseTarget();
    }

    public override EnemyState Tick(EnemyStateChanger stateChanger, BaseHealth health, BaseAnimator animator, AIDestinationSetter aiDestinationSetter, EnemyAttacks attacks, AIPath aiPath)
    {
        if(aiPath.enabled == false) aiPath.enabled = true;

        stateChanger.CanRotateForwardToggle(false);

        BaseHealth foundTarget = FindRandomReachableTarget(stateChanger);
        if (foundTarget != null)
        {
            SetTargetAndStartPursuit(foundTarget, attacks, aiPath);
            return _pursueTargetState;
        }

        if (_aiDestinationSetter.CurrentTarget == null)
        {
            SetBaseTarget();
        }

        return this;
    }

    /// <summary>
    /// Находит несколько ближайших зданий, проверяет доступность (IsPathPossible),
    /// и выбирает одну случайно.
    /// </summary>
    private BaseHealth FindRandomReachableTarget(EnemyStateChanger stateChanger)
    {
        // 1) Берём все объекты в радиусе
        Collider[] colliders = Physics.OverlapSphere(
            transform.position, 
            stateChanger.DetectionRadius(), 
            stateChanger.DetectionLayer()
        );

        List<BaseHealth> allTargets = new();
        foreach (var col in colliders)
        {
            var bh = col.GetComponent<BaseHealth>();
            if (bh != null && !bh.IsDeath())
                allTargets.Add(bh);
        }
        if (allTargets.Count == 0) return null;

        // 2) Сортируем по прямому расстоянию
        allTargets = allTargets.OrderBy(t => Vector3.Distance(transform.position, t.transform.position)).ToList();

        // 3) Берём N ближайших
        int takeCount = Mathf.Min(allTargets.Count, _maxTargets);
        var subset = allTargets.GetRange(0, takeCount);

        // 4) Перемешиваем и берём M случайных
        Shuffle(subset);
        int randomCount = Mathf.Min(subset.Count, _randomNearTargets);
        var randomSubset = subset.GetRange(0, randomCount);

        // 5) Проверяем кто из них достижим
        var startNode = AstarPath.active.GetNearest(transform.position).node;
        if (startNode == null) return null;

        List<BaseHealth> reachable = new();

        foreach (var candidate in randomSubset)
        {
            var endNode = AstarPath.active.GetNearest(candidate.transform.position).node;
            if (endNode == null) continue;

            // Если есть путь
            if (PathUtilities.IsPathPossible(startNode, endNode))
            {
                reachable.Add(candidate);
            }
        }

        // 6) Нет доступных зданий, значит скорее всего база окружена стенами
        if (reachable.Count == 0)
        {
            //возвращаем самое близжайшее здание
            return allTargets[0];
        }

        // 7) Из достижимых выбираем случайного
        int idx = Random.Range(0, reachable.Count);
        return reachable[idx];
    }

    /// <summary>
    /// Fisher–Yates shuffle
    /// </summary>
    private void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var tmp = list[i];
            list[i] = list[j];
            list[j] = tmp;
        }
    }

    private void SetTargetAndStartPursuit(BaseHealth targetHealth, EnemyAttacks attacks, AIPath aiPath)
    {
        var buildingTile = targetHealth.BuildingTile();
        var destinationTarget = buildingTile != null
            ? (buildingTile.IsFourTile ? targetHealth.GetFoutTileTransform() : targetHealth.gameObject.transform)
            : targetHealth.gameObject.transform;

        attacks.UpdateCreatureAttackDistance(buildingTile);
        aiPath.endReachedDistance = attacks.MaxMeleeAtkRange();
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
