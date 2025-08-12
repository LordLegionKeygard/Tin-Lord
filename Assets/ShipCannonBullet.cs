using UnityEngine;

public class ShipCannonBullet : MonoBehaviour
{
    [SerializeField] private LayerMask _hitMask;
    private BulletsPool _pool;
    private BulletEnum _type;
    private float _speed;
    private float _damage;
    private float _explosionDamage;
    private float _knockback;
    private float _lifeTime;
    private float _life;
    private bool _inited;

    private GameObject _explosionPrefab;
    private float _impactYOffset;

    public void Setup(BulletsPool pool, BulletEnum type, float speed, float damage, float explosionDamage, float knockback, float lifeTime, GameObject explosionPrefab, float impactYOffset)
    {
        _pool = pool;
        _type = type;

        _speed = speed;
        _damage = damage;
        _explosionDamage = explosionDamage;
        _knockback = knockback;
        _lifeTime = Mathf.Max(0.05f, lifeTime);

        _explosionPrefab = explosionPrefab;
        _impactYOffset = impactYOffset;

        _life = 0f;
        _inited = true;
    }

    private void OnEnable()
    {
        _life = 0f;
    }

    private void Update()
    {
        if (!_inited) return;

        float dt = Time.deltaTime;
        _life += dt;

        Vector3 from = transform.position;
        Vector3 to = from + transform.forward * (_speed * dt);

        if (Physics.Linecast(from, to, out RaycastHit hit, _hitMask, QueryTriggerInteraction.Ignore))
        {
            var bh = hit.collider.GetComponentInParent<BaseHealth>();
            if (bh != null)
                bh.CalculateDamage(_damage, _knockback);

            if (_explosionPrefab != null)
            {
                Vector3 pos = hit.point + Vector3.up * _impactYOffset;
                var go = Instantiate(_explosionPrefab, pos, Quaternion.identity);
                var exp = go.GetComponent<Explosion>();
                if (exp != null)
                    exp.SetDamage(_explosionDamage, _knockback);
            }

            Return();
            return;
        }

        transform.position = to;

        if (_life >= _lifeTime)
            Return();
    }

    private void Return()
    {
        _inited = false;
        if (_pool != null) _pool.ReturnBullet(_type, gameObject);
        else gameObject.SetActive(false);
    }
}
