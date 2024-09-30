using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTakeDamageVFX : MonoBehaviour
{
    [SerializeField] private GameObject[] _takeDamageFX;

    public virtual void SpawnTakeDamageVFX(Transform hit, Transform weapon)
    {
        if (!WorldGameInfo.StaticBlood) return;
        var rnd = Random.Range(0, _takeDamageFX.Length);
        var instance = Instantiate(_takeDamageFX[rnd], new Vector3(hit.position.x, weapon.position.y, hit.transform.position.z), transform.rotation * Quaternion.Euler(0, 90, 0));
        Destroy(instance, 35);
    }
}
