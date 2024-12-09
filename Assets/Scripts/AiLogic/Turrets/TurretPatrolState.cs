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

    private void Start()
    {
        var rnd = Random.Range(1, 10);
        StartCoroutine(PatrolTimerCoroutine(rnd));
    }

    public override TurretState Tick(TurretStateChanger stateChanger, BaseAnimator animator, AIDestinationSetter aiDestinationSetter)
    {
        stateChanger.CanRotateForwardToggle(false);
        stateChanger.StopAllAttacks();

        BaseHealth targetHealth = FindNearestTargetInRange(stateChanger);

        if (targetHealth != null)
        {
            SetTarget(aiDestinationSetter, targetHealth);
            return _turretCombatState;
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

    private BaseHealth FindNearestTargetInRange(TurretStateChanger stateChanger)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, stateChanger.AttackRadius(), stateChanger.DetectionLayer());

        BaseHealth nearestTarget = null;
        float closestAngle = Mathf.Infinity;

        for (int i = 0; i < colliders.Length; i++)
        {
            BaseHealth targetHealth = colliders[i].transform.GetComponent<BaseHealth>();

            if (targetHealth == null || targetHealth.IsDeath()) continue;

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

        // Начинаем поворот к случайной позиции
        float randomYRotation = Random.Range(0, 360);
        _patrolTimerCoroutine = StartCoroutine(RotateToRandomPosition(randomYRotation));
    }

    // Корутин для плавного поворота на случайную позицию
    private IEnumerator RotateToRandomPosition(float targetYRotation)
    {
        Quaternion targetRotation = Quaternion.Euler(0, targetYRotation, 0);

        while (Quaternion.Angle(_rotateObject.rotation, targetRotation) > 0.01f) // Пока не достигнем цели
        {
            // Вращаем объект с постоянной скоростью
            _rotateObject.rotation = Quaternion.RotateTowards(_rotateObject.rotation, targetRotation, _turretBuilding.Building().RotationSpeed * Time.deltaTime);

            yield return null;
        }

        // После поворота запускаем новый патруль
        StartPatrolTimer();
    }


    private void StartPatrolTimer()
    {
        StopPatrolTimerCoroutine();
        _currentPatrolTimer = Random.Range(1, 10);
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
