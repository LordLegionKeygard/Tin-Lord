using UnityEngine;

public class TurretLaserAttack : TurretBaseAttack
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private LineRenderer _lineRenderer;

    private void Update()
    {
        if (_isAttack && _aIDestinationSetter.CurrentTarget != null)
        {
            UpdateLaser();
        }
        else
        {
            StopDamageCoroutine();
            _lineRenderer.enabled = false;
        }
    }

    private void UpdateLaser()
    {
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, _firePoint.position);
        _lineRenderer.SetPosition(1, _aIDestinationSetter.CurrentTarget.position);
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
