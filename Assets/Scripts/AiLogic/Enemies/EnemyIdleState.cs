using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Pathfinding;
using Crosstales.TrueRandom;

public class EnemyIdleState : EnemyState
{
    [SerializeField] private EnemyPursueTargetState _pursueTargetState;
    [SerializeField] private AIDestinationSetter _aiDestinationSetter;
    [SerializeField] private BaseDamage _creatureDamage;
    private readonly int _numberOfClosestTargets = 10;

    private void Start()
    {
        SetBaseTarget();
    }

    public override EnemyState Tick(EnemyStateChanger stateChanger, BaseHealth health, BaseAnimator animator, AIDestinationSetter aiDestinationSetter, EnemyAttacks attacks, AIPath aiPath)
    {
        if (!aiPath.enabled) aiPath.enabled = true;

        stateChanger.CanRotateForwardToggle(false);

        BaseHealth foundTarget = FindNearestAmongTop(stateChanger);
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

    private BaseHealth FindNearestAmongTop(EnemyStateChanger stateChanger)
    {
        // 1) Берём все объекты в радиусе
        Collider[] colliders = Physics.OverlapSphere(transform.position, stateChanger.DetectionRadius(), stateChanger.DetectionLayer());

        List<BaseHealth> allTargets = new();
        foreach (var col in colliders)
        {
            var bh = col.GetComponent<BaseHealth>();
            if (bh != null && !bh.IsDeath())
            {
                allTargets.Add(bh);
            }
        }

        // Если нет никаких зданий
        if (allTargets.Count == 0) return null;

        // 2) Сортируем по прямой дистанции (ближайшее будет в allTargets[0])
        allTargets = allTargets.OrderBy(t => Vector3.Distance(transform.position, t.transform.position)).ToList();

        // 3) Берём numberOfClosestTargets ближайших (или меньше, если зданий < нужного числа)
        int takeCount = Mathf.Min(allTargets.Count, _numberOfClosestTargets);
        var closestSubset = allTargets.GetRange(0, takeCount);

        // 4) Проверяем достижимость (IsPathPossible)
        var startNode = AstarPath.active.GetNearest(transform.position).node;
        if (startNode == null) return null;

        List<BaseHealth> reachable = new();
        foreach (var candidate in closestSubset)
        {
            var endNode = AstarPath.active.GetNearest(candidate.transform.position).node;
            if (endNode == null) continue;

            if (PathUtilities.IsPathPossible(startNode, endNode))
            {
                reachable.Add(candidate);
            }
        }

        // 5) Если никто из N ближайших не достижим,
        //    возвращаем самое ближайшее здание из всех (allTargets[0])
        if (reachable.Count == 0)
        {
            return allTargets[0];
        }

        // 6) Из достижимых берём случайное через TrueRandom PRNG
        //    GenerateIntegerPRNG(min, max, number) вернёт List<int>
        //    Нужно 1 число в диапазоне [0..(reachable.Count - 1)]
        List<int> randomInts = TRManager.Instance.GenerateIntegerPRNG(0, reachable.Count - 1, 1);

        int rndIndex = randomInts[0]; // Берём первый элемент (единственный)
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
