using UnityEngine;

public class ExplosionBullet : Bullet
{
    [SerializeField] private GameObject _explosionPrefab;
    
    public override void HitTarget()
    {
        base.HitTarget();

        if (_targetHealth != null)
        {
            var prefab = Instantiate(_explosionPrefab, _targetTransform.position, Quaternion.identity);
            prefab.GetComponent<Explosion>().SetDamage(_damage * 0.5f, _knockbackPoints);
        }
    }
}
