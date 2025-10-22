using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCurves : MonoBehaviour
{
    [SerializeField] private AnimationCurve _floatCurve;
    [SerializeField] private float _graphTimeMultiplier = 1;
    [SerializeField] private float _graphIntensityMultiplier = 1;

    private float _startTime;
    private Transform _trans;
    private Vector3 _startScale;

    private void Awake()
    {
        _trans = GetComponent<Transform>();
        _startScale = _trans.localScale;
    }

    private void OnEnable()
    {
        _startTime = Time.time;
        _trans.localScale = Vector3.zero;
    }

    private void Update()
    {
        var time = Time.time - _startTime;

        var eval = _floatCurve.Evaluate(time / _graphTimeMultiplier) * _graphIntensityMultiplier;
        _trans.localScale = eval * _startScale;

    }
}
