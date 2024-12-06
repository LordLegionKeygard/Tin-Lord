using UnityEngine;

public class ExplosionBullet : Bullet
{
    [SerializeField] private GameObject _explosionPrefab;
    
    public override void HitTarget()
    {
        base.HitTarget();

        if (_targetHealth != null)
        {
            var prefab = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            prefab.GetComponent<Explosion>().SetDamage(_damage, _knockbackPoints);
        }
    }
}
