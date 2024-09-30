using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamageVFX : BaseTakeDamageVFX
{
    [SerializeField] private GameObject[] _bloodAttach;

    public override void SpawnTakeDamageVFX(Transform hit, Transform weapon)
    {
        base.SpawnTakeDamageVFX(hit, weapon);
        if (_bloodAttach[0] != null) SpawnBloodAttach(hit, transform);
    }

    private void SpawnBloodAttach(Transform hit, Transform weapon)
    {
        if (!WorldGameInfo.StaticBlood) return;
        var rndAttach = Random.Range(0, _bloodAttach.Length);
        var attachBloodInstance = Instantiate(_bloodAttach[rndAttach]);
        var bloodT = attachBloodInstance.transform;

        bloodT.SetLocalPositionAndRotation(new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
        bloodT.localScale = Vector3.one * Random.Range(0.55f, 1f);
   
        if (hit.gameObject.TryGetComponent<BaseHealth>(out var takeDamage))
        {
            bloodT.transform.parent = hit.gameObject.transform;
        }

        Destroy(attachBloodInstance, 35);
    }
}