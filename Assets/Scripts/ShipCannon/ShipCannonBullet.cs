using System.Collections;
using UnityEngine;

public class ShipCannonBullet : MonoBehaviour
{
    [SerializeField] private LayerMask _hitMask;
    [SerializeField] private GameObject _model;
    [SerializeField] private float _destroyTimeAfterHit;
    private BulletsPool _pool;
    private BulletEnum _type;
    private float _speed;
    private float _explosionDamage;
    private float _lifeTime;
    private float _life;
    private bool _inited;

    private GameObject _explosionPrefab;
    private float _impactYOffset;

    public void Setup(BulletsPool pool, BulletEnum type, float speed, float explosionDamage, float lifeTime, GameObject explosionPrefab, float impactYOffset)
    {
        _pool = pool;
        _type = type;

        _speed = speed;
        _explosionDamage = explosionDamage;
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

        float deltaTime = Time.deltaTime;
        _life += deltaTime;

        Vector3 from = transform.position;
        Vector3 to = from + transform.forward * (_speed * deltaTime);

        if (Physics.Linecast(from, to, out RaycastHit hit, _hitMask, QueryTriggerInteraction.Ignore))
        {
            Vector3 pos = hit.point + Vector3.up * _impactYOffset;
            var go = Instantiate(_explosionPrefab, pos, Quaternion.identity);
            go.GetComponent<ShipCannonExplosion>().SetDamage(_explosionDamage, 0);

            TryReturnBullet();
            return;
        }

        transform.position = to;

        if (_life >= _lifeTime) TryReturnBullet();
    }

    private void TryReturnBullet()
    {
        _inited = false;
        if (_destroyTimeAfterHit > 0)
        {
            if (_model != null) _model.SetActive(false);
            StartCoroutine(nameof(ReturnBulletCoroutine));
        }
        else
        {
            _pool.ReturnBullet(_type, gameObject);
        }
    }

    private IEnumerator ReturnBulletCoroutine()
    {
        float elapsedTime = 0;

        while (elapsedTime < _destroyTimeAfterHit)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _pool.ReturnBullet(_type, gameObject);
        if (_model != null) _model.SetActive(true);
    }
}
