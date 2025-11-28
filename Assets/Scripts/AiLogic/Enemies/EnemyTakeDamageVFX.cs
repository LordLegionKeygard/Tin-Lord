using UnityEngine;
using System.Collections;

public class EnemyTakeDamageVFX : BaseTakeDamageVFX
{
    private EnemyCenterPoint _enemyCenterPoint;

    private void Awake()
    {
        _enemyCenterPoint = GetComponent<EnemyCenterPoint>();
    }
    public override void SpawnTakeDamageVFX()
    {
        if (!WorldGameInfo.StaticBlood) return;

        GameObject vfx = _pool.GetVFX(_vfxType);

        vfx.transform.SetPositionAndRotation(_enemyCenterPoint.GetTransform().position, transform.rotation * Quaternion.Euler(0, 90, 0));
        StartCoroutine(ReturnVFXAfterDelay(_vfxType, vfx));
    }

    public override IEnumerator ReturnVFXAfterDelay(DamageVFXType vfxType, GameObject vfx)
    {
        return base.ReturnVFXAfterDelay(vfxType, vfx);
    }
}
