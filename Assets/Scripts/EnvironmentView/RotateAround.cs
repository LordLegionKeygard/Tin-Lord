using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] private Vector3 _vector;
    [SerializeField] private float _rotateSpeed;
    private float _defaultRotateSpeed;
    [SerializeField] private bool _isRandomStartRotation = true;
    private float _axis;

    private void Awake()
    {
        _defaultRotateSpeed = _rotateSpeed;
        if (_isRandomStartRotation) SetRandomRotation();
    }

    private void SetRandomRotation()
    {
        var rnd = Random.Range(0, 360);
        transform.Rotate(_vector.x == 0 ? 0 : rnd, _vector.y == 0 ? 0 : rnd, _vector.z == 0 ? 0 : rnd);
    }

    private void FixedUpdate()
    {
        _axis += Time.deltaTime * _rotateSpeed;

        if (_axis > 360.0f) _axis = 0.0f;

        transform.localRotation = Quaternion.Euler(_vector.x * _axis, _vector.y * _axis, _vector.z * _axis);
    }

    public void RotationToggle(bool state)
    {
        if(_defaultRotateSpeed == 0) _defaultRotateSpeed = _rotateSpeed;
        _rotateSpeed = state ? _defaultRotateSpeed : 0;
    }
}