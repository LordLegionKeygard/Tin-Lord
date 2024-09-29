using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureTakeDamageVFX : MonoBehaviour
{
    [SerializeField] private GameObject[] _bloodAttach;
    [SerializeField] private GameObject[] _bloodFX;
    [SerializeField] private GameObject[] _bleedingDotFX;

    public void SpawnBleedingDot()
    {
        if (!WorldGameInfo.StaticBlood) return;
        var rnd = Random.Range(0, _bleedingDotFX.Length);
        var instance = Instantiate(_bleedingDotFX[rnd], new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation * Quaternion.Euler(0, 90, 0));
        Destroy(instance, 35);

        SpawnBloodAttach(gameObject.transform, gameObject.transform);
    }

    public void SpawnBlood(Transform hit, Transform weapon)
    {
        if (!WorldGameInfo.StaticBlood) return;
        var rnd = Random.Range(0, _bloodFX.Length);
        var instance = Instantiate(_bloodFX[rnd], new Vector3(hit.position.x, weapon.position.y, hit.transform.position.z), transform.rotation * Quaternion.Euler(0, 90, 0));
        Destroy(instance, 35);

        if (_bloodAttach[0] != null) SpawnBloodAttach(hit, transform);
    }

    private void SpawnBloodAttach(Transform hit, Transform weapon)
    {
        if (!WorldGameInfo.StaticBlood) return;
        var rndAttach = Random.Range(0, _bloodAttach.Length);
        var attachBloodInstance = Instantiate(_bloodAttach[rndAttach]);
        var bloodT = attachBloodInstance.transform;

        bloodT.localPosition = new Vector3(0, 0, 0);
        bloodT.localRotation = Quaternion.Euler(0, 0, 0);
        bloodT.localScale = Vector3.one * Random.Range(0.55f, 1f);

        var takeDamage = hit.gameObject.GetComponent<BaseHealth>();

        if (takeDamage != null)
        {
            // if (takeDamage.MainModel()) bloodT.transform.parent = null;
            bloodT.transform.parent = hit.gameObject.transform;
        }

        Destroy(attachBloodInstance, 35);
    }
}