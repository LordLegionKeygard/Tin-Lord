using System.Collections;
using UnityEngine;

public class DeathExplosion : MonoBehaviour
{
    [SerializeField] private Collider _col;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float _explosionTime;
    [SerializeField] private float _duration;
    private float _damage;
    private float _knockbackPoints;
    private DeathExplosionPool _deathExplosionPool;
    private DeathExplosionEnum _explosionEnum;
    private Coroutine _routine;
    private EnemyHealth _enemyHealth;

    private void OnEnable()
    {
        _col.enabled = false;
        _routine = StartCoroutine(ExplosionRoutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BuildingHealth health))
        {
            health.CalculateDamage(_damage, _knockbackPoints);
        }
    }

    public void Setup(float damageAmount, float knockback, DeathExplosionPool explosionPool, DeathExplosionEnum type, EnemyHealth enemyHealth)
    {
        _damage = damageAmount;
        _knockbackPoints = knockback;
        _deathExplosionPool = explosionPool;
        _explosionEnum = type;
        _enemyHealth = enemyHealth;
    }

    private IEnumerator ExplosionRoutine()
    {
        yield return new WaitForSeconds(_explosionTime);

        _col.enabled = true;
        _particleSystem.Play();
        _enemyHealth.Death();
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.DeathExplosion[(int)_explosionEnum], transform.position);

        yield return new WaitForFixedUpdate();

        _col.enabled = false;

        yield return new WaitForSeconds(_duration);

        _deathExplosionPool.ReturnDeathExplosion(_explosionEnum, gameObject);
        _routine = null;
    }

    private void OnDisable()
    {
        if (_routine != null)
        {
            StopCoroutine(_routine);
            _routine = null;
        }

        _col.enabled = false;
    }
}

[System.Serializable]
public enum DeathExplosionEnum
{
    None = -1,
    Pistripod = 0,
    Funglicane = 1,
}
