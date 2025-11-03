using UnityEngine;

public class BaseSkillTriggerStayDamage : MonoBehaviour
{
    private float _damage;

    public void SetDamage(float damage) => _damage = damage;

    private void OnTriggerStay(Collider collision)
    {
        if (collision.TryGetComponent(out BuildingHealth health))
        {
            health.CalculateDamage(_damage, 0);
        }
    }
}
