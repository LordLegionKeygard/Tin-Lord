using System.Collections;
using UnityEngine;
using Zenject;

public class ShipCannonBullet : MonoBehaviour
{
    [SerializeField] private LayerMask _hitMask;
    [SerializeField] private GameObject _model;
    [SerializeField] private float _destroyTimeAfterHit;
    private SpawnedHazardSystem _spawnedHazardSystem;
    private BulletsPool _pool;
    private float _explosionDamage;
    private float _lifeTime;
    private float _life;
    private bool _inited;
    private ShipWeaponInfo _shipWeaponInfo;

    public void Setup(BulletsPool pool, float explosionDamage, ShipWeaponInfo shipWeaponInfo, SpawnedHazardSystem spawnedHazardSystem)
    {
        _pool = pool;
        _explosionDamage = explosionDamage;
        _lifeTime = Mathf.Max(0.05f, shipWeaponInfo.LifeTime);
        _life = 0f;
        _inited = true;
        _shipWeaponInfo = shipWeaponInfo;
        _spawnedHazardSystem = spawnedHazardSystem;
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
        Vector3 to = from + transform.forward * (_shipWeaponInfo.BulletSpeed * deltaTime);

        if (Physics.Linecast(from, to, out RaycastHit hit, _hitMask, QueryTriggerInteraction.Ignore))
        {
            Vector3 pos = hit.point + Vector3.up * _shipWeaponInfo.ImpactYOffset;

            SpawnExplosion(pos);
            SpawnDot(pos);
            TryReturnBullet();
            return;
        }

        transform.position = to;

        if (_life >= _lifeTime) TryReturnBullet();
    }

    private void SpawnExplosion(Vector3 pos)
    {
        var go = Instantiate(_shipWeaponInfo.ExplosionPrefab, pos, Quaternion.identity);
        go.GetComponent<ShipCannonExplosion>().SetDamage(_explosionDamage, 0);
    }

    private void SpawnDot(Vector3 pos)
    {
        if (_shipWeaponInfo.DotPrefab == null) return;

        var _currentPrefab = Instantiate(_shipWeaponInfo.DotPrefab, pos, Quaternion.identity);
        _currentPrefab.GetComponent<OnTriggerStayDealDamage>().SetInfo(_shipWeaponInfo.DotDurationTicks, _shipWeaponInfo.DotDamageFactor);
        _spawnedHazardSystem.RegisterHazard((int)HazardEnum.IgniteSkill, _currentPrefab, _shipWeaponInfo.DotDurationTicks, _shipWeaponInfo.DotDamageFactor);
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
            _pool.ReturnBullet(_shipWeaponInfo.BulletType, gameObject);
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

        _pool.ReturnBullet(_shipWeaponInfo.BulletType, gameObject);
        if (_model != null) _model.SetActive(true);
    }
}
