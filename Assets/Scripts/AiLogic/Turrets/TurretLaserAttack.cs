using UnityEngine;

public class TurretLaserAttack : TurretBaseAttack
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private LineRenderer _lineRenderer;
    private Camera _mainCamera;
    private Vector3 _cameraForwardNormalized;
    private Transform _transformPoint;
    private bool _laserActive;
    private float _tilingFactor = 0.35f;
    private static readonly int TilingProperty = Shader.PropertyToID("_Tiling");

    private void Start()
    {
        _mainCamera = Camera.main;
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
        Vector3 startPosition = _firePoint.position;
        Vector3 endPosition = AdjustTargetPosition(_transformPoint.position);

        _lineRenderer.SetPosition(0, startPosition);
        _lineRenderer.SetPosition(1, endPosition);

        // Вычисляем длину лазера
        float laserLength = Vector3.Distance(startPosition, endPosition);

        // Устанавливаем значение _Tiling в зависимости от длины лазера
        UpdateLaserTiling(laserLength);
    }

    private void UpdateLaserTiling(float laserLength)
    {
        // Получаем текущий материал LineRenderer
        Material laserMaterial = _lineRenderer.material;

        // Устанавливаем X на основе длины лазера, Y остается равным 1
        Vector2 tiling = new Vector2(laserLength * _tilingFactor, 1);
        laserMaterial.SetVector(TilingProperty, tiling);
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
