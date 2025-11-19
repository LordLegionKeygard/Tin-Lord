using DG.Tweening;
using UnityEngine;
using Zenject;

public class ShipWeaponAimer : MonoBehaviour
{
    [Inject] private readonly MissionModeSystem _missionModeSystem;
    [Inject] readonly BulletsPool _bulletsPool;

    [Header("Refs")]
    [SerializeField] private Transform _cannonPivot;
    private Transform _modelTransform;
    private Transform _firePoint;
    private ParticleSystem _muzzlePs;

    [Header("Raycast")]
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _rayMaxDistance = 5000f;
    [SerializeField] private float _fallbackGroundY = 0f;

    [Header("Other")]
    [SerializeField] private GameSpeedSystem _gameSpeedSystem;
    [SerializeField] private MissionShipWeaponSystem _missionShipWeaponSystem;
    [SerializeField] private bool _isLeftWeapon;
    [SerializeField] private Camera _camera;
    private readonly float _rotationSpeedDeg = 540f;
    private float _currentCooldown;
    private float _cooldownMax;
    public float GetSliderCooldown() => (_cooldownMax <= 0f) ? 0f : Mathf.Clamp01(_currentCooldown / _cooldownMax);

    private void Update()
    {
        if (_missionModeSystem.IsPlanetMode()) return;

        if (_currentCooldown > 0f)
        {
            _currentCooldown -= Time.deltaTime;
        }
    }

    public void SetupWeapon(WeaponSetter weaponSetter)
    {
        _modelTransform = weaponSetter.WeaponModel;
        _firePoint = weaponSetter.FirePoint;
        _muzzlePs = weaponSetter.Muzzle;
    }

    private void LateUpdate()
    {
        if (IsPointerOverUISystem.IsPointerOverUI || _modelTransform == null) return;

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
        _cannonPivot.rotation = Quaternion.RotateTowards(_cannonPivot.rotation, targetRot, _rotationSpeedDeg * Time.unscaledDeltaTime);
    }

    public void TryFireHold()
    {
        var info = _missionShipWeaponSystem.GetShipCannonInfo(_isLeftWeapon);

        if (_currentCooldown > 0f || _gameSpeedSystem.IsPause() || info == null) return;

        _cooldownMax = (info.FireRate > 0f) ? 1f / info.FireRate : 0f;
        _currentCooldown = _cooldownMax;

        if (!_missionShipWeaponSystem.IsHaveShipCannonBulletsCount(_isLeftWeapon))
        {
            RecoilAnim();
            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.NotEnoughtAmmo, transform.position);
            return;
        }
        Fire(info);
    }

    public void Fire(ShipWeaponInfo shipCannonInfo)
    {
        _missionShipWeaponSystem.UseBullet(_isLeftWeapon);
        RecoilAnim();
        _muzzlePs.Play();
        var go = _bulletsPool.GetBullet(shipCannonInfo.BulletType);

        go.transform.SetPositionAndRotation(_firePoint.position, _firePoint.rotation);

        var bullet = go.GetComponent<ShipCannonBullet>();
        if (!bullet) bullet = go.AddComponent<ShipCannonBullet>();

        bullet.Setup(
            _bulletsPool,
            shipCannonInfo.BulletType,
            shipCannonInfo.BulletSpeed,
            _missionShipWeaponSystem.GetWeaponDamage(_isLeftWeapon),
            shipCannonInfo.LifeTime,
            shipCannonInfo.ExplosionPrefab,
            shipCannonInfo.ImpactYOffset
        );
    }

    private void RecoilAnim()
    {
        float recoilDistance = 6; // насколько откатится назад
        float recoilTime = 0.1f;    // время отката
        float returnTime = 0.2f;     // время возврата

        _modelTransform.DOLocalMoveZ(-recoilDistance, recoilTime).SetRelative(true).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                _modelTransform.DOLocalMoveZ(recoilDistance, returnTime)
                    .SetRelative(true)
                    .SetEase(Ease.Linear);
            });
    }
}
