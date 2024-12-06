using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSniperExplosionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private Transform _spawnPoint;

    private void Start()
    {
        CustomEvents.OnRobotDie += SpawnExplosion;
    }

    private void SpawnExplosion()
    {
        var prefab = Instantiate(_explosionPrefab, _spawnPoint.position, Quaternion.identity);
        prefab.GetComponent<Explosion>().SetDamage(RobotsData.Instance.GetCurrentRangeDamage() * 5, 100);
    }

    private void OnDestroy()
    {
        CustomEvents.OnRobotDie -= SpawnExplosion;
    }
}
