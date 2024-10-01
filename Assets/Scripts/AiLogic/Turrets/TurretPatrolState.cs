using System.Collections;
using UnityEngine;

public class TurretPatrolState : TurretState
{
    [SerializeField] private Transform _rotateObject;
    [SerializeField] private float _patrolRotationSpeed;
    [SerializeField] private BaseDamage _creatureDamage;
    [SerializeField] private TurretCombatState _turretCombatState;
    private Coroutine _patrolTimerCoroutine;
    private float _currentPatrolTimer;

    private void Start()
    {
        var rnd = Random.Range(1, 10);
        StartCoroutine(PatrolTimerCoroutine(rnd));
    }

    public override TurretState Tick(TurretStateChanger stateChanger, BaseAnimator animator, AIDestinationSetter aiDestinationSetter, BaseAttacks attacks)
    {
        stateChanger.CanRotateForwardToggle(false);

        Collider[] colliders = Physics.OverlapSphere(transform.position, stateChanger.CurrentDetectionRadius, stateChanger.DetectionLayer);

        Transform bestTarget = null;
        float closestAngle = float.MaxValue;

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
                bestTarget = targetHealth.transform;
            }
        }

        if (bestTarget != null)
        {
            aiDestinationSetter.CurrentTarget = bestTarget;
            _creatureDamage.SetTargetHealth(bestTarget.GetComponent<BaseHealth>());
            return _turretCombatState;
        }

        return this;
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
            _rotateObject.rotation = Quaternion.RotateTowards(_rotateObject.rotation, targetRotation, _patrolRotationSpeed * Time.deltaTime);

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
