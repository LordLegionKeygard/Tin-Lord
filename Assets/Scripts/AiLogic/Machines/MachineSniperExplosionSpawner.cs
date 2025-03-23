using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineSniperExplosionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private Transform _spawnPoint;

    private void Start()
    {
        CustomEvents.OnMachineDie += SpawnExplosion;
    }

    private void SpawnExplosion()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.RobotSniperExplosion, transform.position);
        var prefab = Instantiate(_explosionPrefab, _spawnPoint.position, Quaternion.identity);
        prefab.GetComponent<Explosion>().SetDamage(MachinesDataWorld.Instance.GetCurrentRangeDamage() * 5, 100);
    }

    private void OnDestroy()
    {
        CustomEvents.OnMachineDie -= SpawnExplosion;
    }
}
