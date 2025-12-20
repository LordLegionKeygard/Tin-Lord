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

        var startNode = AstarPath.active.GetNearest(transform.position).node;
        if (startNode != null)
        {
            var endNode = AstarPath.active.GetNearest(target.transform.position).node;
            if (endNode != null && PathUtilities.IsPathPossible(startNode, endNode))
            {
                return target;
            }
        }

        var reachableTarget = GetRandomReachableTarget(allTargets, allTargets);
        if (reachableTarget != null)
        {
            return reachableTarget;
        }

        return target;
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
            var endNode = AstarPath.active.GetNearest(candidate.transform.position).node;
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
                float sqr = (target.transform.position - transform.position).sqrMagnitude;
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
