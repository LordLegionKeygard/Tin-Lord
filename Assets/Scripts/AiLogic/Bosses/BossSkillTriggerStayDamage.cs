using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillTriggerStayDamage : MonoBehaviour
{
    [SerializeField] private float _damage;

    private void OnTriggerStay(Collider collision)
    {
       if (collision.TryGetComponent(out BuildingHealth health))
        {
            health.CalculateDamage(_damage, 0);
        } 
    }
}
