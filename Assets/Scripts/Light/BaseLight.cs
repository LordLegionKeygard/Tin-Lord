using DG.Tweening;
using UnityEngine;

public class BaseLight : MonoBehaviour
{
    [Header("Start Light Settings")]
    [SerializeField] private bool _isNeedSmoothStart = false;
    [SerializeField] private int _needIntensity = 1;

    [Header("Light Settings")]
    [SerializeField] private Light[] _lights;
    [SerializeField] private float _lightEndTime = 0.5f;

    private void OnEnable()
    {
        if (!_isNeedSmoothStart) return;

        foreach (var light in _lights)
        {
            if (light == null) continue;
            light.DOIntensity(_needIntensity, _lightEndTime).SetUpdate(true);
        }
    }

    public virtual void ChangeIntensity()
    {
        if (_lights == null || _lights.Length == 0) return;

        foreach (var light in _lights)
        {
            if (light == null) continue;
            light.DOIntensity(0, _lightEndTime).SetUpdate(true);
        }
    }
}
