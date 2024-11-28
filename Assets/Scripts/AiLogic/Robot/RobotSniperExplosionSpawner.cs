using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSniperExplosionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefabVFX;
    [SerializeField] private Transform _spawnPoint;

    private void Start()
    {
        CustomEvents.OnRobotDie += SpawnExplosion;
    }

    private void SpawnExplosion()
    {
        Instantiate(_prefabVFX, _spawnPoint.position, Quaternion.identity);
    }

    private void OnDestroy()
    {
        CustomEvents.OnRobotDie -= SpawnExplosion;
    }
}
