using System.Collections;
using UnityEngine;


public class EnemyIdleState : EnemyState
{
    [SerializeField] private EnemyStateChanger _enemyStateChanger;
    [SerializeField] private EnemyPursueTargetState _pursueTargetState;
    [SerializeField] private AIDestinationSetter _aiDestinationSetter;
    [SerializeField] private BaseDamage _creatureDamage;
    [SerializeField] private EnemyReachedDistance _creatureReachedDistance;

    private void Start()
    {
        SetBaseTarget();
    }

    public override EnemyState Tick(EnemyStateChanger stateChanger, BaseHealth health, BaseAnimator animator, AIDestinationSetter aiDestinationSetter, EnemyAttacks attacks)
    {
        stateChanger.CanRotateForwardToggle(false);

        Collider[] colliders = Physics.OverlapSphere(transform.position, stateChanger.CurrentDetectionRadius, stateChanger.DetectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            BaseHealth targetHealth = colliders[i].transform.GetComponent<BaseHealth>();

            if (targetHealth == null || targetHealth.IsDeath()) continue;

            var buildingTile = targetHealth.BuildingTile();

            var targetTransform = buildingTile != null
                ? (buildingTile.IsFourTile ? targetHealth.GetFoutTileTransform() : targetHealth.gameObject.transform)
                : targetHealth.gameObject.transform;

            _creatureReachedDistance.UpdateAiEndReachedDistance(buildingTile);
            attacks.UpdateCreatureAttackDistance(buildingTile);

            _aiDestinationSetter.CurrentTarget = targetTransform;
            _creatureDamage.SetTargetHealth(targetHealth);
            return _pursueTargetState;

        }

        if (_aiDestinationSetter.CurrentTarget == null)
        {
            SetBaseTarget();
        }
        return this;
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
