using UnityEngine;
using Pathfinding;
using Zenject;
using System.Collections;

public class BaseTakeDamageVFX : MonoBehaviour
{
    [Inject] private readonly TakeDamageVFXPool _pool;
    [SerializeField] private DamageVFXType _vfxType; // Тип текущего VFX
    private float _delay = 2;
    private AIPath _aiPath;

    private void Awake()
    {
        _aiPath = GetComponent<AIPath>();
    }

    public virtual void SpawnTakeDamageVFX()
    {
        if (!WorldGameInfo.StaticBlood) return;

        var rndHeight = Random.Range(_aiPath.height * 0.5f, _aiPath.height);

        // Получаем случайный VFX из пула
        GameObject vfx = _pool.GetVFX(_vfxType);

        // Устанавливаем позицию и вращение
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
