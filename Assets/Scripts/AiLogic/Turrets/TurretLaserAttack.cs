using UnityEngine;

public class TurretLaserAttack : TurretBaseAttack
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private LineRenderer _lineRenderer;
    private Camera _mainCamera;
    private Vector3 _cameraForwardNormalized;
    private Transform _transformPoint;
    private bool _laserActive;

    private void Start()
    {
        _mainCamera = Camera.main;

        // Предрасчёт нормализованного направления камеры
        _cameraForwardNormalized = _mainCamera.transform.forward;
        _cameraForwardNormalized.y = 0;
        _cameraForwardNormalized.Normalize();
    }

    private void Update()
    {
        if (_isAttack && _aIDestinationSetter.GetTargetTransform() != null)
        {
            if (!_laserActive)
            {
                _lineRenderer.enabled = true;
                _laserActive = true;
            }

            if (_aIDestinationSetter.GetTargetTransform() != _transformPoint)
            {
                _transformPoint = _aIDestinationSetter.GetTargetTransform();
            }

            UpdateLaser();
        }
        else
        {
            if (_laserActive)
            {
                StopDamageCoroutine();
                _lineRenderer.enabled = false;
                _laserActive = false;
            }
        }
    }

    private void UpdateLaser()
    {
        _lineRenderer.SetPosition(0, _firePoint.position);

        // Рассчитываем смещённую позицию цели
        Vector3 adjustedTargetPosition = AdjustTargetPosition(_transformPoint.position);
        _lineRenderer.SetPosition(1, adjustedTargetPosition);
    }

    private Vector3 AdjustTargetPosition(Vector3 targetPosition)
    {
        // Смещение по высоте
        targetPosition.y += WorldGameInfo.BulletHeightOffset;

        // Горизонтальное смещение на основе предрасчёта
        targetPosition += _cameraForwardNormalized * WorldGameInfo.BulletLateralOffset;

        return targetPosition;
    }

    protected override void OnAttackStart()
    {
        _lineRenderer.enabled = true;
    }

    protected override void OnAttackStop()
    {
        _lineRenderer.enabled = false;
    }
}
