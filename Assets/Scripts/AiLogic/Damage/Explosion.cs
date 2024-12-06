using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float _damage;
    private int _knockbackPoints;

    public void SetDamage(float damageAmount, int knockback)
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
