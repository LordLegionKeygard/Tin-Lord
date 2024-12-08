using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float _damage;
    private float _knockbackPoints;

    public void SetDamage(float damageAmount, float knockback)
    {
        _damage = damageAmount;
        _knockbackPoints = knockback;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BaseHealth baseHealth))
        {
            baseHealth.TakeDamage(_damage, _knockbackPoints);
        }
    }
}
