using System.Collections.Generic;
using UnityEngine;

public class DeathExplosionPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> _explosionPrefabs;
    private int _poolSize = 5;
    private Dictionary<DeathExplosionEnum, Queue<GameObject>> _explosionPools;

    private void Start()
    {
        _explosionPools = new Dictionary<DeathExplosionEnum, Queue<GameObject>>();

        for (int i = 0; i < _explosionPrefabs.Count; i++)
        {
            DeathExplosionEnum explosionType = (DeathExplosionEnum)i;
            Queue<GameObject> pool = new();

            for (int j = 0; j < _poolSize; j++)
            {
                GameObject explosion = Instantiate(_explosionPrefabs[i], transform);
                explosion.SetActive(false);
                pool.Enqueue(explosion);
            }

            _explosionPools[explosionType] = pool;
        }
    }

    public GameObject GetDeathExplosion(DeathExplosionEnum explosionType)
    {
        if (_explosionPools.ContainsKey(explosionType))
        {
            Queue<GameObject> pool = _explosionPools[explosionType];

            if (pool.Count > 0)
            {
                GameObject explosion = pool.Dequeue();
                explosion.SetActive(true);
                return explosion;
            }
            else
            {
                GameObject explosion = Instantiate(_explosionPrefabs[(int)explosionType], transform);
                explosion.SetActive(true);

                _explosionPools[explosionType].Enqueue(explosion);
                return explosion;
            }
        }

        return null;
    }

    public void ReturnDeathExplosion(DeathExplosionEnum explosionType, GameObject explosion)
    {
        explosion.SetActive(false);
        if (_explosionPools.ContainsKey(explosionType))
        {
            _explosionPools[explosionType].Enqueue(explosion);
        }
    }
}
