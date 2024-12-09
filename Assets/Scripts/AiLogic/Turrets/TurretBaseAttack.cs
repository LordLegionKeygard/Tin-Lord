using System.Collections;
using UnityEngine;

public abstract class TurretBaseAttack : MonoBehaviour
{
    [SerializeField] private float attackInterval;
    protected TurretDamage _turretDamage;
    protected AIDestinationSetter _aIDestinationSetter;
    private Coroutine _damageCoroutine;
    protected bool _isAttack;

    protected virtual void Awake()
    {
        _turretDamage = GetComponent<TurretDamage>();
        _aIDestinationSetter = GetComponent<AIDestinationSetter>();
    }

    public void AttackToggle(bool state)
    {
        if (state)
        {
            _isAttack = true;
            StartDamageCoroutine();
            OnAttackStart();
        }
        else
        {
            _isAttack = false;
            StopDamageCoroutine();
            OnAttackStop();
        }
    }

    private void StartDamageCoroutine()
    {
        if (_damageCoroutine == null)
        {
            _damageCoroutine = StartCoroutine(DealDamageOverTime());
        }
    }

    public void StopDamageCoroutine()
    {
        if (_damageCoroutine != null)
        {
            StopCoroutine(_damageCoroutine);
            _damageCoroutine = null;
        }
    }

    private IEnumerator DealDamageOverTime()
    {
        while (_isAttack && _aIDestinationSetter.CurrentTarget != null)
        {
            _turretDamage.Attack(0);
            yield return new WaitForSeconds(attackInterval);
        }

        _damageCoroutine = null;
    }

    protected virtual void OnAttackStart() { }
    protected virtual void OnAttackStop() { }
}
