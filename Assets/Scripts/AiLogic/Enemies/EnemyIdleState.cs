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
    private readonly int _closestBuildingCount = 3;

    private void Start()
    {
        SetBaseTarget();
    }

    public override EnemyState Tick(EnemyStateChanger stateChanger,BaseHealth health,BaseAnimator animator,AIDestinationSetter aiDestinationSetter,EnemyAttacks attacks,AIPath aiPath)
    {
        if (!aiPath.enabled)
            aiPath.enabled = true;

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

    /// <summary>
    /// Логика поиска цели:
    /// 1) Если в радиусе stateChanger.DetectionRadius() (начальный, 50) обнаружено хотя бы одно здание,
    ///    выполняется расширенный поиск во всём радиусе = stateChanger.DetectionRadius() + extraRadius.
    /// 2) Из найденных в расширенном радиусе берутся topCount (3) ближайших,
    ///    затем фильтруются по достижимости (IsPathPossible).
    /// 3) Если ни одно из topCount не достижимо, возвращается самое ближайшее здание из всех.
    /// 4) Иначе случайно выбирается одно из достижимых (через GenerateIntegerPRNG).
    /// </summary>
    private BaseHealth FindTargetWithExtendedRadius(EnemyStateChanger stateChanger)
    {
        // 1) Поиск в начальном радиусе
        Collider[] smallColliders = Physics.OverlapSphere(transform.position,stateChanger.DetectionRadius(),stateChanger.DetectionLayer());

        if (smallColliders.Length == 0)
        {
            // Если в начальном радиусе здание не обнаружено – враг продолжает идти к базе
            return null;
        }

        // 2) Расширенный радиус
        float extendedRadius = stateChanger.ExtraDetectionRadius();Collider[] bigColliders = Physics.OverlapSphere(transform.position,extendedRadius,stateChanger.DetectionLayer());

        List<BaseHealth> allTargets = new();
        foreach (var col in bigColliders)
        {
            var bh = col.GetComponent<BaseHealth>();
            if (bh != null && !bh.IsDeath())
            {
                allTargets.Add(bh);
            }
        }
        if (allTargets.Count == 0)
        {
            return null;
        }

        // 3) Сортировка по расстоянию (самое ближнее – первое)
        allTargets = allTargets.OrderBy(t => Vector3.Distance(transform.position, t.transform.position)).ToList();

        // 4) Берем topCount ближайших (или меньше, если зданий меньше)
        int countToTakeBuildings = Mathf.Min(allTargets.Count, _closestBuildingCount);
        var closestBuildings = allTargets.GetRange(0, countToTakeBuildings);

        // 5) Фильтруем по достижимости
        var startNode = AstarPath.active.GetNearest(transform.position).node;
        if (startNode == null)
            return null;

        List<BaseHealth> reachable = new();
        foreach (var candidate in closestBuildings)
        {
            var endNode = AstarPath.active.GetNearest(candidate.transform.position).node;
            if (endNode == null)
                continue;
            if (PathUtilities.IsPathPossible(startNode, endNode))
            {
                reachable.Add(candidate);
            }
        }

        // 6) Если ни одно из topCount не достижимо, возвращаем fallback – самое ближайшее здание из всех
        if (reachable.Count == 0)
        {
            return allTargets[0];
        }

        // 7) Иначе, используя TrueRandom PRNG (локальный), выбираем случайное число в диапазоне [0, reachable.Count - 1]
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
