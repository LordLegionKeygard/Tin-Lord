using DG.Tweening;
using UnityEngine;
using Zenject;

public class ShipCannonAimer : MonoBehaviour
{
    [Inject] readonly BulletsPool _bulletsPool;

    [Header("Refs")]
    [SerializeField] private Transform _modelTransform;
    [SerializeField] private Transform _cannonPivot;
    [SerializeField] private Transform _firePoint;

    [Header("Raycast")]
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _rayMaxDistance = 5000f;
    [SerializeField] private float _fallbackGroundY = 0f;

    [Header("Other")]
    [SerializeField] private float _rotationSpeedDeg = 540f;
    [SerializeField] private ParticleSystem _muzzlePs;
    [SerializeField] private ShipCannonInfo _shipCannonInfo;
    [SerializeField] private Camera _camera;
    private float _cooldown;

    private void Update()
    {
        if (_cooldown > 0f)
            _cooldown -= Time.deltaTime;
    }

    private void LateUpdate()
    {
        // игнор, если мышь над UI
        if (IsPointerOverUISystem.IsPointerOverUI) return;

        if (_camera == null || _cannonPivot == null) return;

        // Луч из камеры в курсор
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        // Целевая точка на земле
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out RaycastHit hit, _rayMaxDistance, _groundMask, QueryTriggerInteraction.Ignore))
        {
            targetPoint = hit.point;
        }
        else
        {
            // запасной вариант — бесконечная плоскость на высоте fallbackGroundY
            var plane = new Plane(Vector3.up, new Vector3(0f, _fallbackGroundY, 0f));
            if (!plane.Raycast(ray, out float enter)) return;
            targetPoint = ray.GetPoint(enter);
        }

        // Направление от пушки до цели
        Vector3 dir = targetPoint - _cannonPivot.position;
        if (dir.sqrMagnitude < 0.0001f) return;

        // Куда хотим смотреть
        Quaternion targetRot = Quaternion.LookRotation(dir.normalized, Vector3.up);

        // Плавный доворот
        _cannonPivot.rotation = Quaternion.RotateTowards(
            _cannonPivot.rotation, targetRot, _rotationSpeedDeg * Time.deltaTime);
    }

    public void TryFireHold()
    {
        if (_cooldown > 0f) return;
        Fire();
        _cooldown = (_shipCannonInfo.FireRate > 0f) ? 1f / _shipCannonInfo.FireRate : 0f;
    }

    public void Fire()
    {
        RecoilAnim();
        _muzzlePs.Play();
        var go = _bulletsPool.GetBullet(_shipCannonInfo.BulletType);

        go.transform.SetPositionAndRotation(_firePoint.position, _firePoint.rotation);

        var bullet = go.GetComponent<ShipCannonBullet>();
        if (!bullet) bullet = go.AddComponent<ShipCannonBullet>();

        bullet.Setup(
            _bulletsPool,
            _shipCannonInfo.BulletType,
            _shipCannonInfo.BulletSpeed,
            _shipCannonInfo.ExplosionDamage,
            _shipCannonInfo.LifeTime,
            _shipCannonInfo.ExplosionPrefab,
            _shipCannonInfo.ImpactYOffset
        );
    }

    private void RecoilAnim()
    {
        float recoilDistance = 6; // насколько откатится назад
        float recoilTime = 0.1f;    // время отката
        float returnTime = 0.2f;     // время возврата

        _modelTransform.DOLocalMoveZ(-recoilDistance, recoilTime)
            .SetRelative(true)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                _modelTransform.DOLocalMoveZ(recoilDistance, returnTime)
                    .SetRelative(true)
                    .SetEase(Ease.Linear);
            });
    }
}
