using System.Collections.Generic;
using UnityEngine;

public class BulletsPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> _bulletPrefabs; // Список всех префабов пуль
    [SerializeField] private int _poolSize = 20;
    private Dictionary<BulletEnum, Queue<GameObject>> _bulletPools;

    private void Start()
    {
        _bulletPools = new Dictionary<BulletEnum, Queue<GameObject>>();

        // Создаем пулы для каждого типа пули (с учетом порядка в списке)
        for (int i = 0; i < _bulletPrefabs.Count; i++)
        {
            BulletEnum bulletType = (BulletEnum)i; // Преобразуем индекс в тип пули
            Queue<GameObject> pool = new Queue<GameObject>();

            for (int j = 0; j < _poolSize; j++)
            {
                GameObject bullet = Instantiate(_bulletPrefabs[i], transform);
                bullet.SetActive(false);
                pool.Enqueue(bullet);
            }

            _bulletPools[bulletType] = pool; // Привязываем пул к типу пули
        }
    }

    public GameObject GetBullet(BulletEnum bulletType)
    {
        if (_bulletPools.ContainsKey(bulletType))
        {
            Queue<GameObject> pool = _bulletPools[bulletType];

            if (pool.Count > 0)
            {
                GameObject bullet = pool.Dequeue();
                bullet.SetActive(true);
                return bullet;
            }
            else
            {
                // Если пул закончился, создаем новую пулю (по желанию)
                GameObject bullet = Instantiate(_bulletPrefabs[(int)bulletType]);
                return bullet;
            }
        }

        return null; // Возвращаем null, если тип пули не найден
    }

    public void ReturnBullet(BulletEnum bulletType, GameObject bullet)
    {
        bullet.SetActive(false);
        if (_bulletPools.ContainsKey(bulletType))
        {
            _bulletPools[bulletType].Enqueue(bullet);
        }
    }
}
