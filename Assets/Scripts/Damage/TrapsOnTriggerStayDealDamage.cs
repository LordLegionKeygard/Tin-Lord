using System.Collections.Generic;
using UnityEngine;

public class TrapsOnTriggerStayDealDamage : MonoBehaviour
{
    [SerializeField] private Building _building;
    private readonly HashSet<BaseHealth> _targets = new();


    private void Start()
    {
        CustomEvents.OnTimeTick += OnTimeTick;
        CustomEvents.OnTimeHalfTick += OnTimeTick;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out EnemyHealth enemyHealth))
        {
            if (!enemyHealth.IsDeath())
            {
                _targets.Add(enemyHealth);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out EnemyHealth enemyHealth))
        {
            _targets.Remove(enemyHealth);
        }
    }

    private void OnTimeTick()
    {
        foreach (var health in _targets)
        {
            if (health != null && !health.IsDeath())
            {
                health.CalculateDamage(_building.Damage, _building.KnockbackPoints);
            }
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnTimeTick -= OnTimeTick;
        CustomEvents.OnTimeHalfTick -= OnTimeTick;
    }
}
