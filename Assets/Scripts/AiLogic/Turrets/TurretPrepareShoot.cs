// using FMODUnity;
using UnityEngine;

public class TurretPrepareShoot : MonoBehaviour
{
    [SerializeField] private GameObject _bulletEmptyPrefab;

    public void ActiveBullet()
    {
        _bulletEmptyPrefab.SetActive(true);
    }

    public void UnactiveBullet()
    {
        _bulletEmptyPrefab.SetActive(false);
    }
}
