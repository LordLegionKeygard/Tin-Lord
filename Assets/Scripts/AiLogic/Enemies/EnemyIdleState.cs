using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Crosstales.TrueRandom;

public class EnemyIdleState : EnemyState
{
    [SerializeField] private EnemyPursueTargetState _pursueTargetState;
    [SerializeField] private AIDestinationSetter _aiDestinationSetter;
    [SerializeField] private BaseDamage _creatureDamage;
    private float _nextTargetScan;

    private void Start()
    {
        SetBaseTarget();
    }

    public override EnemyState Tick(EnemyStateChanger stateChanger, BaseHealth health, BaseAnimator animator, AIDestinationSetter aiDestinationSetter, EnemyAttacks attacks, AIPath aiPath)
    {
        animator.IsCombat(false);
        stateChanger.CanRotateForwardToggle(false);

        if (Time.time >= _nextTargetScan)
        {
            _nextTargetScan = Time.time + WorldGameInfo.TargetScanInterval;

            BaseHealth foundTarget = FindTargetWithExtendedRadius(stateChanger);
            if (foundTarget != null)
            {
                SetTargetAndStartPursuit(foundTarget, attacks, aiPath);
                return _pursueTargetState;
            }

            if (!HasReachableBasePoint())
            {
                var fallbackTarget = FindNearestBuildingTargetGlobal();
                if (fallbackTarget != null)
                {
                    SetTargetAndStartPursuit(fallbackTarget, attacks, aiPath);
                    return _pursueTargetState;
                }
            }

            if (_aiDestinationSetter.CurrentTarget == null)
            {
                SetBaseTarget();
            }
        }

        return this;
    }


    private BaseHealth FindTargetWithExtendedRadius(EnemyStateChanger stateChanger)
    {
        // Поиск в начальном радиусе
        Collider[] smallColliders = Physics.OverlapSphere(transform.position, WorldGameInfo.EnemiesSmallDetectionRadius, stateChanger.DetectionLayer());
        if (smallColliders.Length == 0)
        {
            // Если в начальном радиусе здание не обнаружено – враг продолжает идти к базе
            return null;
        }

        // Расширенный радиус
        Collider[] bigColliders = Physics.OverlapSphere(transform.position, WorldGameInfo.EnemiesBigDetectionRadius, stateChanger.DetectionLayer());

        List<BaseHealth> allTargets = new();
        foreach (var collider in bigColliders)
        {
            var baseHealth = collider.GetComponent<BaseHealth>();
            var trap = baseHealth != null && baseHealth.BuildingTile() != null && baseHealth.BuildingTile().BuildingTileView == BuildingTileViewEnum.Traps;
            if (baseHealth != null && !baseHealth.IsDeath() && !trap)
            {
                allTargets.Add(baseHealth);
            }
        }
        if (allTargets.Count == 0)
        {
            return null;
        }

        var rnd = TRManager.Instance.GenerateIntegerPRNG(0, 100);

        // Берем случайную цель, кроме стен
        if (rnd[0] > 70)
        {
            // Составляем список всех целей кроме стен
            List<BaseHealth> nonWallTargets = new();
            foreach (var candidate in allTargets)
            {
                var tile = candidate.BuildingTile();
                var isWallOrGate = tile != null && (tile.BuildingTileView is BuildingTileViewEnum.Walls or BuildingTileViewEnum.Gates);
                if (!isWallOrGate)
                {
                    nonWallTargets.Add(candidate);
                }
            }

            // берем случайную цель из доступных
            var nonWallTarget = GetRandomReachableTarget(nonWallTargets, allTargets);
            if (nonWallTarget != null)
            {
                return nonWallTarget;
            }
        }

        // Берем любую цель, включая стены
        List<int> randomInts = TRManager.Instance.GenerateIntegerPRNG(0, allTargets.Count - 1);
        int rndIndex = randomInts[0];
        var target = allTargets[rndIndex];

        if (IsReachable(target))
        {
            return target;
        }

        var reachableTarget = GetRandomReachableTarget(allTargets, allTargets);
        if (reachableTarget != null)
        {
            return reachableTarget;
        }

        return GetNearestTarget(allTargets);
    }

    private BaseHealth GetRandomReachableTarget(List<BaseHealth> needTargets, List<BaseHealth> allTargets)
    {
        // Фильтруем по достижимости: из всех найденных зданий оставляем только те, до которых есть путь
        var startNode = AstarPath.active.GetNearest(transform.position).node;
        if (startNode == null) return null;

        List<BaseHealth> reachable = new();

        if (needTargets == null || needTargets.Count == 0)
        {
            needTargets = allTargets;
        }

        // Проверяем достижимость ограниченного числа случайных целей, чтобы не грузить CPU на больших пачках
        int checks = Mathf.Min(WorldGameInfo.MaxReachabilityChecks, needTargets.Count);
        List<int> indices = TRManager.Instance.GenerateIntegerPRNG(0, needTargets.Count - 1, checks);

        for (int i = 0; i < checks; i++)
        {
            var candidate = needTargets[indices[i]];
            var destination = GetDestinationTransform(candidate);
            var endNode = destination != null ? AstarPath.active.GetNearest(destination.position).node : null;
            if (endNode != null && PathUtilities.IsPathPossible(startNode, endNode))
            {
                reachable.Add(candidate);
            }
        }

        // Если ни одно здание не достижимо значит база окружена стенами, возвращаем близжайшее достижимое здание 
        if (reachable.Count == 0)
        {
            BaseHealth nearestTarget = null;
            float minSqr = float.MaxValue;

            foreach (var target in allTargets)
            {
                var tile = target.BuildingTile();
                var isWallOrGate = tile != null && (tile.BuildingTileView is BuildingTileViewEnum.Walls or BuildingTileViewEnum.Gates);
                if (!isWallOrGate)
                {
                    continue;
                }

                var destination = GetDestinationTransform(target);
                var targetPosition = destination != null ? destination.position : target.transform.position;
                float sqr = (targetPosition - transform.position).sqrMagnitude;
                if (sqr < minSqr)
                {
                    minSqr = sqr;
                    nearestTarget = target;
                }
            }
            return nearestTarget;
        }

        // Иначе, выбираем случайное из всех достижимых в радиусе
        List<int> randomInts = TRManager.Instance.GenerateIntegerPRNG(0, reachable.Count - 1, 1);
        int rndIndex = randomInts[0];
        return reachable[rndIndex];
    }


    private void SetTargetAndStartPursuit(BaseHealth targetHealth, EnemyAttacks attacks, AIPath aiPath)
    {
        var buildingTile = targetHealth.BuildingTile();
        var destinationTarget = GetDestinationTransform(targetHealth);

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

    private Transform GetDestinationTransform(BaseHealth targetHealth)
    {
        if (targetHealth == null) return null;

        var buildingTile = targetHealth.BuildingTile();
        return buildingTile != null
            ? (buildingTile.IsFourTile ? targetHealth.GetFoutTileTransform() : targetHealth.transform)
            : targetHealth.transform;
    }

    private bool IsReachable(BaseHealth targetHealth)
    {
        if (AstarPath.active == null) return false;

        var destination = GetDestinationTransform(targetHealth);
        if (destination == null) return false;

        return IsReachable(destination);
    }

    private bool IsReachable(Transform destination)
    {
        if (AstarPath.active == null || destination == null) return false;

        var startNode = AstarPath.active.GetNearest(transform.position).node;
        if (startNode == null) return false;

        var endNode = AstarPath.active.GetNearest(destination.position).node;
        return endNode != null && PathUtilities.IsPathPossible(startNode, endNode);
    }

    private bool HasReachableBasePoint()
    {
        if (AstarPath.active == null || BasePoint.Instance == null) return false;

        var basePoints = BasePoint.Instance.GetBasePoints();
        if (basePoints == null || basePoints.Length == 0) return false;

        foreach (var point in basePoints)
        {
            if (point != null && IsReachable(point))
            {
                return true;
            }
        }

        return false;
    }

    private BaseHealth FindNearestBuildingTargetGlobal()
    {
        var buildings = FindObjectsOfType<BuildingHealth>();
        if (buildings == null || buildings.Length == 0) return null;

        BaseHealth nearestTarget = null;
        float minSqr = float.MaxValue;

        foreach (var building in buildings)
        {
            if (building == null || building.IsDeath()) continue;

            var tile = building.BuildingTile();
            if (tile == null || tile.BuildingTileView == BuildingTileViewEnum.Traps) continue;

            var destination = GetDestinationTransform(building);
            var targetPosition = destination != null ? destination.position : building.transform.position;
            float sqr = (targetPosition - transform.position).sqrMagnitude;
            if (sqr < minSqr)
            {
                minSqr = sqr;
                nearestTarget = building;
            }
        }

        return nearestTarget;
    }

    private BaseHealth GetNearestTarget(List<BaseHealth> targets)
    {
        if (targets == null || targets.Count == 0) return null;

        BaseHealth nearestTarget = null;
        float minSqr = float.MaxValue;

        foreach (var target in targets)
        {
            var destination = GetDestinationTransform(target);
            var targetPosition = destination != null ? destination.position : target.transform.position;
            float sqr = (targetPosition - transform.position).sqrMagnitude;
            if (sqr < minSqr)
            {
                minSqr = sqr;
                nearestTarget = target;
            }
        }

        return nearestTarget;
    }
}
