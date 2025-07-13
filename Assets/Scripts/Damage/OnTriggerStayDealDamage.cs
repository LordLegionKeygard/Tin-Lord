using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerStayDealDamage : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _ps;
    [SerializeField] private Light[] _effectLights;
    [SerializeField] private GameObject[] _objectsToScale;
    private int _ticksLeft;
    private readonly HashSet<BaseHealth> _targets = new();
    private float _damage;
    private float _destroyTime = 3;
    private float _fadeOutTime = 2;
    private bool _isActive;

    public void SetInfo(int duration, float damageFactor)
    {
        _ticksLeft = duration;
        _damage = damageFactor * (1 + CurrentMissionInfo.Instance.GetMissionDeckIndex());
        _isActive = true;
        CustomEvents.OnTimeTick += OnTimeTick;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out BaseHealth baseHealth))
        {
            if (!baseHealth.IsDeath())
            {
                _targets.Add(baseHealth);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out BaseHealth baseHealth))
        {
            if (!baseHealth.IsDeath())
            {
                _targets.Remove(baseHealth);
            }
        }
    }

    private void OnTimeTick()
    {
        if (!_isActive) return;

        foreach (var health in _targets)
        {
            if (health != null && !health.IsDeath())
                health.CalculateDamage(_damage, 0);
        }

        _ticksLeft--;
        if (_ticksLeft <= 0)
        {
            _isActive = false;
            StartCoroutine(FadeOutCoroutine());
            Destroy(gameObject, _destroyTime);
            CustomEvents.OnTimeTick -= OnTimeTick;
        }
    }

    private IEnumerator FadeOutCoroutine()
    {

        float elapsed = 0f;

        foreach (var ps in _ps)
        {
            var main = ps.main;
            main.maxParticles = 0;
        }

        float[] startIntensity = new float[_effectLights.Length];
        for (int i = 0; i < _effectLights.Length; i++)
        {
            startIntensity[i] = _effectLights[i] ? _effectLights[i].intensity : 0f;
        }

        Vector3[] startScales = new Vector3[_objectsToScale.Length];
        for (int i = 0; i < _objectsToScale.Length; i++)
        {
            startScales[i] = _objectsToScale[i] ? _objectsToScale[i].transform.localScale : Vector3.zero;
        }

        while (elapsed < _fadeOutTime)
        {
            float t = elapsed / _fadeOutTime;

            for (int i = 0; i < _effectLights.Length; i++)
            {
                _effectLights[i].intensity = Mathf.Lerp(startIntensity[i], 0f, t);
            }

            for (int i = 0; i < _objectsToScale.Length; i++)
            {
                _objectsToScale[i].transform.localScale = Vector3.Lerp(startScales[i], Vector3.zero, t);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Гарантируем финал
        foreach (var light in _effectLights)
        {
            light.intensity = 0f;
        }

        foreach (var obj in _objectsToScale)
        {
            obj.transform.localScale = Vector3.zero;
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnTimeTick -= OnTimeTick;
    }
}
