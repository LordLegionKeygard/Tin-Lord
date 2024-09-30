using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureDamage : MonoBehaviour
{
    [SerializeField] private CreatureLevel _enemyLevel;
    private BaseHealth _currentTargetBaseHealth;
    private float _damage;

    private void Start()
    {
        SetDamage();
    }

    private void SetDamage()
    {
        _damage = _enemyLevel.GetEnemiesInformation().PhysAttack[_enemyLevel.GetLevel()];
    }

    public void SetTargetHealth(BaseHealth baseHealth)
    {
        _currentTargetBaseHealth = baseHealth;
    }

    public void Attack()
    {
        if (_currentTargetBaseHealth == null) return;

        _currentTargetBaseHealth.CalculateDamage(_damage, KnockBackType.Zero);
    }
}
