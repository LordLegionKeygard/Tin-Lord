using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BaseTakeDamageVFX : MonoBehaviour
{
    [SerializeField] private GameObject[] _takeDamageFX;
    private AIPath _aiPath;

    private void Awake()
    {
        _aiPath = GetComponent<AIPath>();
    }

    public virtual void SpawnTakeDamageVFX()
    {
        if (!WorldGameInfo.StaticBlood) return;
        var rndHeaight = Random.Range(0, _aiPath.height);
        var rnd = Random.Range(0, _takeDamageFX.Length);
        var prefab = Instantiate(_takeDamageFX[rnd], new Vector3(transform.position.x, transform.position.y + rndHeaight, transform.position.z), transform.rotation * Quaternion.Euler(0, 90, 0));
        Destroy(prefab, 35);
    }
}
