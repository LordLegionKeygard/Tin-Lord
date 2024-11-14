using System.Collections.Generic;
using UnityEngine;

public class PlayerPatrolState : PlayerState
{
    [SerializeField] private Transform _player;
    [SerializeField] private PlayerSpeed _playerSpeed;
    [SerializeField] private PlayerCombatState _combatState;
    [SerializeField] private BaseDamage _creatureDamage;

    [Header("Patrol")]
    [SerializeField] private float _patrolRotationSpeed = 1;
    private List<Vector3> _patrolPoints;
    private int _currentPatrolPointIndex = 0;
    private bool _isInitialized = false;

    public void InitializePatrol(int startIndex, List<Vector3> patrolPoints)
    {
        _patrolPoints = patrolPoints;
        _currentPatrolPointIndex = startIndex;
        _isInitialized = true;
    }

    public override PlayerState Tick(PlayerStateChanger stateChanger, BaseHealth health, BaseAnimator animator, AIDestinationSetter aiDestinationSetter, PlayerAttacks attacks)
    {
        if (!_isInitialized)
        {
            // Если патруль не инициализирован, выходим из метода
            return this;
        }

        stateChanger.CanRotateForwardToggle(false);

        BaseHealth targetHealth = FindNearestTargetInRange(stateChanger);

        if (targetHealth != null)
        {
            SetCombatTarget(aiDestinationSetter, targetHealth, stateChanger);
            return _combatState;
        }

        Patrol();
        return this;
    }

    private BaseHealth FindNearestTargetInRange(PlayerStateChanger stateChanger)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, stateChanger.DetectionRadius(), stateChanger.DetectionLayer());

        BaseHealth nearestTarget = null;
        float nearestDistance = Mathf.Infinity;

        foreach (var collider in colliders)
        {
            BaseHealth targetHealth = collider.GetComponent<BaseHealth>();

            if (targetHealth != null && !targetHealth.IsDeath())
            {
                float distanceToTarget = Vector3.Distance(transform.position, targetHealth.transform.position);

                if (distanceToTarget < nearestDistance)
                {
                    nearestDistance = distanceToTarget;
                    nearestTarget = targetHealth;
                }
            }
        }

        return nearestTarget;
    }

    private void Patrol()
    {
        // Определяем индекс следующей точки
        int nextPointIndex = (_currentPatrolPointIndex + 1) % _patrolPoints.Count;
        Vector3 nextPoint = _patrolPoints[nextPointIndex];
        float distanceToNextPoint = Vector3.Distance(_player.position, nextPoint);

        if (distanceToNextPoint > 0.1f)
        {
            // Плавно поворачиваемся к следующей точке
            Vector3 direction = (nextPoint - _player.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                _player.rotation = Quaternion.Slerp(_player.rotation, lookRotation, Time.deltaTime * _patrolRotationSpeed);
            }

            // Двигаемся к следующей точке
            _player.position = Vector3.MoveTowards(_player.position, nextPoint, Time.deltaTime * _playerSpeed.Speed());
        }
        else
        {
            // Обновляем текущий индекс патруля
            _currentPatrolPointIndex = nextPointIndex;
        }
    }



    private void SetCombatTarget(AIDestinationSetter aiDestinationSetter, BaseHealth targetHealth, PlayerStateChanger stateChanger)
    {
        stateChanger.AttackToggle(true);
        aiDestinationSetter.CurrentTarget = targetHealth.transform;
        _creatureDamage.SetTargetHealth(targetHealth);
    }
}
