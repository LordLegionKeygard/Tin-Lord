using System.Collections;
using UnityEngine;

public class TurretPatrolState : TurretState
{
    [SerializeField] private TurretBuilding _turretBuilding;
    [SerializeField] private Transform _rotateObject;
    [SerializeField] private BaseDamage _creatureDamage;
    [SerializeField] private TurretCombatState _turretCombatState;
    private Coroutine _patrolTimerCoroutine;
    private float _currentPatrolTimer;
    private float _targetYRotation;

    private void Start()
    {
        var rnd = Random.Range(1, 10);
        StartCoroutine(PatrolTimerCoroutine(rnd));
    }

    public override TurretState Tick(TurretStateChanger stateChanger, BaseAnimator animator, AIDestinationSetter aiDestinationSetter)
    {
        stateChanger.CanRotateForwardToggle(false);
        stateChanger.StopAllAttacks();
        
        if(stateChanger.IsToxicGasActive()) return this;

        BaseHealth targetHealth = FindNearestTargetInRange(stateChanger);

        if (targetHealth != null)
        {
            SetTarget(aiDestinationSetter, targetHealth);
            return _turretCombatState;
        }

        RotateTowardsTarget();

        return this;
    }

    private void SetTarget(AIDestinationSetter aiDestinationSetter, BaseHealth targetHealth)
    {
        aiDestinationSetter.CurrentTarget = targetHealth.transform;
        var targetTransform = targetHealth.gameObject.GetComponent<EnemyCenterPoint>().GetTransform();
        aiDestinationSetter.SetTargetTransform(targetTransform);
        _creatureDamage.SetTarget(targetHealth, targetTransform);
    }

    private BaseHealth FindNearestTargetInRange(TurretStateChanger stateChanger)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, stateChanger.AttackRadius(), stateChanger.DetectionLayer());

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

    public void PatrolToRandomPosition()
    {
        // Останавливаем предыдущую корутину если она была запущена
        if (_patrolTimerCoroutine != null)
        {
            StopPatrolTimerCoroutine();
        }

        // Устанавливаем случайное направление для поворота
        _targetYRotation = Random.Range(0, 360);
        StartPatrolTimer();
    }

    private void RotateTowardsTarget()
    {
        // Целевая ротация
        Vector3 targetDirection = Quaternion.Euler(0, _targetYRotation, 0) * Vector3.forward;

        // Плавное вращение
        Vector3 newDirection = Vector3.RotateTowards(_rotateObject.forward, targetDirection, 
        _turretBuilding.Building().RotationSpeed * WorldGameInfo.TurretPatrolRotateSpeedFactor * Time.deltaTime, 0);

        // Применяем новую ротацию
        _rotateObject.rotation = Quaternion.LookRotation(newDirection);
    }

    private void StartPatrolTimer()
    {
        StopPatrolTimerCoroutine();
        _currentPatrolTimer = Random.Range(WorldGameInfo.MinTurretPatrolTime, WorldGameInfo.MaxTurretPatrolTime);
        _patrolTimerCoroutine = StartCoroutine(PatrolTimerCoroutine(_currentPatrolTimer));
    }

    private IEnumerator PatrolTimerCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        PatrolToRandomPosition();
    }

    public void StopPatrolTimerCoroutine()
    {
        if (_patrolTimerCoroutine != null)
        {
            StopCoroutine(_patrolTimerCoroutine);
            _patrolTimerCoroutine = null;
        }
    }
}
