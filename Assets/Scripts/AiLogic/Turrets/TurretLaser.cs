using System.Collections;
using UnityEngine;

public class TurretLaser : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private LineRenderer _lineRenderer;
    private TurretDamage _turretDamage;
    private AIDestinationSetter _aIDestinationSetter;

    private bool _laserEnabled;
    private Coroutine _damageCoroutine;

    private void Awake()
    {
        _turretDamage = GetComponent<TurretDamage>();
        _aIDestinationSetter = GetComponent<AIDestinationSetter>();
    }

    public void LaserToggle(bool state)
    {
        if (state)
        {
            _laserEnabled = true;
            StartDamageCoroutine();
        }
        else
        {
            _laserEnabled = false;
            StopDamageCoroutine();
            _lineRenderer.enabled = false;
        }
    }

    private void Update()
    {
        if (_laserEnabled && _aIDestinationSetter.CurrentTarget != null)
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

    private void StartDamageCoroutine()
    {
        if (_damageCoroutine == null)
        {
            _damageCoroutine = StartCoroutine(DealDamageOverTime());
        }
    }

    private void StopDamageCoroutine()
    {
        if (_damageCoroutine != null)
        {
            StopCoroutine(_damageCoroutine);
            _damageCoroutine = null;
        }
    }

    private IEnumerator DealDamageOverTime()
    {
        while (_laserEnabled && _aIDestinationSetter.CurrentTarget != null)
        {
            _turretDamage.Attack(0);
            yield return new WaitForSeconds(0.5f);
        }

        _damageCoroutine = null;
    }
}
