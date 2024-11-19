using UnityEngine;
using Zenject;
using System.Collections;

public class BaseTakeDamageVFX : MonoBehaviour
{
    [Inject] private readonly TakeDamageVFXPool _pool;
    [SerializeField] private DamageVFXType _vfxType;
    private float _delay = 2;
    protected float Height;
    
    public virtual void SpawnTakeDamageVFX()
    {
        var rndHeight = Random.Range(Height * 0.5f, Height);

        GameObject vfx = _pool.GetVFX(_vfxType);

        vfx.transform.position = new Vector3(transform.position.x, transform.position.y + rndHeight, transform.position.z);
        vfx.transform.rotation = transform.rotation * Quaternion.Euler(0, 90, 0);

        StartCoroutine(ReturnVFXAfterDelay(_vfxType, vfx));
    }

    private IEnumerator ReturnVFXAfterDelay(DamageVFXType vfxType, GameObject vfx)
    {
        yield return new WaitForSeconds(_delay);
        _pool.ReturnVFX(vfxType, vfx);
    }
}
