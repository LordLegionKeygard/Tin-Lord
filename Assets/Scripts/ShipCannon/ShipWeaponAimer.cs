using DG.Tweening;
using UnityEngine;
using Zenject;

public class ShipWeaponAimer : MonoBehaviour
{
    [Inject] private readonly MissionModeSystem _missionModeSystem;
    [Inject] readonly BulletsPool _bulletsPool;
    [Inject] private readonly SpawnedHazardSystem _spawnedHazardSystem;

    [Header("Refs")]
    [SerializeField] private Transform _cannonPivot;
    private Transform _barrelTransform;
    private Transform _modelTransform;
    private Vector3 _modelStartLocalPos;
    private Transform _firePoint;
    private ParticleSystem _muzzlePs;

    [Header("Raycast")]
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _rayMaxDistance = 5000f;
    [SerializeField] private float _fallbackGroundY = 0f;

    [Header("Barrel spin")]
    private float _barrelSpinSpeedOnHold = 1;
    private float _barrelSpinDegreesPerSecond = 1000;
    private float _barrelSpinLerpSpeed = 1;
    private readonly float _rotationSpeedDeg = 540f;
    private float _currentBarrelSpinSpeed01;
    private float _barrelSpinCommand;

    [Header("Slider Cooldown")]
    private float _currentCooldown;
    private float _cooldownMax;

    [Header("Other")]
    [SerializeField] private GameSpeedSystem _gameSpeedSystem;
    [SerializeField] private MissionShipWeaponSystem _missionShipWeaponSystem;
    [SerializeField] private bool _isLeftWeapon;
    [SerializeField] private Camera _camera;



    public float GetSliderCooldown() => (_cooldownMax <= 0f) ? 0f : Mathf.Clamp01(_currentCooldown / _cooldownMax);

    private void Update()
    {
        UpdateBarrelSpin();

        if (_missionModeSystem.IsPlanetMode()) return;

        if (_currentCooldown > 0f)
        {
            _currentCooldown -= Time.deltaTime;
        }
    }

    public void SetupWeapon(WeaponSetter weaponSetter)
    {
        _modelTransform = weaponSetter.WeaponModel;
        _modelStartLocalPos = _modelTransform != null ? _modelTransform.localPosition : Vector3.zero;
        _firePoint = weaponSetter.FirePoint;
        _muzzlePs = weaponSetter.Muzzle;
        _barrelTransform = weaponSetter.BarrelTransform;
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
        if (_barrelTransform != null)
        {
            _barrelSpinCommand = _barrelSpinSpeedOnHold;
        }

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

    public void Fire(ShipWeaponInfo shipWeaponInfo)
    {
        _missionShipWeaponSystem.UseBullet(_isLeftWeapon);
        RecoilAnim();
        _muzzlePs.Play();
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.ShipWeaponBullets[(int)shipWeaponInfo.ShipWeaponEnum], _firePoint.transform.position);
        int bulletsPerShot = Mathf.Max(1, shipWeaponInfo.BulletsPerShot);

        for (int i = 0; i < bulletsPerShot; i++)
        {
            var go = _bulletsPool.GetBullet(shipWeaponInfo.BulletType);
            Quaternion shotRotation = GetShotRotation(shipWeaponInfo.SpreadDeg);

            go.transform.SetPositionAndRotation(_firePoint.position, shotRotation);

            var bullet = go.GetComponent<ShipCannonBullet>();
            bullet.Setup(_bulletsPool, _missionShipWeaponSystem.GetWeaponDamage(_isLeftWeapon), shipWeaponInfo, _spawnedHazardSystem);
        }
    }

    private Quaternion GetShotRotation(float spreadDeg)
    {
        if (spreadDeg <= 0f) return _firePoint.rotation;

        float spreadTan = Mathf.Tan(spreadDeg * Mathf.Deg2Rad);
        Vector2 spreadOffset = Random.insideUnitCircle * spreadTan;
        Vector3 direction = (_firePoint.forward + _firePoint.up * spreadOffset.y + _firePoint.right * spreadOffset.x).normalized;

        return Quaternion.LookRotation(direction, _firePoint.up);
    }

    private void RecoilAnim()
    {
        float recoilDistance = 6; // насколько откатится назад
        float recoilTime = 0.1f;    // время отката
        float returnTime = 0.2f;     // время возврата

        _modelTransform.DOKill();
        _modelTransform.localPosition = _modelStartLocalPos;

        float targetZ = _modelStartLocalPos.z - recoilDistance;
        var sequence = DOTween.Sequence();
        sequence.Append(_modelTransform.DOLocalMoveZ(targetZ, recoilTime).SetEase(Ease.Linear));
        sequence.Append(_modelTransform.DOLocalMoveZ(_modelStartLocalPos.z, returnTime).SetEase(Ease.Linear));
    }

    private void UpdateBarrelSpin()
    {
        if (_barrelTransform == null) return;

        float target = Mathf.Clamp01(_barrelSpinCommand);
        _currentBarrelSpinSpeed01 = Mathf.MoveTowards(_currentBarrelSpinSpeed01, target, _barrelSpinLerpSpeed * Time.deltaTime);

        if (_currentBarrelSpinSpeed01 > 0f)
        {
            float deltaDegrees = _currentBarrelSpinSpeed01 * _barrelSpinDegreesPerSecond * Time.deltaTime;
            _barrelTransform.Rotate(0f, 0f, deltaDegrees, Space.Self);
        }

        _barrelSpinCommand = 0f;
    }
}
