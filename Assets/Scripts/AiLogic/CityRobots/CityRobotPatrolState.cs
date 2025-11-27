using UnityEngine;

public class CityRobotPatrolState : CityRobotState
{
    [SerializeField] private BaseDamage _creatureDamage;
    [SerializeField] private CityRobotCombatState _combatState;
    private float _nextTargetScan;


    public override CityRobotState Tick(CityRobotStateChanger stateChanger, BaseAnimator animator, AIDestinationSetter aiDestinationSetter)
    {
        animator.IsCombat(false);
        stateChanger.CanRotateForwardToggle(false);

        if (Time.time >= _nextTargetScan)
        {
            _nextTargetScan = Time.time + WorldGameInfo.TargetScanInterval;

            BaseHealth targetHealth = FindNearestTargetInRange(stateChanger);

            if (targetHealth != null)
            {
                SetTarget(aiDestinationSetter, targetHealth);
                return _combatState;
            }

            RotateTowardsTarget(stateChanger.GetRotationSpeed());
        }


        return this;
    }

    private void SetTarget(AIDestinationSetter aiDestinationSetter, BaseHealth targetHealth)
    {
        aiDestinationSetter.CurrentTarget = targetHealth.transform;
        var targetTransform = targetHealth.gameObject.GetComponent<EnemyCenterPoint>().GetTransform();
        aiDestinationSetter.SetTargetTransform(targetTransform);
        _creatureDamage.SetTarget(targetHealth, targetTransform);
    }

    private BaseHealth FindNearestTargetInRange(CityRobotStateChanger stateChanger)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, stateChanger.GetAttackRadius(), stateChanger.GetDetectionLayer());

        BaseHealth nearestTarget = null;
        float closestAngle = Mathf.Infinity;

        for (int i = 0; i < colliders.Length; i++)
        {
            BaseHealth targetHealth = colliders[i].transform.GetComponent<BaseHealth>();

            if (targetHealth == null || targetHealth.IsDeath() || !targetHealth.IsCanTarget()) continue;

            // Вычисляем направление до цели
            Vector3 directionToTarget = (colliders[i].transform.position - transform.position).normalized;
            float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

            // Проверяем, является ли этот угол самым маленьким
            if (angleToTarget < closestAngle)
            {
                closestAngle = angleToTarget;
                nearestTarget = targetHealth;
            }
        }

        return nearestTarget;
    }

    private void RotateTowardsTarget(float rotationSpeed)
    {
        // Целевая ротация
        Vector3 targetDirection = Quaternion.Euler(0, transform.rotation.y, 0) * Vector3.forward;

        // Плавное вращение
        Vector3 newDirection = Vector3.RotateTowards(gameObject.transform.forward, targetDirection,
        rotationSpeed * WorldGameInfo.TurretPatrolRotateSpeedFactor * Time.deltaTime, 0);

        // Применяем новую ротацию
        gameObject.transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
