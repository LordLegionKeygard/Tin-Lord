// using FMODUnity;
using UnityEngine;

public class TurretPrepareShoot : MonoBehaviour
{
    [SerializeField] private GameObject _bulletEmptyPrefab;
    // [SerializeField] private EventReference _shootSound;
    // [SerializeField] private EventReference _activeBulletSound;

    public void ActiveBullet()
    {
        // AudioManager.Instance.PlayerOneShot(_activeBulletSound, transform.position);
        _bulletEmptyPrefab.SetActive(true);
    }

    public void UnactiveBullet()
    {
        _bulletEmptyPrefab.SetActive(false);
    }

    public void ShootSound()
    {
        // AudioManager.Instance.PlayerOneShot(_shootSound, transform.position);
    }
}
