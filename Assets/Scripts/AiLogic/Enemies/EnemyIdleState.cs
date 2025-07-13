using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Crosstales.TrueRandom;

public class EnemyIdleState : EnemyState
{
    [SerializeField] private EnemyPursueTargetState _pursueTargetState;
    [SerializeField] private AIDestinationSetter _aiDestinationSetter;
    [SerializeField] private BaseDamage _creatureDamage;

    private void Start()
    {
        SetBaseTarget();
    }

    public override EnemyState Tick(EnemyStateChanger stateChanger, BaseHealth health, BaseAnimator animator, AIDestinationSetter aiDestinationSetter, EnemyAttacks attacks, AIPath aiPath)
    {
        animator.IsCombat(false);
        stateChanger.CanRotateForwardToggle(false);

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

        return this;
    }


    private BaseHealth FindTargetWithExtendedRadius(EnemyStateChanger stateChanger)
    {
        // Поиск в начальном радиусе
        Collider[] smallColliders = Physics.OverlapSphere(transform.position, stateChanger.DetectionRadius(), stateChanger.DetectionLayer());
        if (smallColliders.Length == 0)
        {
            // Если в начальном радиусе здание не обнаружено – враг продолжает идти к базе
            return null;
        }

        // Расширенный радиус
        float extendedRadius = stateChanger.ExtraDetectionRadius();
        Collider[] bigColliders = Physics.OverlapSphere(transform.position, extendedRadius, stateChanger.DetectionLayer());

        List<BaseHealth> allTargets = new();
        foreach (var collider in bigColliders)
        {
            var baseHealth = collider.GetComponent<BaseHealth>();
            if (baseHealth != null && !baseHealth.IsDeath() && baseHealth.BuildingTile().BuildingTileView != BuildingTileViewEnum.Traps)
            {
                allTargets.Add(baseHealth);
            }
        }
        if (allTargets.Count == 0)
        {
            return null;
        }

        var rnd = TRManager.Instance.GenerateIntegerPRNG(0, 100);

        // Берем случаную цель, кроме стен
        if (rnd[0] > 50)
        {
            return GetRandomReachableTarget(allTargets);
        }
        // Берем любую цель, включая стены
        else
        {
            List<int> randomInts = TRManager.Instance.GenerateIntegerPRNG(0, allTargets.Count - 1);
            int rndIndex = randomInts[0];
            return allTargets[rndIndex];
        }
    }

    private BaseHealth GetRandomReachableTarget(List<BaseHealth> allTargets)
    {
        // Фильтруем по достижимости: из всех найденных зданий оставляем только те, до которых есть путь
        var startNode = AstarPath.active.GetNearest(transform.position).node;
        if (startNode == null) return null;

        List<BaseHealth> reachable = new();
        foreach (var candidate in allTargets)
        {
            var endNode = AstarPath.active.GetNearest(candidate.transform.position).node;
            if (endNode != null && PathUtilities.IsPathPossible(startNode, endNode))
            {
                reachable.Add(candidate);
            }
        }

        // Если ни одно здание не достижимо значит база окружена стенами, возвращаем близжайшее здание 
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
